using Abstractions.Models;
using Abstractions.Services;
using System;

namespace Services.Services
{
    public class HoursSuppliesLastService : IHoursSuppliesLastService
    {
        private readonly int _hoursInDay = 24;
        private readonly int _hoursInWeek;
        private readonly int _hoursInMonth;
        private readonly int _hoursInYear;

        public HoursSuppliesLastService()
        {
            _hoursInWeek = _hoursInDay * 7;
            _hoursInMonth = _hoursInDay * 30;
            _hoursInYear = _hoursInMonth * 12;
        }
        public void FillSuppliesHoursForShip(IShipDetailsModel shipDetails)
        {
            var suppliesValues = shipDetails.Supplies.Split(' ');

            int suppliesTime;
            if (suppliesValues.Length != 2 || !Int32.TryParse(suppliesValues[0], out suppliesTime))
            {
                shipDetails.HoursSuppliesLastFor = 0;
                return;
            }

            switch (suppliesValues[1])
            {
                case "hour":
                case "hours":                    
                    break;
                case "day":
                case "days":
                    suppliesTime *= _hoursInDay;
                    break;
                case "week":
                case "weeks":
                    suppliesTime *= _hoursInWeek;
                    break;
                case "month":
                case "months":
                    suppliesTime *= _hoursInMonth;
                    break;
                case "year":
                case "years":
                    suppliesTime *= _hoursInYear;
                    break;
            }

            shipDetails.HoursSuppliesLastFor = suppliesTime;
        }
    }
}
