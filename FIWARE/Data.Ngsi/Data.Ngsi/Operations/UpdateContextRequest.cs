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
   [XmlRoot( "updateContextRequest" )]
   public class UpdateContextRequest
   {
      /// <summary>
      /// List of Context Elements containing only the subset of 
      /// Context Information (related attributes (or context domain) 
      /// and metadata) to be modified.
      /// </summary>
      [XmlArray( "contextElementList" )]
      [XmlArrayItem( "contextElement" )]
      public List<ContextElement> ContextElements { get; set; }

      /// <summary>
      /// Indicates the type of action that is performed within the 
      /// update operation: 
      /// • update: it replaces the value and metadata of the 
      /// existing attributes with the same name;
      /// • append: it adds the new attribute. Note: this may 
      /// result in multiple attributes with the same name;
      /// • delete: it removes the existing value.
      /// </summary>
      [XmlElement( "updateAction" )]
      public UpdateActionType UpdateAction { get; set; }
   }
}
