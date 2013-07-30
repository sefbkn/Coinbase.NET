using System;
using System.Net;

namespace Coinbase.NET.Endpoints
{
    internal static class RestEndpoint
    {
        public static string BaseUrl
        {
            get { return "https://coinbase.com/api/v1/"; }
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

        private static string AppendFormatIfNotNull(this string url, string currencyFormat, string currency)
        {
            return String.IsNullOrWhiteSpace(currency) ? url : url + String.Format(currencyFormat);
        }
    }
}
