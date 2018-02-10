using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Archspace2.Web
{
    public class CreatePlayerForm
    {
        [JsonProperty("race")]
        [JsonConverter(typeof(StringEnumConverter))]
        public RaceType Race { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
