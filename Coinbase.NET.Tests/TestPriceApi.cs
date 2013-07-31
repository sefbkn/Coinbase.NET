using System;
using System.Linq;
using Coinbase.NET.API;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coinbase.NET.Tests
{
    [TestClass]
    public class TestPriceApi
    {
        [TestMethod]
        public void TestGetSpotRate()
        {
            var spotRate = CoinbaseClient.GetSpotPrice().Result;

            Assert.IsFalse(String.IsNullOrWhiteSpace(spotRate.Price.Currency));
        }

        [TestMethod]
        public void TestSellPrice()
        {
            var prices = CoinbaseClient.GetBitcoinSalePrice().Result;

            // Assert that currencies are returned from service.
            // We can't go on prices since fees can be waived, negated, etc.
            Assert.IsFalse(String.IsNullOrWhiteSpace(prices.BankFee.Currency));
            Assert.IsFalse(String.IsNullOrWhiteSpace(prices.CoinbaseFee.Currency));
            Assert.IsFalse(String.IsNullOrWhiteSpace(prices.Subtotal.Currency));
            Assert.IsFalse(String.IsNullOrWhiteSpace(prices.TotalAmount.Currency));
        }

        [TestMethod]
        public void TestInvalidGetSellPrice()
        {
            try
            {
                CoinbaseClient.GetBitcoinSalePrice(-1).Wait();
                Assert.Fail("Should not be able to pass negative bitcoin quantity");
            }

            catch (Exception exception)
            {
                if (exception is ArgumentOutOfRangeException)
                    return;
                if (exception is AggregateException)
                {
                    var aggregateException = (AggregateException) exception;
                    if (aggregateException.InnerExceptions != null)
                        if (aggregateException.InnerExceptions.All(e => e is ArgumentOutOfRangeException))
                            return;
                }

                throw;
            }
        }

        [TestMethod]
        public void TestGetBuyPrice()
        {
            var prices = CoinbaseClient.GetBitcoinBuyPrice().Result;

            // Assert that currencies are returned from service.
            // We can't go on prices since fees can be waived, negated, etc.
            Assert.IsFalse(String.IsNullOrWhiteSpace(prices.BankFee.Currency));
            Assert.IsFalse(String.IsNullOrWhiteSpace(prices.CoinbaseFee.Currency));
            Assert.IsFalse(String.IsNullOrWhiteSpace(prices.Subtotal.Currency));
            Assert.IsFalse(String.IsNullOrWhiteSpace(prices.TotalAmount.Currency));
        }

        [TestMethod]
        public void TestInvalidBuyParameters()
        {
            try
            {
                CoinbaseClient.GetBitcoinBuyPrice(-1).Wait();
                Assert.Fail("Should not be able to pass negative bitcoin quantity");
            }
            catch (Exception exception)
            {
                if (exception is ArgumentOutOfRangeException)
                    return;
                if (exception is AggregateException)
                {
                    var aggregateException = (AggregateException)exception;
                    if (aggregateException.InnerExceptions != null)
                        if (aggregateException.InnerExceptions.All(e => e is ArgumentOutOfRangeException))
                            return;
                }

                throw;
            }

        }
    }
}
