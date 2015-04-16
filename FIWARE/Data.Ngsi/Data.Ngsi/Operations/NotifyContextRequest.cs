using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FIWARE.Data.Ngsi.Model;

namespace FIWARE.Data.Ngsi.Operations
{
   [XmlRoot( "notifyContextRequest" )]
   public class NotifyContextRequest
   {
      /// <summary>
      /// The identifier of the subscription to which the notification 
      /// belongs to.
      /// </summary>
      [XmlElement( "subscriptionId" )]
      public string SubscriptionID { get; set; }

      /// <summary>
      /// The original requestor of the subscription which caused this 
      /// notification.
      /// </summary>
      [XmlElement( "originator", DataType = "anyURI" )]
      public string Originator { get; set; }

      /// <summary>
      /// List of Context Information, related attributes (or group of 
      /// attributes) and metadata. 
      /// </summary>
      [XmlArray( "contextResponseList" )]
      [XmlArrayItem( "contextElementResponse" )]
      public List<ContextElementResponse> ContextResponses { get; set; }
   }
}
