using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIWARE.Data.Ngsi.Model
{
   [XmlRoot( "notifyCondition" )]
   public class NotifyCondition
   {
      /// <summary>
      /// This element specifies the notifyCondition, and SHALL
      /// assume one of the following values:
      /// • ONTIMEINTERVAL
      /// • ONCHANGE
      /// • ONVALUE
      /// </summary>
      [XmlElement( "type" )]
      public NotificationCondition Type { get; set; }

      /// <summary>
      /// When present, this element qualifies the NotifyCondition
      /// based on the type as follows:
      /// 
      /// • Type ONTIMEINTERVAL: exactly one condValue
      /// SHALL be present and SHALL represent the time
      /// interval between notifications.
      /// 
      /// • Type ONCHANGE: this element SHALL be present
      /// and contain the name(s) of the Context Attributes to
      /// be monitored for changes.
      /// 
      /// • Type ONVALUE: this element SHALL not be
      /// present for this type.
      /// </summary>
      [XmlArray( "condValueList" )]
      [XmlArrayItem( "condValue" )]
      public List<string> ConditionValue { get; set; }

      /// <summary>
      /// This element SHALL be present only if the NotifyCondition
      /// type is set to ONVALUE. When present, this element
      /// indicates the restriction that applies before a notification is
      /// sent.
      /// The parameter is specified as XPath expression.
      /// </summary>
      [XmlElement( "restriction" )]
      public string Restriction { get; set; }
   }
}
