using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Archspace2.Web
{
    public class ShipDesignForm
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("class")]
        public int Class { get; set; }

        [JsonProperty("armor")]
        public int Armor { get; set; }
        [JsonProperty("computer")]
        public int Computer { get; set; }
        [JsonProperty("engine")]
        public int Engine { get; set; }
        [JsonProperty("shield")]
        public int Shield { get; set; }

        [JsonProperty("weapons")]
        public List<int> Weapons { get; set; }
        [JsonProperty("devices")]
        public List<int> Devices { get; set; }
    }
}
