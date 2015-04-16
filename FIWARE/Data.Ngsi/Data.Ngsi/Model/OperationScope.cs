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
using System.Xml.Linq;
using System.Xml.Serialization;
using FIWARE.Data.Ngsi.Model.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FIWARE.Data.Ngsi.Model
{
   /// <summary>
   /// The OperationScope data structure defines:
   /// • the ScopeType which selects the type of Scope that is used, and
   /// • the ScopeValue which defines parameter of the scope.
   /// </summary>
   [XmlRoot( "operationScope" )]
   [JsonObject( MemberSerialization.OptIn, Id = "statusCode" )]
   public class OperationScope
   {
      /// <summary>
      /// Name of the scope type.
      /// </summary>
      [XmlElement( "scopeType" )]
      [JsonProperty( "scopeType", NullValueHandling = NullValueHandling.Ignore )]
      public string ScopeType { get; set; }

      /// <summary>
      /// Contains the scope value for the defined scope type.
      /// 
      /// Do NOT use this property. Use ScopeValue instead.
      /// </summary>
      [XmlAnyElement( Name = "scopeValue" )]
      [JsonIgnore]
      public XElement ScopeValueXElement
      {
         get;
         set;
      }

      /// <summary>
      /// The actual value of the Context Information attribute.
      /// 
      /// Do NOT use this property. Use ContextValue instead.
      /// </summary>
      [XmlIgnore]
      [JsonProperty( "scopeValue", NullValueHandling = NullValueHandling.Ignore )]
      public object ScopeValueJElement
      {
         get
         {
            if ( ScopeValueXElement == null )
            {
               return null;
            }

            var item = ScopeValueXElement.DescendantNodes().FirstOrDefault();
            if ( item == null )
            {
               return null;
            }

            if ( item is XText )
            {
               return ( (XText)item ).Value;
            }
            return item;
         }
      }

      /// <summary>
      /// Gets or sets the ScopeValue associated with this 
      /// OperationScope.
      /// 
      /// The following types are supported with the set operation:
      /// * String
      /// * Simple types
      /// * Other CLR objects that are XML serializable
      /// 
      /// The following types are supported with the get operation:
      /// * String
      /// 
      /// If the getter of this property is used on a type that is not
      /// a string, the method will return an XElement that can be parsed
      /// to the required CLR type through the XmlSerializer. However,
      /// the recommended approach for retrieving these values are through
      /// the ReadContextValueAs method.
      /// </summary>
      [XmlIgnore]
      [JsonIgnore]
      public object ScopeValue
      {
         set
         {
            m_ScopeValue = value;
            if ( m_ScopeValue == null )
            {
               ScopeValueXElement = null;
            }
            else
            {
               ScopeValueXElement = XmlAnyUtilities.SetValue( value, "scopeValue" );
            }
         }
      }

      private object m_ScopeValue;

      /// <summary>
      /// Reads the ScopeValue by parsing it as the specified CLR type.
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <returns></returns>
      public T ReadScopeValueAs<T>()
      {
         if ( m_ScopeValue != null )
         {
            return (T)m_ScopeValue;
         }

         if ( ScopeValueXElement != null )
         {
            m_ScopeValue = XmlAnyUtilities.GetValueAs<T>( ScopeValueXElement );
         }
         else
         {
            return default( T );
         }

         return (T)m_ScopeValue;
      }
   }
}
