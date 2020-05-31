using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleTrader.FinancialModelingPrepAPI
{
    public class FinancialModelingPrepHttpClientFactory
    {
        private readonly string _apiKey;

        public FinancialModelingPrepHttpClientFactory(string apiKey)
        {
            _apiKey = apiKey;
        }

        public FinancialModelingPrepHttpClient CreateHttpClient()
        {
            return new FinancialModelingPrepHttpClient(_apiKey);
        }
    }
}
