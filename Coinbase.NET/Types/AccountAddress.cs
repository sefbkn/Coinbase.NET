using System;

namespace Coinbase.NET.Types
{
    public class AccountAddress
    {
        public string Address { get; private set; }
        public string CallbackUrl { get; private set; }
        public string Label { get; private set; }
        public DateTime CreatedAt { get; private set; }

        internal AccountAddress(string address, string callbackUrl, string label, DateTime createdAt)
        {
            this.Address = address;
            this.CallbackUrl = callbackUrl;
            this.Label = label;
            this.CreatedAt = createdAt;
        }
    }
}