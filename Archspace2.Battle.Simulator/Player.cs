using Newtonsoft.Json;
using Universal.Common.Serialization;

namespace Archspace2.Battle.Simulator
{
    public class Player : JsonSerializable<Player>
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Race")]
        public int Race { get; set; }
    }
}
