using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIWARE.Data.Ngsi.Operations;

namespace FIWARE.Data.Ngsi.Http.Events
{
   public class ContextNotifyEventArgs : EventArgs
   {
      public ContextNotifyEventArgs( NotifyContextRequest request )
      {
         Request = request;
      }

      public NotifyContextRequest Request { get; private set; }
   }
}
