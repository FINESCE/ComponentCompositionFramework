using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace FIWARE.Data.Ngsi.Model.Serialization
{
   internal static class JsonAnyUtilities
   {
      internal static object GetValueAs<T>( JToken token )
      {
         if ( token == null )
         {
            return null;
         }

         if ( token is JValue )
         {
            return token;
         }
         else if ( token is JArray )
         {
            throw new InvalidOperationException();
         }
         else
         {
            using ( var reader = token.CreateReader() )
            {
               var serializer = new JsonSerializer();
               return serializer.Deserialize<T>( reader );
            }
         }
      }
   }
}
