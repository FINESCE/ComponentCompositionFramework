using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insero.ComponentCompositionFramework.Components;

namespace Insero.ComponentCompositionFramework.Sample.Comp1
{
   public interface IComponent1ExposedInterface : IComponentCommunication
   {
      void SayHello();
   }
}
