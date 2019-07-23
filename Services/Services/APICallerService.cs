using Abstractions.Services;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Services.Services
{
    /// <summary>
    /// Generic class for calling API
    /// </summary>
    public class APICallerService : IAPICallerService
    {
        private readonly HttpClient _client;

        /// <summary>
        /// Default constructor with option to specify a HttpClient that will be used
        /// </summary>
        /// <param name="client">Optional paramether to specify client</param>
        public APICallerService(HttpClient client = null)
        {
            _client = client ?? new HttpClient();
        }

        /// <summary>
        /// Method to call API with given URL address and. Deserializes result as specified type
        /// </summary>
        /// <typeparam name="T">Type that API call result should be deserialized as</typeparam>
        /// <param name="url">URL to call</param>
        /// <returns>Result of call as a specified type</returns>
        public async Task<T> CallAPI<T>(string url)
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var data = await response.Content.ReadAsStringAsync();
            var pageModel = JsonConvert.DeserializeObject<T>(data);

            return pageModel;
        }
    }
}
