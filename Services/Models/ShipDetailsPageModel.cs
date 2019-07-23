using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Services.Models
{
    /// <summary>
    /// Model for result of API call to https://swapi.co/api/starships, represents single page of results
    /// </summary>
    [DataContract]
    public class ShipDetailsPageModel
    {
        /// <summary>
        /// Total number of results on all pages
        /// </summary>
        [DataMember(Name = "count")]
        public int Count { get; set; }

        /// <summary>
        /// URL to next page of results
        /// </summary>
        [DataMember(Name = "next")]
        public string Next { get; set; }

        /// <summary>
        /// List of ship details on current page
        /// </summary>
        [DataMember(Name = "results")]
        public List<ShipDetailsModel> Results { get; set; }
    }
}
