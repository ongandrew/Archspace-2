using Newtonsoft.Json;

namespace Archspace2
{
    public abstract class Entity
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
