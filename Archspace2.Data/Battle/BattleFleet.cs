using Archspace2.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archspace2
{
    public enum BattleFleetStatus
    {
        None,
        Berserk,
        Disorder,
        Rout,
        Retreat,
        Panic,
        Annihilated,
        AnnihilatedThisTurn,
        RetreatedThisTurn,
        Retreated
    };

    public enum BattleFleetSubstatus
    {
        None,
        TurningToCentre,
        Penetration,
        Charge,
        MoveStraight,
        TurnToForward,
        TurnToBackward,
        TurnToNearestBorder
    };

    public enum BattleFleetMorale
    {
        Normal,
        WeakBreak,
        NormalBreak,
        CompleteBreak
    };

    public enum BattleFleetAttribute
    {
        WeakCloaking = 1,
        CompleteCloaking,
        WeakCloakingDetection,
        CompleteCloakingDetection,
        PsiRace,
        EnhancedPsi,
        EnhancedMobility
    };

    public class BattleFleet : Vector
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Player Owner { get; set; }
        public List<Ship> Ships { get; set; }
        public Fleet Fleet { get; set; }

        public Command Command { get; set; }
        public BattleFleetStatus Status { get; set; }
        public BattleFleetSubstatus Substatus { get; set; }
        public BattleFleetMorale MoraleStatus { get; set; }

        public HashSet<BattleFleetAttribute> Attributes { get; set; }

        public int MaxHP { get; set; }
        public int Morale { get; set; }
        public int StatusTurns { get; set; }

        public int ActiveShipCount
        {
            get
            {
                int count = 0;

                foreach (Ship ship in Ships)
                {
                    if (ship.HP > 0)
                    {
                        count++;
                    }
                }

                return count;
            }
        }
        public int MaxShipCount { get; set; }

        public int MoraleModifier { get; set; }
        public int BerserkModifier { get; set; }

        public Admiral Admiral { get => Fleet.Admiral; }
        public ShipClass ShipClass { get => Fleet.ShipDesign.ShipClass; }
        public Armor Armor { get => Fleet.ShipDesign.Armor; }
        public Engine Engine { get => Fleet.ShipDesign.Engine; }
        public Computer Computer { get => Fleet.ShipDesign.Computer; }
        public Shield Shield { get => Fleet.ShipDesign.Shield; }
        public List<Device> Devices { get => Fleet.ShipDesign.Devices; }
        public List<Turret> Turrets { get; set; }

        public int RedZoneRadius { get; set; }

        public int FleetsKilled { get; set; }
        public int ShipsKilled { get; set; }
        public int AdmiralExp { get; set; }

        public int ShieldSolidity { get; set; }
        public int MaxShieldStrength { get; set; }
        public int ShieldRechargeRate { get; set; }

        public int HP {
            get
            {
                return Ships.Sum(x => x.HP);
            }
        }

        public int Speed
        {
            get
            {
                int result = Engine.BattleSpeed[ShipClass.Class];
                result = StaticEffects.Where(x => x.Type == FleetEffectType.Speed).CalculateTotalEffect(result, x => x.Amount.Value);

                return result;
            }
        }

        public double Mobility
        {
            get
            {
                int result = Engine.BattleMobility[ShipClass.Class];
                result = StaticEffects.Where(x => x.Type == FleetEffectType.Mobility).CalculateTotalEffect(result, x => x.Amount.Value);

                return result/5000.0;
            }
        }

        public int DetectionRange
        {
            get
            {
                int result = 20 + Computer.TechLevel + Admiral.Skills.Detection;

                result = StaticEffects.Where(x => x.Type == FleetEffectType.Computer).CalculateTotalEffect(result, x => x.Amount.Value);
                result = StaticEffects.Where(x => x.Type == FleetEffectType.DetectionRange).CalculateTotalEffect(result, x => x.Amount.Value);

                return 20 * result;
            }
        }

        private int CalculateActiveRatio()
        {
            int count = 0;

            foreach (Ship ship in Ships)
            {
                count += ship.HP;
            }
            count = count * 100 / (MaxShipCount * MaxHP);

            return count;
        }

        public int Power
        {
            get
            {
                return CalculateActiveRatio() * Fleet.Power;
            }
        }

        public bool IsCapital { get; set; }

        public List<FleetEffect> StaticEffects { get; set; }

        public BattleFleet(Player aPlayer, Fleet aFleet)
        {
            Attributes = new HashSet<BattleFleetAttribute>();
            Turrets = new List<Turret>();
            StaticEffects = new List<FleetEffect>();

            Id = aFleet.Id;
            Name = aFleet.Name;

            Owner = aPlayer;

            Fleet = aFleet;

            Command = Command.Normal;
            Status = BattleFleetStatus.None;
            Substatus = BattleFleetSubstatus.None;

            MaxHP = (int)(ShipClass.BaseHp * Armor.HpMultiplier);

            if (Owner.Traits.Contains(RacialTrait.HighMorale))
            {
                Morale = 125;
            }
            else
            {
                Morale = 100;
            }

            MoraleStatus = BattleFleetMorale.Normal;
            StatusTurns = 0;

            MaxShipCount = aFleet.CurrentShipCount;

            MoraleModifier = -(aFleet.Experience / 10) + 5;
            MoraleModifier = Owner.Race.BaseFleetEffects.Where(x => x.Type == FleetEffectType.MoraleModifier).CalculateTotalEffect(MoraleModifier, x => x.Amount.Value);

            BerserkModifier = Owner.Race.BaseFleetEffects.Where(x => x.Type == FleetEffectType.BerserkModifier).CalculateTotalEffect(MoraleModifier, x => x.Amount.Value);

            // This was a later addition.
            /*
            int averageHonor = (Owner.Council.Honor + Owner.Honor) / 2;

            MoraleModifier -= (averageHonor - 50) / 2;
            BerserkModifier += (averageHonor + 50) / 10;
            */

            if (Owner.Traits.Any(x => x == RacialTrait.Psi))
            {
                Attributes.Add(BattleFleetAttribute.PsiRace);
            }
            if (Owner.Traits.Any(x => x == RacialTrait.EnhancedPsi))
            {
                Attributes.Add(BattleFleetAttribute.EnhancedPsi);
            }
            if (Owner.Traits.Any(x => x == RacialTrait.FastManeuver))
            {
                Attributes.Add(BattleFleetAttribute.EnhancedMobility);
            }

            foreach (Weapon weapon in Fleet.ShipDesign.Weapons)
            {
                Turrets.Add(new Turret(weapon, ShipClass.Space / (weapon.Space * ShipClass.WeaponSlotCount)));
            }

            RedZoneRadius = 0;

            StaticEffects = Fleet.ShipDesign.Devices.SelectMany(x => x.Effects).Union(Fleet.ShipDesign.Armor.Effects).ToList();

            IsCapital = false;

            InitializeCommon();

            Ships = new List<Ship>();
        }
        public BattleFleet(DefenseDeployment aDefenseDeployment) : this(aDefenseDeployment.Fleet.Player, aDefenseDeployment.Fleet)
        {
            X = aDefenseDeployment.X;
            Y = aDefenseDeployment.Y;
            IsCapital = aDefenseDeployment.Type == DefenseDeploymentType.Capital;
        }

        public void InitializeCommon()
        {
            ShipsKilled = 0;
            FleetsKilled = 0;

            AdmiralExp = Game.Configuration.Mission.BattleExperience;

            if (Attributes.Contains(BattleFleetAttribute.EnhancedMobility))
            {
                StaticEffects.Add(new FleetEffect()
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
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.ArmorDefenseRating,
                            Amount = Admiral.Level * 2,
                            ModifierType = ModifierType.Proportional
                        });
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.RepairSpeed,
                            Amount = Admiral.Level * 2,
                            ModifierType = ModifierType.Proportional
                        });
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.ImpenetrableArmor,
                            Amount = Admiral.Level * 2,
                            ModifierType = ModifierType.Proportional
                        });

                        if (Admiral.Level <= 12)
                        {
                            StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.Repair,
                                Amount = 1,
                                ModifierType = ModifierType.Proportional,
                                Period = 10
                            });
                        }
                        else if (Admiral.Level <= 19)
                        {
                            StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.Repair,
                                Amount = 2,
                                ModifierType = ModifierType.Proportional,
                                Period = 10
                            });
                        }
                        else
                        {
                            StaticEffects.Add(new FleetEffect()
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
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.ShieldStrength,
                            Amount = 10 + (Admiral.Level * 2),
                            ModifierType = ModifierType.Proportional
                        });
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.ShieldRechargeRate,
                            Amount = 10 + (Admiral.Level * 2),
                            ModifierType = ModifierType.Proportional
                        });
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.ImpenetrableShield,
                            Amount = 10 + (Admiral.Level * 2),
                            ModifierType = ModifierType.Absolute
                        });
                        if (Admiral.Level <= 12)
                        {
                            StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.ShieldSolidity,
                                Amount = 2,
                                ModifierType = ModifierType.Absolute
                            });
                        }
                        else if (Admiral.Level <= 19)
                        {
                            StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.ShieldSolidity,
                                Amount = 3,
                                ModifierType = ModifierType.Absolute
                            });
                        }
                        else
                        {
                            StaticEffects.Add(new FleetEffect()
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
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.MissileAttackRating,
                            Amount = (Admiral.Level * 2),
                            ModifierType = ModifierType.Proportional
                        });
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.MissileDamage,
                            Amount = (Admiral.Level * 2),
                            ModifierType = ModifierType.Proportional
                        });
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.MissileCoolingTime,
                            Amount = - (Admiral.Level * 2),
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralSpecialAbility.EnergySystemSpecialist:
                    {
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.BeamAttackRating,
                            Amount = (Admiral.Level * 2),
                            ModifierType = ModifierType.Proportional
                        });
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.BeamDamage,
                            Amount = (Admiral.Level * 2),
                            ModifierType = ModifierType.Proportional
                        });
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.BeamCoolingTime,
                            Amount = -(Admiral.Level * 2),
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralSpecialAbility.BallisticExpert:
                    {
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.ProjectileAttackRating,
                            Amount = (Admiral.Level * 2),
                            ModifierType = ModifierType.Proportional
                        });
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.ProjectileDamage,
                            Amount = (Admiral.Level * 2),
                            ModifierType = ModifierType.Proportional
                        });
                        StaticEffects.Add(new FleetEffect()
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
                            StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.DefenseRating,
                                Amount = 10,
                                ModifierType = ModifierType.Proportional
                            });
                        }
                        else if (Admiral.Level <= 10)
                        {
                            StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.DefenseRating,
                                Amount = 15,
                                ModifierType = ModifierType.Proportional
                            });
                        }
                        else if (Admiral.Level <= 15)
                        {
                            StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.DefenseRating,
                                Amount = 20,
                                ModifierType = ModifierType.Proportional
                            });
                        }
                        else if (Admiral.Level <= 19)
                        {
                            StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.DefenseRating,
                                Amount = 25,
                                ModifierType = ModifierType.Proportional
                            });
                        }
                        else
                        {
                            StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.DefenseRating,
                                Amount = 30,
                                ModifierType = ModifierType.Proportional
                            });
                        }

                        StaticEffects.Add(new FleetEffect()
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
                            Attributes.Add(BattleFleetAttribute.WeakCloakingDetection);
                        }

                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.CriticalHit,
                            Amount = 5 + (Admiral.Level / 2),
                            ModifierType = ModifierType.Absolute
                        });
                    }
                    break;
                case AdmiralRacialAbility.LoneWolf:
                    {
                        BerserkModifier += 5;
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.Mobility,
                            Amount = 5 + Admiral.Level,
                            ModifierType = ModifierType.Proportional
                        });
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.Stealth,
                            Amount = Admiral.Level,
                            ModifierType = ModifierType.Proportional
                        });
                        if (Admiral.Level >= 10)
                        {
                            Attributes.Add(BattleFleetAttribute.WeakCloaking);
                        }
                    }
                    break;
                case AdmiralRacialAbility.DnaPoisonReplicater:
                    {
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.ProjectileDamage,
                            Amount = Admiral.Level,
                            ModifierType = ModifierType.Proportional
                        });
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.MissileDamage,
                            Amount = Admiral.Level,
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralRacialAbility.BreederMale:
                    {
                        StaticEffects.Add(new FleetEffect()
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
                        BerserkModifier += 5;
                        MoraleModifier -= 5;
                        if (Admiral.Level <= 5)
                        {
                            StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.PsiDefense,
                                Amount = -5,
                                ModifierType = ModifierType.Proportional
                            });
                        }
                        else if (Admiral.Level <= 15)
                        {
                            StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.PsiDefense,
                                Amount = -10,
                                ModifierType = ModifierType.Proportional
                            });
                        }
                        else if (Admiral.Level <= 19)
                        {
                            StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.PsiDefense,
                                Amount = -15,
                                ModifierType = ModifierType.Proportional
                            });
                        }
                        else
                        {
                            StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.PsiDefense,
                                Amount = -20,
                                ModifierType = ModifierType.Proportional
                            });
                        }

                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.Mobility,
                            Amount = 5 + (Admiral.Level/2),
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralRacialAbility.MentalGiant:
                    {
                        BerserkModifier -= 5;
                        MoraleModifier -= 5;
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.PsiAttack,
                            Amount = Admiral.Level,
                            ModifierType = ModifierType.Proportional
                        });
                        if (Admiral.Level <= 6)
                        {
                            StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.PsiDefense,
                                Amount = -10,
                                ModifierType = ModifierType.Proportional
                            });
                        }
                        else if (Admiral.Level <= 9)
                        {
                            StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.PsiDefense,
                                Amount = -5,
                                ModifierType = ModifierType.Proportional
                            });
                            Attributes.Add(BattleFleetAttribute.WeakCloakingDetection);
                        }
                        else
                        {
                            Attributes.Add(BattleFleetAttribute.CompleteCloakingDetection);
                        }
                    }
                    break;
                case AdmiralRacialAbility.ArtifactCrystal:
                    {
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.BeamDamage,
                            Amount = Admiral.Level * 2,
                            ModifierType = ModifierType.Proportional
                        });
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.ShieldRechargeRate,
                            Amount = Admiral.Level * 2,
                            ModifierType = ModifierType.Proportional
                        });
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.Speed,
                            Amount = Admiral.Level * 2,
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralRacialAbility.PsychicProgenitor:
                    {
                        BerserkModifier += 5;
                        MoraleModifier += 10;

                        Attributes.Add(BattleFleetAttribute.CompleteCloakingDetection);
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.PsiAttack,
                            Amount = 20 + (Admiral.Level * 4),
                            ModifierType = ModifierType.Proportional
                        });

                        if (Admiral.Level >= 13)
                        {
                            Attributes.Add(BattleFleetAttribute.CompleteCloaking);
                        }
                        else if (Admiral.Level >= 7)
                        {
                            Attributes.Add(BattleFleetAttribute.WeakCloaking);
                        }
                    }
                    break;
                case AdmiralRacialAbility.ArtifactCoolingEngine:
                    {
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.CoolingTime,
                            Amount = -Admiral.Level * 5 / 2,
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralRacialAbility.LyingDormant:
                    Attributes.Add(BattleFleetAttribute.WeakCloaking);
                    {
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.Stealth,
                            Amount = (Admiral.Level * 3) + 10,
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralRacialAbility.MissileCraters:
                    {
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.CoolingTime,
                            Amount = -Admiral.Level * 2,
                            ModifierType = ModifierType.Proportional
                        });
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.ArmorDefenseRating,
                            Amount = -Admiral.Level * 3 / 2,
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralRacialAbility.MeteorDrones:
                    {
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.GenericDefense,
                            Amount = Admiral.Level * 3,
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralRacialAbility.CyberScanUnit:
                    {
                        Attributes.Add(BattleFleetAttribute.WeakCloakingDetection);
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.AttackRating,
                            Amount = Admiral.Level / 2,
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralRacialAbility.PatternBroadcaster:
                    {
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.Stealth,
                            Amount = -(10 + (Admiral.Level * 2)),
                            ModifierType = ModifierType.Proportional
                        });
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.Misinterpret,
                            Amount = (Admiral.Level * 4) + 10,
                            ModifierType = ModifierType.Proportional
                        });
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.DefenseRatingAgainstMissile,
                            Amount = (Admiral.Level * 2) + 20,
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralRacialAbility.FamousPrivateer:
                    {
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.Speed,
                            Amount = Admiral.Level/2,
                            ModifierType = ModifierType.Proportional
                        });
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.Stealth,
                            Amount = Admiral.Level + 10,
                            ModifierType = ModifierType.Proportional
                        });

                        if (Admiral.Level >= 7)
                        {
                            Attributes.Add(BattleFleetAttribute.WeakCloaking);
                        }
                    }
                    break;
                case AdmiralRacialAbility.CommerceKing:
                    {
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.Hp,
                            Amount = Admiral.Level,
                            ModifierType = ModifierType.Proportional
                        });
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.Speed,
                            Amount = Admiral.Level,
                            ModifierType = ModifierType.Proportional
                        });
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.Mobility,
                            Amount = Admiral.Level,
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralRacialAbility.RetreatShield:
                    {
                        BerserkModifier -= 5;
                        MoraleModifier -= 10;
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.ShieldStrength,
                            Amount = Admiral.Level * 5,
                            ModifierType = ModifierType.Proportional
                        });
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.ImpenetrableShield,
                            Amount = Admiral.Level * 5,
                            ModifierType = ModifierType.Absolute
                        });
                        if (Admiral.Level <= 5)
                        {
                            StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.ShieldSolidity,
                                Amount = 1,
                                ModifierType = ModifierType.Absolute
                            });
                        }
                        else if (Admiral.Level <= 10)
                        {
                            Attributes.Add(BattleFleetAttribute.WeakCloaking);
                            StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.ShieldSolidity,
                                Amount = 2,
                                ModifierType = ModifierType.Absolute
                            });
                        }
                        else if (Admiral.Level <= 15)
                        {
                            Attributes.Add(BattleFleetAttribute.WeakCloaking);
                            StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.ShieldSolidity,
                                Amount = 3,
                                ModifierType = ModifierType.Absolute
                            });
                        }
                        else if (Admiral.Level <= 19)
                        {
                            Attributes.Add(BattleFleetAttribute.WeakCloaking);
                            StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.ShieldSolidity,
                                Amount = 4,
                                ModifierType = ModifierType.Absolute
                            });
                        }
                        else
                        {
                            Attributes.Add(BattleFleetAttribute.WeakCloaking);
                            StaticEffects.Add(new FleetEffect()
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
                        MoraleModifier += 5;

                        if (Admiral.Level < 10)
                        {
                            Attributes.Add(BattleFleetAttribute.WeakCloaking);
                        }
                        else
                        {
                            Attributes.Add(BattleFleetAttribute.CompleteCloaking);
                        }

                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.PsiAttack,
                            Amount = 5 * Admiral.Level,
                            ModifierType = ModifierType.Proportional
                        });
                    }
                    break;
                case AdmiralRacialAbility.RigidThinking:
                    {
                        BerserkModifier -= 5;
                        MoraleModifier -= 10;

                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.PsiDefense,
                            Amount = 10 + (2 * Admiral.Level),
                            ModifierType = ModifierType.Proportional
                        });
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.DefenseRating,
                            Amount = Admiral.Level,
                            ModifierType = ModifierType.Proportional
                        });
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.CriticalHit,
                            Amount = Admiral.Level / 4,
                            ModifierType = ModifierType.Absolute
                        });
                    }
                    break;
                case AdmiralRacialAbility.Blitzkreig:
                    {
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.RepairSpeed,
                            Amount = 5 + (Admiral.Level / 2),
                            ModifierType = ModifierType.Proportional
                        });
                        StaticEffects.Add(new FleetEffect()
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

            ShieldSolidity = Shield.Deflection;
            MaxShieldStrength = Shield.Strength[ShipClass.Class];
            ShieldRechargeRate = Shield.RechargeRate[ShipClass.Class];

            ShieldSolidity = StaticEffects.Where(x => x.Type == FleetEffectType.ShieldSolidity).CalculateTotalEffect(ShieldSolidity, x => x.Amount.Value);
            MaxShieldStrength = StaticEffects.Where(x => x.Type == FleetEffectType.ShieldStrength).CalculateTotalEffect(MaxShieldStrength, x => x.Amount.Value);

            MaxHP = StaticEffects.Where(x => x.Type == FleetEffectType.Hp).CalculateTotalEffect(MaxHP, x => x.Amount.Value);

            for (int i = 0; i < MaxShipCount; i++)
            {
                Ships.Add(new Ship() { HP = MaxHP, ShieldStrength = MaxShieldStrength });
            }

            if (StaticEffects.Any(x => x.Type ==  FleetEffectType.CompleteCloaking))
            {
                Attributes.Add(BattleFleetAttribute.CompleteCloaking);
            }
            if (StaticEffects.Any(x => x.Type == FleetEffectType.WeakCloaking))
            {
                Attributes.Add(BattleFleetAttribute.WeakCloaking);
            }
            if (StaticEffects.Any(x => x.Type == FleetEffectType.CompleteCloakingDetection))
            {
                Attributes.Add(BattleFleetAttribute.CompleteCloakingDetection);
            }
            if (StaticEffects.Any(x => x.Type == FleetEffectType.WeakCloakingDetection))
            {
                Attributes.Add(BattleFleetAttribute.WeakCloakingDetection);
            }

            MoraleModifier = StaticEffects.Where(x => x.Type == FleetEffectType.MoraleModifier).CalculateTotalEffect(MoraleModifier, x => x.Amount.Value);
            BerserkModifier = StaticEffects.Where(x => x.Type == FleetEffectType.BerserkModifier).CalculateTotalEffect(BerserkModifier, x => x.Amount.Value);
        }

    }
}
