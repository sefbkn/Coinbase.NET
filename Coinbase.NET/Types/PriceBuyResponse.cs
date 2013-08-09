using System.Collections.Generic;
using System.Linq;

namespace Coinbase.Net.Types
{
    public class PriceBuyResponse
    {
        public PriceUnit Subtotal { get; private set; }
        public CoinbaseFee[] Fees { get; private set; }
        public PriceUnit TotalAmount { get; private set; }

        public PriceBuyResponse(PriceUnit subtotal, IEnumerable<CoinbaseFee> fees, PriceUnit totalAmount)
        {
            this.Subtotal = subtotal;
            this.Fees = fees.ToArray();
            this.TotalAmount = totalAmount;
        }
    }
}
