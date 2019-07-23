namespace Abstractions.Models
{
    /// <summary>
    /// Interface for class able to represent single ship's details
    /// </summary>
    public interface IShipDetailsModel
    {
        /// <summary>
        /// Ship's name
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Ship's speed in MGLT
        /// </summary>
        string Speed { get; set; }

        /// <summary>
        /// Numric representation of Speed field
        /// </summary>
        int NumericSpeed { get; }

        /// <summary>
        /// String representation of timespan that sip's supplies will last for
        /// </summary>
        string Supplies { get; set; }

        /// <summary>
        /// Representation of hours for which ship's supplies will slast for
        /// </summary>
        int HoursSuppliesLastFor { get; set; }

        /// <summary>
        /// Method albe to calculate number of jumps needed for ship to travel given distance
        /// </summary>
        /// <param name="distance">Distance in MGLT to make calulations for</param>
        /// <returns>Number of jumps to travel the distance</returns>
        string NumberOfJumpsForDistance(long distance);
    }
}
