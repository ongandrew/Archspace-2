using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
