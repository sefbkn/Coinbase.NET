using System;
using Coinbase.NET.API;
using Coinbase.NET.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coinbase.NET.Tests
{
    [TestClass]
    public class Initialization
    {
        [TestMethod]
        public void CreateClientNoApiKey()
        {
            try
            {
                new CoinbaseClient(null, AuthenticationMode.ApiKey);
            }

            catch (ArgumentException)
            {
                return;
            }

            Assert.Fail();
        }

        [TestMethod]
        public void CreateClientWithApiKey()
        {
            var coinbaseClient = new CoinbaseClient("APIKEY", AuthenticationMode.ApiKey);
        }

        [TestMethod]
        public void DisposeClientOnce()
        {
            var coinbaseClient = new CoinbaseClient("APIKEY", AuthenticationMode.ApiKey);
            coinbaseClient.Dispose();
        }

        [TestMethod]
        public void DisposeClientTwice()
        {
            var coinbaseClient = new CoinbaseClient("APIKEY", AuthenticationMode.ApiKey);
            coinbaseClient.Dispose();

            try
            {
                coinbaseClient.Dispose();
            }

            catch (ObjectDisposedException)
            {
                return;
            }

            Assert.Fail("Object disposed of multiple times.");
        }
    }
}
