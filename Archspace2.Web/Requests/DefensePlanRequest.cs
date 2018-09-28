using Newtonsoft.Json;
using System.Collections.Generic;

namespace Archspace2.Web
{
    public class DefensePlanRequest
    {
        [JsonProperty("capitalFleetId")]
        public int CapitalFleetId { get; set; }
        [JsonProperty("fleets")]
        public List<int> Fleets { get; set; }
    }
}
