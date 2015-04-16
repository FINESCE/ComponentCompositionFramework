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
