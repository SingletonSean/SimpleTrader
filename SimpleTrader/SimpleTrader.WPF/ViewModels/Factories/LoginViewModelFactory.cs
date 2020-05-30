using SimpleTrader.WPF.State.Authenticators;
using SimpleTrader.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.ViewModels.Factories
{
    public class LoginViewModelFactory : ISimpleTraderViewModelFactory<LoginViewModel>
    {
        private readonly IAuthenticator _authenticator;
        private readonly IRenavigator _renavigator;

        public LoginViewModelFactory(IAuthenticator authenticator, IRenavigator renavigator)
        {
            _authenticator = authenticator;
            _renavigator = renavigator;
        }

        public LoginViewModel CreateViewModel()
        {
            return new LoginViewModel(_authenticator, _renavigator);
        }
    }
}
