using Newtonsoft.Json;
using System.Collections.Generic;

namespace Archspace2
{
    public class Shield : ShipComponent
    {
        public Shield() : base(ComponentCategory.Shield)
        {
            RechargeRate = new Dictionary<int, int>();
            Strength = new Dictionary<int, int>();
        }

        [JsonProperty("deflection")]
        public int Deflection { get; set; }

        [JsonProperty("rechargeRate")]
        public Dictionary<int, int> RechargeRate { get; set; }
        [JsonProperty("strength")]
        public Dictionary<int, int> Strength { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
