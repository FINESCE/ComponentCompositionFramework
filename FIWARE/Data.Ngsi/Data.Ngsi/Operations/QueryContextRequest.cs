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
   [XmlRoot( "queryContextRequest" )]
   public class QueryContextRequest
   {
      /// <summary>
      /// List of identifiers of the Context Entity(ies) for which the 
      /// Context Information is requested. 
      /// Identifiers can contain patterns represented as regular 
      /// expressions.
      /// </summary>
      [XmlArray( "entityIdList" )]
      [XmlArrayItem( "entityId" )]
      public List<EntityID> EntityIDs { get; set; }

      /// <summary>
      /// List of ContextAttributes and/or AttributeDomains that are 
      /// queried. 
      /// Note: If this parameter is absent, the receiver of the request 
      /// SHALL return all attributes available. 
      /// NGSI component SHALL return an error if this parameter is 
      /// required by an service provider policy and is not specified in 
      /// the message.
      /// </summary>
      [XmlArray( "attributeList" )]
      [XmlArrayItem( "attribute" )]
      public List<string> Attributes { get; set; }

      /// <summary>
      /// Restriction on the result set of the query. Restrictions are 
      /// based on the values of attributes and meta-data of the 
      /// Context Information 
      /// </summary>
      [XmlElement( "restriction" )]
      public Restriction Restriction { get; set; }
   }
}
