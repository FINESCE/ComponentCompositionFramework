using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Insero.ComponentCompositionFramework.Composition.Events;
using Insero.ComponentCompositionFramework.Components;
using Insero.ComponentCompositionFramework.Composition;
using NLog;

namespace Insero.ComponentCompositionFramework.Composition.Static
{
   /// <summary>
   /// This is a static component connection composer.
   /// </summary>
   public sealed class StaticComponentComposer : ComponentComposerBase, IDisposable
   {
      private static Logger _logger = LogManager.GetCurrentClassLogger();

      private object _syncRoot = new object();
      private bool _isDisposed = false;
      private bool _isStarted = false;

      private Func<IEnumerable<IComponent>> _componentFactory;
      private HashSet<ComponentModel> _startedComponents = new HashSet<ComponentModel>();

      public StaticComponentComposer( Func<IEnumerable<IComponent>> componentFactory )
      {
         _componentFactory = componentFactory;
      }

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

      public override bool IsStarted
      {
         get
         {
            return _isStarted;
         }
      }

      public override void Start()
      {
         lock ( _syncRoot )
         {
            if ( !_isStarted )
            {
               _logger.Info( "Starting the '{0}' service", GetType().Name );
               CreateAndCombineComponents();

               _isStarted = true;
               RaiseStateChanged( MediatorState.Started );
               _logger.Info( "Started the '{0}' service", GetType().Name );
            }
         }
      }

      public override void Stop()
      {
         lock ( _syncRoot )
         {
            if ( _isStarted )
            {
               _logger.Info( "Stopping the '{0}' service", GetType().Name );
               DestroyConnections();
               _isStarted = false;
               RaiseStateChanged( MediatorState.Stopped );
               _logger.Info( "Stopped the '{0}' service", GetType().Name );
            }
         }
      }

      private void CreateAndCombineComponents()
      {
         var newComponents = _componentFactory();

         // for each backend that must be started
         foreach ( var newComponent in newComponents )
         {
            try
            {
               newComponent.Disposed += Component_Disposed;

               // start it!
               _logger.Info( "Starting the new component '{0}'", newComponent.Name );
               newComponent.Start();

               var componentModel = new ComponentModel( newComponent );
               _startedComponents.Add( componentModel );
               RaiseComponentsChanged( componentModel, ChangeAction.Added );

               // connect new backend to relevant frontends
               foreach ( var connectableComponentModel in _startedComponents )
               {
                  try
                  {
                     TryConnect( connectableComponentModel, componentModel );
                  }
                  catch ( Exception e )
                  {
                     _logger.Log( 
                        LogLevel.Error, 
                        string.Format(
                           "Failed connecting the component '{0}' to the component '{1}'", 
                           connectableComponentModel.Component.Name, 
                           newComponent.Name ),
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
                     "Failed starting new the component '{0}' due to: " + e.Message,
                     newComponent.Name ),
                  e );

               // make sure it is disposed
               newComponent.Dispose();
            }

            // all new backends are connected to existing frontends
         }
      }

      private void DestroyConnections()
      {
         var backends = _startedComponents.ToList();
         foreach ( var backend in backends )
         {
            try
            {
               backend.Component.Dispose();
            }
            catch ( Exception e )
            {
               _logger.Log(
                  LogLevel.Error,
                  string.Format(
                     "Exception occured while disposing '{0}'",
                     backend.Component.Name ),
                  e );
            }
         }
      }

      private void Component_Disposed( object sender, EventArgs e )
      {
         lock ( _syncRoot )
         {
            var component = (IComponent)sender;
            component.Disposed -= Component_Disposed;

            _logger.Info( "The component '{0}' was disposed", component.Name );

            // Find the started backend model, if it exist. 
            // It may not have been added to this collection yet.
            var componentModel = _startedComponents.FirstOrDefault( x => x.Component == component );

            // If the backend was started, we need to disconnect it!
            if ( componentModel != null )
            {
               // this disconnect is nessecary because otherwise the
               // frontend have no way of knowing the backend disposed
               _startedComponents.Remove( componentModel );

               if ( component is IConnectableComponent )
               {
                  foreach ( var otherComponentModel in _startedComponents )
                  {
                     try
                     {
                        TryDisconnect( otherComponentModel, componentModel );
                     }
                     catch ( Exception ex )
                     {
                        _logger.Log(
                           LogLevel.Error,
                           string.Format(
                              "Failed disconnecting '{0}' from '{1}'",
                              otherComponentModel.Component.Name,
                              component.Name ),
                           ex );
                     }
                  }
               }

               RaiseComponentsChanged( componentModel, ChangeAction.Removed );
            }
         }
      }

      #region IDisposable Members

      ~StaticComponentComposer()
      {
         lock ( _syncRoot )
         {
            Dispose( false );
         }
      }

      public void Dispose()
      {
         lock ( _syncRoot )
         {
            if ( !_isDisposed )
            {
               Dispose( true );
               _isDisposed = true;
               GC.SuppressFinalize( this );
            }
         }
      }

      private void Dispose( bool disposing )
      {
         if ( disposing )
         {
            Stop();
         }
      }

      #endregion
   }
}
