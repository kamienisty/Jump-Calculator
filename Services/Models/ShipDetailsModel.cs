using Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Services.Models
{
    [DataContract]
    public class ShipDetailsModel : IShipDetailsModel
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "MGLT")]
        public string Range { get; set; }

        [DataMember(Name = "consumables")]
        public string Supplies { get; set; }
        public int NumericRange
        {
            get
            {
                int result;
                Int32.TryParse(Range, out result);
                return result;
            }
        }
        public int HoursSuppliesLastFor { get; set; }

        public string NumberOfJumpsForDistance(long distance)
        {
            if (NumericRange == 0 || HoursSuppliesLastFor == 0)
            {
                return "couldn't calculate";
            }

            return Math.Floor((decimal)(distance / (NumericRange * HoursSuppliesLastFor))).ToString();
        }
    }
}
