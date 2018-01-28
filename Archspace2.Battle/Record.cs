using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2.Battle
{
    public class Record
    {
        [JsonIgnore]
        public Battle Battle { get; set; }

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
        public RecordBattlefield Battlefield { get; set; }

        [JsonProperty("BattleOccurred")]
        public bool BattleOccurred { get; set; }

        [JsonProperty("Events")]
        public List<RecordEvent> Events { get; set; }

        internal Record()
        {
            Events = new List<RecordEvent>();
        }

        public Record(Battle aBattle, Player aAttacker, Player aDefender, BattleType aBattleType, Battlefield aBattlefield, Armada aAttackingFleets, Armada aDefendingFleets) : this()
        {
            Battle = aBattle;

            Attacker = new RecordPlayer(aAttacker);
            Defender = new RecordPlayer(aDefender);

            if (aBattlefield != null)
            {
                Battlefield = new RecordBattlefield(aBattlefield);
            }

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
                Attacker.Fleets.Add(new RecordFleet(fleet));
            }

            foreach (Fleet fleet in aDefendingFleets)
            {
                Defender.Fleets.Add(new RecordFleet(fleet));
            }
        }

        public void AddFireEvent(Fleet aFiringFleet, Fleet aTargetFleet, Turret aTurret, int aHitChance)
        {
            FireEvent fireEvent = new FireEvent(Battle.CurrentTurn)
            {
                FiringFleetId = aFiringFleet.Id,
                TargetFleetId = aTargetFleet.Id,
                Weapon = aTurret.Name,
                WeaponType = aTurret.Type,
                Quantity = aTurret.Number * aFiringFleet.ActiveShipCount
            };

            Events.Add(fireEvent);
        }

        public void AddHitEvent(Fleet aFiringFleet, Fleet aTargetFleet, int aHitCount, int aMissCount, int aTotalDamage, int aSunkenCount)
        {
            HitEvent hitEvent = new HitEvent(Battle.CurrentTurn)
            {
                FiringFleetId = aFiringFleet.Id,
                TargetFleetId = aTargetFleet.Id,
                TotalDamage = aTotalDamage,
                SunkCount = aSunkenCount
            };

            Events.Add(hitEvent);
        }

        public void AddMovementEvent(Fleet aFleet)
        {
            MovementEvent movementEvent = new MovementEvent(Battle.CurrentTurn)
            {
                FleetId = aFleet.Id,
                X = aFleet.X,
                Y = aFleet.Y,
                Direction = aFleet.Direction,
                Command = aFleet.Command,
                Status = aFleet.Status,
                Substatus = aFleet.Substatus,
                RemainingShips = aFleet.ActiveShipCount
            };

            Events.Add(movementEvent);
        }

        public void AddFleetDisabledEvent(Fleet aFleet)
        {
            FleetDisabledEvent fleetDisabledEvent = new FleetDisabledEvent(Battle.CurrentTurn)
            {
                DisabledFleetId = aFleet.Id
            };

            Events.Add(fleetDisabledEvent);
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
