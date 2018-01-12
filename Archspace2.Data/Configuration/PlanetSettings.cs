using Newtonsoft.Json;
using System.Collections.Generic;

namespace Archspace2
{
    public class PlanetSettings
    {
        public PlanetSettings()
        {
            WasteSettings = new Dictionary<int, WasteSetting>();
        }

        [JsonProperty("UltraPoorMultiplier")]
        public int UltraPoorMultiplier { get; set; }
        [JsonProperty("PoorMultiplier")]
        public int PoorMultiplier { get; set; }
        [JsonProperty("NormalMultiplier")]
        public int NormalMultiplier { get; set; }
        [JsonProperty("RichMultiplier")]
        public int RichMultiplier { get; set; }
        [JsonProperty("UltraRichMultiplier")]
        public int UltraRichMultiplier { get; set; }

        [JsonProperty("WasteSettings")]
        public Dictionary<int, WasteSetting> WasteSettings { get; set; }

        public static PlanetSettings CreateDefault()
        {
            return new PlanetSettings()
            {
                UltraPoorMultiplier = 50,
                PoorMultiplier = 75,
                NormalMultiplier = 100,
                RichMultiplier = 175,
                UltraRichMultiplier = 250,
                WasteSettings = new Dictionary<int, WasteSetting>()
                {
                    [-5] = new WasteSetting() { WasteFreePlanetCount = 3, WastePerPlanet = 25 },
                    [-4] = new WasteSetting() { WasteFreePlanetCount = 3, WastePerPlanet = 12.5 },
                    [-3] = new WasteSetting() { WasteFreePlanetCount = 4, WastePerPlanet = 10 },
                    [-2] = new WasteSetting() { WasteFreePlanetCount = 4, WastePerPlanet = 8.3 },
                    [-1] = new WasteSetting() { WasteFreePlanetCount = 5, WastePerPlanet = 8.3 },
                    [0] = new WasteSetting() { WasteFreePlanetCount = 6, WastePerPlanet = 8.3 },
                    [1] = new WasteSetting() { WasteFreePlanetCount = 7, WastePerPlanet = 8.3 },
                    [2] = new WasteSetting() { WasteFreePlanetCount = 7, WastePerPlanet = 7.1 },
                    [3] = new WasteSetting() { WasteFreePlanetCount = 8, WastePerPlanet = 7.1 },
                    [4] = new WasteSetting() { WasteFreePlanetCount = 9, WastePerPlanet = 5.5 },
                    [5] = new WasteSetting() { WasteFreePlanetCount = 10, WastePerPlanet = 4.1 },
                    [6] = new WasteSetting() { WasteFreePlanetCount = 12, WastePerPlanet = 3.8 },
                    [7] = new WasteSetting() { WasteFreePlanetCount = 13, WastePerPlanet = 3.3 },
                    [8] = new WasteSetting() { WasteFreePlanetCount = 15, WastePerPlanet = 3.1 },
                    [9] = new WasteSetting() { WasteFreePlanetCount = 17, WastePerPlanet = 2.7 },
                    [10] = new WasteSetting() { WasteFreePlanetCount = 20, WastePerPlanet = 2.5 },
                }
            };
        }
    }
}
