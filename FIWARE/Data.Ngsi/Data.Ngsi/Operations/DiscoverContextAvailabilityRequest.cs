using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FIWARE.Data.Ngsi.Model;

namespace FIWARE.Data.Ngsi.Operations
{
   [XmlRoot( "discoverContextAvailabilityRequest" )]
   public class DiscoverContextAvailabilityRequest
   {
      /// <summary>
      /// List of Modifier to identify the Context Entity(ies) to discover.
      /// </summary>
      [XmlElement( "entityId" )]
      public EntityID EntityId { get; set; }

      /// <summary>
      /// List of attributes or group of attributes to discover.
      /// </summary>
      [XmlArray( "attributeList" )]
      [XmlArrayItem( "attribute" )]
      public List<string> Attributes { get; set; }

      /// <summary>
      /// Restriction on the attributes and meta-data of the Context
      /// Information
      /// </summary>
      [XmlElement( "restriction" )]
      public Restriction Restriction { get; set; }
   }
}
