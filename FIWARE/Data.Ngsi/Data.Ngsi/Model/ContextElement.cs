using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace FIWARE.Data.Ngsi.Model
{
   [XmlRoot( ElementName = "contextElement" )]
   public sealed class ContextElement : IEquatable<ContextElement>
   {
      public ContextElement()
      {
      }

      /// <summary>
      /// Identifies the Context Entity for which the Context Information is provided.
      /// </summary>
      [XmlElement( ElementName = "entityId" )]
      public EntityID EntityID { get; set; }

      /// <summary>
      /// Name of the attribute domain that logically groups together set of Context Information 
      /// attributes. Examples of attribute domain are: device info (battery level, screen 
      /// size, …), location info (position, civil address, …).
      /// </summary>
      [XmlElement( ElementName = "attributeDomainName" )]
      public string AttributeDomainName { get; set; }

      /// <summary>
      /// List of Context Information attributes.
      /// Note: In case of the attributeDomainName is specified all
      /// contextAttribute have to belong to the same
      /// attributeDomainName.
      /// </summary>
      [XmlArray( ElementName = "contextAttributeList" )]
      [XmlArrayItem( ElementName = "contextAttribute" )]
      public List<ContextAttribute> ContextAttributes { get; set; }

      /// <summary>
      /// Metadata common to all attributes of the logical domain
      /// (related to the AttributeDomain)
      /// </summary>
      [XmlArray( ElementName = "domainMetadata" )] // CHANGED FROM metadata
      [XmlArrayItem( ElementName = "contextMetadata" )]
      public List<ContextMetadata> DomainMetadata { get; set; }

      [XmlIgnore]
      public bool HasContextAttributes
      {
         get
         {
            return ContextAttributes != null || ContextAttributes.Count > 0;
         }
      }

      public bool Matches( IEnumerable<EntityID> entityIds )
      {
         foreach ( var entityId in entityIds )
         {
            if ( entityId.Matches( EntityID ) )
            {
               return true;
            }
         }
         return false;
      }

      public bool ContainsOneOrMoreAttributesWithName( IEnumerable<string> names )
      {
         if ( !HasContextAttributes )
         {
            return false;
         }

         foreach ( var name in names )
         {
            foreach ( var attr in ContextAttributes )
            {
               if ( attr.Name == name )
               {
                  return true;
               }
            }
         }

         return false;
      }

      public ContextElement Copy()
      {
         var elem = new ContextElement
         {
            AttributeDomainName = AttributeDomainName,
            EntityID = EntityID.Copy()
         };

         //if ( DomainMetadata != null )
         //{
         //   elem.DomainMetadata = new List<ContextMetadata>();
         //   for ( int i = 0 ; i < DomainMetadata.Count ; i++ )
         //   {
         //      elem.DomainMetadata.Add( DomainMetadata[ i ].Copy() );
         //   }
         //}

         if ( ContextAttributes != null )
         {
            elem.ContextAttributes = new List<ContextAttribute>();
            for ( int i = 0 ; i < ContextAttributes.Count ; i++ )
            {
               elem.ContextAttributes.Add( ContextAttributes[ i ].Copy() );
            }
         }

         return elem;
      }

      public ContextElement CopyWith( IEnumerable<string> attributes )
      {
         var elem = new ContextElement
         {
            AttributeDomainName = AttributeDomainName,
            EntityID = EntityID.Copy()
         };

         //if ( DomainMetadata != null )
         //{
         //   elem.DomainMetadata = new List<ContextMetadata>();
         //   for ( int i = 0 ; i < DomainMetadata.Count ; i++ )
         //   {
         //      elem.DomainMetadata.Add( DomainMetadata[ i ].Copy() );
         //   }
         //}

         if ( ContextAttributes != null )
         {
            elem.ContextAttributes = new List<ContextAttribute>();
            for ( int i = 0 ; i < ContextAttributes.Count ; i++ )
            {
               var attr = ContextAttributes[ i ];

               if( attributes.Contains( attr.Name ) )
               {
                  elem.ContextAttributes.Add( attr.Copy() );
               }
            }
         }

         return elem;
      }

      public override bool Equals( object obj )
      {
         if ( obj is ContextElement )
         {
            return Equals( (ContextElement)obj );
         }
         return false;
      }

      public override int GetHashCode()
      {
         return EntityID.GetHashCode();
      }

      #region IEquatable<ContextElement> Members

      public bool Equals( ContextElement other )
      {
         return EntityID.Equals( other.EntityID );
      }

      #endregion
   }
}
