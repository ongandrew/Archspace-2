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

        [JsonProperty("attackRating")]
        public int AttackRating { get; set; }
        [JsonProperty("defenseRating")]
        public int DefenseRating { get; set; }
    }
}
