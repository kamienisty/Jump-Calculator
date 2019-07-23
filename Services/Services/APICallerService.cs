using Abstractions.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class APICallerService : IAPICallerService
    {
        private readonly HttpClient _client = new HttpClient();
        public async Task<T> CallAPI<T>(string url)
        {
            var response = await _client.GetAsync(url);

            var data = await response.Content.ReadAsStringAsync();
            var pageModel = JsonConvert.DeserializeObject<T>(data);

            return pageModel;
        }
    }
}
