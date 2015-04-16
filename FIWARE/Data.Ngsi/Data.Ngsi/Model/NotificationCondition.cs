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
   public enum NotificationCondition
   {
      /// <summary>
      /// The condition is true when the time interval specified in the value field
      /// is reached.
      /// </summary>
      [XmlEnum( "ONTIMEINTERVAL" )]
      OnTimeInterval,

      /// <summary>
      /// The condition is true if the value of one or more context attributes fits
      /// a reference value and/or range, specified in the Restriction parameter
      /// </summary>
      [XmlEnum( "ONVALUE" )]
      OnValue,

      /// <summary>
      /// The condition is true when a change in one of the specified context
      /// attributes has occurred
      /// </summary>
      [XmlEnum( "ONCHANGE" )]
      OnChange
   }
}
