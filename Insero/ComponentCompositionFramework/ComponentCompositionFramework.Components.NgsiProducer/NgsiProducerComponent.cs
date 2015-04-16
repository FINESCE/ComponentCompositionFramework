using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIWARE.Data.Ngsi.Http;
using Insero.ComponentCompositionFramework.Components.Utilities;
using Insero.ComponentCompositionFramework.Components;
using Insero.ComponentCompositionFramework.Components.NgsiProducer.Internal;

namespace Insero.ComponentCompositionFramework.Components.NgsiProducer
{
   /// <summary>
   /// Component that produces ngsi context events to a publish subscriber.
   /// </summary>
   public class NgsiProducerComponent : ExtendedDisposableBase, IConnectableComponent
   {
      private ContextManagementComponent _contextManagement;

      public NgsiProducerComponent()
      {
         _contextManagement = new ContextManagementComponent();
      }

      #region IBackendConnection Members

      public string Name
      {
         get 
         {
            return "NGSI10 Producer component";
         }
      }

      public string Description
      {
         get
         {
            return "This component produces NGSI10 context data for a publish subscriber.";
         }
      }

      public void Start()
      {
         _contextManagement.Start();
      }

      public IComponentCommunication GetCommunicationInterface()
      {
         return _contextManagement;
      }

      #endregion

      protected override void Dispose( bool disposing )
      {
         try
         {
            if ( disposing )
            {
               _contextManagement.Dispose();
            }
         }
         finally
         {
            base.Dispose( disposing );
         }
      }
   }
}
