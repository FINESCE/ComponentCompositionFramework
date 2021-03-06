﻿/*
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
   [XmlRoot( "contextRegistrationAttribute" )]
   public class ContextRegistrationAttribute
   {
      /// <summary>
      /// Name of the ContextAttribute and or AttributeDomain.
      /// </summary>
      [XmlElement( "name" )]
      public string Name { get; set; }

      /// <summary>
      /// Indicates the type of the ContextAttribute value
      /// </summary>
      [XmlElement( "type" )]
      public string Type { get; set; }

      /// <summary>
      /// Indicates if this structure refers to a ContexAttribute or a
      /// AttributeDomain
      /// </summary>
      [XmlElement( "isDomain" )]
      public bool IsDomain { get; set; }

      /// <summary>
      /// Metadata about the Context Information attribute
      /// (information valid only for the specific attribute)
      /// </summary>
      [XmlArray( "metadata" )]
      [XmlArrayItem( "contextMetadata" )]
      public List<ContextMetadata> Metadata { get; set; }
   }
}
