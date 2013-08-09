using System;
using Newtonsoft.Json.Linq;

namespace Coinbase.Net.Types
{
    public struct PriceUnit
    {
        public readonly decimal Quantity;
        public readonly string Currency;

        public PriceUnit(decimal amount, string currency)
        {
            Quantity = amount;
            Currency = currency;
        }

        public static PriceUnit FromJToken(JToken jToken, string amountKey = "amount", string currencyKey = "currency")
        {
            var amount = jToken[amountKey].Value<decimal>();
            var currency = jToken[currencyKey].Value<string>();
            return new PriceUnit(amount, currency);
        }

        public override string ToString()
        {
            return String.Format("({0} {1})", Quantity, Currency);
        }
    }
}
