using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace FIWARE.Data.Ngsi.Model
{
   public class XmlTimeSpan
   {
      private TimeSpan m_Value = TimeSpan.Zero;

      public XmlTimeSpan() 
      {
      }

      public XmlTimeSpan( TimeSpan value ) 
      {
         m_Value = value; 
      }

      public static implicit operator TimeSpan?( XmlTimeSpan o )
      {
         return o == null ? default( TimeSpan? ) : o.m_Value;
      }

      public static implicit operator XmlTimeSpan( TimeSpan? o )
      {
         return o == null ? null : new XmlTimeSpan( o.Value );
      }

      public static implicit operator TimeSpan( XmlTimeSpan o )
      {
         return o == null ? default( TimeSpan ) : o.m_Value;
      }

      public static implicit operator XmlTimeSpan( TimeSpan o )
      {
         return o == default( TimeSpan ) ? null : new XmlTimeSpan( o );
      }

      [XmlText]
      public string Default
      {
         get 
         {
            return XmlConvert.ToString( m_Value );
         }
         set
         {
            m_Value = XmlConvert.ToTimeSpan( value );
         }
      }
   }
}
