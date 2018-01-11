using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class Shield : ShipComponent
    {
        public Shield() : base(ComponentCategory.Shield)
        {
            RechargeRate = new Dictionary<int, int>();
            Strength = new Dictionary<int, int>();
        }

        [JsonProperty("Deflection")]
        public int Deflection { get; set; }

        [JsonProperty("RechargeRate")]
        public Dictionary<int, int> RechargeRate { get; set; }
        [JsonProperty("Strength")]
        public Dictionary<int, int> Strength { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
