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
using FIWARE.Data.Ngsi.Model;

namespace FIWARE.Data.Ngsi.Operations
{
   [XmlRoot( "updateContextSubscriptionRequest" )]
   public class UpdateContextSubscriptionRequest
   {
      /// <summary>
      /// Requested duration of the subscription. Negative values will 
      /// result in an error. 
      /// The receiver of the request MUST reset the new subscription 
      /// duration starting at the time of reception of the request. 
      /// If the Context Management component has a policy to 
      /// always require duration, the operation SHALL return an error 
      /// in case the parameter is not present. 
      /// When the parameter is omitted it means that the previous 
      /// negotiated Duration value is applied, if any. 
      /// </summary>
      [XmlElement( "duration", Type = typeof( XmlTimeSpan ) )]
      public TimeSpan? Duration { get; set; }

      /// <summary>
      /// Restriction on the attributes and meta-data of the Context 
      /// Information. 
      /// When the parameter is omitted it means that the previous 
      /// Restriction is applied.
      /// </summary>
      [XmlElement( "restriction" )]
      public Restriction Restriction { get; set; }

      /// <summary>
      /// Identifier of the reference subscription to be updated 
      /// </summary>
      [XmlElement( "subscriptionId" )]
      public string SubscriptionID { get; set; }

      /// <summary>
      /// Conditions when to send notifications 
      /// When the parameter is omitted it means that the previous 
      /// NotifyCondition is applied. 
      /// </summary>
      [XmlArray( "notifyConditions" )]
      [XmlArrayItem( "notifyCondition" )]
      public List<NotifyCondition> NotifyConditions { get; set; }

      /// <summary>
      /// Proposed minimum interval between notifications. When the 
      /// parameter is omitted it means that the previous Throttling 
      /// value is applied. 
      /// </summary>
      [XmlElement( "throttling", Type = typeof( XmlTimeSpan ) )]
      public TimeSpan? Throttling { get; set; }
   }
}
