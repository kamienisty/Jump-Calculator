using Abstractions.Models;

namespace Abstractions.Services
{
    /// <summary>
    /// Interface for classes that will be able to fill HoursSuppliesLastFor in ship details
    /// </summary>
    public interface IHoursSuppliesLastService
    {
        /// <summary>
        /// Method able to calculate and fill HoursSuppliesLastFor
        /// </summary>
        /// <param name="shipDetails">Ship details to make calcualtions for</param>
        void FillSuppliesHoursForShip(IShipDetailsModel shipDetails);
    }
}
