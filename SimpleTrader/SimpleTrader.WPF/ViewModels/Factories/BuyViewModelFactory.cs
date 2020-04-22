using SimpleTrader.Domain.Services;
using SimpleTrader.Domain.Services.TransactionServices;
using SimpleTrader.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class BuyViewModelFactory : INavigationSimpleTraderViewModelFactory<BuyViewModel>
    {
        private readonly IStockPriceService _stockPriceService;
        private readonly IBuyStockService _buyStockService;

        public BuyViewModelFactory(IStockPriceService stockPriceService, IBuyStockService buyStockService)
        {
            _stockPriceService = stockPriceService;
            _buyStockService = buyStockService;
        }

        public BuyViewModel CreateViewModel(INavigator navigator)
        {
            return new BuyViewModel(_stockPriceService, _buyStockService, navigator);
        }
    }
}
