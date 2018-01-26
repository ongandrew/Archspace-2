using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class Computer : ShipComponent
    {
        public Computer() : base(ComponentCategory.Computer)
        {
        }

        [JsonProperty("AttackRating")]
        public int AttackRating { get; set; }
        [JsonProperty("DefenseRating")]
        public int DefenseRating { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
