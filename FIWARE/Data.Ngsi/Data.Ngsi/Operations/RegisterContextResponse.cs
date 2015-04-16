using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FIWARE.Data.Ngsi.Model;

namespace FIWARE.Data.Ngsi.Operations
{
   [XmlRoot( "registerContextResponse" )]
   public class RegisterContextResponse
   {
      /// <summary>
      /// Confirmed availability period
      /// </summary>
      [XmlElement( "duration", Type = typeof( XmlTimeSpan ) )]
      public TimeSpan? Duration { get; set; }

      /// <summary>
      /// Registration identifier that could be used to update this
      /// registration
      /// </summary>
      [XmlElement( "registrationId" )]
      public string RegistrationID { get; set; }

      /// <summary>
      /// Error reported by the operation
      /// </summary>
      [XmlElement( "errorCode" )]
      public StatusCode ErrorCode { get; set; }
   }
}
