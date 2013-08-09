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
    public class ExchangeCurrencyToFromBitcoin
    {
        [TestMethod]
        public void TestSellBitcoin()
        {
            var quantity = 0.0m;

            var coinbaseClient = new CoinbaseClient(Settings.Default.ApiKey, new ApiKeyAuthenticator());
            var response = coinbaseClient.SellBitcoin(quantity).Result;

            Assert.IsTrue(response.Errors.Any());
        }

        [TestMethod]
        public void TestPurchaseBitcoin()
        {
            // Intentional to ensure that we are getting a properly formatted
            // response back from the request.
            var quantity = 0.0m;

            var coinbaseClient = new CoinbaseClient(Settings.Default.ApiKey, new ApiKeyAuthenticator());
            var response = coinbaseClient.PurchaseBitcoin(quantity).Result;

            Assert.IsTrue(response.Errors.Any());
        }
    }
}
