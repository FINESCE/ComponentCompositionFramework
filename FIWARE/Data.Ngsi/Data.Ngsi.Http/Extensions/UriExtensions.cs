using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIWARE.Data.Ngsi.Http.Extensions
{
   public static class UriExtensions
   {
      public static Uri Append( this Uri that, string relativeUrl )
      {
         string baseUrl = that.ToString();
         baseUrl = baseUrl.TrimEnd( '/', '\\' );
         relativeUrl = relativeUrl.TrimStart( '/', '\\' );

         if ( relativeUrl.Length == 0 )
         {
            return that;
         }

         return new Uri( baseUrl + '/' + relativeUrl );
      }
   }
}
