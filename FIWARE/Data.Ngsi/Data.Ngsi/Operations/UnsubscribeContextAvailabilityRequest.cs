using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIWARE.Data.Ngsi.Operations
{
   [XmlRoot( "unsubscribeContextAvailabilityRequest" )]
   public class UnsubscribeContextAvailabilityRequest
   {
      /// <summary>
      /// Identifier used in the discoverContextAvailabilityRequest
      /// messages 
      /// </summary>
      [XmlElement( "subscriptionId" )]
      public string subscriptionID { get; set; }
   }
}
