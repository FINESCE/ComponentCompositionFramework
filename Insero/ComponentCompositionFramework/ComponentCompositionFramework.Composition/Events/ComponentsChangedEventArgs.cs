using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insero.ComponentCompositionFramework.Composition.Events
{
   public class ComponentsChangedEventArgs : EventArgs
   {
      public ComponentsChangedEventArgs( ComponentModel componentModel, ChangeAction action )
      {
         ComponentModel = componentModel;
         Action = action;
      }

      public ComponentModel ComponentModel { get; private set; }

      public ChangeAction Action { get; private set; }
   }
}
