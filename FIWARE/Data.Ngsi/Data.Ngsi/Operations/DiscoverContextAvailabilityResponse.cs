using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FIWARE.Data.Ngsi.Model;

namespace FIWARE.Data.Ngsi.Operations
{
   [XmlRoot( "discoverContextAvailabilityResponse" )]
   public class DiscoverContextAvailabilityResponse
   {
      /// <summary>
      /// List of Context Registration responses
      /// </summary>
      [XmlArray( "contextRegistationResponseList" )]
      [XmlArrayItem( "contextRegistrationResponse" )]
      public List<ContextRegistrationResponse> ContextRegistrationsResponses { get; set; }

      /// <summary>
      /// Error codes for general operation errors
      /// </summary>
      [XmlElement( "errorCode" )]
      public StatusCode ErrorCode { get; set; }
   }
}
