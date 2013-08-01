using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Coinbase.NET.Extensions;
using Coinbase.NET.Types;
using Newtonsoft.Json.Linq;

namespace Coinbase.NET.API
{
    public partial class CoinbaseClient
    {
        public async static Task<SpotRate> GetSpotPrice(string currency = null)
        {
            var url = GetSpotRateUrl(currency);
            var spotPriceJObject = await GetUnauthenticatedJResource(url);

            return new SpotRate(PriceUnit.FromJToken(spotPriceJObject));
        }

        public async static Task<PriceSellResponse> GetBitcoinSalePrice(decimal bitcoinQuantity = 1, string currency = null)
        {
            if (bitcoinQuantity < 0)
                throw new ArgumentOutOfRangeException("bitcoinQuantity", "Argument bitCoinQuantity must be a non-negative value.");

            // Do not validate currency.
            var url = GetSellPriceUrl(bitcoinQuantity, currency);
            var priceJObject = await GetUnauthenticatedJResource(url);

            var subtotal = PriceUnit.FromJToken(priceJObject["subtotal"]);
            var fees = priceJObject["fees"]
                .SelectMany(t => t)
                .Cast<JProperty>()
                .Select(jProperty => new CoinbaseFee(jProperty.Name,
                    PriceUnit.FromJToken(jProperty.Value)
                ));

            var total = PriceUnit.FromJToken(priceJObject["total"]);

            return new PriceSellResponse(subtotal, fees, total);
        }

        public async static Task<PriceBuyResponse> GetBitcoinBuyPrice(decimal bitcoinQuantity = 1, string currency = null)
        {
            if (bitcoinQuantity < 0)
                throw new ArgumentOutOfRangeException("bitcoinQuantity", "Argument bitcoinQuantity must be a non-negative value.");

            // Do not validate currency.
            var url = GetBuyPricesUrl(bitcoinQuantity, currency);
            var priceJObject = await GetUnauthenticatedJResource(url);

            var subtotal = PriceUnit.FromJToken(priceJObject["subtotal"]);
            var fees = priceJObject["fees"]
                .SelectMany(t => t)
                .Cast<JProperty>()
                .Select(jProperty => new CoinbaseFee(jProperty.Name,
                    PriceUnit.FromJToken(jProperty.Value)
                ));
            var total = PriceUnit.FromJToken(priceJObject["total"]);

            return new PriceBuyResponse(subtotal, fees, total);
        }

        public static string GetBuyPricesUrl(decimal quantity, string currency)
        {
            return BaseUrl
                + "prices/buy"
                + String.Format("/?qty={0}", quantity)
                .AppendFormatIfNotNull(
                    "&currency={0}",
                        WebUtility.UrlEncode(currency));
        }

        public static string GetSellPriceUrl(decimal quantity, string currency)
        {
            return BaseUrl
                + "prices/sell"
                + String.Format("/?qty={0}", quantity)
                .AppendFormatIfNotNull(
                    "&currency={0}",
                        WebUtility.UrlEncode(currency));
        }

        public static string GetSpotRateUrl(string currency)
        {
            return BaseUrl
                + "prices/spot_rate".AppendFormatIfNotNull("/?currency={0}", WebUtility.UrlEncode(currency));
        }
    }
}
