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
   /// <summary>
   /// The Restriction data structure contains two different kinds of restrictions:
   /// • the AttributeExpression filters the result set based on expressions on the values of the context attributes
   /// • the Scope restricts the operational search space on which a given operation needs to operate
   /// Compared to attributeExpression parameter, scopes a-priori limit the set of context sources that are needed for serving the
   /// request. The criteria used in operational scopes do not need to be part of the Context Information itself. Appendix D gives
   /// more explanation for scopes
   /// </summary>
   [XmlRoot( ElementName = "restriction" )]
   public class Restriction
   {
      /// <summary>
      /// String containing an XPath restriction.
      /// Note: The XPath expression will be evaluated against
      /// ContextEntity structures.
      /// </summary>
      [XmlElement( "attributeExpression" )]
      public string AttributeExpression { get; set; }

      /// <summary>
      /// List of scope definition
      /// </summary>
      [XmlElement( "scope" )]
      public OperationScope Scope { get; set; }
   }
}
