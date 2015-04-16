using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FIWARE.Data.Ngsi.Model;

namespace FIWARE.Data.Ngsi.Operations
{
   [XmlRoot( "unsubscribeContextResponse" )]
   public class UnsubscribeContextResponse
   {
      /// <summary>
      /// The identifier of the subscription 
      /// </summary>
      [XmlElement( "subscriptionId" )]
      public string SubscriptionID { get; set; }

      /// <summary>
      /// The status reported by the receiver of the request 
      /// </summary>
      [XmlElement( "statusCode" )]
      public StatusCode StatusCode { get; set; }
   }
}
