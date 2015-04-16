/*
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FIWARE.Data.Ngsi.Model
{
   public sealed class EntityID : IEquatable<EntityID>
   {
      private Regex m_Regex;
      private string m_Id;

      /// <summary>
      /// Identifier of the Context Entity(ies).
      /// This value MAY be a string following the
      /// anyURI restrictions or a pattern represented as
      /// regular expressions following Appendix F of
      /// [XML-Schema-Part2].
      /// </summary>
      [XmlElement( ElementName = "id", IsNullable = false )]
      public string ID 
      {
         get
         {
            return m_Id;
         }
         set
         {
            m_Id = value;
            m_Regex = null;
         }
      }

      /// <summary>
      /// Indicates the type of Context Entity(ies) for
      /// which the Context Information is requested.
      /// If EntityId uniqueness is only guaranteed in
      /// combination with Type, then Type SHALL be
      /// present.
      /// </summary>
      [XmlAttribute( AttributeName = "type", DataType = "anyURI" )]
      public string Type { get; set; }

      /// <summary>
      /// Indicates whether the EntityId is a pattern
      /// (expressed as a regular expression following
      /// Appendix of [XML-Schema-Part2]) or an id.
      /// If this attribute is omitted, it SHALL be treated
      /// as false.
      /// http://www.w3.org/TR/2004/REC-xmlschema-2-20041028/datatypes.html#regexs
      /// </summary>
      [XmlAttribute( AttributeName = "isPattern" )]
      public bool IsPattern { get; set; }

      public EntityID Copy()
      {
         return new EntityID
         {
            ID = ID,
            IsPattern = IsPattern,
            Type = Type,
            m_Regex = m_Regex
         };
      }

      public override string ToString()
      {
         return "[ " + ID + ( IsPattern ? " (pattern), " : ", " ) + "Type = " + Type + " ]";
      }

      public bool Matches( EntityID other )
      {
         if ( other.IsPattern && IsPattern )
         {
            throw new InvalidOperationException( "Both EntityIDs cannot be a pattern" );
         }

         if ( !other.IsPattern && !IsPattern )
         {
            return Type == other.Type && ID == other.ID;
         }
         else if ( IsPattern )
         {
            if ( m_Regex == null )
            {
               string thisId = ID;
               if ( !thisId.StartsWith( "^" ) )
               {
                  thisId = "^" + thisId;
               }
               if ( !thisId.EndsWith( "$" ) )
               {
                  thisId = thisId + "$";
               }
               m_Regex = new Regex( thisId );
            }

            return Type == other.Type && m_Regex.IsMatch( other.ID );
         }
         else // if ( other.IsPattern )
         {
            if ( other.m_Regex == null )
            {
               string otherId = ID;
               if ( !otherId.StartsWith( "^" ) )
               {
                  otherId = "^" + otherId;
               }
               if ( !otherId.EndsWith( "$" ) )
               {
                  otherId = otherId + "$";
               }
               other.m_Regex = new Regex( otherId );
            }

            return Type == other.Type && other.m_Regex.IsMatch( ID );
         }
      }

      public bool Matches( string id, string type, bool isPattern )
      {
         if ( isPattern && IsPattern )
         {
            throw new InvalidOperationException( "Both EntityIDs cannot be a pattern" );
         }

         if ( !isPattern && !IsPattern )
         {
            return Type == type && ID == id;
         }
         else if ( IsPattern )
         {
            if ( m_Regex == null )
            {
               string thisId = ID;
               if ( !thisId.StartsWith( "^" ) )
               {
                  thisId = "^" + thisId;
               }
               if ( !thisId.EndsWith( "$" ) )
               {
                  thisId = thisId + "$";
               }
               m_Regex = new Regex( thisId );
            }

            return Type == type && m_Regex.IsMatch( id );
         }
         else // if ( isPattern )
         {
            string thisId = id;
            if ( !thisId.StartsWith( "^" ) )
            {
               thisId = "^" + thisId;
            }
            if ( !thisId.EndsWith( "$" ) )
            {
               thisId = thisId + "$";
            }
            var regex = new Regex( thisId );

            return Type == type && m_Regex.IsMatch( ID );
         }
      }

      public override int GetHashCode()
      {
         int hash = ID.GetHashCode();
         if ( Type != null )
         {
            hash *= Type.GetHashCode();
         }
         if ( IsPattern )
         {
            hash *= ( IsPattern.GetHashCode() + 23 );
         }

         return hash;
      }

      public override bool Equals( object obj )
      {
         if ( obj is EntityID )
         {
            return Equals( (EntityID)obj );
         }
         return false;
      }

      #region IEquatable<EntityID> Members

      public bool Equals( EntityID other )
      {
         return ID == other.ID && Type == other.Type && IsPattern == other.IsPattern;
      }

      #endregion
   }
}
