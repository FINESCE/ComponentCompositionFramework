using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FIWARE.Data.Ngsi.Model;

namespace FIWARE.Data.Ngsi.Operations
{
   [XmlRoot( "updateContextSubscriptionRequest" )]
   public class UpdateContextSubscriptionRequest
   {
      /// <summary>
      /// Requested duration of the subscription. Negative values will 
      /// result in an error. 
      /// The receiver of the request MUST reset the new subscription 
      /// duration starting at the time of reception of the request. 
      /// If the Context Management component has a policy to 
      /// always require duration, the operation SHALL return an error 
      /// in case the parameter is not present. 
      /// When the parameter is omitted it means that the previous 
      /// negotiated Duration value is applied, if any. 
      /// </summary>
      [XmlElement( "duration", Type = typeof( XmlTimeSpan ) )]
      public TimeSpan? Duration { get; set; }

      /// <summary>
      /// Restriction on the attributes and meta-data of the Context 
      /// Information. 
      /// When the parameter is omitted it means that the previous 
      /// Restriction is applied.
      /// </summary>
      [XmlElement( "restriction" )]
      public Restriction Restriction { get; set; }

      /// <summary>
      /// Identifier of the reference subscription to be updated 
      /// </summary>
      [XmlElement( "subscriptionId" )]
      public string SubscriptionID { get; set; }

      /// <summary>
      /// Conditions when to send notifications 
      /// When the parameter is omitted it means that the previous 
      /// NotifyCondition is applied. 
      /// </summary>
      [XmlArray( "notifyConditions" )]
      [XmlArrayItem( "notifyCondition" )]
      public List<NotifyCondition> NotifyConditions { get; set; }

      /// <summary>
      /// Proposed minimum interval between notifications. When the 
      /// parameter is omitted it means that the previous Throttling 
      /// value is applied. 
      /// </summary>
      [XmlElement( "throttling", Type = typeof( XmlTimeSpan ) )]
      public TimeSpan? Throttling { get; set; }
   }
}
