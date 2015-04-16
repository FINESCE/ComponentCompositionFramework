using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FIWARE.Data.Ngsi.Operations;

namespace FIWARE.Data.Ngsi.Http
{
   public interface IIncomingSubscriberOperations
   {
      Task<HttpResponseMessage> NotifyAsync( HttpRequestMessage message, NotifyContextRequest request );
   }
}
