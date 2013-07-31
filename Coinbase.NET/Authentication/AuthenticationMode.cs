using System;
using System.Security;
using System.Text;

namespace Coinbase.NET.Authentication
{
    public class AuthenticationMode
    {
        public static readonly AuthenticationMode ApiKey = new AuthenticationMode("api_key");
        public static readonly AuthenticationMode OAuth2 = new AuthenticationMode("access_token");

        public readonly string FieldName;

        private AuthenticationMode() { }
        private AuthenticationMode(string fieldName)
        {
            FieldName = fieldName;
        }

        /// <summary>
        /// Attaches API key to URL for authorization
        /// </summary>
        /// <param name="url"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public string AuthorizeUrl(string url, string key)
        {
            if(String.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException("url");
            
            var uri = new Uri(url);
            var stringBuilder = 
                new StringBuilder(url)
                .Append(String.IsNullOrWhiteSpace(uri.Query) ? "?" : "&")
                .Append(FieldName)
                .Append("=")
                .Append(key);

            return stringBuilder.ToString();
        }
    }
}
