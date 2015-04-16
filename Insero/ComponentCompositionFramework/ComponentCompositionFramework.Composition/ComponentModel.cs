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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insero.ComponentCompositionFramework.Composition.Events;
using Insero.ComponentCompositionFramework.Components;
using NLog;

namespace Insero.ComponentCompositionFramework.Composition
{
   public class ComponentModel
   {
      private static Logger _logger = LogManager.GetCurrentClassLogger();

      public event EventHandler<ComponentsChangedEventArgs> ConnectedComponentsChanged;

      private IComponent _component;
      private List<Type> _connectableCommunicationIntefaces;
      private List<ComponentModel> _connectedComponents;

      public ComponentModel( IComponent frontend )
      {
         _component = frontend;
         _connectableCommunicationIntefaces = frontend.GetType()
                                                       .GetInterfaces()
                                                       .Where( type => type.Name == "IConnectingComponent`1" )
                                                       .Select( x => x.GetGenericArguments().First() )
                                                       .ToList();

         _connectedComponents = new List<ComponentModel>();
      }

      public IReadOnlyList<ComponentModel> ConnectedComponents
      {
         get
         {
            return _connectedComponents;
         }
      }

      public IComponent Component
      {
         get
         {
            return _component;
         }
      }

      private bool CanConnect( IComponentCommunication connectingCommunicationInterface )
      {
         return connectingCommunicationInterface.GetType()
                                                .GetInterfaces()
                                                .Any( x => _connectableCommunicationIntefaces.Contains( x ) );
      }

      internal void TryConnect( ComponentModel connectingComponent )
      {
         var connectingComponentInterface = connectingComponent.Component as IConnectableComponent;
         if ( connectingComponentInterface != null )
         {
            var connectingCommunicationInterface = connectingComponentInterface.GetCommunicationInterface();
            if ( connectingCommunicationInterface != null )
            {
               var canConnect = CanConnect( connectingCommunicationInterface );
               if ( canConnect )
               {
                  _logger.Info( "Connecting the component '{0}' to the component '{1}'", Component.Name, connectingComponentInterface.Name );

                  dynamic fe = Component;
                  dynamic bc = connectingCommunicationInterface;
                  fe.ConnectComponent( bc );

                  // update model
                  Add( connectingComponent );
                  connectingComponent.Add( this );

                  _logger.Info( "Connected the component '{0}' to the component '{1}'", Component.Name, connectingComponentInterface.Name );
               }
            }
         }
         else
         {
            // try to connect the other way, if the connecting component was not a connectable
            var thisComponent = Component as IConnectableComponent;
            if ( thisComponent != null )
            {
               var thisComponentCommunicationInterface = thisComponent.GetCommunicationInterface();
               if ( thisComponentCommunicationInterface != null )
               {
                  var canConnect = connectingComponent.CanConnect( thisComponentCommunicationInterface );
                  if ( canConnect )
                  {
                     _logger.Info( "Connecting the component '{0}' to the component '{1}'", connectingComponent.Component.Name, Component.Name );

                     dynamic fe = connectingComponent.Component;
                     dynamic bc = thisComponentCommunicationInterface;
                     fe.ConnectComponent( bc );

                     // update model
                     Add( connectingComponent );
                     connectingComponent.Add( this );

                     _logger.Info( "Connected the component '{0}' to the component '{1}'", connectingComponent.Component.Name, Component.Name );
                  }
               }
            }
         }
      }

      internal void TryDisconnect( ComponentModel disconnectingComponent )
      {
         var disconnectingComponentInterface = disconnectingComponent.Component as IConnectableComponent;
         if ( disconnectingComponentInterface != null )
         {
            var disconnectingCommunicationInterface = disconnectingComponentInterface.GetCommunicationInterface();
            if ( disconnectingCommunicationInterface != null )
            {
               var canDisconnect = CanConnect( disconnectingCommunicationInterface );
               if ( canDisconnect )
               {
                  _logger.Info( "Disconnecting the component '{0}' to the component '{1}'", Component.Name, disconnectingComponentInterface.Name );

                  dynamic fe = Component;
                  dynamic bc = disconnectingCommunicationInterface;
                  fe.DisconnectComponent( bc ); // TODO: Change this to another method name

                  // update model
                  Remove( disconnectingComponent );
                  disconnectingComponent.Remove( this );

                  _logger.Info( "Disconnected the component '{0}' to the component '{1}'", Component.Name, disconnectingComponentInterface.Name );
               }
            }
         }
         else
         {
            // try to connect the other way, if the connecting component was not a connectable
            var thisComponent = Component as IConnectableComponent;
            if ( thisComponent != null )
            {
               var thisComponentCommunicationInterface = thisComponent.GetCommunicationInterface();
               if ( thisComponentCommunicationInterface != null )
               {
                  var canDisconnect = disconnectingComponent.CanConnect( thisComponentCommunicationInterface );
                  if ( canDisconnect )
                  {
                     _logger.Info( "Connecting the component '{0}' to the component '{1}'", disconnectingComponentInterface.Name, Component.Name );

                     dynamic fe = disconnectingComponent.Component;
                     dynamic bc = thisComponentCommunicationInterface;
                     fe.DisconnectComponent( bc ); // TODO: Change this to another method name

                     // update model
                     Remove( disconnectingComponent );
                     disconnectingComponent.Remove( this );

                     _logger.Info( "Connected the component '{0}' to the component '{1}'", disconnectingComponentInterface.Name, Component.Name );
                  }
               }
            }
         }
      }

      internal void Add( ComponentModel component )
      {
         _connectedComponents.Add( component );

         RaiseConnectedComponentsChanged( component, ChangeAction.Added );
      }

      internal void Remove( ComponentModel component )
      {
         _connectedComponents.Remove( component );

         RaiseConnectedComponentsChanged( component, ChangeAction.Removed );
      }

      private void RaiseConnectedComponentsChanged( ComponentModel component, ChangeAction action )
      {
         var handler = ConnectedComponentsChanged;
         if ( handler != null )
         {
            handler( this, new ComponentsChangedEventArgs( component, action ) );
         }
      }
   }
}
