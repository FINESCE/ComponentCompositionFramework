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
using System.Xml.Serialization;

namespace FIWARE.Data.Ngsi.Model
{
   [XmlRoot( "subscribeResponse" )]
   public class SubscribeResponse
   {
      /// <summary>
      /// The identifier of the subscription
      /// </summary>
      [XmlElement( "subscriptionId" )]
      public string SubscriptionID { get; set; }

      /// <summary>
      /// Negotiated duration of the subscription.
      /// The Context Management component MAY omit this
      /// parameter if it allows indefinite subscriptions
      /// </summary>
      [XmlElement( "duration", Type = typeof( XmlTimeSpan ) )]
      public TimeSpan? Duration { get; set; }

      /// <summary>
      /// Negotiated minimum interval between notifications. If a
      /// Throttling value were proposed into the
      /// subscribeContextRequest, this parameter SHALL be
      /// specified.
      /// </summary>
      [XmlElement( "throttling", Type = typeof( XmlTimeSpan ) )]
      public TimeSpan? Throttling { get; set; }
   }
}
