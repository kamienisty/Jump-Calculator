using System.Linq;

using Abstractions.Models;
using Abstractions.Services;
using System;
using System.Collections.Generic;

namespace Services.Services
{
    public class ConsolePrintService : IConsolePrintService
    {
        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        public void PrintNumberOfJumpsForShips(List<IShipDetailsModel> shipList, long distance)
        {
            shipList = shipList.OrderBy(x => x.Name).ToList();
            PrintMessage($"Number of jumps to travel {distance} MGLT:\n");

            foreach (var ship in shipList)
            {
                PrintMessage($"{ship.Name} \nJumps needed: {ship.NumberOfJumpsForDistance(distance)}\n");
            }
        }
    }
}
