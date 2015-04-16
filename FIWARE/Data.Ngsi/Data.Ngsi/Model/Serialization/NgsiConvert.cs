using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace FIWARE.Data.Ngsi.Model.Serialization
{
   public static class NgsiConvert
   {
      private static readonly XmlSerializerNamespaces NoNamespaces;

      static NgsiConvert()
      {
         NoNamespaces = new XmlSerializerNamespaces();

         // This may or may not be supported...
         NoNamespaces.Add( "", "" );
      }

      public static void WriteObject( Stream stream, object obj, bool indent )
      {
         var settings = new XmlWriterSettings();
         settings.Indent = indent;
         WriteObject( XmlWriter.Create( stream, settings ), obj );
      }

      public static void WriteObject( Stream stream, object obj )
      {
         WriteObject( XmlWriter.Create( stream ), obj );
      }

      public static void WriteObject( XmlWriter writer, object obj )
      {
         var serializer = new XmlSerializer( obj.GetType() );
         serializer.Serialize( writer, obj, NoNamespaces );
      }

      public static string WriteObject( object obj )
      {
         using ( var stream = new MemoryStream() )
         {
            WriteObject( stream, obj, true );
            stream.Seek( 0, SeekOrigin.Begin );
            return new UTF8Encoding( false ).GetString( stream.ToArray() );
         }
      }

      public static T ReadObject<T>( string xml )
      {
         // http://kristofmattei.be/2012/06/10/deserializing-xml-data-at-the-root-level-is-invalid-line-1-position-1-and-there-is-an-error-in-xml-document-1-1/
         var serializer = new XmlSerializer( typeof( T ) );
         using ( var reader = new StringReader( xml.TrimStart( (char)65279 ) ) )
         {
            return (T) serializer.Deserialize( reader );
         }
      }

      public static T ReadObject<T>( Stream stream )
      {
         return ReadObject<T>( XmlReader.Create( stream ) );
      }

      public static T ReadObject<T>( XmlReader reader )
      {
         var serializer = new XmlSerializer( typeof( T ) );
         return (T)serializer.Deserialize( reader );
      }

      public static object ReadObject( Stream stream, Type type )
      {
         var serializer = new XmlSerializer( type );
         return serializer.Deserialize( stream );
      }

      public static object ReadObject( string xml, Type type )
      {
         // http://kristofmattei.be/2012/06/10/deserializing-xml-data-at-the-root-level-is-invalid-line-1-position-1-and-there-is-an-error-in-xml-document-1-1/
         var serializer = new XmlSerializer( type );
         using ( var reader = new StringReader( xml.TrimStart( (char)65279 ) ) )
         {
            return serializer.Deserialize( reader );
         }
      }
   }
}
