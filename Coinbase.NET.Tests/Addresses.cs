using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coinbase.Net.Api;
using Coinbase.Net.Authentication;
using Coinbase.Net.Tests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coinbase.Net.Tests
{
    [TestClass]
    public class Addresses
    {
        [TestMethod]
        public void GetAddressesAssociatedWithAccount()
        {
            var client = new CoinbaseClient(Settings.Default.ApiKey, new ApiKeyAuthenticator());
            var accountAddresses = client.GetAccountAddresses().Result;

            Assert.IsNotNull(accountAddresses);
            Assert.IsTrue(accountAddresses.AccountAddresses.Any());
        }
    }
}
