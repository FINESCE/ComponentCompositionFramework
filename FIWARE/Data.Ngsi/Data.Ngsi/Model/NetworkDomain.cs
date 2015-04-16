using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIWARE.Data.Ngsi.Model
{
   [XmlRoot( "NetworkDomain" )]
   public class NetworkDomain
   {
      [XmlElement( "NetworkDomain" )]
      public List<string> NetworkDomains { get; set; }
   }
}
