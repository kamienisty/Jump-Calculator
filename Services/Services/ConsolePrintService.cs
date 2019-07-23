using System.Linq;

using Abstractions.Models;
using Abstractions.Services;
using System;
using System.Collections.Generic;
using CommonResources;

namespace Services.Services
{
    /// <summary>
    /// Service for outputting messages from application to system console
    /// </summary>
    public class ConsolePrintService : IConsolePrintService
    {
        /// <summary>
        /// Method for printing message in a new console line
        /// </summary>
        /// <param name="message">Message to be printed</param>
        public void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }

        /// <summary>
        /// Method for printing summary of calculating the number of jumps needed to travel given distance
        /// </summary>
        /// <param name="shipList">List of all ships to print summary for</param>
        /// <param name="distance">Distance that the calculations were performed for</param>
        public void PrintNumberOfJumpsForShips(List<IShipDetailsModel> shipList, long distance)
        {
            shipList = shipList.OrderBy(x => x.Name).ToList();
            PrintMessage(String.Format(StringResources.NUMBER_OF_JUMPS_MESSAGE, distance));

            foreach (var ship in shipList)
            {
                PrintMessage(String.Format(StringResources.JUMPS_NEEDED_MESSAGE, ship.Name, ship.NumberOfJumpsForDistance(distance)));
            }
        }
    }
}
