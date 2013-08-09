namespace Coinbase.Net.Types
{
    public class SpotRate
    {
        public PriceUnit Price { get; set; }

        public SpotRate(PriceUnit price)
        {
            this.Price = price;
        }
    }
}
