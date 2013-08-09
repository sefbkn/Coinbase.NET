using System;
using Coinbase.Net.Api;
using Coinbase.Net.Authentication;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coinbase.Net.Tests
{
    [TestClass]
    public class Initialization
    {
        [TestMethod]
        public void CreateClientNoApiKey()
        {
            try
            {
                new CoinbaseClient(null, new ApiKeyAuthenticator());
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
            var coinbaseClient = new CoinbaseClient("APIKEY", new ApiKeyAuthenticator());
        }

        [TestMethod]
        public void DisposeClientOnce()
        {
            var coinbaseClient = new CoinbaseClient("APIKEY", new ApiKeyAuthenticator());
            coinbaseClient.Dispose();
        }

        [TestMethod]
        public void DisposeClientTwice()
        {
            var coinbaseClient = new CoinbaseClient("APIKEY", new ApiKeyAuthenticator());
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
