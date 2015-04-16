using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Http.Dispatcher;

namespace FIWARE.Data.Ngsi.Http
{
   public class SingleAssemblyResolver : IAssembliesResolver
   {
      private List<Assembly> _Assemblies = new List<Assembly>();

      public SingleAssemblyResolver( Assembly singleAssembly )
      {
         _Assemblies.Add( singleAssembly );
      }

      #region IAssembliesResolver Members

      public ICollection<Assembly> GetAssemblies()
      {
         return _Assemblies;
      }

      #endregion
   }
}
