using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Coinbase.NET.Types;
using Newtonsoft.Json.Linq;

namespace Coinbase.NET.API
{
    public partial class CoinbaseClient
    {
        /// <summary>
        /// Purchase bitcoin using primary bank account.
        /// </summary>
        /// <param name="bitcoinQuantity"></param>
        /// <returns></returns>
        public async Task<ExchangeBitcoinResponse> PurchaseBitcoin(decimal bitcoinQuantity)
        {
            var url = GetBuyUrl();
            var jObject = await GetAuthenticatedResource(url, HttpMethod.Post, new Dictionary<string, object>{{"qty", bitcoinQuantity}});
            return ExchangeBitcoinResponse.FromJObject(jObject);
        }

        public static string GetBuyUrl()
        {
            return BaseUrl
                   + "buys";
        }
    }
}
