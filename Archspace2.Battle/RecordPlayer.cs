using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2.Battle
{
    public class RecordPlayer : NamedEntity
    {
        [JsonProperty("Race")]
        [JsonConverter(typeof(StringEnumConverter))]
        public RaceType Race { get; set; }
        [JsonProperty("Fleets")]
        public List<RecordFleet> Fleets { get; set; }

        internal RecordPlayer()
        {
            Fleets = new List<RecordFleet>();
        }

        public RecordPlayer(int aId, string aName, RaceType aRace) : this()
        {
            Id = aId;
            Name = aName;
            Race = aRace;
        }

        public RecordPlayer(Player aPlayer) : this(aPlayer.Id, aPlayer.Name, (RaceType)aPlayer.Race.Id)
        {
        }
    }
}
