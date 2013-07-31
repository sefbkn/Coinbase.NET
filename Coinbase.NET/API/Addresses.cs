using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coinbase.NET.Types;
using Newtonsoft.Json.Linq;

namespace Coinbase.NET.API
{
    public partial class CoinbaseClient
    {
        // TODO: Implement paging
        public async Task<AccountAddressContainer> GetAccountAddresses()
        {
            var url = GetAccountAddressesUrl();
            var response = await GetAuthenticatedResource(url);

            var addresses = 
                new JArray(response["addresses"]).SelectMany(t => t)
                .Select(a =>
                {
                    var currentAddress = a["address"];
                    var address = currentAddress["address"].Value<string>();
                    var callbackUrl = currentAddress["callback_url"].Value<string>();
                    var label = currentAddress["label"].Value<string>();
                    var createdAt = currentAddress["created_at"].Value<DateTime>();
                    
                    return new AccountAddress(address, callbackUrl, label, createdAt);
                });

            var totalCount = response["total_count"].Value<int>();
            var pageCount = response["num_pages"].Value<int>();
            var currentPage = response["current_page"].Value<int>();

            return new AccountAddressContainer(addresses, 
                totalCount, 
                pageCount, 
                currentPage
            );
        }

        private string GetAccountAddressesUrl()
        {
            return BaseUrl + "addresses";
        }
    }
}
