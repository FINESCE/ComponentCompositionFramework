using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIWARE.Data.Ngsi.Operations
{
   [XmlRoot( "unsubscribeContextRequest" )]
   public class UnsubscribeContextRequest
   {
      /// <summary>
      /// Identifier of the reference subscription to be deleted. 
      /// </summary>
      [XmlElement( "subscriptionId" )]
      public string SubscriptionID { get; set; }
   }
}
