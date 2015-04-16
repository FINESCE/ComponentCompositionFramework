using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIWARE.Data.Ngsi.Model
{
   /// <summary>
   /// This structure SHALL be used in the scopeValue field of OperationScope structure if scopeType contains the
   /// keyword “SimpleScope”.
   /// 
   /// A SimpleScope is a XPath expression that is evaluated against the ContextRegistration data structure used to
   /// register Context Entities.
   /// </summary>
   [XmlRoot( "scopeExpression" )]
   public class SimpleScope
   {
      /// <summary>
      /// String containing an XPath restriction.
      /// Note: The XPath expression will be evaluated against the
      /// ContextRegistration structures.
      /// </summary>
      [XmlText]
      public string SimpleScopeExpression { get; set; }
   }
}
