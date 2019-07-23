using Abstractions.Models;
using CommonResources;
using System;
using System.Runtime.Serialization;

namespace Services.Models
{
    /// <summary>
    /// Model for representing details of a single ship
    /// </summary>
    [DataContract]
    public class ShipDetailsModel : IShipDetailsModel
    {
        /// <summary>
        /// Ship's name
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; }

        /// <summary>
        /// Ship's speed in MGLT (mega light years per hour)
        /// </summary>
        [DataMember(Name = "MGLT")]
        public string Speed { get; set; }

        /// <summary>
        /// String representation of time that ship's supplies will last for
        /// </summary>
        [DataMember(Name = "consumables")]
        public string Supplies { get; set; }

        /// <summary>
        /// Int32 representation of Speed field
        /// </summary>
        public int NumericSpeed
        {
            get
            {
                int result;
                Int32.TryParse(Speed, out result);
                return result;
            }
        }

        /// <summary>
        /// Number of hours the ship's supplies will last for
        /// </summary>
        public int HoursSuppliesLastFor { get; set; }

        /// <summary>
        /// Method to calculate how many jumps are necessary to travel given distance
        /// </summary>
        /// <param name="distance">Distance to make calculation for</param>
        /// <returns>Number of jumps to travel the distance</returns>
        public string NumberOfJumpsForDistance(long distance)
        {
            if (NumericSpeed <= 0 || HoursSuppliesLastFor <= 0)
            {
                return StringResources.COULDNT_CALCULATE;
            }

            return (distance / (NumericSpeed * HoursSuppliesLastFor)).ToString();
        }
    }
}
