namespace Coinbase.NET.Types
{
    public class PriceSellResponse
    {
        public PriceUnit Subtotal { get; set; }
        public PriceUnit CoinbaseFee { get; set; }
        public PriceUnit BankFee { get; set; }
        public PriceUnit TotalAmount { get; set; }
    }
}
