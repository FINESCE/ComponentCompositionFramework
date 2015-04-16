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

namespace Insero.ComponentCompositionFramework.Composition
{
   public abstract class ComponentComposerBase : IComponentComposer
   {
      public event EventHandler<ComponentsChangedEventArgs> ComponentsChanged;
      public event EventHandler<MediatorStateChangedEventArgs> StateChanged;

      protected internal void TryConnect( ComponentModel component1, ComponentModel component2 )
      {
         component1.TryConnect( component2 );
      }

      protected internal void TryDisconnect( ComponentModel component1, ComponentModel component2 )
      {
         component1.TryDisconnect( component2 );
      }

      protected void RaiseComponentsChanged( ComponentModel componentModel, ChangeAction action )
      {
         var handler = ComponentsChanged;
         if ( handler != null )
         {
            handler( this, new ComponentsChangedEventArgs( componentModel, action ) );
         }
      }

      protected void RaiseStateChanged( MediatorState state )
      {
         var handler = StateChanged;
         if ( handler != null )
         {
            handler( this, new MediatorStateChangedEventArgs( state ) );
         }
      }

      public abstract IEnumerable<ComponentModel> Components { get; }

      public abstract bool IsStarted { get; }

      public abstract void Start();

      public abstract void Stop();
   }
}
