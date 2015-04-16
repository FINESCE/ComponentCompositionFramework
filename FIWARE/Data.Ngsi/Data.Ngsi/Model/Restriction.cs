using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIWARE.Data.Ngsi.Model
{
   /// <summary>
   /// The Restriction data structure contains two different kinds of restrictions:
   /// • the AttributeExpression filters the result set based on expressions on the values of the context attributes
   /// • the Scope restricts the operational search space on which a given operation needs to operate
   /// Compared to attributeExpression parameter, scopes a-priori limit the set of context sources that are needed for serving the
   /// request. The criteria used in operational scopes do not need to be part of the Context Information itself. Appendix D gives
   /// more explanation for scopes
   /// </summary>
   [XmlRoot( ElementName = "restriction" )]
   public class Restriction
   {
      /// <summary>
      /// String containing an XPath restriction.
      /// Note: The XPath expression will be evaluated against
      /// ContextEntity structures.
      /// </summary>
      [XmlElement( "attributeExpression" )]
      public string AttributeExpression { get; set; }

      /// <summary>
      /// List of scope definition
      /// </summary>
      [XmlElement( "scope" )]
      public OperationScope Scope { get; set; }
   }
}
