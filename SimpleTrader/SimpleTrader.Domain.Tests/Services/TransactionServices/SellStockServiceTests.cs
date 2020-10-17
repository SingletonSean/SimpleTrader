using Moq;
using NUnit.Framework;
using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.TransactionServices;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.Domain.Tests.Services.TransactionServices
{
    [TestFixture]
    public class SellStockServiceTests
    {
        private SellStockService _sellStockService;

        private Mock<IStockPriceService> _mockStockPriceService;
        private Mock<IDataService<Account>> _mockAccountService;

        [SetUp]
        public void SetUp()
        {
            _mockStockPriceService = new Mock<IStockPriceService>();
            _mockAccountService = new Mock<IDataService<Account>>();

            _sellStockService = new SellStockService(_mockStockPriceService.Object, _mockAccountService.Object);
        }

        [Test]
        public void SellStock_WithInsufficientShares_ThrowsInsufficientSharesException()
        {
            string expectedSymbol = "T";
            int expectedAccountShares = 0;
            int expectedRequiredShares = 10;
            Account seller = CreateAccount(expectedSymbol, expectedAccountShares);

            InsufficientSharesException exception = Assert.ThrowsAsync<InsufficientSharesException>(
                () => _sellStockService.SellStock(seller, expectedSymbol, expectedRequiredShares));
            string actualSymbol = exception.Symbol;
            double actualAccountBalance = exception.AccountShares;
            double actualRequiredBalance = exception.RequiredShares;

            Assert.AreEqual(expectedSymbol, actualSymbol);
            Assert.AreEqual(expectedAccountShares, actualAccountBalance);
            Assert.AreEqual(expectedRequiredShares, actualRequiredBalance);
        }

        [Test]
        public void SellStock_WithInvalidSymbol_ThrowsInvalidSymbolExceptionForSymbol()
        {
            string expectedInvalidSymbol = "bad_symbol";
            Account seller = CreateAccount(expectedInvalidSymbol, 10);
            _mockStockPriceService.Setup(s => s.GetPrice(expectedInvalidSymbol)).ThrowsAsync(new InvalidSymbolException(expectedInvalidSymbol));

            InvalidSymbolException exception = Assert.ThrowsAsync<InvalidSymbolException>(() => _sellStockService.SellStock(seller, expectedInvalidSymbol, 5));
            string actualInvalidSymbol = exception.Symbol;

            Assert.AreEqual(expectedInvalidSymbol, actualInvalidSymbol);
        }

        [Test]
        public void SellStock_WithGetPriceFailure_ThrowsException()
        {
            Account seller = CreateAccount(It.IsAny<string>(), 10);
            _mockStockPriceService.Setup(s => s.GetPrice(It.IsAny<string>())).ThrowsAsync(new Exception());

            Assert.ThrowsAsync<Exception>(() => _sellStockService.SellStock(seller, It.IsAny<string>(), 5));
        }

        [Test]
        public void SellStock_WithAccountUpdateFailure_ThrowsException()
        {
            Account seller = CreateAccount(It.IsAny<string>(), 10);
            _mockAccountService.Setup(s => s.Update(It.IsAny<int>(), It.IsAny<Account>())).ThrowsAsync(new Exception());

            Assert.ThrowsAsync<Exception>(() => _sellStockService.SellStock(seller, It.IsAny<string>(), 5));
        }

        [Test]
        public async Task SellStock_WithSuccessfulSell_ReturnsAccountWithNewTransaction()
        {
            int expectedTransactionCount = 2;
            Account seller = CreateAccount(It.IsAny<string>(), 10);

            seller = await _sellStockService.SellStock(seller, It.IsAny<string>(), 5);
            int actualTransactionCount = seller.AssetTransactions.Count;

            Assert.AreEqual(expectedTransactionCount, actualTransactionCount);
        }

        [Test]
        public async Task SellStock_WithSuccessfulSell_ReturnsAccountWithNewBalance()
        {
            double expectedBalance = 100;
            Account seller = CreateAccount(It.IsAny<string>(), 10);
            _mockStockPriceService.Setup(s => s.GetPrice(It.IsAny<string>())).ReturnsAsync(50);

            seller = await _sellStockService.SellStock(seller, It.IsAny<string>(), 2);
            double actualBalance = seller.Balance;

            Assert.AreEqual(expectedBalance, actualBalance);
        }

        private Account CreateAccount(string symbol, int shares)
        {
            return new Account()
            {
                AssetTransactions = new List<AssetTransaction>()
                {
                    new AssetTransaction()
                    {
                        Asset = new Asset()
                        {
                            Symbol = symbol
                        },
                        IsPurchase = true,
                        Shares = shares
                    }
                }
            };
        }
    }
}
