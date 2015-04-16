using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FIWARE.Data.Ngsi.Model;

namespace FIWARE.Data.Ngsi.Operations
{
   [XmlRoot( "updateContextRequest" )]
   public class UpdateContextRequest
   {
      /// <summary>
      /// List of Context Elements containing only the subset of 
      /// Context Information (related attributes (or context domain) 
      /// and metadata) to be modified.
      /// </summary>
      [XmlArray( "contextElementList" )]
      [XmlArrayItem( "contextElement" )]
      public List<ContextElement> ContextElements { get; set; }

      /// <summary>
      /// Indicates the type of action that is performed within the 
      /// update operation: 
      /// • update: it replaces the value and metadata of the 
      /// existing attributes with the same name;
      /// • append: it adds the new attribute. Note: this may 
      /// result in multiple attributes with the same name;
      /// • delete: it removes the existing value.
      /// </summary>
      [XmlElement( "updateAction" )]
      public UpdateActionType UpdateAction { get; set; }
   }
}
