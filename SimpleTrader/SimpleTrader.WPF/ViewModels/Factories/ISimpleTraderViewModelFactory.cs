using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public interface ISimpleTraderViewModelFactory<T> where T : ViewModelBase
    {
        T CreateViewModel();
    }
}
