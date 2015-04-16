using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FIWARE.Data.Ngsi.Model;

namespace FIWARE.Data.Ngsi.Operations
{
   [XmlRoot( "updateContextResponse" )]
   public class UpdateContextResponse
   {
      /// <summary>
      /// Error codes
      /// </summary>
      [XmlElement( "errorCode" )]
      public StatusCode ErrorCode { get; set; }

      /// <summary>
      /// List of response containing the indication of the Context 
      /// Element and the related statusCode
      /// </summary>
      [XmlArray( "contextResponseList" )]
      [XmlArrayItem( "contextElementResponse" )]
      public List<ContextElementResponse> ContextResponses { get; set; }
   }
}
