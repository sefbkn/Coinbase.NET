namespace Coinbase.Net.Types
{
    public class AccountReceiveAddress
    {
        public bool IsSuccessful { get; private set; }
        public string Address { get; private set; }
        public string CallbackUrl { get; private set; }

        public AccountReceiveAddress(bool isSuccessful, string address, string callbackUrl)
        {
            this.IsSuccessful = isSuccessful;
            this.Address = address;
            this.CallbackUrl = callbackUrl;
        }
    }
}
