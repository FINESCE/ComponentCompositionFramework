using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using FIWARE.Data.Ngsi.Operations;

namespace FIWARE.Data.Ngsi.Http.Internal
{
   [NgsiControllerConfig]
   public class Ngsi10SubscriberController : ApiController
   {
      private IIncomingSubscriberOperations m_Operations;

      public Ngsi10SubscriberController( IIncomingSubscriberOperations operations )
      {
         m_Operations = operations;
      }

      [HttpGet]
      public void Ping()
      {
      }

      [HttpPost]
      [ActionName( "notify" )]
      public async Task<HttpResponseMessage> NotifyAsync( /*NotifyContextRequest request*/ )
      {
         var request = await Request.Content.ReadAsAsync<NotifyContextRequest>( Configuration.Formatters ).ConfigureAwait( false );
         return await m_Operations.NotifyAsync( Request, request ).ConfigureAwait( false );
      }
   }
}
