using Abstractions.Models;
using System.Collections.Generic;

namespace Abstractions.Services
{
    public interface IConsolePrintService
    {
        void PrintNumberOfJumpsForShips(List<IShipDetailsModel> shipList, long distance);
        void PrintMessage(string message);
    }
}
