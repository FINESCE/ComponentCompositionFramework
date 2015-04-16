using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using Newtonsoft.Json.Serialization;

namespace FIWARE.Data.Ngsi.Http.Internal
{
   public class NgsiControllerConfig : Attribute, IControllerConfiguration
   {
      #region IControllerConfiguration Members

      public void Initialize( HttpControllerSettings controllerSettings, HttpControllerDescriptor controllerDescriptor )
      {
         controllerSettings.Formatters.Clear();

         // XML Configuration
         controllerSettings.Formatters.Add( new XmlMediaTypeFormatter() 
         {
            UseXmlSerializer = true 
         } );

         // JSON Configuration
         var json = new JsonMediaTypeFormatter()
         {
            UseDataContractJsonSerializer = false
         };
         json.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
         controllerSettings.Formatters.Add( json );
      }

      #endregion
   }
}
