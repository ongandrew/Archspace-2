using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Archspace2
{
    public class Atmosphere
    {
        [JsonProperty("H2")]
        public int H2 { get; set; }
        [JsonProperty("Cl2")]
        public int Cl2 { get; set; }
        [JsonProperty("CO2")]
        public int CO2 { get; set; }
        [JsonProperty("O2")]
        public int O2 { get; set; }
        [JsonProperty("N2")]
        public int N2 { get; set; }
        [JsonProperty("CH4")]
        public int CH4 { get; set; }
        [JsonProperty("H2O")]
        public int H2O { get; set; }
    }
}
