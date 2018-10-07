using Newtonsoft.Json;
using System.Collections.Generic;
using Universal.Common.Serialization;

namespace Archspace2.Battle.Simulator
{
    public class Design : JsonSerializable<Design>
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("ShipClass")]
        public int ShipClass { get; set; }

        [JsonProperty("Armor")]
        public int Armor { get; set; }
        [JsonProperty("Computer")]
        public int Computer { get; set; }
        [JsonProperty("Engine")]
        public int Engine { get; set; }
        [JsonProperty("Shield")]
        public int Shield { get; set; }

        [JsonProperty("Weapons")]
        public List<int> Weapons { get; set; }
        [JsonProperty("Devices")]
        public List<int> Devices { get; set; }

        public Design()
        {
            Weapons = new List<int>();
            Devices = new List<int>();
        }
    }
}
