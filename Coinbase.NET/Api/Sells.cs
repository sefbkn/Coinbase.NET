using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Coinbase.Net.Types;

namespace Coinbase.Net.Api
{
    public partial class CoinbaseClient
    {
        public async Task<ExchangeBitcoinResponse> SellBitcoin(decimal bitcoinQuantity)
        {
            var url = GetSellUrl();
            var jObject = await GetAuthenticatedResource(url, HttpMethod.Post, new Dictionary<string, object> { { "qty", bitcoinQuantity } });
            return ExchangeBitcoinResponse.FromJObject(jObject);
        }

        public static string GetSellUrl()
        {
            return BaseUrl + "sells";
        }
    }
}
