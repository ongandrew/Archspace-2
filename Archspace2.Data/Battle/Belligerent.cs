using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class Belligerent : Entity
    {
        [JsonProperty("Race")]
        [JsonConverter(typeof(StringEnumConverter))]
        public RaceType Race { get; set; }

        [JsonProperty("Fleets")]
        public List<BattleRecordFleet> Fleets { get; set; }

        [JsonProperty("FleetsLost")]
        public string FleetsLost { get; set; }
        [JsonProperty("AdmiralsLost")]
        public string AdmiralsLost { get; set; }

        public Belligerent()
        {
            Fleets = new List<BattleRecordFleet>();
        }
    }
}
