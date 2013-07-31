using System;
using System.Collections.Generic;

namespace Coinbase.NET.Types
{
    public class AccountAddress
    {
        public string Address { get; private set; }
        public string CallbackUrl { get; private set; }
        public string Label { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public AccountAddress(string address, string callbackUrl, string label, DateTime createdAt)
        {
            this.Address = address;
            this.CallbackUrl = callbackUrl;
            this.Label = label;
            this.CreatedAt = createdAt;
        }
    }

    public class AccountAddressContainer
    {
        public IEnumerable<AccountAddress> AccountAddresses { get; set; } 

        public int TotalCount { get; set; }
        public int PageCount { get; set; }
        public int CurrentPage { get; set; }

        public AccountAddressContainer(IEnumerable<AccountAddress> addresses, int totalCount, int pageCount, int currentPage)
        {
            this.AccountAddresses = addresses;
            this.TotalCount = totalCount;
            this.PageCount = pageCount;
            this.CurrentPage = currentPage;
        }
    }
}