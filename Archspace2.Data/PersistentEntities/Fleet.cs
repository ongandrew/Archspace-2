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
    public class Fleet : UniverseEntity
    {
        public string Name { get; set; }
        public int? PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }

        public int AdmiralId { get; set; }
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

        public int Power
        {
            get
            {
                return ShipDesign.Power * CurrentShipCount;
            }
        }
        
        public FleetStatus Status { get; set; }
        public Mission Mission { get; set; }

        public Fleet(Universe aUniverse) : base(aUniverse)
        {
            Mission = new Mission()
            {
                Type = MissionType.None
            };
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
                Player.AddNews($"Your fleet {Name} has found a new planet!");

                BeginMission(MissionType.ReturningWithPlanet);
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

            Mission = new Mission() { Type = aMissionType, TerminateTurn = returnTurn };
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
                            Player.AddNews($"{Name} fleet if returning from a privateer mission as there are no valid targets.");
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
                default:
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

                        Player.AddNews($"Your fleet {Name} has gained {newExperience} points of experience and {Admiral.Name} has gained {admiralExperience} points of experience from the training.");
                    }
                    break;
                case MissionType.Patrol:
                    {
                        Player.AddNews($"Your fleet {Name} has returned from a patrol mission.");
                    }
                    break;
                default:
                    break;
            }

            if (Status != FleetStatus.Deactivated)
            {
                BeginMission(MissionType.None);
            }
        }

        public Battle.Fleet ToBattleFleet()
        {
            Battle.Fleet result = new Battle.Fleet();

            Player.ToBattlePlayer();
            result.Power = Power;

            result.MaxHP = (int)(ShipDesign.ShipClass.BaseHp * ShipDesign.Armor.HpMultiplier);

            result.MaxShipCount = CurrentShipCount;

            result.MoraleModifier = Player.Race.BaseFleetEffects.Where(x => x.Type == FleetEffectType.MoraleModifier).CalculateTotalEffect(result.MoraleModifier, x => x.Amount.Value);
            result.BerserkModifier = Player.Race.BaseFleetEffects.Where(x => x.Type == FleetEffectType.BerserkModifier).CalculateTotalEffect(result.BerserkModifier, x => x.Amount.Value);

            if (Player.Traits.Any(x => x == RacialTrait.Psi))
            {
                result.Attributes.Add(FleetAttribute.PsiRace);
            }
            if (Player.Traits.Any(x => x == RacialTrait.EnhancedPsi))
            {
                result.Attributes.Add(FleetAttribute.EnhancedPsi);
            }
            if (Player.Traits.Any(x => x == RacialTrait.FastManeuver))
            {
                result.Attributes.Add(FleetAttribute.EnhancedMobility);
            }

            if (result.Attributes.Contains(FleetAttribute.EnhancedMobility))
            {
                result.StaticEffects.Add(new FleetEffect()
                {
                    Type = FleetEffectType.Speed,
                    Amount = 30,
                    ModifierType = ModifierType.Proportional
                });
            }

            switch (Admiral.SpecialAbility)
            {
                case AdmiralSpecialAbility.EngineeringSpecialist:
                    {
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.ArmorDefenseRating,
                            Amount = Admiral.Level * 2,
                            ModifierType = ModifierType.Proportional
                        });
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.RepairSpeed,
                            Amount = Admiral.Level * 2,
                            ModifierType = ModifierType.Proportional
                        });
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.ImpenetrableArmor,
                            Amount = Admiral.Level * 2,
                            ModifierType = ModifierType.Proportional
                        });

                        if (Admiral.Level <= 12)
                        {
                            result.StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.Repair,
                                Amount = 1,
                                ModifierType = ModifierType.Proportional,
                                Period = 10
                            });
                        }
                        else if (Admiral.Level <= 19)
                        {
                            result.StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.Repair,
                                Amount = 2,
                                ModifierType = ModifierType.Proportional,
                                Period = 10
                            });
                        }
                        else
                        {
                            result.StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.Repair,
                                Amount = 3,
                                ModifierType = ModifierType.Proportional,
                                Period = 10
                            });
                        }
                    }
                    break;
                case AdmiralSpecialAbility.ShieldSystemSpecialist:
                    {
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.ShieldStrength,
                            Amount = 10 + (Admiral.Level * 2),
                            ModifierType = ModifierType.Proportional
                        });
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.ShieldRechargeRate,
                            Amount = 10 + (Admiral.Level * 2),
                            ModifierType = ModifierType.Proportional
                        });
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.ImpenetrableShield,
                            Amount = 10 + (Admiral.Level * 2),
                            ModifierType = ModifierType.Absolute
                        });
                        if (Admiral.Level <= 12)
                        {
                            result.StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.ShieldSolidity,
                                Amount = 2,
                                ModifierType = ModifierType.Absolute
                            });
                        }
                        else if (Admiral.Level <= 19)
                        {
                            result.StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.ShieldSolidity,
                                Amount = 3,
                                ModifierType = ModifierType.Absolute
                            });
                        }
                        else
                        {
                            result.StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.ShieldSolidity,
                                Amount = 5,
                                ModifierType = ModifierType.Absolute
                            });
                        }
                    }
                    break;
                case AdmiralSpecialAbility.MissileSpecialist:
                    {
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.MissileAttackRating,
                            Amount = (Admiral.Level * 2),
                            ModifierType = ModifierType.Proportional
                        });
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.MissileDamage,
                            Amount = (Admiral.Level * 2),
                            ModifierType = ModifierType.Proportional
                        });
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.MissileCoolingTime,
                            Amount = -(Admiral.Level * 2),
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralSpecialAbility.EnergySystemSpecialist:
                    {
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.BeamAttackRating,
                            Amount = (Admiral.Level * 2),
                            ModifierType = ModifierType.Proportional
                        });
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.BeamDamage,
                            Amount = (Admiral.Level * 2),
                            ModifierType = ModifierType.Proportional
                        });
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.BeamCoolingTime,
                            Amount = -(Admiral.Level * 2),
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralSpecialAbility.BallisticExpert:
                    {
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.ProjectileAttackRating,
                            Amount = (Admiral.Level * 2),
                            ModifierType = ModifierType.Proportional
                        });
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.ProjectileDamage,
                            Amount = (Admiral.Level * 2),
                            ModifierType = ModifierType.Proportional
                        });
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.ProjectileCoolingTime,
                            Amount = -(Admiral.Level * 2),
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                default:
                    break;
            }

            switch (Admiral.RacialAbility)
            {
                case AdmiralRacialAbility.IrrationalTactics:
                    {
                        if (Admiral.Level <= 5)
                        {
                            result.StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.DefenseRating,
                                Amount = 10,
                                ModifierType = ModifierType.Proportional
                            });
                        }
                        else if (Admiral.Level <= 10)
                        {
                            result.StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.DefenseRating,
                                Amount = 15,
                                ModifierType = ModifierType.Proportional
                            });
                        }
                        else if (Admiral.Level <= 15)
                        {
                            result.StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.DefenseRating,
                                Amount = 20,
                                ModifierType = ModifierType.Proportional
                            });
                        }
                        else if (Admiral.Level <= 19)
                        {
                            result.StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.DefenseRating,
                                Amount = 25,
                                ModifierType = ModifierType.Proportional
                            });
                        }
                        else
                        {
                            result.StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.DefenseRating,
                                Amount = 30,
                                ModifierType = ModifierType.Proportional
                            });
                        }

                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.ArmorDefenseRating,
                            Amount = Admiral.Level,
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralRacialAbility.Intuition:
                    {
                        if (Admiral.Level >= 5)
                        {
                            result.Attributes.Add(FleetAttribute.WeakCloakingDetection);
                        }

                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.CriticalHit,
                            Amount = 5 + (Admiral.Level / 2),
                            ModifierType = ModifierType.Absolute
                        });
                    }
                    break;
                case AdmiralRacialAbility.LoneWolf:
                    {
                        result.BerserkModifier += 5;
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.Mobility,
                            Amount = 5 + Admiral.Level,
                            ModifierType = ModifierType.Proportional
                        });
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.Stealth,
                            Amount = Admiral.Level,
                            ModifierType = ModifierType.Proportional
                        });
                        if (Admiral.Level >= 10)
                        {
                            result.Attributes.Add(FleetAttribute.WeakCloaking);
                        }
                    }
                    break;
                case AdmiralRacialAbility.DnaPoisonReplicater:
                    {
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.ProjectileDamage,
                            Amount = Admiral.Level,
                            ModifierType = ModifierType.Proportional
                        });
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.MissileDamage,
                            Amount = Admiral.Level,
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralRacialAbility.BreederMale:
                    {
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.RepairSpeed,
                            Amount = Admiral.Level + 20,
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralRacialAbility.ClonalDouble:
                    {
                    }
                    break;
                case AdmiralRacialAbility.XenophobicFanatic:
                    {
                        result.BerserkModifier += 5;
                        result.MoraleModifier -= 5;
                        if (Admiral.Level <= 5)
                        {
                            result.StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.PsiDefense,
                                Amount = -5,
                                ModifierType = ModifierType.Proportional
                            });
                        }
                        else if (Admiral.Level <= 15)
                        {
                            result.StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.PsiDefense,
                                Amount = -10,
                                ModifierType = ModifierType.Proportional
                            });
                        }
                        else if (Admiral.Level <= 19)
                        {
                            result.StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.PsiDefense,
                                Amount = -15,
                                ModifierType = ModifierType.Proportional
                            });
                        }
                        else
                        {
                            result.StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.PsiDefense,
                                Amount = -20,
                                ModifierType = ModifierType.Proportional
                            });
                        }

                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.Mobility,
                            Amount = 5 + (Admiral.Level / 2),
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralRacialAbility.MentalGiant:
                    {
                        result.BerserkModifier -= 5;
                        result.MoraleModifier -= 5;
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.PsiAttack,
                            Amount = Admiral.Level,
                            ModifierType = ModifierType.Proportional
                        });
                        if (Admiral.Level <= 6)
                        {
                            result.StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.PsiDefense,
                                Amount = -10,
                                ModifierType = ModifierType.Proportional
                            });
                        }
                        else if (Admiral.Level <= 9)
                        {
                            result.StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.PsiDefense,
                                Amount = -5,
                                ModifierType = ModifierType.Proportional
                            });
                            result.Attributes.Add(FleetAttribute.WeakCloakingDetection);
                        }
                        else
                        {
                            result.Attributes.Add(FleetAttribute.CompleteCloakingDetection);
                        }
                    }
                    break;
                case AdmiralRacialAbility.ArtifactCrystal:
                    {
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.BeamDamage,
                            Amount = Admiral.Level * 2,
                            ModifierType = ModifierType.Proportional
                        });
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.ShieldRechargeRate,
                            Amount = Admiral.Level * 2,
                            ModifierType = ModifierType.Proportional
                        });
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.Speed,
                            Amount = Admiral.Level * 2,
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralRacialAbility.PsychicProgenitor:
                    {
                        result.BerserkModifier += 5;
                        result.MoraleModifier += 10;

                        result.Attributes.Add(FleetAttribute.CompleteCloakingDetection);
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.PsiAttack,
                            Amount = 20 + (Admiral.Level * 4),
                            ModifierType = ModifierType.Proportional
                        });

                        if (Admiral.Level >= 13)
                        {
                            result.Attributes.Add(FleetAttribute.CompleteCloaking);
                        }
                        else if (Admiral.Level >= 7)
                        {
                            result.Attributes.Add(FleetAttribute.WeakCloaking);
                        }
                    }
                    break;
                case AdmiralRacialAbility.ArtifactCoolingEngine:
                    {
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.CoolingTime,
                            Amount = -Admiral.Level * 5 / 2,
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralRacialAbility.LyingDormant:
                    result.Attributes.Add(FleetAttribute.WeakCloaking);
                    {
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.Stealth,
                            Amount = (Admiral.Level * 3) + 10,
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralRacialAbility.MissileCraters:
                    {
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.CoolingTime,
                            Amount = -Admiral.Level * 2,
                            ModifierType = ModifierType.Proportional
                        });
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.ArmorDefenseRating,
                            Amount = -Admiral.Level * 3 / 2,
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralRacialAbility.MeteorDrones:
                    {
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.GenericDefense,
                            Amount = Admiral.Level * 3,
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralRacialAbility.CyberScanUnit:
                    {
                        result.Attributes.Add(FleetAttribute.WeakCloakingDetection);
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.AttackRating,
                            Amount = Admiral.Level / 2,
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralRacialAbility.PatternBroadcaster:
                    {
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.Stealth,
                            Amount = -(10 + (Admiral.Level * 2)),
                            ModifierType = ModifierType.Proportional
                        });
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.Misinterpret,
                            Amount = (Admiral.Level * 4) + 10,
                            ModifierType = ModifierType.Proportional
                        });
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.DefenseRatingAgainstMissile,
                            Amount = (Admiral.Level * 2) + 20,
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralRacialAbility.FamousPrivateer:
                    {
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.Speed,
                            Amount = Admiral.Level / 2,
                            ModifierType = ModifierType.Proportional
                        });
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.Stealth,
                            Amount = Admiral.Level + 10,
                            ModifierType = ModifierType.Proportional
                        });

                        if (Admiral.Level >= 7)
                        {
                            result.Attributes.Add(FleetAttribute.WeakCloaking);
                        }
                    }
                    break;
                case AdmiralRacialAbility.CommerceKing:
                    {
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.Hp,
                            Amount = Admiral.Level,
                            ModifierType = ModifierType.Proportional
                        });
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.Speed,
                            Amount = Admiral.Level,
                            ModifierType = ModifierType.Proportional
                        });
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.Mobility,
                            Amount = Admiral.Level,
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralRacialAbility.RetreatShield:
                    {
                        result.BerserkModifier -= 5;
                        result.MoraleModifier -= 10;
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.ShieldStrength,
                            Amount = Admiral.Level * 5,
                            ModifierType = ModifierType.Proportional
                        });
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.ImpenetrableShield,
                            Amount = Admiral.Level * 5,
                            ModifierType = ModifierType.Absolute
                        });
                        if (Admiral.Level <= 5)
                        {
                            result.StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.ShieldSolidity,
                                Amount = 1,
                                ModifierType = ModifierType.Absolute
                            });
                        }
                        else if (Admiral.Level <= 10)
                        {
                            result.Attributes.Add(FleetAttribute.WeakCloaking);
                            result.StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.ShieldSolidity,
                                Amount = 2,
                                ModifierType = ModifierType.Absolute
                            });
                        }
                        else if (Admiral.Level <= 15)
                        {
                            result.Attributes.Add(FleetAttribute.WeakCloaking);
                            result.StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.ShieldSolidity,
                                Amount = 3,
                                ModifierType = ModifierType.Absolute
                            });
                        }
                        else if (Admiral.Level <= 19)
                        {
                            result.Attributes.Add(FleetAttribute.WeakCloaking);
                            result.StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.ShieldSolidity,
                                Amount = 4,
                                ModifierType = ModifierType.Absolute
                            });
                        }
                        else
                        {
                            result.Attributes.Add(FleetAttribute.WeakCloaking);
                            result.StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.ShieldSolidity,
                                Amount = 5,
                                ModifierType = ModifierType.Absolute
                            });
                        }
                    }
                    break;
                case AdmiralRacialAbility.GeneticThrowback:
                    {
                        result.MoraleModifier += 5;

                        if (Admiral.Level < 10)
                        {
                            result.Attributes.Add(FleetAttribute.WeakCloaking);
                        }
                        else
                        {
                            result.Attributes.Add(FleetAttribute.CompleteCloaking);
                        }

                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.PsiAttack,
                            Amount = 5 * Admiral.Level,
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralRacialAbility.RigidThinking:
                    {
                        result.BerserkModifier -= 5;
                        result.MoraleModifier -= 10;

                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.PsiDefense,
                            Amount = 10 + (2 * Admiral.Level),
                            ModifierType = ModifierType.Proportional
                        });
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.DefenseRating,
                            Amount = Admiral.Level,
                            ModifierType = ModifierType.Proportional
                        });
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.CriticalHit,
                            Amount = Admiral.Level / 4,
                            ModifierType = ModifierType.Absolute
                        });
                    }
                    break;
                case AdmiralRacialAbility.Blitzkreig:
                    {
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.RepairSpeed,
                            Amount = 5 + (Admiral.Level / 2),
                            ModifierType = ModifierType.Proportional
                        });
                        result.StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.CriticalHit,
                            Amount = Admiral.Level / 4,
                            ModifierType = ModifierType.Absolute
                        });
                    }
                    break;
                default:
                    break;
            }

            result.ShieldSolidity = result.Shield.Deflection;
            result.MaxShieldStrength = result.Shield.Strength[result.ShipClass.Class];
            result.ShieldRechargeRate = result.Shield.RechargeRate[result.ShipClass.Class];

            result.ShieldSolidity = result.StaticEffects.Where(x => x.Type == FleetEffectType.ShieldSolidity).CalculateTotalEffect(result.ShieldSolidity, x => x.Amount.Value);
            result.MaxShieldStrength = result.StaticEffects.Where(x => x.Type == FleetEffectType.ShieldStrength).CalculateTotalEffect(result.MaxShieldStrength, x => x.Amount.Value);

            result.MaxHP = result.StaticEffects.Where(x => x.Type == FleetEffectType.Hp).CalculateTotalEffect(result.MaxHP, x => x.Amount.Value);

            for (int i = 0; i < MaxShipCount; i++)
            {
                result.Ships.Add(new Battle.Ship() { HP = result.MaxHP, ShieldStrength = result.MaxShieldStrength });
            }

            if (result.StaticEffects.Any(x => x.Type == FleetEffectType.CompleteCloaking))
            {
                result.Attributes.Add(FleetAttribute.CompleteCloaking);
            }
            if (result.StaticEffects.Any(x => x.Type == FleetEffectType.WeakCloaking))
            {
                result.Attributes.Add(FleetAttribute.WeakCloaking);
            }
            if (result.StaticEffects.Any(x => x.Type == FleetEffectType.CompleteCloakingDetection))
            {
                result.Attributes.Add(FleetAttribute.CompleteCloakingDetection);
            }
            if (result.StaticEffects.Any(x => x.Type == FleetEffectType.WeakCloakingDetection))
            {
                result.Attributes.Add(FleetAttribute.WeakCloakingDetection);
            }

            result.MoraleModifier = result.StaticEffects.Where(x => x.Type == FleetEffectType.MoraleModifier).CalculateTotalEffect(result.MoraleModifier, x => x.Amount.Value);
            result.BerserkModifier = result.StaticEffects.Where(x => x.Type == FleetEffectType.BerserkModifier).CalculateTotalEffect(result.BerserkModifier, x => x.Amount.Value);

            foreach (Weapon weapon in ShipDesign.Weapons)
            {
                result.Turrets.Add(new Battle.Turret(weapon, ShipDesign.ShipClass.Space / (weapon.Space * ShipDesign.ShipClass.WeaponSlotCount)));
            }

            result.StaticEffects = ShipDesign.Devices.SelectMany(x => x.Effects).Union(ShipDesign.Armor.Effects).ToList();

            return result;
        }
    }
}
