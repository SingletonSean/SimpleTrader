using SimpleTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.WPF.State.Accounts
{
    public class AccountStore : IAccountStore
    {
        public Account CurrentAccount { get; set; }
    }
}
