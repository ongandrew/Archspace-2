using Newtonsoft.Json;

namespace Archspace2
{
    public class Device : ShipComponent
    {
        public Device() : base(ComponentCategory.Device)
        {
        }

        [JsonProperty("minimumClass")]
        public int MinimumClass { get; set; }
        [JsonProperty("maximumClass")]
        public int MaximumClass { get; set; }
    }
}
