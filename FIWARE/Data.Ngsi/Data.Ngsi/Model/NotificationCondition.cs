using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIWARE.Data.Ngsi.Model
{
   public enum NotificationCondition
   {
      /// <summary>
      /// The condition is true when the time interval specified in the value field
      /// is reached.
      /// </summary>
      [XmlEnum( "ONTIMEINTERVAL" )]
      OnTimeInterval,

      /// <summary>
      /// The condition is true if the value of one or more context attributes fits
      /// a reference value and/or range, specified in the Restriction parameter
      /// </summary>
      [XmlEnum( "ONVALUE" )]
      OnValue,

      /// <summary>
      /// The condition is true when a change in one of the specified context
      /// attributes has occurred
      /// </summary>
      [XmlEnum( "ONCHANGE" )]
      OnChange
   }
}
