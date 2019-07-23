using Abstractions.Models;
using System.Collections.Generic;

namespace Abstractions.Services
{
    /// <summary>
    /// Interface for classes able to print messages to console
    /// </summary>
    public interface IConsolePrintService
    {
        /// <summary>
        /// Method able to print summary for number of jumps needed to travel given distance
        /// </summary>
        /// <param name="shipList">List of ships that will be included in summary</param>
        /// <param name="distance">Distance that calculations were done for</param>
        void PrintNumberOfJumpsForShips(List<IShipDetailsModel> shipList, long distance);

        /// <summary>
        /// Method able to print message to console
        /// </summary>
        /// <param name="message">Message to print</param>
        void PrintMessage(string message);
    }
}
