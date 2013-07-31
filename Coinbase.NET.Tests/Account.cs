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
    public class Account
    {
        [TestMethod]
        public void TestGetBalance()
        {
            var coinbaseClient = new CoinbaseClient(Settings.Default.ApiKey, AuthenticationMode.ApiKey);
            var balance = coinbaseClient.GetBalance().Result;

            Assert.IsFalse(String.IsNullOrWhiteSpace(balance.Currency));
        }

        [TestMethod]
        public void TestGetReceiveAddress()
        {
            var coinbaseClient = new CoinbaseClient(Settings.Default.ApiKey, AuthenticationMode.ApiKey);
            var response = coinbaseClient.GetReceiveAddress().Result;

            Assert.IsFalse(String.IsNullOrWhiteSpace(response.Address));
        }

        [TestMethod]
        public void TestGenerateReceiveAddress()
        {
            // TODO:  Re-enable for testing.
            // Disabled to avoid creating so many keys
            // because I love testing.
            //return;

            var callbackUrl = "http://www.google.com";
            var coinbaseClient = new CoinbaseClient(Settings.Default.ApiKey, AuthenticationMode.ApiKey);
            var response = coinbaseClient.GetNewReceiveAddress(callbackUrl).Result;

            Assert.AreEqual(callbackUrl, response.CallbackUrl);
            Assert.IsFalse(String.IsNullOrWhiteSpace(response.Address));
        }
    }
}
