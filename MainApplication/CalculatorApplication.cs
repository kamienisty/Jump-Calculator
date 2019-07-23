using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using Abstractions.Models;
using Abstractions.Services;
using CommonResources;
using MemoryCacheHelper;

namespace MainApplication
{
    /// <summary>
    /// Class for executing the application
    /// </summary>
    public class CalculatorApplication
    {
        private readonly IStarShipService _starshipService;
        private readonly IHoursSuppliesLastService _suppliesService;
        private readonly IConsolePrintService _printService;
        private readonly IInputValidationService _inputValidationService;

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="starShipService">Injected instance of IStarShipService implementation</param>
        /// <param name="supplyService">Injected instance of IHoursSuppliesLastService implementation</param>
        /// <param name="printService">Injected instance of IConsolePrintService implementation</param>
        /// <param name="validationService">Injected instance of IInputValidationService implementation</param>
        public CalculatorApplication(
            IStarShipService starShipService,
            IHoursSuppliesLastService supplyService,
            IConsolePrintService printService,
            IInputValidationService validationService)
        {
            _starshipService = starShipService;
            _suppliesService = supplyService;
            _printService = printService;
            _inputValidationService = validationService;

            var cacheItemPolicy = new CacheItemPolicy();
            cacheItemPolicy.SlidingExpiration = TimeSpan.FromMinutes(1);

            SharedMemoryCache.Instance.DefaultPolicy = cacheItemPolicy;
        }

        /// <summary>
        /// Method to begin application loop, checks if the stop command was issued
        /// </summary>
        public void Run()
        {
            string line = "start";

            while (line.Trim() != StringResources.STOP_COMMAND)
            {
                LoopLogic(line);
            }
        }

        /// <summary>
        /// Method with logic for making calculations for number of jumps needed to travel given distance. 
        /// Validates user input, makes call for all ship definitions and performs necessary calculations. 
        /// Finally outputs result in alfabetically ordered fashion
        /// </summary>
        /// <param name="line">User input</param>
        private void LoopLogic(string line)
        {
            long distance;
            _printService.PrintMessage(StringResources.INPUT_DISTANCE_MESSAGE);
            line = Console.ReadLine();

            if (line.Trim() != StringResources.STOP_COMMAND && _inputValidationService.ValidateDistance(line, out distance))
            {
                var starShipDetailsList = GetShipList();
                starShipDetailsList.Wait();

                if (starShipDetailsList.Result.Any())
                {
                    _printService.PrintMessage(String.Format(StringResources.RECORDS_RETURNED, starShipDetailsList.Result.Count()));

                    foreach (var ship in starShipDetailsList.Result)
                    {
                        _suppliesService.FillSuppliesHoursForShip(ship);
                    }

                    _printService.PrintNumberOfJumpsForShips(starShipDetailsList.Result, distance);
                }

            }
        }

        /// <summary>
        /// Method for retriving llist of avaliable ships. If list is cached it will retrive it from SharedMemoryCache, otherwise an API call is made
        /// </summary>
        /// <returns>List of ship details</returns>
        private async Task<List<IShipDetailsModel>> GetShipList()
        {
            if (SharedMemoryCache.Instance.HasKey(StringResources.STARSHIP_LIST_KEY))
            {
                _printService.PrintMessage(StringResources.RETRIVING_CACHED_LIST);
                return SharedMemoryCache.Instance.Get<List<IShipDetailsModel>>(StringResources.STARSHIP_LIST_KEY);
            }
            else
            {
                _printService.PrintMessage(StringResources.CALLING_API);

                try
                {
                    var output = await _starshipService.GetAllShipsInfoList();
                    SharedMemoryCache.Instance.Set(StringResources.STARSHIP_LIST_KEY, output);

                    return output;
                }
                catch (Exception)
                {
                    _printService.PrintMessage(StringResources.ERROR_OCCURED);
                    return new List<IShipDetailsModel>();
                }
            }
        }
    }
}
