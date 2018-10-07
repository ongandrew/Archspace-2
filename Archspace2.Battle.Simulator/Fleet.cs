using Newtonsoft.Json;
using Universal.Common.Serialization;

namespace Archspace2.Battle.Simulator
{
    public class Fleet : JsonSerializable<Fleet>
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Admiral")]
        public Admiral Admiral { get; set; }
        [JsonProperty("Design")]
        public Design Design { get; set; }
        [JsonProperty("ShipCount")]
        public int ShipCount { get; set; }
    }
}
