using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Coinbase.Net.Types
{
    public class AccountAddressContainer
    {
        public AccountAddress[] AccountAddresses { get; private set; } 

        public int TotalCount { get; private set; }
        public int PageCount { get; private set; }
        public int CurrentPage { get; private set; }

        public AccountAddressContainer(IEnumerable<AccountAddress> addresses, int totalCount, int pageCount, int currentPage)
        {
            this.AccountAddresses = addresses.ToArray();
            this.TotalCount = totalCount;
            this.PageCount = pageCount;
            this.CurrentPage = currentPage;
        }
    }
}