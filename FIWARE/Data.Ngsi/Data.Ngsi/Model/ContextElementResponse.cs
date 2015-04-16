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
   [XmlRoot( "contextElementResponse" )]
   public class ContextElementResponse
   {
      /// <summary>
      /// Context Information related to a Context Entity
      /// Note: In case of error, this data structure can contain only the
      /// EntityId or the EntityId/Attribute combination that cause the
      /// error. In case of success, this data structure contains also
      /// ContextAttribute and needed related ContextMetadata (e.g.
      /// ID).
      /// </summary>
      [XmlElement( "contextElement" )]
      public ContextElement ContextElement { get; set; }

      /// <summary>
      /// Identifies the status of the requested operation related to this
      /// specific ContextElement.
      /// </summary>
      [XmlElement( "statusCode" )]
      public StatusCode StatusCode { get; set; }
   }
}
