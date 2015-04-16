using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIWARE.Data.Ngsi.Model
{
   [XmlRoot( "contextRegistrationResponse" )]
   public class ContextRegistrationResponse
   {
      /// <summary>
      /// The Context Registration that was requested.
      /// Note: In case of error, this data structure can contain only the
      /// EntityId or the EntityId/Attribute combination that cause the
      /// error.
      /// </summary>
      [XmlElement( "contextRegistration" )]
      public ContextRegistration ContextRegistration { get; set; }

      /// <summary>
      /// Identifies the status of the requested operation related to this
      /// specific ContextRegistration.
      /// This element SHALL be omitted in case there is no error.
      /// </summary>
      [XmlElement( "errorCode" )]
      public StatusCode ErrorCode { get; set; }
   }
}
