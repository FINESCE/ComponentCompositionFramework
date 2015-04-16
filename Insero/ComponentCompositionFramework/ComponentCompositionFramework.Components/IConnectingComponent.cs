using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insero.ComponentCompositionFramework.Components
{
   public interface IConnectingComponent<TComponentCommunication> : IComponent
      where TComponentCommunication : IComponentCommunication
   {
      /// <summary>
      /// Method that is called upon connection to a backend connection.
      /// </summary>
      /// <param name="connection">This is the communication interface provided
      /// by the backend connection</param>
      void ConnectComponent( TComponentCommunication connection );

      /// <summary>
      /// Method that is called upon disconnection from a backend connection.
      /// </summary>
      /// <param name="connection">This is the communication interface provided
      /// by the backend connection</param>
      void DisconnectComponent( TComponentCommunication connection );
   }
}
