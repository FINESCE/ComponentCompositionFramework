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
using Newtonsoft.Json.Serialization;

namespace FIWARE.Data.Ngsi.Model
{
   [XmlRoot( "statusCode" )]
   [JsonObject( MemberSerialization.OptIn, Id = "statusCode" )]
   public class StatusCode
   {
      public StatusCode()
      {

      }

      public StatusCode( StatusCodes code )
      {
         Code = (int)code;
         ReasonPhrase = code.AsReasonPhrase();
      }

      /// <summary>
      /// Numerical value that identifies the status code.
      /// </summary>
      [XmlElement( "code" )]
      [JsonProperty( "code", NullValueHandling = NullValueHandling.Ignore )]
      public int Code { get; set; }

      /// <summary>
      /// Human readable text that describes the status code
      /// </summary>
      [XmlElement( "reasonPhrase" )]
      [JsonProperty( "reasonPhrase", NullValueHandling = NullValueHandling.Ignore )]
      public string ReasonPhrase { get; set; }

      /// <summary>
      /// Contains more details on the StatusCode
      /// 
      /// Do NOT use this property. Use Details property instead.
      /// </summary>
      [XmlAnyElement( Name = "details" )]
      [JsonIgnore]
      public XElement DetailsXElement
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
      [JsonProperty( "details", NullValueHandling = NullValueHandling.Ignore )]
      public object DetailsJElement
      {
         get
         {
            if ( DetailsXElement == null )
            {
               return null;
            }

            var item = DetailsXElement.DescendantNodes().FirstOrDefault();
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
      /// Gets or sets the Details associated with this 
      /// StatusCode.
      /// 
      /// The following types are supported with the set operation:
      /// * String
      /// * Simple types
      /// * Other CLR objects that are XML serializable
      /// 
      /// If the getter of this property is used on a type that is not
      /// a string, the method will return an XElement that can be parsed
      /// to the required CLR type through the XmlSerializer. However,
      /// the recommended approach for retrieving these values are through
      /// the ReadDetailsAs method.
      /// </summary>
      [XmlIgnore]
      [JsonIgnore]
      public object Details
      {
         set
         {
            m_Details = value;
            if ( m_Details == null )
            {
               DetailsXElement = null;
            }
            else
            {
               DetailsXElement = XmlAnyUtilities.SetValue( value, "details" );
            }
         }
      }

      private object m_Details;

      /// <summary>
      /// Reads the details by parsing it as the specified CLR type.
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <returns></returns>
      public T ReadDetailsAs<T>()
      {
         if ( m_Details != null )
         {
            return (T)m_Details;
         }

         if ( DetailsXElement != null )
         {
            m_Details = XmlAnyUtilities.GetValueAs<T>( DetailsXElement );
         }
         else
         {
            return default( T );
         }

         return (T)m_Details;
      }

      public StatusCodes GetStatusCode()
      {
         return (StatusCodes)Code;
      }

      public void SetStatusCode( StatusCodes code )
      {
         Code = (int)code;
      }

      public override string ToString()
      {
         return GetStatusCode().AsReasonPhrase();
      }
   }
}
