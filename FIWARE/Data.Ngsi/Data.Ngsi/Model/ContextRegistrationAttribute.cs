using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIWARE.Data.Ngsi.Model
{
   [XmlRoot( "contextRegistrationAttribute" )]
   public class ContextRegistrationAttribute
   {
      /// <summary>
      /// Name of the ContextAttribute and or AttributeDomain.
      /// </summary>
      [XmlElement( "name" )]
      public string Name { get; set; }

      /// <summary>
      /// Indicates the type of the ContextAttribute value
      /// </summary>
      [XmlElement( "type" )]
      public string Type { get; set; }

      /// <summary>
      /// Indicates if this structure refers to a ContexAttribute or a
      /// AttributeDomain
      /// </summary>
      [XmlElement( "isDomain" )]
      public bool IsDomain { get; set; }

      /// <summary>
      /// Metadata about the Context Information attribute
      /// (information valid only for the specific attribute)
      /// </summary>
      [XmlArray( "metadata" )]
      [XmlArrayItem( "contextMetadata" )]
      public List<ContextMetadata> Metadata { get; set; }
   }
}
