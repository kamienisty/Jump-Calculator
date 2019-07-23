using Abstractions.Models;

namespace Abstractions.Services
{
    public interface IHoursSuppliesLastService
    {
        void FillSuppliesHoursForShip(IShipDetailsModel shipDetails);
    }
}
