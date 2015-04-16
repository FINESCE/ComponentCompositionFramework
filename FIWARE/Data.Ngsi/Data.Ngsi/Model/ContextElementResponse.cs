using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIWARE.Data.Ngsi.Model
{
   [XmlRoot( "contextElementResponse" )]
   public class ContextElementResponse
   {
      /// <summary>
      /// Context Information related to a Context Entity
      /// Note: In case of error, this data structure can contain only the
      /// EntityId or the EntityId/Attribute combination that cause the
      /// error. In case of success, this data structure contains also
      /// ContextAttribute and needed related ContextMetadata (e.g.
      /// ID).
      /// </summary>
      [XmlElement( "contextElement" )]
      public ContextElement ContextElement { get; set; }

      /// <summary>
      /// Identifies the status of the requested operation related to this
      /// specific ContextElement.
      /// </summary>
      [XmlElement( "statusCode" )]
      public StatusCode StatusCode { get; set; }
   }
}
