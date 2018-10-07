using Newtonsoft.Json;
using Universal.Common.Serialization;

namespace Archspace2.Battle.Simulator
{
    public class Battlefield : JsonSerializable<Battlefield>
    {
        [JsonProperty("Name")]
        public string Name { get; set; }
    }
}
