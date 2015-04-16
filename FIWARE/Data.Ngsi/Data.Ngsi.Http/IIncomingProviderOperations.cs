using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FIWARE.Data.Ngsi.Operations;

namespace FIWARE.Data.Ngsi.Http
{
   public interface IIncomingProviderOperations
   {
      Task<HttpResponseMessage> QueryContextAsync( HttpRequestMessage message, QueryContextRequest request );

      Task<HttpResponseMessage> SubscribeContextAsync( HttpRequestMessage message, SubscribeContextRequest request );

      Task<HttpResponseMessage> UpdateContextSubscriptionAsync( HttpRequestMessage message, UpdateContextSubscriptionRequest request );

      Task<HttpResponseMessage> UnsubscribeContextAsync( HttpRequestMessage message, UnsubscribeContextRequest request );

      Task<HttpResponseMessage> UpdateContextAsync( HttpRequestMessage message, UpdateContextRequest request );
   }
}
