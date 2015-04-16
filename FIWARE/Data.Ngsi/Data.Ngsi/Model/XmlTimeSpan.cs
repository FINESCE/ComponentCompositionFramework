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

namespace FIWARE.Data.Ngsi.Model
{
   public class XmlTimeSpan
   {
      private TimeSpan m_Value = TimeSpan.Zero;

      public XmlTimeSpan() 
      {
      }

      public XmlTimeSpan( TimeSpan value ) 
      {
         m_Value = value; 
      }

      public static implicit operator TimeSpan?( XmlTimeSpan o )
      {
         return o == null ? default( TimeSpan? ) : o.m_Value;
      }

      public static implicit operator XmlTimeSpan( TimeSpan? o )
      {
         return o == null ? null : new XmlTimeSpan( o.Value );
      }

      public static implicit operator TimeSpan( XmlTimeSpan o )
      {
         return o == null ? default( TimeSpan ) : o.m_Value;
      }

      public static implicit operator XmlTimeSpan( TimeSpan o )
      {
         return o == default( TimeSpan ) ? null : new XmlTimeSpan( o );
      }

      [XmlText]
      public string Default
      {
         get 
         {
            return XmlConvert.ToString( m_Value );
         }
         set
         {
            m_Value = XmlConvert.ToTimeSpan( value );
         }
      }
   }
}
