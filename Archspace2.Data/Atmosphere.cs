using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Archspace2
{
    public class Atmosphere
    {
        [JsonProperty("h2")]
        public int H2 { get; set; }
        [JsonProperty("cl2")]
        public int Cl2 { get; set; }
        [JsonProperty("co2")]
        public int CO2 { get; set; }
        [JsonProperty("o2")]
        public int O2 { get; set; }
        [JsonProperty("n2")]
        public int N2 { get; set; }
        [JsonProperty("ch4")]
        public int CH4 { get; set; }
    }
}
