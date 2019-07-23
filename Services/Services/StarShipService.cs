using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Abstractions.Services;
using Services.Models;
using Abstractions.Models;

namespace Services.Services
{
    /// <summary>
    /// Service for retriving list of all avaliable ships from swapi.co
    /// </summary>
    public class StarShipService : IStarShipService
    {
        private readonly Uri _baseUrl = new Uri("https://swapi.co/api/");
        private readonly IAPICallerService _apiCaller;

        private readonly string shipsSubdirection = "starships";

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="apiCaller">Injected instance of IAPICallerService implementation</param>
        public StarShipService(IAPICallerService apiCaller)
        {
            _apiCaller = apiCaller;
        }

        /// <summary>
        /// Calls swapi.co to retrive list of all ships
        /// </summary>
        /// <returns>List of all ships in swapi database</returns>
        public async Task<List<IShipDetailsModel>> GetAllShipsInfoList()
        {
            var callUrl = new Uri(_baseUrl, shipsSubdirection);
            var callResult = await CallShipAPI(callUrl.ToString());

            return callResult.ToList<IShipDetailsModel>();
        }

        /// <summary>
        /// Makes a call to default starships URL and all following pages of result. It concatenates the returned ship details into single list
        /// </summary>
        /// <param name="apiURL">Address that the api service will call</param>
        /// <returns>List of ships returned from call and subsequent calls for all result pages</returns>
        private async Task<List<ShipDetailsModel>> CallShipAPI(string apiURL)
        {
            var response = await _apiCaller.CallAPI<ShipDetailsPageModel>(apiURL);

            var output = new List<ShipDetailsModel>();

            if(response.Results != null)
            {
                output.AddRange(response.Results);
            }

            if (!string.IsNullOrEmpty(response.Next))
            {
                var nextCall = await CallShipAPI(response.Next);
                output.AddRange(nextCall);
            }

            return output;
        }
    }
}
