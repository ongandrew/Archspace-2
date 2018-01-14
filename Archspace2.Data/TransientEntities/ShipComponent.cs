using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Archspace2
{
    public enum ComponentCategory
    {
        Armor,
        Computer,
        Shield,
        Engine,
        Device,
        Weapon
    }

    public abstract class ShipComponent : Entity, IPlayerUnlockable
    {
        public ShipComponent(ComponentCategory aComponentCategory) : base()
        {
            Category = aComponentCategory;
            Prerequisites = new List<PlayerPrerequisite>();
            Effects = new List<FleetEffect>();
        }

        [JsonProperty("Category")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ComponentCategory Category { get; private set; }

        [JsonProperty("TechLevel")]
        public int TechLevel { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Prerequisites")]
        public List<PlayerPrerequisite> Prerequisites { get; set; }
        [JsonProperty("Effects")]
        public List<FleetEffect> Effects { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
