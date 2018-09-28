using Newtonsoft.Json;

namespace Archspace2
{
    public class Computer : ShipComponent
    {
        public Computer() : base(ComponentCategory.Computer)
        {
        }

        [JsonProperty("attackRating")]
        public int AttackRating { get; set; }
        [JsonProperty("defenseRating")]
        public int DefenseRating { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
