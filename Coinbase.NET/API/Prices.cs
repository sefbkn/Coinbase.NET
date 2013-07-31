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

            return new SpotRate
            {
                Price = PriceUnit.FromJToken(spotPriceJObject)
            };
        }

        public async static Task<PriceSellResponse> GetBitcoinSalePrice(decimal bitcoinQuantity = 1, string currency = null)
        {
            if (bitcoinQuantity < 0)
                throw new ArgumentOutOfRangeException("bitcoinQuantity", "Argument bitCoinQuantity must be a non-negative value.");

            // Do not validate currency.
            var url = GetSellPriceUrl(bitcoinQuantity, currency);
            var priceJObject = await GetUnauthenticatedJResource(url);
            var fees = priceJObject["fees"].Children().ToArray();

            var subtotal = PriceUnit.FromJToken(priceJObject["subtotal"]);
            var coinbaseFee = PriceUnit.FromJToken(fees[0]["coinbase"]);
            var bankFee = PriceUnit.FromJToken(fees[1]["bank"]);
            var total = PriceUnit.FromJToken(priceJObject["total"]);

            return new PriceSellResponse
            {
                Subtotal = subtotal,
                CoinbaseFee = coinbaseFee,
                BankFee = bankFee,
                TotalAmount = total
            };

        }

        public async static Task<PriceBuyResponse> GetBitcoinBuyPrice(decimal bitcoinQuantity = 1, string currency = null)
        {
            if (bitcoinQuantity < 0)
                throw new ArgumentOutOfRangeException("bitcoinQuantity", "Argument bitCoinQuantity must be a non-negative value.");

            // Do not validate currency.
            var url = GetBuyPricesUrl(bitcoinQuantity, currency);
            var priceJObject = await GetUnauthenticatedJResource(url);
            var fees = priceJObject["fees"].Children().ToArray();

            var subtotal = PriceUnit.FromJToken(priceJObject["subtotal"]);
            var coinbaseFee = PriceUnit.FromJToken(fees[0]["coinbase"]);
            var bankFee = PriceUnit.FromJToken(fees[1]["bank"]);
            var total = PriceUnit.FromJToken(priceJObject["total"]);

            return new PriceBuyResponse
            {
                Subtotal = subtotal,
                CoinbaseFee = coinbaseFee,
                BankFee = bankFee,
                TotalAmount = total
            };
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
