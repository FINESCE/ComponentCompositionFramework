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
using Insero.ComponentCompositionFramework.Components;
using Insero.ComponentCompositionFramework.Components.Utilities;
using NLog;

namespace Insero.ComponentCompositionFramework.Sample.Comp1
{
   public class Component1 : ExtendedDisposableBase, IComponent, IConnectableComponent, IComponent1ExposedInterface
   {
      private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

      #region IComponent Members

      public string Name
      {
         get { return "Component 1"; }
      }

      public string Description
      {
         get { return "This is component 1"; }
      }

      public void Start()
      {
         _logger.Debug( "Logged from Start() of Component1" );
      }

      #endregion

      #region IConnectableComponent Members

      public IComponentCommunication GetCommunicationInterface()
      {
         return this;
      }

      #endregion

      #region IComponent1ExposedInterface Members

      public void SayHello()
      {
         _logger.Debug( "Component 1 says 'Hello'!" );
      }

      #endregion
   }
}
