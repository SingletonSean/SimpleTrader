using SimpleTrader.FinancialModelingPrepAPI.Services;
using SimpleTrader.WPF.State.Navigators;
using SimpleTrader.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace SimpleTrader.WPF.Commands
{
    public class UpdateCurrentViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private INavigator _navigator;

        /// <summary>
        /// The following 3 steps show how to maintain the state of a view model 
        /// throughout the application lifetime.
        /// 
        /// When we switch views, the same PortfolioViewModel will be displayed.
        /// 
        /// This ensures that we can switch views without losing the state of 
        /// the PortfolioViewModel (such as TextBox fields, etc.).
        /// </summary>

        // STEP 1: Create a field to hold the state of the view model.
        private readonly PortfolioViewModel _portfolioViewModel;

        public UpdateCurrentViewModelCommand(INavigator navigator)
        {
            _navigator = navigator;

            // STEP 2: Set the field to a new instance of the PortfolioViewModel. This instance
            // will be used for the entire duration of the application.
            _portfolioViewModel = new PortfolioViewModel();
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter is ViewType)
            {
                ViewType viewType = (ViewType)parameter;

                switch(viewType)
                {
                    case ViewType.Home:
                        _navigator.CurrentViewModel = new HomeViewModel(MajorIndexViewModel.LoadMajorIndexViewModel(new MajorIndexService()));
                        break;
                    case ViewType.Portfolio:
                        // STEP 3: Now when we switch views, we set the CurrentViewModel to our stored PortfolioViewModel.
                        // The same instance of the PortfolioViewModel will be used every time we switch views. This 
                        // ensures that the PortfolioViewModel data is not lost when we switch views since we do not
                        // create a new PortfolioViewModel everytime we switch views.
                        _navigator.CurrentViewModel = _portfolioViewModel;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}