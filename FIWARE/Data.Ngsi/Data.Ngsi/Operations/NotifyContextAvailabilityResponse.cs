using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FIWARE.Data.Ngsi.Model;

namespace FIWARE.Data.Ngsi.Operations
{
   [XmlRoot( "notifyContextAvailabilityResponse" )]
   public class NotifyContextAvailabilityResponse
   {
      /// <summary>
      /// Status codes for general operation errors 
      /// </summary>
      [XmlElement( "responseCode" )]
      public StatusCode ResponseCode { get; set; }
   }
}
