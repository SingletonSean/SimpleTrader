using Newtonsoft.Json;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.FinancialModelingPrepAPI.Services
{
    public class MajorIndexService : IMajorIndexService
    {
        public async Task<MajorIndex> GetMajorIndex(MajorIndexType indexType)
        {
            using(HttpClient client = new HttpClient())
            {
                string uri = "https://financialmodelingprep.com//api/v3/majors-indexes/" + GetUriSuffix(indexType);

                HttpResponseMessage response = await client.GetAsync(uri);
                string jsonResponse = await response.Content.ReadAsStringAsync();

                MajorIndex majorIndex = JsonConvert.DeserializeObject<MajorIndex>(jsonResponse);
                majorIndex.Type = indexType;

                return majorIndex;
            }
        }

        private string GetUriSuffix(MajorIndexType indexType)
        {
            switch(indexType)
            {
                case MajorIndexType.DowJones:
                    return ".DJI";
                case MajorIndexType.Nasdaq:
                    return ".IXIC";
                case MajorIndexType.SP500:
                    return ".INX";
                default:
                    return ".DJI";
            }
        }
    }
}