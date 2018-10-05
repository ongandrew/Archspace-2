using Newtonsoft.Json;
using Universal.Common.Serialization;

namespace Archspace2
{
    public abstract class Entity : JsonSerializable<Entity>
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}
