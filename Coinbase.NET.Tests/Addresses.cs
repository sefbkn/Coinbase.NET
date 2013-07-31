using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coinbase.NET.API;
using Coinbase.NET.Authentication;
using Coinbase.NET.Tests.Properties;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coinbase.NET.Tests
{
    [TestClass]
    public class Addresses
    {
        [TestMethod]
        public void GetAddressesAssociatedWithAccount()
        {
            var client = new CoinbaseClient(Settings.Default.ApiKey, AuthenticationMode.ApiKey);
            var accountAddresses = client.GetAccountAddresses().Result;

            Assert.IsNotNull(accountAddresses);
            Assert.IsTrue(accountAddresses.AccountAddresses.Any());
        }
    }
}
