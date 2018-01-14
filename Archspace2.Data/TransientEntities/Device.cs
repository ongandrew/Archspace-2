using Newtonsoft.Json;

namespace Archspace2
{
    public class Device : ShipComponent
    {
        public Device() : base(ComponentCategory.Device)
        {
        }

        [JsonProperty("MinimumClass")]
        public int MinimumClass { get; set; }
        [JsonProperty("MaximumClass")]
        public int MaximumClass { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
