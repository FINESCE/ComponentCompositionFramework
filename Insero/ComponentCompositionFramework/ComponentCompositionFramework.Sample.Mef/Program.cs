using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insero.ComponentCompositionFramework.Composition.Dynamic;

namespace Insero.ComponentCompositionFramework.Sample.Mef
{
   class Program
   {
      static void Main( string[] args )
      {
         // HOW TO RUN:
         // 1. Create the directory named "C:\temp\components"
         // 2. Start this application
         // 3. Place the .dll files for Component 1 and Component 2 in this directory in the specified order (because Component 2 has a dependency on Component 1)

         var composer = new MefComponentComposer( "C:\\temp\\components" );

         composer.Start();

         Console.ReadKey();
      }
   }
}
