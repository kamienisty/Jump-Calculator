using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Abstractions.Services;
using Services.Models;
using Abstractions.Models;

namespace Services.MainApplication
{
    public class StarShipService : IStarShipService
    {
        private readonly Uri _baseUrl = new Uri("https://swapi.co/api/");
        private readonly IAPICallerService _apiCaller;

        private const string API_SHIPS_SUBDIRECTION = "starships";

        public StarShipService(IAPICallerService apiCaller)
        {
            _apiCaller = apiCaller;
        }
        public async Task<List<IShipDetailsModel>> GetAllShipsInfoList()
        {
            var callUrl = new Uri(_baseUrl, API_SHIPS_SUBDIRECTION);
            var callResult = await CallShipAPI(callUrl.ToString(), new List<ShipDetailsModel>());

            return callResult.ToList<IShipDetailsModel>();
        }

        private async Task<List<ShipDetailsModel>> CallShipAPI(string apiURL, List<ShipDetailsModel> output = null)
        {
            var response = await _apiCaller.CallAPI<ShipDetailsPageModel>(apiURL);
            output.AddRange(response.Results);

            return !string.IsNullOrEmpty(response.Next) ? await CallShipAPI(response.Next, output) : output;
        }
    }
}
