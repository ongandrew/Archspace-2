using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class BattleRecord
    {
        [JsonProperty("UniverseId")]
        public int UniverseId { get; set; }
        [JsonProperty("Id")]
        public int Id { get; set; }

        [JsonProperty("Attacker")]
        public Belligerent Attacker { get; set; }
        [JsonProperty("Defender")]
        public Belligerent Defender { get; set; }
        
        [JsonProperty("DateTime")]
        public DateTime DateTime { get; set; }

        [JsonProperty("BattleType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BattleType BattleType { get; set; }

        [JsonProperty("IsDraw")]
        public bool IsDraw { get; set; }

        [JsonProperty("Battlefield")]
        public Entity Battlefield { get; set; }

        [JsonProperty("BattleOccurred")]
        public bool BattleOccurred { get; set; }

        [JsonProperty("Events")]
        public List<BattleRecordEvent> Events { get; set; }

        public BattleRecord()
        {
            Attacker = new Belligerent();
            Defender = new Belligerent();
            Battlefield = new Entity();

            Events = new List<BattleRecordEvent>();
        }
        
        public BattleRecord(Player aAttacker, Player aDefender, BattleType aBattleType, Planet aBattlefield, List<BattleFleet> aAttackingFleets, List<BattleFleet> aDefendingFleets) : this()
        {
            UniverseId = aBattlefield.UniverseId;
            Attacker.Id = aAttacker.Id;
            Defender.Id = aDefender.Id;
            Attacker.Name = aAttacker.Name;
            Defender.Name = aDefender.Name;
            Attacker.Race = (RaceType)aAttacker.RaceId;
            Defender.Race = (RaceType)aDefender.RaceId;

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

            foreach (BattleFleet fleet in aAttackingFleets)
            {
                Attacker.Fleets.Add(new BattleRecordFleet(fleet));
            }

            foreach (BattleFleet fleet in aDefendingFleets)
            {
                Defender.Fleets.Add(new BattleRecordFleet(fleet));
            }
        }
    }
}
