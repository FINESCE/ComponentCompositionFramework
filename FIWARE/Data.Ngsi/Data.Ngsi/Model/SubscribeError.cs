using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIWARE.Data.Ngsi.Model
{
   [XmlRoot( "subscribeError" )]
   public class SubscribeError
   {
      /// <summary>
      /// The identifier of the subscription. This parameter is
      /// mandatory in case of updateContextSubscriptionResponse
      /// </summary>
      [XmlElement( "subscriptionId" )]
      public string SubscriptionID { get; set; }

      /// <summary>
      /// The error reported by the receiver of the request
      /// </summary>
      [XmlElement( "errorCode" )]
      public StatusCode ErrorCode { get; set; }
   }
}
