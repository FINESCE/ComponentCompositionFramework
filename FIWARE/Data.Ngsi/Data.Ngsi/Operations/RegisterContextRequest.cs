using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FIWARE.Data.Ngsi.Model;

namespace FIWARE.Data.Ngsi.Operations
{
   [XmlRoot( "registerContextRequest" )]
   public class RegisterContextRequest
   {
      /// <summary>
      /// List of ContextRegistration structures.
      /// </summary>
      [XmlArray( "contextRegistrationList" )]
      [XmlArrayItem( "contextRegistration" )]
      public List<ContextRegistration> ContextRegistrations { get; set; }

      /// <summary>
      /// Desired availability period.
      /// </summary>
      [XmlElement( "duration", Type = typeof( XmlTimeSpan ) )]
      public TimeSpan? Duration { get; set; }

      /// <summary>
      /// Registration identifier used to update previous registrations.
      /// </summary>
      [XmlElement( "registrationId" )]
      public string RegistrationID { get; set; }
   }
}
