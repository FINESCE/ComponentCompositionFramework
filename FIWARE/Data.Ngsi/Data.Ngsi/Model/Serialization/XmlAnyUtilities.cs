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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FIWARE.Data.Ngsi.Model.Serialization
{
   internal static class XmlAnyUtilities
   {
      internal static object GetValue( XElement xElements )
      {
         if ( !xElements.HasElements )
         {
            return xElements.Value;
         }
         else
         {
            var firstChildElement = xElements.Elements().First();

            return firstChildElement;
         }
      }

      internal static T GetValueAs<T>( XElement xElement )
      {
         if ( typeof( T ) == typeof( string ) )
         {
            return (T)GetValue( xElement );
         }

         if ( xElement.HasElements )
         {
            var firstChildElement = xElement.Elements().First();
            using ( var reader = firstChildElement.CreateReader() )
            {
               return NgsiConvert.ReadObject<T>( reader );
            }
         }

         return default( T );
      }

      internal static XElement SetValue( object value, string rootValueName )
      {
         if ( value == null )
         {
            return null;
         }

         XElement valueElement = null;

         if ( value is string )
         {
            valueElement = new XElement( rootValueName, (string)value );
         }
         else if ( value is XElement )
         {
            valueElement = new XElement( rootValueName, (XElement)value );
         }
         else
         {
            // arbitrary object, create object graph
            XDocument doc = new XDocument();
            using ( var writer = doc.CreateWriter() )
            {
               // write xml into the writer
               NgsiConvert.WriteObject( writer, value );
            }
            valueElement = new XElement( rootValueName, doc.Root );
         }

         return valueElement;
      }
   }
}
