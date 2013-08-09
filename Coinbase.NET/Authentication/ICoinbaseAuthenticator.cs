using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coinbase.Net.Authentication
{
    public abstract class AuthenticatorBase
    {
        public abstract string FieldName { get; }

        /// <summary>
        /// Attaches authorization token to URL for authorization
        /// </summary>
        /// <param name="url">The URL to append the token to</param>
        /// <param name="token">The token to attach to the URL</param>
        /// <returns></returns>
        public virtual string Authorize(string url, string token)
        {
            if (String.IsNullOrWhiteSpace(url))
                throw new ArgumentNullException("url");

            var uri = new Uri(url);
            var stringBuilder =
                new StringBuilder(url)
                .Append(String.IsNullOrWhiteSpace(uri.Query) ? "?" : "&")
                .Append(FieldName)
                .Append("=")
                .Append(token);

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Adds authorization token to POST body
        /// </summary>
        /// <param name="body">Dictionary containing the body parameters</param>
        /// <param name="token">The token to add to the post body</param>
        /// <returns></returns>
        public virtual Dictionary<string, object> Authorize(Dictionary<string, object> body, string token)
        {
            if (body == null)
                throw new ArgumentNullException("body");

            var d = new Dictionary<string, object>(body);

            if (d.All(b => !String.Equals(b.Key, this.FieldName, StringComparison.CurrentCultureIgnoreCase)))
                d.Add(this.FieldName, token);

            return d;
        }
    }
}
