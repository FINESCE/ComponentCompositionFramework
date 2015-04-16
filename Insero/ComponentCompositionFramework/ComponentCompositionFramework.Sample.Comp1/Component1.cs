using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insero.ComponentCompositionFramework.Components;
using Insero.ComponentCompositionFramework.Components.Utilities;
using NLog;

namespace Insero.ComponentCompositionFramework.Sample.Comp1
{
   public class Component1 : ExtendedDisposableBase, IComponent, IConnectableComponent, IComponent1ExposedInterface
   {
      private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

      #region IComponent Members

      public string Name
      {
         get { return "Component 1"; }
      }

      public string Description
      {
         get { return "This is component 1"; }
      }

      public void Start()
      {
         _logger.Debug( "Logged from Start() of Component1" );
      }

      #endregion

      #region IConnectableComponent Members

      public IComponentCommunication GetCommunicationInterface()
      {
         return this;
      }

      #endregion

      #region IComponent1ExposedInterface Members

      public void SayHello()
      {
         _logger.Debug( "Component 1 says 'Hello'!" );
      }

      #endregion
   }
}
