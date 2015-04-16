using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIWARE.Data.Ngsi.Model
{
   public static class StatusCodesExtensions
   {
      public static string AsReasonPhrase( this StatusCodes code )
      {
         switch ( code )
         {
            case StatusCodes.Ok:
               return "Ok";
            case StatusCodes.BadRequest:
               return "Bad request";
            case StatusCodes.Forbidden:
               return "Forbidden";
            case StatusCodes.ContextElementNotFound:
               return "ContextElement not found";
            case StatusCodes.SubscriptionIDNotFound:
               return "Subscription ID not found";
            case StatusCodes.MissingParameter:
               return "Missing parameter";
            case StatusCodes.InvalidParameter:
               return "Invalid parameter";
            case StatusCodes.ErrorInMetadata:
               return "Error in metadata";
            case StatusCodes.RegularExpressionForEntityIDNotAllowed:
               return "Regular Expression for EntityId not allowed";
            case StatusCodes.EntityTypeRequired:
               return "Entity Type required";
            case StatusCodes.AttributeListRequired:
               return "AttributeList required";
            case StatusCodes.ReceiverInternalError:
               return "Receiver internal error";
            default:
               throw new ArgumentException( "code" );
         }
      }
   }
}
