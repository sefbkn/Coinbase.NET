using System;
using System.Linq;
using System.Net;
using Coinbase.NET.Endpoints;
using Coinbase.NET.Types;
using Newtonsoft.Json.Linq;

namespace Coinbase.NET
{
    public static class CoinBaseClient
    {
        public static SpotRate GetSpotPrice(string currency = null)
        {
            var url = RestEndpoint.GetSpotRateUrl(currency);
            var responseJson = new WebClient().DownloadString(url);
            var spotPriceJObject = JToken.Parse(responseJson);

            return new SpotRate
            {
                Price = PriceUnit.FromJToken(spotPriceJObject)
            };
        }

        public static PriceSellResponse GetBitcoinSalePrice(decimal bitcoinQuantity = 1, string currency = null)
        {
            if (bitcoinQuantity < 0)
                throw new ArgumentOutOfRangeException("bitcoinQuantity", "Argument bitCoinQuantity must be a non-negative value.");

            // Do not validate currency.
            var url = RestEndpoint.GetSellPriceUrl(bitcoinQuantity, currency);
            var responseJson = new WebClient().DownloadString(url);
            var priceJObject = JToken.Parse(responseJson);
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

        public static PriceBuyResponse GetBitcoinBuyPrice(decimal bitcoinQuantity = 1, string currency = null)
        {
            if (bitcoinQuantity < 0)
                throw new ArgumentOutOfRangeException("bitcoinQuantity", "Argument bitCoinQuantity must be a non-negative value.");

            // Do not validate currency.
            var url = RestEndpoint.GetBuyPricesUrl(bitcoinQuantity, currency);
            var responseJson = new WebClient().DownloadString(url);
            var priceJObject = JToken.Parse(responseJson);
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



    }
}
