using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Threading.Tasks;
using Abstractions.Models;
using Abstractions.Services;
using MemoryCacheHelper;

namespace MainApplication
{
    public class CalculatorApplication
    {
        private readonly IStarShipService _starshipService;
        private readonly IHoursSuppliesLastService _suppliesService;
        private readonly IConsolePrintService _printService;
        private readonly IInputValidationService _inputValidationService;

        private const string STARSHIP_LIST_KEY = "shipList";
        public const string STOP_COMMAND = "stop";

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

        public void Run()
        {
            string line = "start";

            while (line.Trim() != STOP_COMMAND)
            {
                LoopLogic(line);
            }
        }

        private void LoopLogic(string line)
        {
            long distance;
            _printService.PrintMessage("Enter distance in MGLT to calculate number of jumps for each ship.");
            line = Console.ReadLine();

            if (line.Trim() != STOP_COMMAND && _inputValidationService.ValidateInput(line, out distance))
            {
                var starShipDetailsList = GetShipList();
                starShipDetailsList.Wait();
                _printService.PrintMessage($"Records returned: {starShipDetailsList.Result.Count()}");

                foreach (var ship in starShipDetailsList.Result)
                {
                    _suppliesService.FillSuppliesHoursForShip(ship);
                }

                _printService.PrintNumberOfJumpsForShips(starShipDetailsList.Result, distance);
            }
        }

        private async Task<List<IShipDetailsModel>> GetShipList()
        {
            if (SharedMemoryCache.Instance.HasKey(STARSHIP_LIST_KEY))
            {
                _printService.PrintMessage("Retriving cached list");
                return SharedMemoryCache.Instance.Get<List<IShipDetailsModel>>(STARSHIP_LIST_KEY);
            }
            else
            {
                _printService.PrintMessage("Calling API");
                var output = await _starshipService.GetAllShipsInfoList();

                SharedMemoryCache.Instance.Set(STARSHIP_LIST_KEY, output);

                return output;
            }
        }
    }
}
