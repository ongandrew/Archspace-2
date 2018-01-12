using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

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
