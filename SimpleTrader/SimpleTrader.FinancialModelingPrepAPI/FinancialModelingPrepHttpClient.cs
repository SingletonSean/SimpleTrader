using Newtonsoft.Json;
using SimpleTrader.FinancialModelingPrepAPI.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.FinancialModelingPrepAPI
{
    public class FinancialModelingPrepHttpClient
    {
        private readonly HttpClient _client;
        private readonly string _apiKey;

        public FinancialModelingPrepHttpClient(HttpClient client, FinancialModelingPrepAPIKey apiKey)
        {
            _client = client;
            _apiKey = apiKey.Key;
        }

        public async Task<T> GetAsync<T>(string uri)
        {
            HttpResponseMessage response = await _client.GetAsync($"{uri}?apikey={_apiKey}");
            string jsonResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }
    }
}
