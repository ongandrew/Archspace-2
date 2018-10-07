using Newtonsoft.Json;
using Universal.Common.Serialization;

namespace Archspace2.Battle.Simulator
{
    public class Deployment : JsonSerializable<Deployment>
    {
        [JsonProperty("Fleet")]
        public Fleet Fleet { get; set; }
        [JsonProperty("X")]
        public int X { get; set; }
        [JsonProperty("Y")]
        public int Y { get; set; }
        [JsonProperty("Direction")]
        public int Direction { get; set; }
        [JsonProperty("Command")]
        public int Command { get; set; }
        [JsonProperty("IsCapital")]
        public bool IsCapital { get; set; }
    }
}
