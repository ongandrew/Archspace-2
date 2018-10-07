using Newtonsoft.Json;
using Universal.Common.Serialization;

namespace Archspace2.Battle.Simulator
{
    public class Admiral : JsonSerializable<Admiral>
    {
        [JsonProperty("Level")]
        public int Level { get; set; }
        [JsonProperty("SpecialAbility")]
        public int SpecialAbility { get; set; }
        [JsonProperty("RacialAbility")]
        public int RacialAbility { get; set; }
    }
}