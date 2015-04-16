using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insero.ComponentCompositionFramework.Components;
using Insero.ComponentCompositionFramework.Composition.Static;
using Insero.ComponentCompositionFramework.Sample.Comp1;
using Insero.ComponentCompositionFramework.Sample.Comp2;

namespace ComponentCompositionFramework.Sample.Static
{
   public class Program
   {
      public static void Main( string[] args )
      {
         var composer = new StaticComponentComposer( CreateComponents );

         composer.Start();

         Console.WriteLine( "Press any key to exit..." );

         Console.ReadKey();
      }

      public static IEnumerable<IComponent> CreateComponents()
      {
         return new IComponent[] { new Component1(), new Component2() };
      }
   }
}
