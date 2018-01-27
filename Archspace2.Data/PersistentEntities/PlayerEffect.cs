using Archspace2.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Universal.Common.Extensions;

namespace Archspace2
{
    [Table("PlayerEffect")]
    public class PlayerEffectInstance : UniverseEntity, IPlayerEffect
    {
        public int PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }
        
        public int RemainingDuration { get; set; }
        public PlayerEffectSource SourceType { get; set; }

        public PlayerEffectType Type { get; set; }
        public ModifierType ModifierType { get; set; }

        public int Target { get; set; }
        public int Argument1 { get; set; }
        public int Argument2 { get; set; }
        public ControlModel ControlModelModifier { get; set; }

        public bool IsInstant { get; set; }

        public PlayerEffectInstance(Universe aUniverse) : base(aUniverse)
        {
            ControlModelModifier = new ControlModel();
        }

        public void Apply()
        {
            if (Player != null)
            {
                switch (Type)
                {
                    case PlayerEffectType.ChangePlanetResource:
                        {
                            Planet planet = Player.Planets.SingleOrDefault(x => x.Id == Target);

                            if (planet != null)
                            {
                                planet.Resource = planet.Resource.ModifyByInt(Argument1);
                            }
                        }
                        break;
                    case PlayerEffectType.SwitchPlanetOrder:
                        {
                            Planet planet1 = Player.Planets.SingleOrDefault(x => x.Id == Argument1);
                            Planet planet2 = Player.Planets.SingleOrDefault(x => x.Id == Argument2);
                            if (planet1 != null && planet2 != null)
                            {
                                int temp = planet1.Order;
                                planet1.Order = planet2.Order;
                                planet2.Order = temp;
                            }
                        }
                        break;
                    case PlayerEffectType.SetFleetMission:
                        {
                            Fleet fleet = Player.Fleets.SingleOrDefault(x => x.Id == Target && x.Status == FleetStatus.StandBy);
                            if (fleet != null)
                            {
                                throw new NotImplementedException();
                            }
                        }
                        break;
                    case PlayerEffectType.LosePlanet:
                        {
                            Planet planet = Player.Planets.SingleOrDefault(x => x.Id == Target);

                            if (planet != null && Player.Planets.Count > 1)
                            {
                                Player.Planets.Remove(planet);
                                planet.Player = null;
                                Universe.BlackMarket.AddListing(planet);
                            }
                        }
                        break;
                    case PlayerEffectType.DestroyAllDockedShip:
                        {
                            throw new NotImplementedException();
                        }
                        break;
                    case PlayerEffectType.WinAndGainPlanet:
                        {
                            throw new NotImplementedException();
                        }
                        break;
                    case PlayerEffectType.LoseTech:
                        {
                            Tech tech = Player.Techs.SingleOrDefault(x => x.Id == Target);

                            if (tech != null)
                            {
                                Player.Techs.Remove(tech);
                            }
                        }
                        break;
                    case PlayerEffectType.LoseProject:
                        {
                            Project project = Player.Projects.SingleOrDefault(x => x.Id == Target);

                            if (project != null)
                            {
                                Player.Projects.Remove(project);
                            }
                        }
                        break;
                    case PlayerEffectType.PlanetLostBuilding:
                        {
                            Planet planet = Player.Planets.SingleOrDefault(x => x.Id == Target);
                            if (planet != null)
                            {
                                throw new NotImplementedException();
                            }
                        }
                        break;
                    case PlayerEffectType.ChangeProduction:
                        {
                            int diffProduction = 0;
                            if (ModifierType == ModifierType.Absolute)
                            {
                                diffProduction = Argument1;
                            }
                            else
                            {
                                diffProduction = Player.Resource.ProductionPoint * (Argument1 / 100);
                            }

                            Player.Resource.ProductionPoint += diffProduction;
                        }
                        break;
                    case PlayerEffectType.ChangeAllCommanderAbility:
                        {
                            throw new NotImplementedException();
                        }
                        break;
                    case PlayerEffectType.GainTech:
                        {
                            Tech tech = Game.Configuration.Techs.SingleOrDefault(x => x.Id == Target);
                            if (tech != null && !Player.Techs.Contains(tech))
                            {
                                Player.Techs.Add(tech);
                            }
                        }
                        break;
                    case PlayerEffectType.GainFleet:
                        {
                            throw new NotImplementedException();
                        }
                        break;
                    case PlayerEffectType.GainProject:
                    case PlayerEffectType.GainSecretProject:
                        {
                            Project project = Game.Configuration.Projects.SingleOrDefault(x => x.Id == Target);

                            if (project != null && !Player.Projects.Contains(project))
                            {
                                Player.Projects.Add(project);
                            }
                        }
                        break;
                    case PlayerEffectType.ChangeEmpireRelation:
                        {
                            throw new NotImplementedException();
                        }
                        break;
                    case PlayerEffectType.CommanderLevelUp:
                        {
                            Admiral admiral = Player.Admirals.SingleOrDefault(x => x.Id == Target);

                            if (admiral != null)
                            {
                                admiral.GiveLevels(Target);
                            }
                        }
                        break;
                    case PlayerEffectType.GrantBoon:
                        {
                            throw new NotImplementedException();
                        }
                        break;
                    case PlayerEffectType.LoseCommander:
                        {
                            int lose;

                            if (ModifierType == ModifierType.Absolute)
                            {
                                lose = Math.Min(Argument1, Player.Admirals.Count);
                            }
                            else
                            {
                                lose = Player.Admirals.Count * (Argument1 / 100);
                            }

                            for (int i = 0; i < lose; i++)
                            {
                                Admiral admiral = Player.Admirals.Random();

                                if (admiral != null)
                                {
                                    Player.Admirals.Remove(admiral);
                                }
                            }
                        }
                        break;
                    case PlayerEffectType.ChangePlanetPopulation:
                        {
                            Planet planet = Player.Planets.SingleOrDefault(x => x.Id == Target);

                            if (planet != null)
                            {
                                int change = 0;

                                if (ModifierType == ModifierType.Absolute)
                                {
                                    change = Argument1 * 1000;
                                }
                                else
                                {
                                    change = planet.Population * (Argument1 / 100);
                                }

                                planet.Population += change;
                            } 
                        }
                        break;
                    case PlayerEffectType.LosePlanetGravityControl:
                        {
                            Planet planet = Player.Planets.SingleOrDefault(x => x.Id == Target);

                            if (planet != null && planet.Attributes.Where(x => x.Type == PlanetAttributeType.GravityControlled).Any())
                            {
                                planet.Attributes.RemoveAll(x => x.Type == PlanetAttributeType.GravityControlled);
                            }
                        }
                        break;
                    case PlayerEffectType.GainPlanetGravityControl:
                        {
                            Planet planet = Player.Planets.SingleOrDefault(x => x.Id == Target);

                            if (planet != null && !planet.Attributes.Where(x => x.Type == PlanetAttributeType.GravityControlled).Any())
                            {
                                planet.Attributes.Add(Game.Configuration.PlanetAttributes.Single(x => x.Type == PlanetAttributeType.GravityControlled));
                            }
                        }
                        break;
                    case PlayerEffectType.DamageFleet:
                        {
                            throw new NotImplementedException();
                        }
                        break;
                    case PlayerEffectType.ChangeHonor:
                        {
                            Player.Honor += Argument1;
                        }
                        break;
                    case PlayerEffectType.CouncilDeclareTotalWar:
                        {
                            throw new NotImplementedException();
                        }
                        break;
                    case PlayerEffectType.ChangeConcentrationMode:
                        {
                            int mode = Game.Random.Next(0, 3);

                            Player.ConcentrationMode = (ConcentrationMode)mode;
                        }
                        break;
                    case PlayerEffectType.KillCommanderAndDisbandFleet:
                        {
                            Fleet fleet = Player.Fleets.SingleOrDefault(x => x.Id == Target);

                            if (fleet != null)
                            {
                                Admiral admiral = fleet.Admiral;

                                admiral.Player = null;
                                Player.Admirals.Remove(admiral);

                                fleet.Player = null;
                                Player.Fleets.Remove(fleet);
                            }
                        }
                        break;
                    case PlayerEffectType.GainAbility:
                        {
                            if (!Player.Traits.Contains((RacialTrait)Argument1))
                            {
                                Player.Traits.Add((RacialTrait)Argument1);
                            }
                        }
                        break;
                    case PlayerEffectType.LoseAbility:
                        {
                            if (Player.Traits.Contains((RacialTrait)Argument1))
                            {
                                Player.Traits.Remove((RacialTrait)Argument1);
                            }
                        }
                        break;
                    case PlayerEffectType.GainCommander:
                        {
                            Admiral admiral = Player.CreateAdmiral().AsPlayerAdmiral(Player);
                            admiral.GiveLevels(Argument1);

                            Player.Admirals.Add(admiral);
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void UpdateTurn()
        {
            RemainingDuration--;
            if (RemainingDuration <= 0)
            {
                Player.Effects.Remove(this);
                Player = null;
            }
        }
    }
}
