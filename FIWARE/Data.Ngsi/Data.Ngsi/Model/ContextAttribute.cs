using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;
using FIWARE.Data.Ngsi.Model.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FIWARE.Data.Ngsi.Model
{
   [XmlRoot( "contextAttribute" )]
   [JsonObject( MemberSerialization.OptIn, Id = "contextAttribute" )]
   public sealed class ContextAttribute
   {
      private DateTime? _MetadataTimestamp = null;

      /// <summary>
      /// Name of the Context Information attribute
      /// </summary>
      [XmlElement( ElementName = "name", DataType = "anyURI" )]
      [JsonProperty( "name", NullValueHandling = NullValueHandling.Ignore )]
      public string Name { get; set; }

      /// <summary>
      /// Indicates the type of the value field
      /// </summary>
      [XmlElement( ElementName = "type" )]
      [JsonProperty( "type", NullValueHandling = NullValueHandling.Ignore )]
      public string Type { get; set; }

      /// <summary>
      /// Metadata about the Context Information attribute
      /// (information valid only for the specific attribute)
      /// </summary>
      [XmlArray( ElementName = "metadata" )]
      [XmlArrayItem( ElementName = "contextMetadata" )]
      [JsonProperty( "metadata", NullValueHandling = NullValueHandling.Ignore )]
      public List<ContextMetadata> ContextMetadata { get; set; }

      /// <summary>
      /// The actual value of the Context Information attribute.
      /// 
      /// Do NOT use this property. Use ContextValue instead.
      /// </summary>
      [XmlAnyElement( Name = "contextValue" )]
      [JsonIgnore]
      public XElement ContextValueXElement
      {
         get;
         set;
      }

      /// <summary>
      /// The actual value of the Context Information attribute.
      /// 
      /// Do NOT use this property. Use ContextValue instead.
      /// </summary>
      [JsonProperty( "contextValue", NullValueHandling = NullValueHandling.Ignore )]
      [XmlIgnore]
      public object ContextValueJElement
      {
         get
         {
            if ( ContextValueXElement == null )
            {
               return null;
            }

            var item = ContextValueXElement.DescendantNodes().FirstOrDefault();
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
      /// Gets or sets the ContextValue associated with this 
      /// ContextAttribute.
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
      /// the ReadContextValueAs method.
      /// </summary>
      [XmlIgnore]
      [JsonIgnore]
      public object ContextValue
      {
         set
         {
            m_ContextValue = value;
            if ( m_ContextValue == null )
            {
               ContextValueXElement = null;
            }
            else
            {
               ContextValueXElement = XmlAnyUtilities.SetValue( value, "contextValue" );
            }
         }
      }

      private object m_ContextValue;

      /// <summary>
      /// Reads a ContextValue by parsing it as the specified CLR type.
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <returns></returns>
      public T ReadContextValueAs<T>()
      {
         if ( m_ContextValue != null )
         {
            return (T)m_ContextValue;
         }

         if ( ContextValueXElement != null )
         {
            m_ContextValue = XmlAnyUtilities.GetValueAs<T>( ContextValueXElement );
         }
         else
         {
            return default( T );
         }

         return (T)m_ContextValue;
      }

      public DateTime? GetTimestampMetadata()
      {
         if ( _MetadataTimestamp == null )
         {
            var metadata = ContextMetadata.FirstOrDefault( x => x.Name == "Timestamp" );
            if ( metadata == null )
            {
               return null;
            }
            string timestampString = metadata.ReadValueAs<string>();
            _MetadataTimestamp = XmlConvert.ToDateTime( timestampString, XmlDateTimeSerializationMode.Utc );
            return _MetadataTimestamp;
         }
         else
         {
            return _MetadataTimestamp;
         }
      }

      public ContextAttribute Copy()
      {
         var attr = new ContextAttribute
         {
            m_ContextValue = m_ContextValue,
            ContextValueXElement = ContextValueXElement,
            Name = Name,
            Type = Type
         };

         if ( ContextMetadata != null )
         {
            attr.ContextMetadata = new List<ContextMetadata>();
            for ( int i = 0 ; i < ContextMetadata.Count ; i++ )
            {
               attr.ContextMetadata.Add( ContextMetadata[ i ].Copy() );
            }
         }

         return attr;
      }
   }
}
