using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIWARE.Data.Ngsi.Model
{
   [XmlRoot( "subscribeResponse" )]
   public class SubscribeResponse
   {
      /// <summary>
      /// The identifier of the subscription
      /// </summary>
      [XmlElement( "subscriptionId" )]
      public string SubscriptionID { get; set; }

      /// <summary>
      /// Negotiated duration of the subscription.
      /// The Context Management component MAY omit this
      /// parameter if it allows indefinite subscriptions
      /// </summary>
      [XmlElement( "duration", Type = typeof( XmlTimeSpan ) )]
      public TimeSpan? Duration { get; set; }

      /// <summary>
      /// Negotiated minimum interval between notifications. If a
      /// Throttling value were proposed into the
      /// subscribeContextRequest, this parameter SHALL be
      /// specified.
      /// </summary>
      [XmlElement( "throttling", Type = typeof( XmlTimeSpan ) )]
      public TimeSpan? Throttling { get; set; }
   }
}
