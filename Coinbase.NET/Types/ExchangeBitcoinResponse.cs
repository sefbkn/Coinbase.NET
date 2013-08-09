using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Coinbase.Net.Types
{
    /// <summary>
    /// Response type that is returned when a bitcoin is
    /// either bought or sold for another currency.
    /// </summary>
    public class ExchangeBitcoinResponse
    {
        public bool IsSuccessful { get; private set; }
        public string[] Errors { get; private set; }
        public TransferInfo Transfer { get; private set; }

        public ExchangeBitcoinResponse(bool isSuccessful, IEnumerable<string> errors, TransferInfo transfer)
        {
            this.IsSuccessful = isSuccessful;
            this.Transfer = transfer;

            var enumerable = errors == null ? null : errors.ToArray();
            if(enumerable != null && enumerable.Any())
                this.Errors = enumerable;
        }

        public static ExchangeBitcoinResponse FromJObject(JObject jObject)
        {
            var isSuccess = jObject["success"].ToObject<bool>();
            var errors = jObject["errors"] == null
                ? null : jObject["errors"].Values<string>();
            var tjo = jObject["transfer"];
            var transferInfo = new TransferInfo(
                    tjo.Value<string>("_type"),
                    tjo.Value<string>("code"),
                    tjo.Value<DateTime?>("created_at"),
                    tjo["fees"]
                        .Select(token => (JProperty) token)
                        .Select(jProperty => 
                            new CoinbaseFee(
                                jProperty.Name, 
                                new PriceUnit(
                                    jProperty.Value["cents"].Value<decimal>() * .01m,
                                    jProperty.Value["currency_iso"].Value<string>()))),
                    tjo.Value<string>("status"),
                    tjo.Value<DateTime?>("payout_date"),
                    PriceUnit.FromJToken(tjo["btc"]),
                    PriceUnit.FromJToken(tjo["subtotal"]),
                    PriceUnit.FromJToken(tjo["total"])
                );

            return new ExchangeBitcoinResponse(isSuccess, errors, transferInfo);
        }
    }
}
;