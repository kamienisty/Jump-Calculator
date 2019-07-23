using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Services.Models
{
    [DataContract]
    public class ShipDetailsPageModel
    {
        [DataMember(Name = "count")]
        public int Count { get; set; }

        [DataMember(Name = "next")]
        public string Next { get; set; }

        [DataMember(Name = "results")]
        public List<ShipDetailsModel> Results { get; set; }
    }
}
