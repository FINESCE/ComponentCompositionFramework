using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insero.ComponentCompositionFramework.Composition.Events;

namespace Insero.ComponentCompositionFramework.Composition
{
   public abstract class ComponentComposerBase : IComponentComposer
   {
      public event EventHandler<ComponentsChangedEventArgs> ComponentsChanged;
      public event EventHandler<MediatorStateChangedEventArgs> StateChanged;

      protected internal void TryConnect( ComponentModel component1, ComponentModel component2 )
      {
         component1.TryConnect( component2 );
      }

      protected internal void TryDisconnect( ComponentModel component1, ComponentModel component2 )
      {
         component1.TryDisconnect( component2 );
      }

      protected void RaiseComponentsChanged( ComponentModel componentModel, ChangeAction action )
      {
         var handler = ComponentsChanged;
         if ( handler != null )
         {
            handler( this, new ComponentsChangedEventArgs( componentModel, action ) );
         }
      }

      protected void RaiseStateChanged( MediatorState state )
      {
         var handler = StateChanged;
         if ( handler != null )
         {
            handler( this, new MediatorStateChangedEventArgs( state ) );
         }
      }

      public abstract IEnumerable<ComponentModel> Components { get; }

      public abstract bool IsStarted { get; }

      public abstract void Start();

      public abstract void Stop();
   }
}
