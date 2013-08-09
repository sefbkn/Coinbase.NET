using System;
using System.Collections.Generic;
using System.Linq;

namespace Coinbase.Net.Types
{
    public class TransferInfo
    {
        public string Type { get; private set; }
        public string Code { get; private set; }
        public DateTime? CreatedAt { get; private set; }
        public CoinbaseFee[] Fees { get; private set; }
        public string Status { get; private set; }
        public DateTime? PayoutDate { get; private set; }
        public PriceUnit BitcoinAmount { get; private set; }
        public PriceUnit Subtotal { get; private set; }
        public PriceUnit Total { get; private set; }

        public TransferInfo(string type, string code, DateTime? createdAt,
            IEnumerable<CoinbaseFee> fees, string status, DateTime? payoutDate,
            PriceUnit bitcoinAmount, PriceUnit subtotal, PriceUnit total)
        {
            this.Type = type;
            this.Code = code;
            this.CreatedAt = createdAt;
            this.Fees = fees.ToArray();
            this.Status = status;
            this.PayoutDate = payoutDate;
            this.BitcoinAmount = bitcoinAmount;
            this.Subtotal = subtotal;
            this.Total = total;
        }
    }
}