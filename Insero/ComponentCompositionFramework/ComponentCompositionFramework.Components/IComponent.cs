using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Insero.ComponentCompositionFramework.Components.Utilities;

namespace Insero.ComponentCompositionFramework.Components
{
   /// <summary>
   /// This interface must be implemented by all class that are going to act 
   /// as a component in the API Mediator.
   /// </summary>
   [InheritedExport( typeof( IComponent ) )]
   public interface IComponent : IExtendedDisposable
   {
      /// <summary>
      /// Gets the name of the IComponent.
      /// </summary>
      string Name { get; }

      /// <summary>
      /// Gets the description of the IComponent.
      /// </summary>
      string Description { get; }

      /// <summary>
      /// Method that is called upon startup of the IComponent.
      /// </summary>
      /// <param name="administration">This is the administration service that
      /// is shared amongst the entire API Mediator</param>
      void Start();
   }
}
