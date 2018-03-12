using Newtonsoft.Json;

namespace Archspace2
{
    public abstract class NamedEntity : Entity
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
