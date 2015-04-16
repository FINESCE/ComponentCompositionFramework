using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using FIWARE.Data.Ngsi.Model;

namespace FIWARE.Data.Ngsi.Operations
{
   [XmlRoot( "subscribeContextResponse" )]
   public class SubscribeContextResponse
   {
      public static readonly SubscribeContextResponse InvalidParameter = new SubscribeContextResponse
      {
         SubscribeError = new SubscribeError
         {
            ErrorCode = new StatusCode( StatusCodes.InvalidParameter )
         }
      };

      public static readonly SubscribeContextResponse MissingParameter = new SubscribeContextResponse
      {
         SubscribeError = new SubscribeError
         {
            ErrorCode = new StatusCode( StatusCodes.MissingParameter )
         }
      };

      /// <summary>
      /// Response to the subscribeContextRequest 
      /// </summary>
      [XmlElement( "subscribeResponse" )]
      public SubscribeResponse SubscribeResponse { get; set; }

      /// <summary>
      /// The error reported by the receiver of the request
      /// </summary>
      [XmlElement( "subscribeError" )]
      public SubscribeError SubscribeError { get; set; }
   }
}
