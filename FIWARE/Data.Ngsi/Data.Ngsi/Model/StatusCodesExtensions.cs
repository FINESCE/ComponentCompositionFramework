/*
 * Licensed to the Apache Software Foundation (ASF) under one
 * or more contributor license agreements.  See the NOTICE file
 * distributed with this work for additional information
 * regarding copyright ownership.  The ASF licenses this file
 * to you under the Apache License, Version 2.0 (the
 * "License"); you may not use this file except in compliance
 * with the License.  You may obtain a copy of the License at
 *
 *  http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing,
 * software distributed under the License is distributed on an
 * "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
 * KIND, either express or implied.  See the License for the
 * specific language governing permissions and limitations
 * under the License.
 */

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
