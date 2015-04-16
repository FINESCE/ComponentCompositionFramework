using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
   [XmlRoot( ElementName = "contextMetadata" )]
   [JsonObject( MemberSerialization.OptIn, Id = "contextMetadata" )]
   public sealed class ContextMetadata
   {
      /// <summary>
      /// Name of the metadata.
      /// </summary>
      [XmlElement( ElementName = "name" )]
      [JsonProperty( "name", NullValueHandling = NullValueHandling.Ignore )]
      public string Name { get; set; }

      /// <summary>
      /// Indicates the type of the value field
      /// </summary>
      [XmlElement( ElementName = "type", DataType = "anyURI" )]
      [JsonProperty( "type", NullValueHandling = NullValueHandling.Ignore )]
      public string Type { get; set; }

      /// <summary>
      /// The actual value of the metadata
      /// </summary>
      [XmlAnyElement( Name = "value" )]
      [JsonIgnore]
      public XElement ValueXElement
      {
         get;
         set;
      }

      /// <summary>
      /// The actual value of the Context Information attribute.
      /// 
      /// Do NOT use this property. Use Value instead.
      /// </summary>
      [XmlIgnore]
      [JsonProperty( "value", NullValueHandling = NullValueHandling.Ignore )]
      public object ValueJElement
      {
         get
         {
            if ( ValueXElement == null )
            {
               return null;
            }

            var item = ValueXElement.DescendantNodes().FirstOrDefault();
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
      /// Gets or sets the Value associated with this 
      /// ContextMetadata.
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
      public object Value
      {
         set
         {
            m_Value = value;
            if ( m_Value == null )
            {
               ValueXElement = null;
            }
            else
            {
               ValueXElement = XmlAnyUtilities.SetValue( value, "value" );
            }
         }
      }

      private object m_Value;

      /// <summary>
      /// Reads a Value by parsing it as the specified CLR type.
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <returns></returns>
      public T ReadValueAs<T>()
      {
         if ( m_Value != null )
         {
            return (T)m_Value;
         }

         if ( ValueXElement != null )
         {
            m_Value = XmlAnyUtilities.GetValueAs<T>( ValueXElement );
         }
         else
         {
            return default( T );
         }

         return (T)m_Value;
      }

      public ContextMetadata Copy()
      {
         var metadata = new ContextMetadata
         {
            m_Value = m_Value,
            Name = Name,
            Type = Type
         };

         return metadata;
      }
   }
}
