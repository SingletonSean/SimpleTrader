using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.Domain.Services.TransactionServices
{
    public class SellStockService : ISellStockService
    {
        private readonly IStockPriceService _stockPriceService;
        private readonly IDataService<Account> _accountService;

        public SellStockService(IStockPriceService stockPriceService, IDataService<Account> accountService)
        {
            _stockPriceService = stockPriceService;
            _accountService = accountService;
        }

        public async Task<Account> SellStock(Account seller, string symbol, int shares)
        {
            // Validate seller has sufficient shares.
            int accountShares = GetAccountSharesForSymbol(seller, symbol);
            if(accountShares < shares)
            {
                throw new InsufficientSharesException(symbol, accountShares, shares);
            }

            double stockPrice = await _stockPriceService.GetPrice(symbol);

            seller.AssetTransactions.Add(new AssetTransaction()
            {
                Account = seller,
                Asset = new Asset()
                {
                    PricePerShare = stockPrice,
                    Symbol = symbol
                },
                DateProcessed = DateTime.Now,
                IsPurchase = false,
                Shares = shares
            });

            seller.Balance += stockPrice * shares;

            await _accountService.Update(seller.Id, seller);

            return seller;
        }

        private int GetAccountSharesForSymbol(Account seller, string symbol)
        {
            IEnumerable<AssetTransaction> accountTransactionsForSymbol = seller.AssetTransactions.Where(a => a.Asset.Symbol == symbol);
            
            return accountTransactionsForSymbol.Sum(a => a.IsPurchase ? a.Shares : -a.Shares);
        }
    }
}
