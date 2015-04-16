using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using FIWARE.Data.Ngsi.Http.Internal;
using Microsoft.Practices.Unity;
using FIWARE.Data.Ngsi.Http.Extensions;
using Microsoft.Owin.Hosting;
using System.Web.Http.Dispatcher;
using System.Reflection;
using Owin;
using Unity.WebApi;

namespace FIWARE.Data.Ngsi.Http
{
   public class Ngsi10ProviderService : IDisposable
   {
      private IDisposable _service;
      private IIncomingProviderOperations _serverOperation;
      private Uri _baseAddress;
      private bool _bisposed;

      public Ngsi10ProviderService( Uri baseAddress, IIncomingProviderOperations serverOperations )
      {
         _baseAddress = baseAddress.Append( "/NGSI10" );
         _serverOperation = serverOperations;
      }

      public Uri BaseAddress
      {
         get
         {
            return _baseAddress;
         }
      }

      public void Start()
      {
         Stop();

         // Setup web service
         _service = WebApp.Start(
            url: _baseAddress.ToString(),
            startup: builder =>
            {
               var config = new HttpConfiguration();
               config.Routes.MapHttpRoute(
                  "NGSI Server API",
                  "{action}",
                  new { controller = "ngsi10provider" } );
               config.Formatters.XmlFormatter.UseXmlSerializer = true;

               var container = new UnityContainer();
               container.RegisterType<Ngsi10ProviderController>( new InjectionConstructor( _serverOperation ) );
               config.DependencyResolver = new UnityDependencyResolver( container );
               config.Services.Replace(
                  typeof( IAssembliesResolver ),
                  new SingleAssemblyResolver( Assembly.GetExecutingAssembly() ) );

               builder.UseWebApi( config );
            } );
      }

      public void Stop()
      {
         if ( _service != null )
         {
            _service.Dispose();
            _service = null;
         }
      }

      #region IDisposable Members

      ~Ngsi10ProviderService()
      {
         Dispose( false );
      }

      public void Dispose()
      {
         if ( !_bisposed )
         {
            Dispose( true );
            GC.SuppressFinalize( this );
            _bisposed = true;
         }
      }

      private void Dispose( bool disposing )
      {
         if ( disposing )
         {
            Stop();
         }
      }

      #endregion
   }
}
