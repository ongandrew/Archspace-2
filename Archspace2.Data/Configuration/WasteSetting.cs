using Newtonsoft.Json;

namespace Archspace2
{
    public struct WasteSetting
    {
        [JsonProperty("WasteFreePlanetCount")]
        public int WasteFreePlanetCount { get; set; }
        [JsonProperty("WastePerPlanet")]
        public double WastePerPlanet { get; set; }
    }
}
