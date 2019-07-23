using Abstractions.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Abstractions.Services
{
    /// <summary>
    /// Interface for classes that are able to get list of ship details
    /// </summary>
    public interface IStarShipService
    {
        /// <summary>
        /// Method able to get ship details
        /// </summary>
        /// <returns></returns>
        Task<List<IShipDetailsModel>> GetAllShipsInfoList();
    }
}
