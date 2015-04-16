using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insero.ComponentCompositionFramework.Composition.Events;

namespace Insero.ComponentCompositionFramework.Composition
{
   public interface IComponentComposer
   {
      event EventHandler<ComponentsChangedEventArgs> ComponentsChanged;

      event EventHandler<MediatorStateChangedEventArgs> StateChanged;

      IEnumerable<ComponentModel> Components { get; }

      bool IsStarted { get; }

      void Start();

      void Stop();
   }
}
