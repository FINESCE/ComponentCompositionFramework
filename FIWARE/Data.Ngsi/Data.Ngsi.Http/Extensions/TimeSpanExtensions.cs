using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIWARE.Data.Ngsi.Http.Extensions
{
   public static class TimeSpanExtensions
   {
      public static bool IsPositiveAndNonZero( this TimeSpan that )
      {
         return that.Ticks > 0;
      }
   }
}
