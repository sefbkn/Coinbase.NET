using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Coinbase.NET.Tests
{
    [TestClass]
    public class TestPriceApi
    {
        [TestMethod]
        public void TestGetSpotRate()
        {
            var spotRate = CoinBaseClient.GetSpotPrice();

            Assert.IsFalse(String.IsNullOrWhiteSpace(spotRate.Price.Currency));
        }

        [TestMethod]
        public void TestSellPrice()
        {
            var prices = CoinBaseClient.GetBitcoinSalePrice();

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
                CoinBaseClient.GetBitcoinSalePrice(-1);
                Assert.Fail("Should not be able to pass negative bitcoin quantity");
            }
            catch (ArgumentOutOfRangeException exception)
            {

            }
        }

        [TestMethod]
        public void TestGetBuyPrice()
        {
            var prices = CoinBaseClient.GetBitcoinBuyPrice();

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
                CoinBaseClient.GetBitcoinBuyPrice(-1);
                Assert.Fail("Should not be able to pass negative bitcoin quantity");
            }
            catch (ArgumentOutOfRangeException exception)
            {
                
            }
        }
    }
}
