using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Coinbase.NET.Authentication;
using Coinbase.NET.Extensions;
using Newtonsoft.Json.Linq;

namespace Coinbase.NET
{
    public class CoinbaseClient : IDisposable
    {
        private const string ClassName = "CoinbaseClient";

        private bool _isDisposed;
        private CbAuthMethod _method;
        private SecureString _authToken;

        private HttpClient _httpClient;

        public CoinbaseClient(string token, CbAuthMethod method = CbAuthMethod.ApiKey)
        {
            _isDisposed = false;

            if(String.IsNullOrWhiteSpace(token))
                throw new ArgumentException(String.Format("Parameter 'token' passed to {0} constructor must not be empty.", ClassName));

            _authToken = token.ToSecureString();
            _method = method;
            _httpClient = new HttpClient();
        }

        private Uri AuthenticateUri(Uri uri)
        {
            var newUri = new Uri(uri.AbsoluteUri);
            switch (_method)
            {
                case CbAuthMethod.OAuth2:
                    return newUri;
                case CbAuthMethod.ApiKey:
                    return newUri;
                default:
                    throw new InvalidOperationException("Cannot authenticate url with invalid Authentication Method");
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
