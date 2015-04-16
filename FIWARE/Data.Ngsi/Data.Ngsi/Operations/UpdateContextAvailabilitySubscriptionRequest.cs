using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FIWARE.Data.Ngsi.Model;

namespace FIWARE.Data.Ngsi.Operations
{
   [XmlRoot( "updateContextAvailabilitySubscriptionRequest" )]
   public class UpdateContextAvailabilitySubscriptionRequest
   {
      /// <summary>
      /// List of identifiers or name patterns of the Context Entity(ies) 
      /// to discover
      /// </summary>
      [XmlArray( "entityIdList" )]
      [XmlArrayItem( "entityId" )]
      public List<EntityID> EntityIDS { get; set; }

      /// <summary>
      /// List of attributes or group of attributes to be discovered
      /// </summary>
      [XmlArray( "attributeList" )]
      [XmlArrayItem( "attribute" )]
      public List<string> Attributes { get; set; }

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
      /// Information
      /// </summary>
      [XmlElement( "restriction" )]
      public Restriction Restriction { get; set; }

      /// <summary>
      /// Identifier used in the discoverContextAvailabilityRequest
      /// messages 
      /// </summary>
      [XmlElement( "subscriptionId" )]
      public string SubscriptionID { get; set; }
   }
}
