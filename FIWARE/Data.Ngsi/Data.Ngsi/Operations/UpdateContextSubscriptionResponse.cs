using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FIWARE.Data.Ngsi.Model;

namespace FIWARE.Data.Ngsi.Operations
{
   [XmlRoot( "updateContextSubscriptionResponse" )]
   public class UpdateContextSubscriptionResponse
   {
      public static UpdateContextSubscriptionResponse CreateInvalidParameter( string subscriptionId )
      {
         return new UpdateContextSubscriptionResponse
         {
            SubscribeError = new SubscribeError
            {
               ErrorCode = new StatusCode( StatusCodes.InvalidParameter ),
               SubscriptionID = subscriptionId
            }
         };
      }

      public static UpdateContextSubscriptionResponse CreateMissingParameter( string subscriptionId )
      {
         return new UpdateContextSubscriptionResponse
         {
            SubscribeError = new SubscribeError
            {
               ErrorCode = new StatusCode( StatusCodes.MissingParameter ),
               SubscriptionID = subscriptionId
            }
         };
      }

      [XmlElement( "subscribeResponse" )]
      public SubscribeResponse SubscribeResponse { get; set; }

      [XmlElement( "subscribeError" )]
      public SubscribeError SubscribeError { get; set; }
   }
}
