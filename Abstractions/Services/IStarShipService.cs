using Abstractions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstractions.Services
{
    public interface IStarShipService
    {
        Task<List<IShipDetailsModel>> GetAllShipsInfoList();
    }
}
