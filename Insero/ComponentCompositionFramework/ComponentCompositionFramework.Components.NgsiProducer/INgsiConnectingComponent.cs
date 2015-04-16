using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using FIWARE.Data.Ngsi.Model;

namespace Insero.ComponentCompositionFramework.Components.NgsiProducer
{
   /// <summary>
   /// A representing of an frontend connection that can
   /// provide context information to the IContextManagementComponent.
   /// </summary>
   public interface INgsiConnectingComponent
   {
      /// <summary>
      /// Gets the context elements with the requested EntityIDs.
      /// </summary>
      /// <param name="entityIds">This is the EntityIDs that identifies the 
      /// ContextElements to retrieve</param>
      /// <returns>This is a list retrieved context elements</returns>
      Task<IReadOnlyList<ContextElement>> GetContextElementsAsync( IEnumerable<EntityID> entityIds );
   }
}
