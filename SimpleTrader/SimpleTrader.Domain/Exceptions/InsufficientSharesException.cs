using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SimpleTrader.Domain.Exceptions
{
    public class InsufficientSharesException : Exception
    {
        public string Symbol { get; }
        public int AccountShares { get; }
        public int RequiredShares { get; }

        public InsufficientSharesException(string symbol, int accountShares, int requiredShares)
        {
            Symbol = symbol;
            AccountShares = accountShares;
            RequiredShares = requiredShares;
        }

        public InsufficientSharesException(string symbol, int accountShares, int requiredShares, string message) : base(message)
        {
            Symbol = symbol;
            AccountShares = accountShares;
            RequiredShares = requiredShares;
        }

        public InsufficientSharesException(string symbol, int accountShares, int requiredShares, string message, Exception innerException) : base(message, innerException)
        {
            Symbol = symbol;
            AccountShares = accountShares;
            RequiredShares = requiredShares;
        }
    }
}
