using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insero.ComponentCompositionFramework.Components
{
   /// <summary>
   /// Interface for a component that other components can connect to.
   /// </summary>
   public interface IConnectableComponent : IComponent
   {
      /// <summary>
      /// Gets the implementation of a component communication interface,
      /// that the connecting IComponent will be able to use.
      /// </summary>
      /// <returns>An implementation of IComponentCommunication.</returns>
      IComponentCommunication GetCommunicationInterface();
   }
}
