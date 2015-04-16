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
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml.Serialization;
using FIWARE.Data.Ngsi.Http;
using FIWARE.Data.Ngsi.Operations;

namespace FIWARE.Data.Ngsi.Http.Internal
{
   [NgsiControllerConfig]
   public class Ngsi10ProviderController : ApiController
   {
      private IIncomingProviderOperations m_Operations;

      public Ngsi10ProviderController( IIncomingProviderOperations operations )
      {
         m_Operations = operations;
      }

      [HttpGet]
      public void Ping()
      {
      }

      [HttpPost]
      [ActionName( "queryContext" )]
      public async Task<HttpResponseMessage> QueryContextAsync( /*QueryContextRequest request*/ )
      {
         var request = await Request.Content.ReadAsAsync<QueryContextRequest>( Configuration.Formatters ).ConfigureAwait( false );
         return await m_Operations.QueryContextAsync( Request, request ).ConfigureAwait( false );
      }

      [HttpPost]
      [ActionName( "subscribeContext" )]
      public async Task<HttpResponseMessage> SubscribeContextAsync( /*SubscribeContextRequest request*/ )
      {
         var request = await Request.Content.ReadAsAsync<SubscribeContextRequest>( Configuration.Formatters ).ConfigureAwait( false );
         return await m_Operations.SubscribeContextAsync( Request, request ).ConfigureAwait( false );
      }

      [HttpPost]
      [ActionName( "updateContextSubscription" )]
      public async Task<HttpResponseMessage> UpdateContextSubscriptionAsync( /*UpdateContextSubscriptionRequest request*/ )
      {
         var request = await Request.Content.ReadAsAsync<UpdateContextSubscriptionRequest>( Configuration.Formatters ).ConfigureAwait( false );
         return await m_Operations.UpdateContextSubscriptionAsync( Request, request ).ConfigureAwait( false );
      }

      [HttpPost]
      [ActionName( "unsubscribeContext" )]
      public async Task<HttpResponseMessage> UnsubscribeContext( /*UnsubscribeContextRequest request*/ )
      {
         var request = await Request.Content.ReadAsAsync<UnsubscribeContextRequest>( Configuration.Formatters ).ConfigureAwait( false );
         return await m_Operations.UnsubscribeContextAsync( Request, request ).ConfigureAwait( false );
      }

      [HttpPost]
      [ActionName( "updateContext" )]
      public async Task<HttpResponseMessage> UpdateContext( /*UpdateContextRequest request*/ )
      {
         var request = await Request.Content.ReadAsAsync<UpdateContextRequest>( Configuration.Formatters ).ConfigureAwait( false );
         return await m_Operations.UpdateContextAsync( Request, request ).ConfigureAwait( false );
      }
   }
}
