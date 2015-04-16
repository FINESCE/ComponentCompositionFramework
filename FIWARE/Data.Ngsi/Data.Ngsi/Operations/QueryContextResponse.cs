using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FIWARE.Data.Ngsi.Model;

namespace FIWARE.Data.Ngsi.Operations
{
   [XmlRoot( "queryContextResponse" )]
   public class QueryContextResponse
   {
      public static readonly QueryContextResponse NotSupported = new QueryContextResponse
      {
         ErrorCode = new StatusCode( StatusCodes.InvalidParameter )
      };

      /// <summary>
      /// List of Context Information, related attributes (or group of 
      /// attributes) and metadata.
      /// </summary>
      [XmlArray( "contextResponseList" )]
      [XmlArrayItem( "contextElementResponse" )]
      public List<ContextElementResponse> ContextResponses { get; set; }

      /// <summary>
      /// Error codes for general operation errors
      /// </summary>
      [XmlElement( "errorCode" )]
      public StatusCode ErrorCode { get; set; }
   }
}
