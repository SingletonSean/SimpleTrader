using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.WPF.State.Accounts;
using SimpleTrader.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    public class BuyStockCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private readonly BuyViewModel _buyViewModel;
        private readonly IBuyStockService _buyStockService;
        private readonly IAccountStore _accountStore;

        public BuyStockCommand(BuyViewModel buyViewModel, IBuyStockService buyStockService, IAccountStore accountStore)
        {
            _buyViewModel = buyViewModel;
            _buyStockService = buyStockService;
            _accountStore = accountStore;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public async void Execute(object parameter)
        {
            try
            {
                Account account = await _buyStockService.BuyStock(_accountStore.CurrentAccount, _buyViewModel.Symbol, _buyViewModel.SharesToBuy);

                _accountStore.CurrentAccount = account;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
