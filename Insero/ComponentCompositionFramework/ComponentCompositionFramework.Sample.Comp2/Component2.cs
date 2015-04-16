using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insero.ComponentCompositionFramework.Components;
using Insero.ComponentCompositionFramework.Components.Utilities;
using Insero.ComponentCompositionFramework.Sample.Comp1;
using NLog;

namespace Insero.ComponentCompositionFramework.Sample.Comp2
{
   public class Component2 : ExtendedDisposableBase, IComponent, IConnectingComponent<IComponent1ExposedInterface>
   {
      private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

      #region IConnectingComponent<IComponent1ExposedInterface> Members

      public void ConnectComponent( IComponent1ExposedInterface connection )
      {
         _logger.Debug( "Component2 connected to Component1" );

         connection.SayHello();
      }

      public void DisconnectComponent( IComponent1ExposedInterface connection )
      {
         _logger.Debug( "Component2 disconnected from Component1" );
      }

      #endregion

      #region IComponent Members

      public string Name
      {
         get { return "Component 2"; }
      }

      public string Description
      {
         get { return "This is component 2"; }
      }

      public void Start()
      {
         _logger.Debug( "Logged from Start() of Component2" );
      }

      #endregion
   }
}
