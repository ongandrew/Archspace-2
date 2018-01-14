using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class ShipClass : Entity, IPlayerUnlockable
    {
        public ShipClass()
        {
            Prerequisites = new List<PlayerPrerequisite>();
        }

        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("Class")]
        public int Class { get; set; }
        [JsonProperty("BaseHp")]
        public int BaseHp { get; set; }
        [JsonProperty("Space")]
        public int Space { get; set; }
        [JsonProperty("WeaponSlotCount")]
        public int WeaponSlotCount { get; set; }
        [JsonProperty("DeviceSlotCount")]
        public int DeviceSlotCount { get; set; }
        [JsonProperty("Cost")]
        public int Cost { get; set; }
        [JsonProperty("Upkeep")]
        public int Upkeep { get; set; }

        [JsonProperty("Prerequisites")]
        public List<PlayerPrerequisite> Prerequisites { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
