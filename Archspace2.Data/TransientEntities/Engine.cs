using Newtonsoft.Json;
using System.Collections.Generic;

namespace Archspace2
{
    public class Engine : ShipComponent
    {
        public Engine() : base(ComponentCategory.Engine)
        {
            BattleSpeed = new Dictionary<int, int>();
            BattleMobility = new Dictionary<int, int>();
        }

        [JsonProperty("BattleSpeed")]
        public Dictionary<int, int> BattleSpeed { get; set; }
        [JsonProperty("BattleMobility")]
        public Dictionary<int, int> BattleMobility { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
