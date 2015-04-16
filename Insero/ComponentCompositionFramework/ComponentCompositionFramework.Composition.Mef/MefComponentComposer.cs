/*
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Insero.ComponentCompositionFramework.Composition.Events;
using Insero.ComponentCompositionFramework.Components;
using NLog;

namespace Insero.ComponentCompositionFramework.Composition.Dynamic
{
   /// <summary>
   /// This is a dynamic backend-to-frontend connection composer
   /// based on extensions that can be found in a folder.
   /// </summary>
   public sealed class MefComponentComposer : ComponentComposerBase, IDisposable
   {
      private static Logger _logger = LogManager.GetCurrentClassLogger();

      private object _syncRoot = new object();
      private bool _disposed = false;
      private bool _isStarted = false;
      private bool _isComposed = false;
      private string _path;

      [ImportMany( AllowRecomposition = true )]
      private IEnumerable<IComponent> _loadedComponents;

      private DirectoryCatalog _catalog;
      private CompositionContainer _container;
      private FileSystemWatcher _watcher;

      private HashSet<ComponentModel> _startedComponents = new HashSet<ComponentModel>();
      private HashSet<IComponent> _stoppedComponents = new HashSet<IComponent>();

      /// <summary>
      /// Constructs the MefConnectionComposer to the given path for extensions.
      /// </summary>
      /// <param name="path"></param>
      public MefComponentComposer( string path )
      {
         // Make sure the path that is passed in becomes rooted path, if it
         // is a relative path
         if ( Path.IsPathRooted( path ) )
         {
            _path = path;
         }
         else
         {
            _path = Path.Combine( Environment.CurrentDirectory, path );
         }
      }

      /// <summary>
      /// Gets the models representing the frontends.
      /// </summary>
      public override IEnumerable<ComponentModel> Components
      {
         get
         {
            lock ( _syncRoot )
            {
               return _startedComponents.ToList();
            }
         }
      }

      private void ComposeOrRefresh()
      {
         // This operation must either compose the container
         // or refresh the catalog. The container should only
         // be composed once, so once it is composed, the 
         // catalog of DLLs should simply be refreshed.
         if ( _isComposed )
         {
            _catalog.Refresh();
         }
         else
         {
            _isComposed = true;
            _container.ComposeParts( this );
         }
      }

      private void InitializeMef()
      {
         // create directory if not exist
         if ( !Directory.Exists( _path ) )
         {
            Directory.CreateDirectory( _path );
         }

         InitializeFileWatcherIfNotExist();

         _catalog = new DirectoryCatalog( _path, "*.dll" );
         _container = new CompositionContainer( _catalog );
      }

      private void InitializeFileWatcherIfNotExist()
      {
         if ( _watcher == null )
         {
            _watcher = new FileSystemWatcher( _path, "*.dll" );
            _watcher.Changed += Watcher_Changed;
            _watcher.Created += Watcher_Created;
            _watcher.Deleted += Watcher_Deleted;
            _watcher.Renamed += Watcher_Renamed;
         }
      }

      private void UninitializeMef()
      {
         // "fake" that there are no available connections
         // because these are not changed when dispoing the
         // catalogue or container, which they should for our
         // purposes...
         _loadedComponents = new List<IComponent>();

         if ( _catalog != null )
         {
            _catalog.Dispose();
            _catalog = null;
         }

         if ( _container != null )
         {
            _container.Dispose();
            _container = null;
         }

         _isComposed = false;
      }

      /// <summary>
      /// Gets a bool indicating if the connection composer is started.
      /// </summary>
      public override bool IsStarted
      {
         get
         {
            lock ( _syncRoot )
            {
               return _isStarted;
            }
         }
      }

      /// <summary>
      /// Starts the ConnectionComposer.
      /// </summary>
      /// <exception cref="ConnectionComposerException">Thrown if the MefConnectionComposer could not be started</exception>
      public override void Start()
      {
         lock ( _syncRoot )
         {
            if ( !_isStarted )
            {
               _logger.Info( "Starting the '{0}' service", GetType().Name );

               try
               {
                  InitializeMef();
                  _watcher.EnableRaisingEvents = true;
                  ComposeOrRefresh();
               }
               catch ( Exception e )
               {
                  try
                  {
                     _watcher.EnableRaisingEvents = false;
                     UninitializeMef();
                  }
                  catch { }

                  _logger.Log(
                     LogLevel.Error,
                     string.Format( "Failed starting the '{0}' service", GetType().Name ),
                     e );

                  throw;
               }

               CombineComponents();
               _isStarted = true;
               RaiseStateChanged( MediatorState.Started );
               _logger.Info( "Started the '{0}' service", GetType().Name );
            }
         }
      }

      /// <summary>
      /// Stops the ConnectionComposer if it is started.
      /// </summary>
      public override void Stop()
      {
         lock ( _syncRoot )
         {
            if ( _isStarted )
            {
               _logger.Info( "Stopping the '{0}' service", GetType().Name );

               try
               {
                  _watcher.EnableRaisingEvents = false;
                  UninitializeMef();
               }
               catch { }

               _isStarted = false;
               RaiseStateChanged( MediatorState.Stopped );
               _logger.Info( "Stopped the '{0}' service", GetType().Name );
            }
         }
      }

      private void CombineComponents()
      {
         // these are all the currently loaded modules
         var loadedComponents = _loadedComponents;

         // these are the modules that has been unloaded (.DLL) removed. 
         // they have already been disposed, so they should not be redisposed.
         var unloadedComponents = _stoppedComponents.Except( loadedComponents )
                                                   .ToList();

         // these are the new modules that have not been started yet.
         var newComponents = _loadedComponents.Except( _startedComponents.Select( x => x.Component ) )
                                             .Except( _stoppedComponents )
                                             .ToList();

         foreach ( var unloadedFrontend in unloadedComponents )
         {
            _stoppedComponents.Remove( unloadedFrontend );
         }

         foreach ( var newComponent in newComponents )
         {
            try
            {
               newComponent.Disposed += Component_Disposed;

               // start it!
               _logger.Info( "Starting the new component '{0}'", newComponent.Name );
               newComponent.Start();

               var frontendModel = new ComponentModel( newComponent );
               _startedComponents.Add( frontendModel );
               RaiseComponentsChanged( frontendModel, ChangeAction.Added );

               // connect new frontend to relevant backends
               foreach ( var backendModel in _startedComponents )
               {
                  try
                  {
                     TryConnect( frontendModel, backendModel );
                  }
                  catch ( Exception e )
                  {
                     _logger.Log(
                        LogLevel.Error,
                        string.Format(
                           "Failed connecting the component '{0}' to the backend '{1}'",
                           newComponent.Name,
                           backendModel.Component.Name ),
                        e );
                  }
               }
               _logger.Info( "Started the new component '{0}'", newComponent.Name );
            }
            catch ( Exception e )
            {
               _logger.Log(
                  LogLevel.Error,
                  string.Format(
                     "Failed starting new the component '{0}'",
                     newComponent.Name ),
                  e );

               // make sure it is disposed
               newComponent.Dispose();
            }
         }
      }

      private void Watcher_Renamed( object sender, RenamedEventArgs e )
      {
         lock ( _syncRoot )
         {
            try
            {
               ComposeOrRefresh();
               CombineComponents();
            }
            catch ( Exception ex )
            {
               _logger.Log(
                  LogLevel.Error,
                  "An error occured while composing.",
                  ex );
            }
         }
      }

      private void Watcher_Deleted( object sender, FileSystemEventArgs e )
      {
         lock ( _syncRoot )
         {
            try
            {
               ComposeOrRefresh();
               CombineComponents();
            }
            catch ( Exception ex )
            {
               _logger.Log(
                  LogLevel.Error,
                  "An error occured while composing.",
                  ex );
            }
         }
      }

      private void Watcher_Created( object sender, FileSystemEventArgs e )
      {
         lock ( _syncRoot )
         {
            try
            {
               ComposeOrRefresh();
               CombineComponents();
            }
            catch ( Exception ex )
            {
               _logger.Log(
                  LogLevel.Error,
                  "An error occured while composing.",
                  ex );
            }
         }
      }

      private void Watcher_Changed( object sender, FileSystemEventArgs e )
      {
         lock ( _syncRoot )
         {
            try
            {
               ComposeOrRefresh();
               CombineComponents();
            }
            catch ( Exception ex )
            {
               _logger.Log(
                  LogLevel.Error,
                  "An error occured while composing.",
                  ex );
            }
         }
      }

      private void Component_Disposed( object sender, EventArgs e )
      {
         lock ( _syncRoot )
         {
            var backend = (IComponent)sender;
            backend.Disposed -= Component_Disposed;

            _logger.Info( "The component '{0}' was disposed", backend.Name );

            // Find the started backend model, if it exist. 
            // It may not have been added to this collection yet.
            var backendModel = _startedComponents.FirstOrDefault( x => x.Component == backend );

            // There are two ways this module could have been disposed:
            // * MEF disposed it because it was removed from the watched folder
            // * It was disposed by itself or by something that is not MEF
            // We can find out which one is the case by looking into the 
            // Import of modules...
            if ( _loadedComponents.Contains( backend ) )
            {
               // It was NOT disposed by MEF, which means it should be added
               // to the stopped modules collection
               _stoppedComponents.Add( backend );
            }
            else
            {
               // it was disposed by MEF, which means it should not be added
               // to the stopped modules collection
            }

            // If the backend was started, we need to disconnect it!
            if ( backendModel != null )
            {
               // this disconnect is nessecary because otherwise the
               // frontend have no way of knowing the backend disposed
               _startedComponents.Remove( backendModel );

               foreach ( var frontendModel in _startedComponents )
               {
                  try
                  {
                     TryDisconnect( frontendModel, backendModel );
                  }
                  catch ( Exception ex )
                  {
                     _logger.Log(
                        LogLevel.Error,
                        string.Format(
                           "Failed disconnecting '{0}' from '{1}'",
                           frontendModel.Component.Name,
                           backend.Name ),
                        ex );
                  }
               }

               RaiseComponentsChanged( backendModel, ChangeAction.Removed );
            }
         }
      }

      #region IDisposable Members

      ~MefComponentComposer()
      {
         lock ( _syncRoot )
         {
            Cleanup( false );
         }
      }

      public void Dispose()
      {
         lock ( _syncRoot )
         {
            if ( !_disposed )
            {
               Cleanup( true );
               GC.SuppressFinalize( this );
               _disposed = true;
            }
         }
      }

      private void Cleanup( bool disposing )
      {
         if ( disposing )
         {
            Stop();
            _watcher.Dispose();
         }
      }

      #endregion
   }
}
