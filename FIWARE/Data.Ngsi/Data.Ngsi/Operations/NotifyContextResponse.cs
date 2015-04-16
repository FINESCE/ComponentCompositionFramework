using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FIWARE.Data.Ngsi.Model;

namespace FIWARE.Data.Ngsi.Operations
{
   [XmlRoot( "notifyContextResponse" )]
   public class NotifyContextResponse
   {
      /// <summary>
      /// The response message reported by the receiver of the 
      /// request 
      /// </summary>
      [XmlElement( "responseCode" )]
      public StatusCode ResponseCode { get; set; }
   }
}
