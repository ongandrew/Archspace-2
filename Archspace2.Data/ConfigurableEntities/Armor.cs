using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Archspace2
{
    public enum ArmorType
    {
        Normal,
        Bio,
        Reactive
    };

    public class Armor : ShipComponent
    {
        public Armor() : base(ComponentCategory.Armor)
        {
        }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ArmorType Type { get; set; }
        [JsonProperty("hpMultiplier")]
        public double HpMultiplier { get; set; }
        [JsonProperty("defenseRating")]
        public int DefenseRating { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
