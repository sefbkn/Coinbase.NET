using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using Coinbase.NET.Authentication;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Coinbase.NET.API
{
    public partial class CoinbaseClient : IDisposable
    {
        private static readonly Uri BaseUrl = new Uri("https://coinbase.com/api/v1/");

        private const string ClassName = "CoinbaseClient";
        private bool _isDisposed;
        private readonly AuthenticationMode Method;
        private readonly string _authToken;

        public CoinbaseClient(string token, AuthenticationMode method)
        {
            _isDisposed = false;

            if(String.IsNullOrWhiteSpace(token))
                throw new ArgumentException(String.Format("Parameter 'token' passed to {0} constructor must not be empty.", ClassName));

            if(method == null)
                throw new ArgumentNullException("method", "Authentication method must be specified.");

            _authToken = token;
            Method = method;
        }

        /// <summary>
        /// Performs appropriate HTTP operations to retrive
        /// requested information from the supplied URL.
        /// </summary>
        /// <param name="url">Path to the resource a request is to me made to</param>
        /// <param name="httpMethod">HTTP Method to make request with.  Default is HttpMethod.Get</param>
        /// <returns>JObject that represents data returned from the request.</returns>
        private async Task<JObject> GetAuthenticatedResource(string url, HttpMethod httpMethod = null, Dictionary<string, object> parameters = null)
        {
            AssertNotDisposed();

            HttpResponseMessage response = null;

            if(parameters == null)
                parameters = new Dictionary<string, object>();

            if (httpMethod == null)
                httpMethod = HttpMethod.Get;

            if (httpMethod == HttpMethod.Get)
                response = await GetAuthenticatedGetRequest(url, parameters);
            else if (httpMethod == HttpMethod.Post)
                response = await GetAuthenticatedPostRequest(url, parameters);
            else
                throw new ArgumentException("Unrecognized value for argument 'httpMethod' supplied.");

            var resultContent = await response
                .EnsureSuccessStatusCode()
                .Content
                .ReadAsStringAsync();

            return JObject.Parse(resultContent);
        }

        private async Task<HttpResponseMessage> GetAuthenticatedGetRequest(string url, Dictionary<string, object> parameters)
        {
            using (var httpClient = new HttpClient())
            {
                var newUrl = this.Method.AuthorizeUrl(url, _authToken);
                var newUri = new Uri(newUrl);

                return await httpClient.GetAsync(newUri);
            }
        }

        private async Task<HttpResponseMessage> GetAuthenticatedPostRequest(string url, Dictionary<string, object> parameters)
        {
            using (var client = new HttpClient())
            {
                parameters = parameters ?? new Dictionary<string, object>();
                Method.AuthorizePostBody(parameters, _authToken);

                var json = JsonConvert.SerializeObject(parameters);
                var stringContent = new StringContent(json, new UTF8Encoding(), "application/json");

                return await client.PostAsync(url, stringContent);
            }
        }

        private async static Task<JObject> GetUnauthenticatedJResource(string url)
        {
            using (var httpClient = new HttpClient())
            {
                var data = await httpClient.GetStringAsync(url);
                return JObject.Parse(data);
            }
        }

        #region Disposal / Cleanup
        private void AssertNotDisposed()
        {
            if (_isDisposed) throw new ObjectDisposedException(String.Format("Cannot perform operation on disposed object '{0}'.", ClassName));
        }

        public void Dispose()
        {
            AssertNotDisposed();
            _isDisposed = true;
        }
        #endregion
    }
}
