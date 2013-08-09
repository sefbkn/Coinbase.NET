namespace Coinbase.Net.Types
{
    public class CoinbaseFee
    {
        public string Description { get; private set; }
        public PriceUnit Price { get; private set; }

        public CoinbaseFee(string description, PriceUnit priceUnit)
        {
            this.Description = description;
            this.Price = priceUnit;
        }

        public override string ToString()
        {
            return Description + ", " + Price ;
        }
    }
}