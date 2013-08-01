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
    public class ExchangeCurrencyToFromBitcoin
    {
        [TestMethod]
        public void TestSellBitcoin()
        {
            var quantity = 0.0m;

            var coinbaseClient = new CoinbaseClient(Settings.Default.ApiKey, AuthenticationMode.ApiKey);
            var response = coinbaseClient.SellBitcoin(quantity).Result;

            Assert.IsTrue(response.Errors.Any());
        }

        [TestMethod]
        public void TestPurchaseBitcoin()
        {
            // Intentional to ensure that we are getting a properly formatted
            // response back from the request.
            var quantity = 0.0m;

            var coinbaseClient = new CoinbaseClient(Settings.Default.ApiKey, AuthenticationMode.ApiKey);
            var response = coinbaseClient.PurchaseBitcoin(quantity).Result;

            Assert.IsTrue(response.Errors.Any());
        }
    }
}
