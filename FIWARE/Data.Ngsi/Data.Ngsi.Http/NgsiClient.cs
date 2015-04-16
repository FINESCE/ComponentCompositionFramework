using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FIWARE.Data.Ngsi.Http.Events;
using FIWARE.Data.Ngsi.Http.Internal;
using FIWARE.Data.Ngsi.Model;
using FIWARE.Data.Ngsi.Operations;
using Newtonsoft.Json.Serialization;
using NLog;

namespace FIWARE.Data.Ngsi.Http
{
    public sealed class NgsiClient : IDisposable
    {
       private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

       private static readonly List<MediaTypeFormatter> Formatters;
       private static readonly XmlMediaTypeFormatter XmlFormatter;
       private static readonly JsonMediaTypeFormatter JsonFormatter;

       private static readonly MediaTypeWithQualityHeaderValue XmlContentType = new MediaTypeWithQualityHeaderValue( "application/xml" );
       private static readonly MediaTypeWithQualityHeaderValue JsonContentType = new MediaTypeWithQualityHeaderValue( "application/json" );

       static NgsiClient()
       {
          Formatters = new List<MediaTypeFormatter>();
          
          // XML Config
          XmlFormatter = new XmlMediaTypeFormatter();
          XmlFormatter.UseXmlSerializer = true;
          Formatters.Add( XmlFormatter );

          // JSON Config
          JsonFormatter = new JsonMediaTypeFormatter();
          JsonFormatter.UseDataContractJsonSerializer = false;
          JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
          Formatters.Add( JsonFormatter );
       }

       private HttpClient _client;
       private HttpClientHandler _handler;
       private MediaTypeFormatter _currentFormatter;
       private bool _disposed = false;

       public NgsiClient()
       {
          _handler = new HttpClientHandler();
          _handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
          _client = new HttpClient( _handler, true );
          _client.DefaultRequestHeaders.Accept.Add( XmlContentType );
          _client.DefaultRequestHeaders.ConnectionClose = true;
          _currentFormatter = XmlFormatter;
       }

       public HttpClient UnderlyingClient
       {
          get
          {
             return _client;
          }
       }

       public MediaFormat AcceptFormat 
       {
          get
          {
             if ( _client.DefaultRequestHeaders.Accept.Contains( XmlContentType ) )
             {
                return MediaFormat.Xml;
             }
             else
             {
                return MediaFormat.Json;
             }
          }
          set
          {
             _client.DefaultRequestHeaders.Accept.Clear();

             if ( value == MediaFormat.Xml )
             {
                _client.DefaultRequestHeaders.Accept.Add( XmlContentType );
             }
             else
             {
                _client.DefaultRequestHeaders.Accept.Add( JsonContentType );
             }
          }
       }

       public MediaFormat ContentTypeFormat 
       {
          get
          {
             if ( _currentFormatter == XmlFormatter )
             {
                return MediaFormat.Xml;
             }
             else
             {
                return MediaFormat.Json;
             }
          }
          set
          {
             if ( value == MediaFormat.Xml )
             {
                _currentFormatter = XmlFormatter;
             }
             else
             {
                _currentFormatter = JsonFormatter;
             }
          }
       }

       public async Task<DiscoverContextAvailabilityResponse> SendAsync( string requestUri, DiscoverContextAvailabilityRequest request, CancellationToken token = default( CancellationToken ) )
       {
          return await PerformTransactionAsync<DiscoverContextAvailabilityResponse, DiscoverContextAvailabilityRequest>( requestUri, request, token ).ConfigureAwait( false );
       }

       public async Task<QueryContextResponse> SendAsync( string requestUri, QueryContextRequest request, CancellationToken token = default( CancellationToken ) )
       {
          return await PerformTransactionAsync<QueryContextResponse, QueryContextRequest>( requestUri, request, token ).ConfigureAwait( false );
       }

       public async Task<RegisterContextResponse> SendAsync( string requestUri, RegisterContextRequest request, CancellationToken token = default( CancellationToken ) )
       {
          return await PerformTransactionAsync<RegisterContextResponse, RegisterContextRequest>( requestUri, request, token ).ConfigureAwait( false );
       }

       public async Task<SubscribeContextAvailabilityResponse> SendAsync( string requestUri, SubscribeContextAvailabilityRequest request, CancellationToken token = default( CancellationToken ) )
       {
          return await PerformTransactionAsync<SubscribeContextAvailabilityResponse, SubscribeContextAvailabilityRequest>( requestUri, request, token ).ConfigureAwait( false );
       }

       public async Task<SubscribeContextResponse> SendAsync( string requestUri, SubscribeContextRequest request, CancellationToken token = default( CancellationToken ) )
       {
          return await PerformTransactionAsync<SubscribeContextResponse, SubscribeContextRequest>( requestUri, request, token ).ConfigureAwait( false );
       }

       public async Task<UnsubscribeContextAvailabilityResponse> SendAsync( string requestUri, UnsubscribeContextAvailabilityRequest request, CancellationToken token = default( CancellationToken ) )
       {
          return await PerformTransactionAsync<UnsubscribeContextAvailabilityResponse, UnsubscribeContextAvailabilityRequest>( requestUri, request, token ).ConfigureAwait( false );
       }

       public async Task<UnsubscribeContextResponse> SendAsync( string requestUri, UnsubscribeContextRequest request, CancellationToken token = default( CancellationToken ) )
       {
          return await PerformTransactionAsync<UnsubscribeContextResponse, UnsubscribeContextRequest>( requestUri, request, token ).ConfigureAwait( false );
       }

       public async Task<UpdateContextAvailabilitySubscriptionResponse> SendAsync( string requestUri, UpdateContextAvailabilitySubscriptionRequest request, CancellationToken token = default( CancellationToken ) )
       {
          return await PerformTransactionAsync<UpdateContextAvailabilitySubscriptionResponse, UpdateContextAvailabilitySubscriptionRequest>( requestUri, request, token ).ConfigureAwait( false );
       }

       public async Task<UpdateContextResponse> SendAsync( string requestUri, UpdateContextRequest request, CancellationToken token = default( CancellationToken ) )
       {
          return await PerformTransactionAsync<UpdateContextResponse, UpdateContextRequest>( requestUri, request, token ).ConfigureAwait( false );
       }

       public async Task<UpdateContextSubscriptionResponse> SendAsync( string requestUri, UpdateContextSubscriptionRequest request, CancellationToken token = default( CancellationToken ) )
       {
          return await PerformTransactionAsync<UpdateContextSubscriptionResponse, UpdateContextSubscriptionRequest>( requestUri, request, token ).ConfigureAwait( false );
       }

       public async Task<NotifyContextResponse> SendAsync( string requestUri, NotifyContextRequest request, CancellationToken token = default( CancellationToken ) )
       {
          return await PerformTransactionAsync<NotifyContextResponse, NotifyContextRequest>( requestUri, request, token ).ConfigureAwait( false );
       }

       public async Task<NotifyContextAvailabilityResponse> SendAsync( string requestUri, NotifyContextAvailabilityRequest request, CancellationToken token = default( CancellationToken ) )
       {
          return await PerformTransactionAsync<NotifyContextAvailabilityResponse, NotifyContextAvailabilityRequest>( requestUri, request, token ).ConfigureAwait( false );
       }

       private async Task<TResult> PerformTransactionAsync<TResult, TInput>( string requestUri, TInput input, CancellationToken token )
       {
          _logger.Debug( "Sending {0} to {1}", input.GetType().Name, requestUri );
          using (var response = await _client.PostAsync<TInput>( requestUri, input, _currentFormatter, token ).ConfigureAwait( false ) )
          {
             if ( response.IsSuccessStatusCode )
             {
                var result = await response.Content.ReadAsAsync<TResult>( Formatters ).ConfigureAwait( false );
                _logger.Debug( "Sent {0} to {1} and received successful response of type {2}", input.GetType().Name, requestUri, result.GetType().Name );
                return result;
             }
             else
             {
                _logger.Debug( "Sent {0} to {1} and received unsuccessful response", input.GetType().Name, requestUri );
                throw new HttpRequestException( response.ReasonPhrase );
             }
          }
       }

       #region IDisposable Members

       ~NgsiClient()
       {
          Dispose( false );
       }

       public void Dispose()
       {
          if ( !_disposed )
          {
             Dispose( true );
             _disposed = true;
             GC.SuppressFinalize( this );
          }
       }

       private void Dispose( bool disposing )
       {
          if ( disposing )
          {
             _client.Dispose();
          }
       }

       #endregion
    }
}
