using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIWARE.Data.Ngsi.Model
{
   /// <summary>
   /// This structure is used in the ContextRegistrationList parameter of registerContext operation and the ContextRegistration field
   /// of the ContextRegistrationResponse structure.
   /// 
   /// This structure can be used either to register/update the information about ProvidingApplication or to register/update the
   /// availability of ContextEntities and their related attributes.
   /// 
   /// If the ContextRegistration structure is used to update the registered information about the ContextEntities, EntityIdList
   /// SHALL be present together with ContextRegistrationAttribute parameter.
   /// 
   /// If the ContextRegistration structure is used to register the metadata about ProvidingApplication, RegistrationMetadata
   /// SHALL be present.
   /// 
   /// The registration/update of ContextEntities and ProvidingApplication can be done at the same time.
   /// </summary>
   [XmlRoot( "contextRegistration" )]
   public class ContextRegistration
   {
      /// <summary>
      /// List of identifiers for the Context Entities being registered
      /// </summary>
      [XmlArray( "entityIdList" )]
      [XmlArrayItem( "entityId" )]
      public List<EntityID> EntityIDs { get; set; }

      /// <summary>
      /// List of ContextAttributes and/or AttributeDomains which are
      /// made available through this registration.
      /// </summary>
      [XmlArray( "attributeList" )]
      [XmlArrayItem( "attribute" )]
      public List<ContextRegistrationAttribute> ContextRegistrationAttributes { get; set; }

      /// <summary>
      /// Metadata characterizing this registration
      /// </summary>
      [XmlArray( "registrationMetadata" )] // TODO: Changed from "metadata"
      [XmlArrayItem( "contextMetadata" )]
      public List<ContextMetadata> ContextMetadata { get; set; }

      /// <summary>
      /// URI identifying the application that provides the values of the
      /// context attributes for the target Context Entities.
      /// </summary>
      [XmlElement( "providingApplication", DataType = "anyURI" )]
      public string ProvidingApplication { get; set; }
   }
}
