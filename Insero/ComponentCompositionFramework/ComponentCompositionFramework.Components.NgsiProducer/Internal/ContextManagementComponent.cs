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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FIWARE.Data.Ngsi.Http;
using FIWARE.Data.Ngsi.Model;
using FIWARE.Data.Ngsi.Operations;
using System.Xml.Linq;
using System.Threading;
using System.Xml;
using System.Configuration;
using NLog;

namespace Insero.ComponentCompositionFramework.Components.NgsiProducer.Internal
{
   internal class ContextManagementComponent : IContextManagementComponent, IDisposable
   {
      private static Logger _logger = LogManager.GetCurrentClassLogger();
      private string _publishSubscriberEndpoint;

      private HashSet<INgsiConnectingComponent> _connectedComponents = new HashSet<INgsiConnectingComponent>();
      private CancellationTokenSource _cancel = new CancellationTokenSource();
      private Accumulator<ContextElement> _accumulator;
      private NgsiClient _client = new NgsiClient();
      private bool _disposed = false;

      public void Start()
      {
         _accumulator = new Accumulator<ContextElement>( TimeSpan.FromSeconds( 5 ) );
         _accumulator.Accumulated += Accumulator_Accumulated;
         _publishSubscriberEndpoint = ConfigurationManager.AppSettings.Get( "ngsi:PublishSubscriberEndpoint" );
      }

      #region IContextManagementComponent Members

      public void BeginProduce( INgsiConnectingComponent component, IEnumerable<ContextElement> elements )
      {
         _accumulator.Accumulate( elements );
      }

      private void Accumulator_Accumulated( object sender, AccumulatedEventArgs<ContextElement> e )
      {
         if ( e.Accumulation.Count > 0 )
         {
            var elements = Merge( e.Accumulation );
            SendUpdates( elements );
         }
      }

      public void Register( INgsiConnectingComponent component )
      {
         lock ( _connectedComponents )
         {
            _connectedComponents.Add( component );
         }
      }

      public void Unregister( INgsiConnectingComponent component )
      {
         lock ( _connectedComponents )
         {
            _connectedComponents.Remove( component );
         }
      }

      #endregion

      #region Helper Methods

      private async void SendUpdates( IEnumerable<ContextElement> elements )
      {
         var request = new UpdateContextRequest
         {
            ContextElements = new List<ContextElement>( elements ),
            UpdateAction = UpdateActionType.Append
         };

         var url = DefaultQueryUrls.GetUpdateContext( _publishSubscriberEndpoint );

         try
         {
            await _client.SendAsync( url, request, _cancel.Token );
         }
         catch ( Exception e )
         {
            _logger.Log(
               LogLevel.Error,
               "Failed sending context update to " + _publishSubscriberEndpoint,
               e );
         }
      }

      private IReadOnlyList<ContextElement> Merge( IEnumerable<ContextElement> elements )
      {
         Dictionary<EntityID, ContextElement> foundElements = new Dictionary<EntityID, ContextElement>();

         foreach ( var element in elements )
         {
            ContextElement existingElement = null;
            if ( foundElements.TryGetValue( element.EntityID, out existingElement ) )
            {
               element.ContextAttributes = element.ContextAttributes ?? new List<ContextAttribute>();

               Dictionary<string, ContextAttribute> foundAttributes = new Dictionary<string, ContextAttribute>();
               foreach ( var existingAttribute in existingElement.ContextAttributes )
               {
                  foundAttributes.Add( existingAttribute.Name, existingAttribute );
               }

               foreach ( var newAttribute in element.ContextAttributes )
               {
                  ContextAttribute existingAttribute = null;
                  if ( foundAttributes.TryGetValue( newAttribute.Name, out existingAttribute ) )
                  {
                     if ( newAttribute.GetTimestampMetadata() > existingAttribute.GetTimestampMetadata() )
                     {
                        foundAttributes[ newAttribute.Name ] = newAttribute;
                     }
                  }
                  else
                  {
                     foundAttributes[ newAttribute.Name ] = newAttribute;
                  }
               }
               existingElement.ContextAttributes = foundAttributes.Values.ToList();
            }
            else
            {
               foundElements.Add( element.EntityID, element );
               element.ContextAttributes = element.ContextAttributes ?? new List<ContextAttribute>();

               Dictionary<string, ContextAttribute> foundAttributes = new Dictionary<string, ContextAttribute>();
               foreach ( var newAttribute in element.ContextAttributes )
               {
                  ContextAttribute existingAttribute = null;
                  if ( foundAttributes.TryGetValue( newAttribute.Name, out existingAttribute ) )
                  {
                     if ( newAttribute.GetTimestampMetadata() > existingAttribute.GetTimestampMetadata() )
                     {
                        foundAttributes[ newAttribute.Name ] = newAttribute;
                     }
                  }
                  else
                  {
                     foundAttributes[ newAttribute.Name ] = newAttribute;
                  }
               }
               element.ContextAttributes = foundAttributes.Values.ToList();
            }
         }

         return foundElements.Values.ToList();
      }

      #endregion

      #region IDisposable Members

      private void ThrowIfDisposed()
      {
         if ( _disposed )
         {
            throw new ObjectDisposedException( GetType().Name );
         }
      }

      ~ContextManagementComponent()
      {
         Cleanup( false );
      }

      public void Dispose()
      {
         if ( !_disposed )
         {
            Cleanup( true );
            _disposed = true;
            GC.SuppressFinalize( this );
         }
      }

      private void Cleanup( bool disposing )
      {
         _cancel.Cancel();

         if ( disposing )
         {
            if ( _accumulator != null )
            {
               _accumulator.Dispose();
            }

            _connectedComponents.Clear();
         }
      }

      #endregion
   }
}
