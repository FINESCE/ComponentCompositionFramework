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
using System.Xml;
using System.Xml.Serialization;
using FIWARE.Data.Ngsi.Model;

namespace FIWARE.Data.Ngsi.Operations
{
   [XmlRoot( "subscribeContextAvailabilityRequest" )]
   public class SubscribeContextAvailabilityRequest
   {
      /// <summary>
      /// List of identifiers or name patterns of the Context Entity(ies) 
      /// to discover
      /// </summary>
      [XmlElement( "entityId" )]
      public EntityID EntityID { get; set; }

      /// <summary>
      /// List of attributes or group of attributes to be discovered 
      /// </summary>
      [XmlArray( "attributeList" )]
      [XmlArrayItem( "attribute" )]
      public List<string> Attributes { get; set; }

      /// <summary>
      /// The interface reference for the notifyContextAvailability 
      /// operation.
      /// </summary>
      [XmlElement( "reference", DataType = "anyURI" )]
      public string Reference { get; set; }

      /// <summary>
      /// Requested duration of the subscription. Negative values 
      /// SHALL result in an error. In case of the value 0, the context 
      /// component SHALL only notify the current value and SHALL 
      /// NOT send subsequent notifications (one-time subscription). 
      /// If the Context Management component has a policy to 
      /// always require duration, the operation SHALL return an error 
      /// in case the parameter is not present. 
      /// If the parameter is omitted, the Context Management 
      /// component MAY select a duration and return this in the 
      /// response.
      /// </summary>
      [XmlElement( "duration", Type = typeof( XmlTimeSpan ) )]
      public TimeSpan? Duration { get; set; }

      /// <summary>
      /// Restriction on the attributes and meta-data of the Context 
      /// Information
      /// </summary>
      [XmlElement( "restriction" )]
      public Restriction Restriction { get; set; }

      /// <summary>
      /// Used in the notification message and subsequent requests 
      /// </summary>
      [XmlElement( "subscriptionId" )]
      public string SubscriptionID { get; set; }
   }
}
