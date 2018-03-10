using Archspace2.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Universal.Common.Extensions;

namespace Archspace2
{
    public enum FleetStatus
    {
        StandBy,
        UnderOperation,
        UnderMission,
        Deactivated,
        Privateer,
        CommanderAbsent
    };

    [Table("Fleet")]
    public class Fleet : UniverseEntity, IPowerContributor
    {
        public int Order { get; set; }

        public string Name { get; set; }
        public int? PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }
        
        public int? AdmiralId { get; set; }
        [ForeignKey("AdmiralId")]
        public Admiral Admiral { get; set; }

        public int ShipDesignId { get; set; }
        [ForeignKey("ShipDesignId")]
        public ShipDesign ShipDesign { get; set; }

        public int CurrentShipCount { get; set; }
        public int MaxShipCount { get; set; }

        private int mExperience;
        public int Experience { get => mExperience; set => 
                mExperience = value < 0 ? 0 : value > 100 ? 100 : value; }

        public double Upkeep
        {
            get
            {
                return CurrentShipCount * ShipDesign.ShipClass.Upkeep;
            }
        }

        public long Power
        {
            get
            {
                return ShipDesign.Power * CurrentShipCount;
            }
        }
        
        public FleetStatus Status { get; set; }
        public Mission Mission { get; set; }

        private Fleet()
        {
        }
        public Fleet(Universe aUniverse) : base(aUniverse)
        {
            Mission = new Mission()
            {
                Type = MissionType.None
            };
        }

        public string GetDisplayName()
        {
            return $"{Order.ToOrdinal()} {Name}";
        }

        public bool MissionIsOver()
        {
            switch (Mission.Type)
            {
                case MissionType.None:
                case MissionType.StationOnPlanet:
                    return false;
                case MissionType.Train:
                case MissionType.Patrol:
                case MissionType.DispatchToAlly:
                case MissionType.Sortie:
                case MissionType.Returning:
                case MissionType.ReturningWithPlanet:
                case MissionType.Expedition:
                case MissionType.OnRoute:
                    return Mission.TerminateTurn <= Universe.CurrentTurn;
                default:
                    throw new IndexOutOfRangeException("Invalid mission type.");
            }
        }
        
        public void TryFindPlanet()
        {
            if (Game.Random.Next(1, 100) <= Game.Configuration.Mission.ExpeditionChance[Math.Min(Player.Planets.Count, Game.Configuration.Mission.ExpeditionChance.Keys.Max())])
            {
                Planet planet = Player.Planets.Select(x => x.Cluster).Random().CreatePlanet().AsRandomPlanet();
                planet.Player = Player;
                Player.Planets.Add(planet);

                planet.StartTerraforming();
                Player.AddNews($"Your fleet {GetDisplayName()} has found a new planet!");

                BeginMission(MissionType.ReturningWithPlanet);
            }
            else
            {
                BeginMission(MissionType.Expedition);
            }
        }

        public void Return(int aTerminateTurn)
        {
            BeginMission(MissionType.Returning, aTerminateTurn);
        }
        public void ReturnFromPlayer()
        {
            int returnTime = (9 - ShipDesign.Engine.TechLevel) * 2;

            Player targetPlayer = Universe.Players.SingleOrDefault(x => x.Id == Mission.Target);

            if (targetPlayer != null && targetPlayer.Planets.Select(x => x.ClusterId).Distinct().Intersect(Player.Planets.Select(x => x.ClusterId).Distinct()).Any())
            {
                returnTime /= 2;
            }

            if (Player.Traits.Contains(RacialTrait.FastManeuver))
            {
                returnTime = returnTime * 70 / 100;
            }

            returnTime = Player.CalculateTotalEffect(returnTime, PlayerEffectType.ChangeFleetReturnTime);
            
            this.Return(Universe.CurrentTurn + returnTime);
        }
        
        public void UpdateTurn()
        {
            if (Status == FleetStatus.CommanderAbsent)
            {
                Status = FleetStatus.StandBy;
            }
            else if (Status == FleetStatus.StandBy)
            {
                if (Admiral.RacialAbility == AdmiralRacialAbility.BreederMale && Game.Random.Next(1, 50) == 1)
                {
                    Status = FleetStatus.CommanderAbsent;
                }
            }
            
            ExecuteMission();
        }

        public void BeginMission(MissionType aMissionType, int aTerminateTurn = 0)
        {
            int returnTurn = 0;

            if (aMissionType == MissionType.Privateer)
            {
                Status = FleetStatus.Privateer;
            }
            else if (aMissionType == MissionType.None)
            {
                Status = FleetStatus.StandBy;
            }
            else
            {
                Status = FleetStatus.UnderMission;
            }

            switch (aMissionType)
            {
                case MissionType.Train:
                    returnTurn = Universe.CurrentTurn + (Game.Configuration.Mission.TrainTime / Game.Configuration.SecondsPerTurn);
                    break;
                case MissionType.Patrol:
                    returnTurn = Universe.CurrentTurn + (Game.Configuration.Mission.PatrolTime / Game.Configuration.SecondsPerTurn);
                    break;
                case MissionType.DispatchToAlly:
                    returnTurn = Universe.CurrentTurn + (Game.Configuration.Mission.DispatchToAllyTime / Game.Configuration.SecondsPerTurn);
                    break;
                case MissionType.Expedition:
                    returnTurn = Universe.CurrentTurn + (Game.Configuration.Mission.ExpeditionTime / Game.Configuration.SecondsPerTurn);
                    break;
                case MissionType.Sortie:
                    returnTurn = Universe.CurrentTurn + 2;
                    break;
                case MissionType.ReturningWithPlanet:
                    returnTurn = Universe.CurrentTurn + (Game.Configuration.Mission.ReturningWithPlanetTime / Game.Configuration.SecondsPerTurn);
                    break;
                case MissionType.Privateer:
                    returnTurn = Universe.CurrentTurn + (Game.Configuration.Mission.PrivateerTime / Game.Configuration.SecondsPerTurn);
                    break;
                case MissionType.Returning:
                    returnTurn = aTerminateTurn;
                    break;
                case MissionType.None:
                case MissionType.StationOnPlanet:
                default:
                    break;
                
            }

            // Temporary workaround until EF Core supports replacement of owned entities.
            Mission.Reset();
            Mission.Type = aMissionType;
            Mission.TerminateTurn = returnTurn;
        }

        public void ExecuteMission()
        {
            switch (Mission.Type)
            {
                case MissionType.Privateer:
                    {
                        Player targetPlayer = Universe.Players.SingleOrDefault(x => x.Id == Mission.Target);

                        if (targetPlayer == null || targetPlayer.IsDead() || !targetPlayer.Planets.Any(x => x.CanPrivateer()))
                        {
                            Player.AddNews($"Your fleet {GetDisplayName()} is returning from a privateer mission as there are no valid targets.");
                            ReturnFromPlayer();
                        }
                        else
                        {
                            throw new NotImplementedException();
                        }

                        if (MissionIsOver())
                        {
                            ReturnFromPlayer();
                        }
                    }
                    break;
                case MissionType.Train:
                    {
                        if (MissionIsOver())
                        {
                            EndMission();
                        }
                    }
                    break;
                case MissionType.Patrol:
                    {
                        Admiral.GainExperience(Game.Configuration.Mission.PatrolExperience);

                        if (MissionIsOver())
                        {
                            EndMission();
                        }
                    }
                    break;
                case MissionType.StationOnPlanet:
                    {
                        Admiral.GainExperience(Game.Configuration.Mission.StationExperience);

                        if (MissionIsOver())
                        {
                            EndMission();
                        }
                    }
                    break;
                case MissionType.Expedition:
                    {
                        if (MissionIsOver())
                        {
                            TryFindPlanet();
                        }
                    }
                    break;
                case MissionType.ReturningWithPlanet:
                    {
                        if (MissionIsOver())
                        {
                            EndMission();
                        }
                    }
                    break;
                default:
                    {
                        if (MissionIsOver())
                        {
                            EndMission();
                        }
                    }
                    break;
            }
        }

        public void EndMission()
        {
            switch (Mission.Type)
            {
                case MissionType.Train:
                    {
                        int newExperience = 10 + Player.ControlModel.Military * 5;

                        if (newExperience < Game.Configuration.Mission.MinFleetTrainExperience)
                        {
                            newExperience = Game.Configuration.Mission.MinFleetTrainExperience;
                        }

                        Experience += newExperience;

                        int admiralExperience = ShipDesign.ShipClass.Class * CurrentShipCount * (Player.ControlModel.Military + 5);

                        if (admiralExperience < Game.Configuration.Mission.MinAdmiralTrainExperience)
                        {
                            admiralExperience = Game.Configuration.Mission.MinAdmiralTrainExperience;
                        }

                        Admiral.GainExperience(admiralExperience);

                        Player.AddNews($"Your fleet {GetDisplayName()} has gained {newExperience} points of experience and {Admiral.Name} has gained {admiralExperience} points of experience from the training.");
                    }
                    break;
                case MissionType.Patrol:
                    {
                        Player.AddNews($"Your fleet {GetDisplayName()} has returned from a patrol mission.");
                    }
                    break;
                case MissionType.ReturningWithPlanet:
                    {
                        Player.AddNews($"Your fleet {GetDisplayName()} has returned with a planet.");
                    }
                    break;
                default:
                    break;
            }

            if (Status != FleetStatus.Deactivated)
            {
                BeginMission(MissionType.None);
                Status = FleetStatus.StandBy;
            }
        }

        public void AbortMission()
        {
            BeginMission(MissionType.None);
            Status = FleetStatus.StandBy;
        }

        public bool CanBeRecalled()
        {
            return Mission.CanTerminateEarly();
        }

        public bool CanBeDisbanded()
        {
            if (Mission.Type != MissionType.None || Status != FleetStatus.StandBy)
            {
                return false;
            }

            return true;
        }

        public Battle.Fleet ToBattleFleet()
        {
            Battle.Fleet result = new Battle.Fleet(Id, Name, Player.ToBattlePlayer(), ShipDesign.ShipClass, ShipDesign.Armor, ShipDesign.Computer, ShipDesign.Engine, ShipDesign.Shield, ShipDesign.Devices, ShipDesign.Weapons, Admiral.ToBattleAdmiral(), CurrentShipCount, Power);

            return result;
        }
    }
}
