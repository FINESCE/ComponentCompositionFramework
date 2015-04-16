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
using FIWARE.Data.Ngsi.Model;
using FIWARE.Data.Ngsi.Operations;
using Insero.ComponentCompositionFramework.Components;

namespace Insero.ComponentCompositionFramework.Components.NgsiProducer
{
   /// <summary>
   /// A component implementing the IContextManagementComponent represents
   /// a class that acts as a manager for Context Information for the NGSI
   /// context information model. It will expose an NGSI10 interface for
   /// clients to communicate with.
   /// </summary>
   public interface IContextManagementComponent : IComponentCommunication
   {
      /// <summary>
      /// Register an INgsiFrontend to the IContextManagementComponent.
      /// 
      /// This makes the IContextManagementComponent aware of possibility
      /// that there may exist context information at the frontend
      /// and allow it to retrieve it as needed.
      /// </summary>
      /// <param name="component">This is the registering INgsiFrontend</param>
      void Register( INgsiConnectingComponent component );

      /// <summary>
      /// Unregisters an INgsiFrontend that was previously registered from 
      /// the IContextMagenemtnComponents.
      /// 
      /// This makes the IContextManagementComponent aware that the 
      /// context elements provided by this INgsiFrontend no longer
      /// should be available. Also, the INgsiFrontend will no longer
      /// be called to retrieve context elements.
      /// </summary>
      /// <param name="component">This is the unregistering INgsiFronend</param>
      void Unregister( INgsiConnectingComponent component );

      /// <summary>
      /// Method to be called whenever new context information
      /// becomes available at an INgsiFrontend.
      /// </summary>
      /// <param name="component">This is the notifying INgsiFrontend</param>
      /// <param name="elements">These are the elements that has been updated</param>
      void BeginProduce( INgsiConnectingComponent component, IEnumerable<ContextElement> elements );
   }
}
