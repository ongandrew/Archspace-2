using Newtonsoft.Json;
using System.Collections.Generic;

namespace Archspace2
{
    public class ShipClass : NamedEntity, IPlayerUnlockable
    {
        public ShipClass()
        {
            Prerequisites = new List<PlayerPrerequisite>();
        }

        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("class")]
        public int Class { get; set; }
        [JsonProperty("baseHp")]
        public int BaseHp { get; set; }
        [JsonProperty("space")]
        public int Space { get; set; }
        [JsonProperty("weaponSlotCount")]
        public int WeaponSlotCount { get; set; }
        [JsonProperty("deviceSlotCount")]
        public int DeviceSlotCount { get; set; }
        [JsonProperty("cost")]
        public int Cost { get; set; }
        [JsonProperty("upkeep")]
        public double Upkeep { get; set; }

        [JsonProperty("Prerequisites")]
        public List<PlayerPrerequisite> Prerequisites { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
