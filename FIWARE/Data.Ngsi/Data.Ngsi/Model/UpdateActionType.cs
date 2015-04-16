using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIWARE.Data.Ngsi.Model
{
   /// <summary>
   /// This enumeration shows the UpdateActionType that can be requested.
   /// </summary>
   public enum UpdateActionType
   {
      /// <summary>
      /// Replaces the value and metadata of the existing attributes
      /// with the same name
      /// </summary>
      [XmlEnum( "UPDATE" )]
      Update,

      /// <summary>
      /// Adds a new attribute.
      /// Note: this may result in multiple attributes with the same name
      /// </summary>
      [XmlEnum( "APPEND" )]
      Append,

      /// <summary>
      /// Removes the existing value
      /// </summary>
      [XmlEnum( "DELETE" )]
      Delete
   }
}
