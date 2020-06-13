using SimpleTrader.WPF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.ViewModels
{
    public delegate TViewModel CreateViewModel<TViewModel>() where TViewModel : ViewModelBase;

    public class ViewModelBase : ObservableObject
    {
    }
}
