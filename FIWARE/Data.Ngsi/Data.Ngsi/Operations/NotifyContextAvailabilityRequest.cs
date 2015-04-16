using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FIWARE.Data.Ngsi.Model;

namespace FIWARE.Data.Ngsi.Operations
{
   [XmlRoot( "notifyContextAvailabilityRequest" )]
   public class NotifyContextAvailabilityRequest
   {
      /// <summary>
      /// The identifier of the subscription to which the notification 
      /// belongs to 
      /// </summary>
      [XmlElement( "subscriptionId" )]
      public string SubscriptionID { get; set; }

      /// <summary>
      /// List of Context Registration responses 
      /// </summary>
      [XmlArray( "contextRegistrationResponseList" )]
      [XmlArrayItem( "contextRegistrationResponse" )]
      public List<ContextRegistrationResponse> ContextRegistrationResponseList { get; set; }

      /// <summary>
      /// Error codes for general operation errors 
      /// </summary>
      [XmlElement( "errorCode" )]
      public StatusCode ErrorCode { get; set; }
   }
}
