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
        /// <returns></returns>
        private async Task<JObject> GetAuthenticatedResource(string url, HttpMethod httpMethod = null, Dictionary<string, object> parameters = null)
        {
            if (httpMethod == null)
                httpMethod = HttpMethod.Get;

            if (httpMethod == HttpMethod.Get)
            {
                using (var httpClient = new HttpClient())
                {
                    var newUrl = this.Method.AuthorizeUrl(url, _authToken);
                    var newUri = new Uri(newUrl);

                    var result = await httpClient.GetAsync(newUri);
                    var resultContent = await result
                        .EnsureSuccessStatusCode()
                        .Content
                        .ReadAsStringAsync();

                    return JObject.Parse(resultContent);
                }
            }

            if (httpMethod == HttpMethod.Post)
            {
                using (var client = new HttpClient())
                {
                    parameters = parameters ?? new Dictionary<string, object>();
                    parameters.Add(Method.FieldName, _authToken);

                    var json = JsonConvert.SerializeObject(parameters);
                    var stringContent = new StringContent(json, new UTF8Encoding(), "application/json");

                    var result = await client.PostAsync(url, stringContent);
                    var resultContent = await result
                        .EnsureSuccessStatusCode()
                        .Content
                        .ReadAsStringAsync();

                    return JObject.Parse(resultContent);
                }
            }

            throw new ArgumentException("Unrecognized value for argument 'httpMethod' supplied.");
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
            if (_isDisposed) throw new ObjectDisposedException(String.Format("Cannot perform operation on disposed {0} object.", ClassName));
        }

        public void Dispose()
        {
            AssertNotDisposed();
            _isDisposed = true;
        }
        #endregion
    }
}
