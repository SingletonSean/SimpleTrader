using SimpleTrader.Domain.Exceptions;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.Commands
{
    public class SellStockCommand : AsyncCommandBase
    {
        private readonly SellViewModel _viewModel;
        private readonly ISellStockService _sellStockService;
        private readonly IAccountStore _accountStore;

        public SellStockCommand(SellViewModel viewModel, ISellStockService sellStockService, IAccountStore accountStore)
        {
            _viewModel = viewModel;
            _sellStockService = sellStockService;
            _accountStore = accountStore;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            _viewModel.StatusMessage = string.Empty;
            _viewModel.ErrorMessage = string.Empty;

            try
            {
                string symbol = _viewModel.Symbol;
                int shares = _viewModel.SharesToSell;
                Account account = await _sellStockService.SellStock(_accountStore.CurrentAccount, symbol, shares);

                _accountStore.CurrentAccount = account;

                _viewModel.SearchResultSymbol = string.Empty;
                _viewModel.StatusMessage = $"Successfully sold {shares} shares of {symbol}.";
            }
            catch (InsufficientSharesException ex)
            {
                _viewModel.ErrorMessage = $"Account has insufficient shares. You only have {ex.AccountShares} shares.";
            }
            catch (InvalidSymbolException)
            {
                _viewModel.ErrorMessage = "Symbol does not exist.";
            }
            catch (Exception)
            {
                _viewModel.ErrorMessage = "Transaction failed.";
            }
        }
    }
}
