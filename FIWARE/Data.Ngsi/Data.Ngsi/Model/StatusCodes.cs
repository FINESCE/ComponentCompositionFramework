using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIWARE.Data.Ngsi.Model
{
   public enum StatusCodes
   {
      Ok = 200,
      BadRequest = 400,
      Forbidden = 403,
      ContextElementNotFound = 404,
      SubscriptionIDNotFound = 470,
      MissingParameter = 471,
      InvalidParameter = 472,
      ErrorInMetadata = 473,
      RegularExpressionForEntityIDNotAllowed = 480,
      EntityTypeRequired = 481,
      AttributeListRequired = 482,
      ReceiverInternalError = 500
   }
}
