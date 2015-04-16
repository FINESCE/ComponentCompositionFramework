using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insero.ComponentCompositionFramework.Composition.Events
{
   public class MediatorStateChangedEventArgs : EventArgs
   {
      public MediatorStateChangedEventArgs( MediatorState state )
      {
         State = state;
      }

      public MediatorState State { get; private set; }
   }
}
