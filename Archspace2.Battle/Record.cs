using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2.Battle
{
    public class Record
    {
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Attacker")]
        public RecordPlayer Attacker { get; set; }
        [JsonProperty("Defender")]
        public RecordPlayer Defender { get; set; }

        [JsonProperty("DateTime")]
        public DateTime DateTime { get; set; }

        [JsonProperty("BattleType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BattleType BattleType { get; set; }

        [JsonProperty("IsDraw")]
        public bool IsDraw { get; set; }

        [JsonProperty("Battlefield")]
        public Battlefield Battlefield { get; set; }

        [JsonProperty("BattleOccurred")]
        public bool BattleOccurred { get; set; }

        [JsonProperty("Events")]
        public List<RecordEvent> Events { get; set; }

        public Record()
        {
            Attacker = new RecordPlayer();
            Defender = new RecordPlayer();
            Battlefield = new Battlefield();

            Events = new List<RecordEvent>();
        }

        public Record(Player aAttacker, Player aDefender, BattleType aBattleType, Battlefield aBattlefield, List<Fleet> aAttackingFleets, List<Fleet> aDefendingFleets) : this()
        {
            Attacker.Id = aAttacker.Id;
            Defender.Id = aDefender.Id;
            Attacker.Name = aAttacker.Name;
            Defender.Name = aDefender.Name;
            Attacker.Race = (RaceType)aAttacker.Race.Id;
            Defender.Race = (RaceType)aDefender.Race.Id;

            DateTime = DateTime.UtcNow;
            BattleType = aBattleType;
            IsDraw = false;

            switch (BattleType)
            {
                case BattleType.Siege:
                case BattleType.Privateer:
                case BattleType.Raid:
                case BattleType.Blockade:
                case BattleType.Magistrate:
                case BattleType.MagistrateCounterattack:
                case BattleType.EmpirePlanet:
                case BattleType.EmpirePlanetCounterattack:
                    {
                        Battlefield.Id = aBattlefield.Id;
                        Battlefield.Name = aBattlefield.Name;
                    }
                    break;
                case BattleType.Fortress:
                    {
                        Battlefield.Id = 0;
                        Battlefield.Name = "Empire Fortress";
                    }
                    break;
                case BattleType.EmpireCapitalPlanet:
                    {
                        Battlefield.Id = 0;
                        Battlefield.Name = "Empire Capital Planet";
                    }
                    break;
                default:
                    break;
            }

            BattleOccurred = false;

            foreach (Fleet fleet in aAttackingFleets)
            {
                Attacker.Fleets.Add(new RecordFleet());
            }

            foreach (Fleet fleet in aDefendingFleets)
            {
                Defender.Fleets.Add(new RecordFleet());
            }
        }
    }
}
