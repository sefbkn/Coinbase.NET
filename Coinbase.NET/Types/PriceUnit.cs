using Newtonsoft.Json.Linq;

namespace Coinbase.NET.Types
{
    public struct PriceUnit
    {
        public decimal Amount;
        public string Currency;

        public PriceUnit(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        public static PriceUnit FromJToken(JToken jToken)
        {
            var amount = jToken["amount"].Value<decimal>();
            var currency = jToken["currency"].Value<string>();
            return new PriceUnit(amount, currency);
        }
    }
}
