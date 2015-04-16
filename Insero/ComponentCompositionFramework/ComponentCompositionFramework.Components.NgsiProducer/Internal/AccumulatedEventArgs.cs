using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insero.ComponentCompositionFramework.Components.NgsiProducer.Internal
{
   internal class AccumulatedEventArgs<T> : EventArgs
   {
      internal AccumulatedEventArgs( ICollection<T> accumulation )
      {
         Accumulation = accumulation;
      }

      internal ICollection<T> Accumulation { get; private set; }
   }
}
