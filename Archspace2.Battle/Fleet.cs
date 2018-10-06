using System;
using System.Collections.Generic;
using System.Linq;
using Universal.Common.Extensions;
using Universal.Common.Reflection.Extensions;

namespace Archspace2.Battle
{
    public enum FleetStatus
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

    public enum FleetSubstatus
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

    public enum FleetMorale
    {
        Normal,
        WeakBreak,
        NormalBreak,
        CompleteBreak
    };

    public enum DamageDistribution
    {
        First,
        Random,
        All,
        Continuous
    };

    public class Fleet : Unit
    {
        protected static HashSet<FleetStatus> AbnormalStatuses = new HashSet<FleetStatus>()
        {
            FleetStatus.Berserk,
            FleetStatus.Disorder,
            FleetStatus.Panic,
            FleetStatus.Retreat,
            FleetStatus.Rout
        };

        public Battle Battle { get; set; }
        public Armada Armada { get; set; }
        
        public Player Owner { get; set; }
        public List<Ship> Ships { get; protected set; }

        public Command Command { get; set; }
        public FleetStatus Status { get; set; }
        public FleetSubstatus Substatus { get; set; }
        public FleetMorale MoraleStatus { get; set; }
        
        public double Morale { get; set; }
        public int StatusTurns { get; set; }

        public int Efficiency
        {
            get
            {
                int result = Admiral.Efficiency;

                result = Effects.OfType(FleetEffectType.Efficiency).CalculateTotalEffect(result, x => x.Amount);

                return result;
            }
        }
        public int Experience { get; set; }

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

        public Admiral Admiral { get; set; }
        public ShipClass ShipClass { get; set; }

        public Armor Armor { get; set; }
        public Engine Engine { get; set; }
        public Computer Computer { get; set; }
        public Shield Shield { get; set; }
        public List<Device> Devices { get; set; }
        public List<Turret> Turrets { get; set; }

        public bool Detected { get; set; }
        protected int mEngagementTimer { get; set; }
        public bool Encountered { get; set; }

        public Fleet TargetEnemy { get; set; }
        public List<Fleet> EncounteredEnemies { get; set; }
        
        public HashSet<FleetAttribute> Attributes { get; set; }

        public int KilledShips { get; set; }
        public int KilledFleets { get; set; }

        public int HP
        {
            get
            {
                return Ships.Sum(x => x.HP);
            }
        }

        public BoundingBox Path
        {
            get
            {
                double leftX = 0;
                double rightX = 0;
                double topY = 0;
                double bottomY = 0;

                switch (Command)
                {
                    case Command.Normal:
                        topY = 750;
                        bottomY = -750;
                        rightX = 1500;
                        break;
                    case Command.Formation:
                        topY = 750;
                        bottomY = -750;
                        rightX = 1500;
                        break;
                    case Command.Penetrate:
                        topY = 250;
                        bottomY = -250;
                        rightX = 1000;
                        break;
                    case Command.Flank:
                        topY = 200;
                        bottomY = -200;
                        break;
                    case Command.Reserve:
                        topY = 750;
                        bottomY = -750;
                        break;
                    case Command.StandGround:
                        topY = 750;
                        bottomY = -750;
                        rightX = 1500;
                        break;
                    case Command.Assault:
                        topY = 200;
                        bottomY = -200;
                        break;
                    case Command.Free:
                        topY = 750;
                        bottomY = -750;
                        rightX = 1500;
                        break;
                    default:
                        topY = 750;
                        bottomY = -750;
                        rightX = 1500;
                        break;
                }

                return new BoundingBox()
                {
                    LeftX = leftX,
                    RightX = rightX,
                    TopY = topY,
                    BottomY = bottomY
                };
            }
        }

        public int Speed
        {
            get
            {
                int result = Engine.BattleSpeed[ShipClass.Class];
                result = StaticEffects.OfType(FleetEffectType.Speed).CalculateTotalEffect(result, x => x.Amount);

                return result;
            }
        }

        public double Mobility
        {
            get
            {
                double result = Engine.BattleMobility[ShipClass.Class];
                result = StaticEffects.OfType(FleetEffectType.Mobility).CalculateTotalEffect(result, x => x.Amount);
                result *= 100 + (int)Morale / 10 + Experience * 3 / 10 - 15;

                return result / 5000.0;
            }
        }

        public int DetectionRange
        {
            get
            {
                int result = 20 + Computer.TechLevel + Admiral.Skills.Detection;

                result = StaticEffects.OfType(FleetEffectType.Computer).CalculateTotalEffect(result, x => x.Amount);
                result = StaticEffects.OfType(FleetEffectType.DetectionRange).CalculateTotalEffect(result, x => x.Amount);

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
            count = count * 100 / (MaxShipCount * CalculateMaxHp());

            return count;
        }

        public long Power { get; set; }

        public bool IsCapital { get; set; }

        public List<FleetEffect> StaticEffects { get; set; }
        public List<FleetEffect> DynamicsEffects { get; set; }
        public IEnumerable<FleetEffect> Effects
        {
            get
            {
                return StaticEffects.Union(DynamicsEffects);
            }
        }

        internal Fleet()
        {
            Attributes = new HashSet<FleetAttribute>();
            Turrets = new List<Turret>();
            Ships = new List<Ship>();
            StaticEffects = new List<FleetEffect>();
            DynamicsEffects = new List<FleetEffect>();
            TargetEnemy = null;
            EncounteredEnemies = new List<Fleet>();

            Command = Command.Normal;
            Status = FleetStatus.None;
            Substatus = FleetSubstatus.None;

            MoraleStatus = FleetMorale.Normal;
            Morale = 100;

            StatusTurns = 0;

            IsCapital = false;
        }

        public Fleet(int aId, string aName, Player aPlayer, ShipClass aShipClass, Armor aArmor, Computer aComputer, Engine aEngine, Shield aShield, List<Device> aDevices, List<Weapon> aWeapons, Admiral aAdmiral, int aShipCount, long aPower, bool aIsCapital = false) : this()
        {
            Id = aId;
            Name = aName;

            Owner = aPlayer;
            Admiral = aAdmiral;

            ShipClass = aShipClass;
            Armor = aArmor;
            Computer = aComputer;
            Engine = aEngine;
            Shield = aShield;
            Devices = aDevices;
            InitializeTurrets(aWeapons);

            ApplyAdmiralStaticEffects();
            ApplyComponentStaticEffects();
            ApplyAttributes();
            ApplyModifiers();

            Power = aPower;

            MaxShipCount = aShipCount;
            InitializeShips();

            IsCapital = aIsCapital;
        }

        public Fleet(Fleet aFleet) : this()
        {
            this.Bind(aFleet);
        }

        public void InitializeTurrets(List<Weapon> aWeapons)
        {
            foreach (Weapon weapon in aWeapons)
            {
                Turrets.Add(new Turret(weapon, ShipClass.Space / (weapon.Space * ShipClass.WeaponSlotCount)));
            }
        }

        public void InitializeShips()
        {
            int maxHp = CalculateMaxHp();
            int maxShieldStrength = CalculateMaxShieldStrength();

            for (int i = 0; i < MaxShipCount; i++)
            {
                Ships.Add(new Ship(maxHp, maxShieldStrength));
            }
        }

        public void ApplyAdmiralStaticEffects()
        {

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
                            Amount = -(Admiral.Level * 2),
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
                            Attributes.Add(FleetAttribute.WeakCloakingDetection);
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
                            Attributes.Add(FleetAttribute.WeakCloaking);
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
                            Amount = 5 + (Admiral.Level / 2),
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
                            Attributes.Add(FleetAttribute.WeakCloakingDetection);
                        }
                        else
                        {
                            Attributes.Add(FleetAttribute.CompleteCloakingDetection);
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

                        Attributes.Add(FleetAttribute.CompleteCloakingDetection);
                        StaticEffects.Add(new FleetEffect()
                        {
                            Type = FleetEffectType.PsiAttack,
                            Amount = 20 + (Admiral.Level * 4),
                            ModifierType = ModifierType.Proportional
                        });

                        if (Admiral.Level >= 13)
                        {
                            Attributes.Add(FleetAttribute.CompleteCloaking);
                        }
                        else if (Admiral.Level >= 7)
                        {
                            Attributes.Add(FleetAttribute.WeakCloaking);
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
                    Attributes.Add(FleetAttribute.WeakCloaking);
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
                        Attributes.Add(FleetAttribute.WeakCloakingDetection);
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
                            Amount = Admiral.Level / 2,
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
                            Attributes.Add(FleetAttribute.WeakCloaking);
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
                            Attributes.Add(FleetAttribute.WeakCloaking);
                            StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.ShieldSolidity,
                                Amount = 2,
                                ModifierType = ModifierType.Absolute
                            });
                        }
                        else if (Admiral.Level <= 15)
                        {
                            Attributes.Add(FleetAttribute.WeakCloaking);
                            StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.ShieldSolidity,
                                Amount = 3,
                                ModifierType = ModifierType.Absolute
                            });
                        }
                        else if (Admiral.Level <= 19)
                        {
                            Attributes.Add(FleetAttribute.WeakCloaking);
                            StaticEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.ShieldSolidity,
                                Amount = 4,
                                ModifierType = ModifierType.Absolute
                            });
                        }
                        else
                        {
                            Attributes.Add(FleetAttribute.WeakCloaking);
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
                            Attributes.Add(FleetAttribute.WeakCloaking);
                        }
                        else
                        {
                            Attributes.Add(FleetAttribute.CompleteCloaking);
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
        }

        public void ApplyComponentStaticEffects()
        {
            StaticEffects.AddRange(Devices.SelectMany(x => x.Effects).Union(Armor.Effects));
        }

        public void ApplyAttributes()
        {

            if (Owner.Traits.Any(x => x == RacialTrait.Psi))
            {
                Attributes.Add(FleetAttribute.PsiRace);
            }
            if (Owner.Traits.Any(x => x == RacialTrait.EnhancedPsi))
            {
                Attributes.Add(FleetAttribute.EnhancedPsi);
            }
            if (Owner.Traits.Any(x => x == RacialTrait.FastManeuver))
            {
                Attributes.Add(FleetAttribute.EnhancedMobility);
            }

            if (StaticEffects.Any(x => x.Type == FleetEffectType.CompleteCloaking))
            {
                Attributes.Add(FleetAttribute.CompleteCloaking);
            }
            if (StaticEffects.Any(x => x.Type == FleetEffectType.WeakCloaking))
            {
                Attributes.Add(FleetAttribute.WeakCloaking);
            }
            if (StaticEffects.Any(x => x.Type == FleetEffectType.CompleteCloakingDetection))
            {
                Attributes.Add(FleetAttribute.CompleteCloakingDetection);
            }
            if (StaticEffects.Any(x => x.Type == FleetEffectType.WeakCloakingDetection))
            {
                Attributes.Add(FleetAttribute.WeakCloakingDetection);
            }

            if (Attributes.Contains(FleetAttribute.EnhancedMobility))
            {
                StaticEffects.Add(new FleetEffect()
                {
                    Type = FleetEffectType.Speed,
                    Amount = 30,
                    ModifierType = ModifierType.Proportional
                });
            }
        }

        public void ApplyModifiers()
        {
            MoraleModifier = Owner.Race.BaseFleetEffects.Where(x => x.Type == FleetEffectType.MoraleModifier).CalculateTotalEffect(MoraleModifier, x => x.Amount);
            BerserkModifier = Owner.Race.BaseFleetEffects.Where(x => x.Type == FleetEffectType.BerserkModifier).CalculateTotalEffect(BerserkModifier, x => x.Amount);

            MoraleModifier = StaticEffects.Where(x => x.Type == FleetEffectType.MoraleModifier).CalculateTotalEffect(MoraleModifier, x => x.Amount);
            BerserkModifier = StaticEffects.Where(x => x.Type == FleetEffectType.BerserkModifier).CalculateTotalEffect(BerserkModifier, x => x.Amount);
        }

        public void Deploy(double aX, double aY, double aDirection, Command aCommand)
        {
            SetVector(aX, aY, aDirection);
            Command = aCommand;
        }

        public void Move()
        {
            Move(Speed);
        }

        public void TurnTo(Unit aUnit)
        {
            TurnTo(aUnit, Mobility);
        }
        
        public void Trace(Unit aUnit)
        {
            Fleet temp = new Fleet(this);

            temp.TurnTo(aUnit);
            int ert1 = temp.CalculateEffectiveReachTime(aUnit);
            temp.Move();
            int ert2 = temp.CalculateEffectiveReachTime(aUnit);
            temp = new Fleet(this);
            temp.Move();
            int ert3 = temp.CalculateEffectiveReachTime(aUnit);

            if (ert1 < ert2 && ert1 < ert3)
            {
                TurnTo(aUnit);
            }
            else if (ert3 < ert1 && ert3 < ert2)
            {
                Move();
            }
            else
            {
                TurnTo(aUnit);
                Move();
            }
        }

        public int CalculateEffectiveReachTime(Unit aUnit)
        {
            int ert = 0;

            double deltaDirection = DeltaDirection(aUnit);
            if (deltaDirection > 180)
            {
                deltaDirection = 360 - deltaDirection;
            }

            if (Mobility == 0.0)
            {
                ert += 1800;
            }
            else
            {
                ert += (int)(deltaDirection / Mobility);
            }

            if (Speed == 0)
            {
                ert += 1800;
            }
            else
            {
                ert += (int)(Distance(aUnit) / Speed);
            }

            return ert;
        }

        public bool IsDisabled()
        {
            if (Status == FleetStatus.Retreated || Status == FleetStatus.RetreatedThisTurn || Status == FleetStatus.Annihilated || Status == FleetStatus.AnnihilatedThisTurn)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsCloaked()
        {
            return Attributes.Contains(FleetAttribute.CompleteCloaking) || Attributes.Contains(FleetAttribute.WeakCloaking);
        }

        public long CalculatePenetrationRatio(Armada aEnemyArmada)
        {
            long penetratedPower = 0;
            long unpenetratedPower = 0;

            foreach (Fleet fleet in aEnemyArmada)
            {
                if (!fleet.IsDisabled())
                {
                    if (X > fleet.X)
                    {
                        penetratedPower += fleet.Power;
                    }
                    else
                    {
                        unpenetratedPower += fleet.Power;
                    }
                }
            }

            if (penetratedPower + unpenetratedPower == 0)
            {
                return 100;
            }
            else
            {
                return Math.Max(100 * penetratedPower / (penetratedPower + unpenetratedPower), 1);
            }
        }

        public void Engage()
        {
            mEngagementTimer = 50;
        }

        public void Disengage()
        {
            mEngagementTimer = 0;
        }

        public bool IsEngaged()
        {
            return mEngagementTimer > 0;
        }

        public void RunTurn()
        {
            if (IsDisabled())
            {
                return;
            }
            else
            {
                if (mEngagementTimer > 0)
                {
                    mEngagementTimer--;
                }

                if (!IsEngaged())
                {
                    Detected = false;
                }

                Encountered = false;
                EncounteredEnemies.Clear();

                if (TargetEnemy != null && TargetEnemy.IsDisabled())
                {
                    TargetEnemy = null;
                }

                // throw new NotImplementedException()
                // Charge periodic effects

                RechargeShields();
                RepairHulls();
                CoolWeapons();
                RecoverStatus();
            }
        }

        private void RechargeShields()
        {
            int rechargeRate = CalculateShieldRechargeRate();
            int maxShieldStrength = CalculateMaxShieldStrength();

            foreach (Ship ship in Ships)
            {
                if (ship.HP > 0)
                {
                    ship.ShieldStrength = Math.Min(ship.ShieldStrength + rechargeRate, maxShieldStrength);
                }
            }
        }

        private void RepairHulls()
        {
            if (Effects.OfType(FleetEffectType.NonRepairable).Any())
            {
                return;
            }
            else
            {
                int repairRate = CalculateRepairRate();
                int maxHp = CalculateMaxHp();

                foreach (Ship ship in Ships)
                {
                    if (ship.HP > 0)
                    {
                        ship.HP = Math.Min(ship.HP + repairRate, maxHp);
                    }
                }
            }
        }

        private void CoolWeapons()
        {
            foreach (Turret turret in Turrets)
            {
                turret.Cool();
            }
        }

        private void RecoverStatus()
        {
            StatusTurns--;
            
            if (StatusTurns <= 0 && AbnormalStatuses.Contains(Status))
            {
                Status = FleetStatus.None;
                Command = Command.Free;
            } 
        }

        public int CalculateShieldRechargeRate()
        {
            int rechargeAmount = Shield.RechargeRate[ShipClass.Class];
            rechargeAmount = Effects.OfType(FleetEffectType.ShieldRechargeRate).CalculateTotalEffect(rechargeAmount, x => x.Amount);

            return rechargeAmount;
        }

        public int CalculateMaxShieldStrength()
        {
            int maxShieldStrength = Shield.Strength[ShipClass.Class];
            maxShieldStrength = StaticEffects.Union(DynamicsEffects).Where(x => x.Type == FleetEffectType.ShieldStrength).CalculateTotalEffect(maxShieldStrength, x => x.Amount);

            return maxShieldStrength;
        }

        public int CalculateShieldSolidity()
        {
            int shieldSolidity = Shield.Deflection;
            return Effects.OfType(FleetEffectType.ShieldSolidity).CalculateTotalEffect(shieldSolidity, x => x.Amount);
        }

        public int CalculateMaxHp()
        {
            int baseHp = (int)(ShipClass.BaseHp * Armor.HpMultiplier);

            return StaticEffects.Union(DynamicsEffects).Where(x => x.Type == FleetEffectType.Hp).CalculateTotalEffect(baseHp, x => x.Amount);
        }

        public int CalculateRepairRate()
        {
            int baseRepair = Effects.OfType(FleetEffectType.Repair).CalculateTotalEffect(CalculateMaxHp(), x => x.Amount) - CalculateMaxHp();

            return Effects.OfType(FleetEffectType.RepairSpeed).CalculateTotalEffect(baseRepair, x => x.Amount);
        }

        public void ApplyDynamicEffects(Armada aAlliedArmada)
        {
            if (IsDisabled())
            {
                return;
            }
            else
            {
                if (Owner.Traits.Contains(RacialTrait.TrainedMind))
                {
                    DynamicsEffects.Add(new FleetEffect()
                    {
                        Type = FleetEffectType.NeverBerserk
                    });
                }
                if (Owner.Traits.Contains(RacialTrait.FanaticFleet))
                {
                    DynamicsEffects.Add(new FleetEffect()
                    {
                        Type = FleetEffectType.NeverRetreatRout
                    });
                }
                if (Owner.Traits.Contains(RacialTrait.FragileMindStructure))
                {
                    DynamicsEffects.Add(new FleetEffect()
                    {
                        Type = FleetEffectType.PsiDefense,
                        ModifierType = ModifierType.Proportional,
                        Amount = aAlliedArmada.CapitalFleet.IsDisabled() ? 50 : 25
                    });
                }

                switch (Status)
                {
                    case FleetStatus.Berserk:
                        {
                            DynamicsEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.Speed,
                                Amount = 20,
                                ModifierType = ModifierType.Proportional
                            });
                            DynamicsEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.Mobility,
                                Amount = -10,
                                ModifierType = ModifierType.Proportional
                            });
                            DynamicsEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.AttackRating,
                                Amount = -10,
                                ModifierType = ModifierType.Proportional
                            });
                            DynamicsEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.DefenseRating,
                                Amount = -10,
                                ModifierType = ModifierType.Proportional
                            });
                            DynamicsEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.CoolingTime,
                                Amount = -40,
                                ModifierType = ModifierType.Proportional
                            });
                        }
                        break;
                    case FleetStatus.Disorder:
                        {
                            DynamicsEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.Speed,
                                Amount = -20,
                                ModifierType = ModifierType.Proportional
                            });
                            DynamicsEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.Mobility,
                                Amount = -20,
                                ModifierType = ModifierType.Proportional
                            });
                            DynamicsEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.AttackRating,
                                Amount = -10,
                                ModifierType = ModifierType.Proportional
                            });
                            DynamicsEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.DefenseRating,
                                Amount = -10,
                                ModifierType = ModifierType.Proportional
                            });
                            DynamicsEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.CoolingTime,
                                Amount = 20,
                                ModifierType = ModifierType.Proportional
                            });
                        }
                        break;
                    case FleetStatus.Rout:
                        {
                            DynamicsEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.Speed,
                                Amount = 25,
                                ModifierType = ModifierType.Proportional
                            });
                            DynamicsEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.Mobility,
                                Amount = 25,
                                ModifierType = ModifierType.Proportional
                            });
                            DynamicsEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.DefenseRating,
                                Amount = -25,
                                ModifierType = ModifierType.Proportional
                            });
                        }
                        break;
                    case FleetStatus.Retreat:
                        {
                            DynamicsEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.Speed,
                                Amount = 10,
                                ModifierType = ModifierType.Proportional
                            });
                        }
                        break;
                    default:
                        break;
                }

                switch (Command)
                {
                    case Command.Formation:
                        {
                            DynamicsEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.DefenseRating,
                                Amount = 10,
                                ModifierType = ModifierType.Proportional
                            });
                        }
                        break;
                    case Command.Free:
                        {
                            DynamicsEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.Speed,
                                Amount = 5,
                                ModifierType = ModifierType.Proportional
                            });
                            DynamicsEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.Mobility,
                                Amount = 5,
                                ModifierType = ModifierType.Proportional
                            });
                        }
                        break;
                    case Command.StandGround:
                        {
                            DynamicsEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.AttackRating,
                                Amount = 10,
                                ModifierType = ModifierType.Proportional
                            });
                            DynamicsEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.DefenseRating,
                                Amount = 20,
                                ModifierType = ModifierType.Proportional
                            });
                        }
                        break;
                    case Command.Assault:
                        {
                            if (Substatus == FleetSubstatus.Penetration)
                            {
                                DynamicsEffects.Add(new FleetEffect()
                                {
                                    Type = FleetEffectType.Speed,
                                    Amount = 20,
                                    ModifierType = ModifierType.Proportional
                                });
                                DynamicsEffects.Add(new FleetEffect()
                                {
                                    Type = FleetEffectType.Mobility,
                                    Amount = 20,
                                    ModifierType = ModifierType.Proportional
                                });
                                DynamicsEffects.Add(new FleetEffect()
                                {
                                    Type = FleetEffectType.AttackRating,
                                    Amount = -10,
                                    ModifierType = ModifierType.Proportional
                                });
                                DynamicsEffects.Add(new FleetEffect()
                                {
                                    Type = FleetEffectType.DefenseRating,
                                    Amount = -10,
                                    ModifierType = ModifierType.Proportional
                                });
                                DynamicsEffects.Add(new FleetEffect()
                                {
                                    Type = FleetEffectType.CoolingTime,
                                    Amount = -10,
                                    ModifierType = ModifierType.Proportional
                                });
                            }
                            if (Substatus == FleetSubstatus.Charge)
                            {
                                DynamicsEffects.Add(new FleetEffect()
                                {
                                    Type = FleetEffectType.Speed,
                                    Amount = 5,
                                    ModifierType = ModifierType.Proportional
                                });
                                DynamicsEffects.Add(new FleetEffect()
                                {
                                    Type = FleetEffectType.Mobility,
                                    Amount = 5,
                                    ModifierType = ModifierType.Proportional
                                });
                                DynamicsEffects.Add(new FleetEffect()
                                {
                                    Type = FleetEffectType.AttackRating,
                                    Amount = 5,
                                    ModifierType = ModifierType.Proportional
                                });
                                DynamicsEffects.Add(new FleetEffect()
                                {
                                    Type = FleetEffectType.DefenseRating,
                                    Amount = -10,
                                    ModifierType = ModifierType.Proportional
                                });
                            }
                        }
                        break;
                    case Command.Penetrate:
                        {
                            if (Substatus == FleetSubstatus.Penetration)
                            {
                                DynamicsEffects.Add(new FleetEffect()
                                {
                                    Type = FleetEffectType.Speed,
                                    Amount = 20,
                                    ModifierType = ModifierType.Proportional
                                });
                                DynamicsEffects.Add(new FleetEffect()
                                {
                                    Type = FleetEffectType.Mobility,
                                    Amount = 20,
                                    ModifierType = ModifierType.Proportional
                                });
                                DynamicsEffects.Add(new FleetEffect()
                                {
                                    Type = FleetEffectType.AttackRating,
                                    Amount = -10,
                                    ModifierType = ModifierType.Proportional
                                });
                                DynamicsEffects.Add(new FleetEffect()
                                {
                                    Type = FleetEffectType.DefenseRating,
                                    Amount = -10,
                                    ModifierType = ModifierType.Proportional
                                });
                            }
                            if (Substatus == FleetSubstatus.Charge)
                            {
                                DynamicsEffects.Add(new FleetEffect()
                                {
                                    Type = FleetEffectType.Speed,
                                    Amount = 5,
                                    ModifierType = ModifierType.Proportional
                                });
                                DynamicsEffects.Add(new FleetEffect()
                                {
                                    Type = FleetEffectType.Mobility,
                                    Amount = 5,
                                    ModifierType = ModifierType.Proportional
                                });
                                DynamicsEffects.Add(new FleetEffect()
                                {
                                    Type = FleetEffectType.AttackRating,
                                    Amount = 5,
                                    ModifierType = ModifierType.Proportional
                                });
                                DynamicsEffects.Add(new FleetEffect()
                                {
                                    Type = FleetEffectType.DefenseRating,
                                    Amount = -10,
                                    ModifierType = ModifierType.Proportional
                                });
                            }
                        }
                        break;
                    case Command.Flank:
                        {
                            if (Substatus == FleetSubstatus.Charge)
                            {
                                DynamicsEffects.Add(new FleetEffect()
                                {
                                    Type = FleetEffectType.Speed,
                                    Amount = 5,
                                    ModifierType = ModifierType.Proportional
                                });
                                DynamicsEffects.Add(new FleetEffect()
                                {
                                    Type = FleetEffectType.Mobility,
                                    Amount = 5,
                                    ModifierType = ModifierType.Proportional
                                });
                                DynamicsEffects.Add(new FleetEffect()
                                {
                                    Type = FleetEffectType.AttackRating,
                                    Amount = 5,
                                    ModifierType = ModifierType.Proportional
                                });
                                DynamicsEffects.Add(new FleetEffect()
                                {
                                    Type = FleetEffectType.DefenseRating,
                                    Amount = -10,
                                    ModifierType = ModifierType.Proportional
                                });
                            }
                            else
                            {
                                DynamicsEffects.Add(new FleetEffect()
                                {
                                    Type = FleetEffectType.Speed,
                                    Amount = 20,
                                    ModifierType = ModifierType.Proportional
                                });
                                DynamicsEffects.Add(new FleetEffect()
                                {
                                    Type = FleetEffectType.Mobility,
                                    Amount = 20,
                                    ModifierType = ModifierType.Proportional
                                });
                                DynamicsEffects.Add(new FleetEffect()
                                {
                                    Type = FleetEffectType.AttackRating,
                                    Amount = -10,
                                    ModifierType = ModifierType.Proportional
                                });
                                DynamicsEffects.Add(new FleetEffect()
                                {
                                    Type = FleetEffectType.DefenseRating,
                                    Amount = -10,
                                    ModifierType = ModifierType.Proportional
                                });
                            }
                        }
                        break;
                    case Command.Normal:
                        {
                            DynamicsEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.Mobility,
                                Amount = -5,
                                ModifierType = ModifierType.Proportional
                            });
                            DynamicsEffects.Add(new FleetEffect()
                            {
                                Type = FleetEffectType.DefenseRating,
                                Amount = 5,
                                ModifierType = ModifierType.Proportional
                            });
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public void ApplyAreaEffects(Armada aAlliedArmada, Armada aEnemyArmada)
        {
            foreach (FleetEffect effect in StaticEffects.Where(x => x.TargetType == FleetEffectTargetType.AreaEffectTargetEnemy || x.TargetType == FleetEffectTargetType.AreaEffectTargetAll))
            {
                foreach (Fleet fleet in aEnemyArmada)
                {
                    if (fleet.IsDisabled())
                    {
                        continue;
                    }
                    else
                    {
                        if (Distance(fleet) <= effect.Range)
                        {
                            FleetEffect currentEffect = fleet.DynamicsEffects.Where(x => x.TargetType == FleetEffectTargetType.AreaLocalEffect && x.Type == effect.Type && x.Amount == effect.Amount && x.ModifierType == effect.ModifierType).SingleOrDefault();

                            if (effect == null)
                            {
                                fleet.DynamicsEffects.Add(new FleetEffect()
                                {
                                    Type = effect.Type,
                                    Amount = effect.Amount,
                                    ModifierType = effect.ModifierType,
                                    TargetType = FleetEffectTargetType.AreaLocalEffect
                                });
                            }
                        }
                    }
                }
            }

            foreach (FleetEffect effect in StaticEffects.Where(x => x.TargetType == FleetEffectTargetType.AreaEffectTargetAlly || x.TargetType == FleetEffectTargetType.AreaEffectTargetAll))
            {
                foreach (Fleet fleet in aAlliedArmada)
                {
                    if (fleet.IsDisabled())
                    {
                        continue;
                    }
                    else
                    {
                        if (Distance(fleet) <= effect.Range)
                        {
                            FleetEffect currentEffect = fleet.DynamicsEffects.Where(x => x.TargetType == FleetEffectTargetType.AreaLocalEffect && x.Type == effect.Type && x.Amount == effect.Amount && x.ModifierType == effect.ModifierType).SingleOrDefault();

                            if (effect == null)
                            {
                                fleet.DynamicsEffects.Add(new FleetEffect()
                                {
                                    Type = effect.Type,
                                    Amount = effect.Amount,
                                    ModifierType = effect.ModifierType,
                                    TargetType = FleetEffectTargetType.AreaLocalEffect
                                });
                            }
                        }
                    }
                }
            }
        }

        public void EncounterEnemyFleets(Armada aEnemyArmada)
        {
            foreach (Fleet enemyFleet in aEnemyArmada)
            {
                if (enemyFleet.IsDisabled() || !CanSee(enemyFleet))
                {
                    continue;
                }
                else
                {
                    double distance = Distance(enemyFleet);
                    double effectiveDistance = distance * (100 + (5 - enemyFleet.ShipClass.Class) * 5) / 100;

                    effectiveDistance = enemyFleet.Effects.OfType(FleetEffectType.Stealth).CalculateTotalEffect((int)effectiveDistance, x => x.Amount);

                    if (effectiveDistance < DetectionRange)
                    {
                        enemyFleet.Detected = true;
                    }

                    if (OnPath(enemyFleet))
                    {
                        Encountered = true;
                        EncounteredEnemies.Add(enemyFleet);
                    }
                }
            }
        }

        public bool CanSee(Fleet aEnemyFleet)
        {
            if (!aEnemyFleet.Attributes.Any(x => x == FleetAttribute.CompleteCloaking || x == FleetAttribute.WeakCloaking) || Attributes.Any(x => x == FleetAttribute.CompleteCloakingDetection))
            {
                return true;
            }

            if (aEnemyFleet.Attributes.Any(x => x == FleetAttribute.WeakCloaking) && Attributes.Any(x => x == FleetAttribute.WeakCloakingDetection))
            {
                return true;
            }

            return false;
        }
        
        public bool PathMeetsVerticalBorder()
        {
            return PathMeetsVerticalBorder(Path);
        }

        public bool OnPath(Unit aUnit)
        {
            return OnPath(aUnit, Path);
        }

        public bool TakeDamage(int aDamageAmount, bool aIsPsi = false, DamageDistribution aDistribution = DamageDistribution.First)
        {
            int damage = aDamageAmount;
            if (aIsPsi)
            {
                damage = Effects.OfType(FleetEffectType.PsiDefense).CalculateTotalEffect(damage, x => x.Amount);
            }

            if (aDistribution == DamageDistribution.First || aDistribution == DamageDistribution.Random)
            {
                Ship ship = null;

                if (aDistribution == DamageDistribution.First)
                {
                    ship = Ships.Where(x => x.HP > 0).FirstOrDefault();
                }
                else if (aDistribution == DamageDistribution.Random)
                {
                    ship = Ships.Where(x => x.HP > 0).RandomOrDefault();
                }

                if (ship == null)
                {
                    return false;
                }
                else
                {
                    if (ship.ShieldStrength < damage)
                    {
                        damage -= ship.ShieldStrength;
                        ship.ShieldStrength = 0;

                        ship.HP -= damage;
                        if (ship.HP < 0)
                        {
                            damage += ship.HP;
                            ship.HP = 0;
                        }
                    }
                    else
                    {
                        ship.ShieldStrength -= damage;
                    }
                }

                double moraleDrop = damage * 2 * 100.0 / (CalculateMaxHp() * MaxShipCount);
                double psiMoraleDrop = moraleDrop;

                if (aIsPsi)
                {
                    psiMoraleDrop = Effects.OfType(FleetEffectType.PsiDefense).CalculateTotalEffect(psiMoraleDrop, x => -x.Amount);

                    moraleDrop += psiMoraleDrop;
                }

                Morale -= moraleDrop;

                if (ship.HP <= 0)
                {
                    ship.HP = 0;
                    if (ActiveShipCount <= 0)
                    {
                        Status = FleetStatus.AnnihilatedThisTurn;
                    }

                    return true;
                }
            }
            
            return false;
        }

        public void RunBerserk(Armada aAlliedArmada, Armada aEnemyArmada)
        {
            if (AtBorder())
            {
                Status = FleetStatus.RetreatedThisTurn;
                return;
            }

            Fleet frontalFleet = null;
            double delta = 360;
            double lowestDelta = 360;

            foreach (Fleet fleet in aAlliedArmada.Union(aEnemyArmada).Where(x => x != this))
            {
                if (fleet.IsDisabled() || !IsInFiringRange(fleet))
                {
                    continue;
                }

                delta = DeltaDirection(fleet);

                if (delta > 180)
                {
                    delta = 360 - delta;
                }

                if (delta < lowestDelta)
                {
                    lowestDelta = delta;
                    frontalFleet = fleet;
                }
            }

            if (frontalFleet != null)
            {
                Attack(frontalFleet);
            }

            lowestDelta = 360;
            frontalFleet = null;

            foreach (Fleet fleet in aEnemyArmada)
            {
                if (fleet.IsDisabled() || fleet.TargetEnemy != this)
                {
                    continue;
                }

                delta = DeltaDirection(fleet);
                if (delta > 180)
                {
                    delta = 360 - delta;
                }

                if (delta < lowestDelta)
                {
                    lowestDelta = delta;
                    frontalFleet = fleet;
                }
            }

            if (frontalFleet != null)
            {
                TurnTo(frontalFleet);
            }

            if (frontalFleet == null || !IsInFiringRange(frontalFleet, true))
            {
                Move();
            }
        }

        public void RunDisorder(Armada aAlliedArmada, Armada aEnemyArmada)
        {
            Fleet target = FindEnemyInRange();
            if (target != null)
            {
                Attack(target);
            }

            if (Encountered)
            {
                if (RandomNumberGenerator.Next(1, 10) <= 7)
                {
                    target = FindMostDangerousEnemyEncountered();
                    if (target != null)
                    {
                        if (!IsInFiringRange(target, true))
                        {
                            Trace(target);
                        }
                        else
                        {
                            TurnTo(target);
                        }
                    }
                }
                else
                {
                    Turn(Mobility * (RandomNumberGenerator.Next(1, 3) - 2));
                }
            }
            else
            {
                if (RandomNumberGenerator.Next(1, 10) <= 7)
                {
                    Move();
                }
                else
                {
                    Turn(Mobility * (RandomNumberGenerator.Next(1, 3) - 2));
                }
            }
        }

        public void RunRetreat(Armada aAlliedArmada, Armada aEnemyArmada)
        {
            if (AtBorder())
            {
                Status = FleetStatus.RetreatedThisTurn;
                return;
            }

            Fleet target = FindEnemyInRange();

            if (target != null)
            {
                Attack(target);
            }

            if (Substatus == FleetSubstatus.TurnToBackward)
            {
                Unit rear = new Unit();

                if (aAlliedArmada.Side == Side.Offense)
                {
                    rear.SetVector(0, Y, 0);
                }
                else
                {
                    rear.SetVector(10000, Y, 0);
                }

                TurnTo(rear);
                if (IsHeadingTo(rear))
                {
                    Substatus = FleetSubstatus.None;
                }
            }
            else
            {
                Move();
            }
        }

        public void RunRout(Armada aAlliedArmada, Armada aEnemyArmada)
        {
            if (AtBorder())
            {
                Status = FleetStatus.RetreatedThisTurn;
                return;
            }

            if (Substatus == FleetSubstatus.TurnToNearestBorder)
            {
                double bottom = Y;
                double top = 10000 - Y;
                double left = X;
                double right = 10000 - X;

                Unit escape = new Unit();

                if (bottom < top && bottom < left && bottom < right)
                {
                    escape.SetVector(X, 0, 0);
                }
                else if (top < bottom && top < left && top < right)
                {
                    escape.SetVector(X, 10000, 0);
                }
                else if (left < bottom && left < top && left < right)
                {
                    escape.SetVector(0, Y, 0);
                }
                else
                {
                    escape.SetVector(10000, Y, 0);
                }

                TurnTo(escape);

                if (IsHeadingTo(escape))
                {
                    Substatus = FleetSubstatus.None;
                }
            }
            else
            {
                Move();
            }

            Rout();
        }

        public void Rout()
        {
            int alive = 0;
            int maxHp = CalculateMaxHp();
            foreach (Ship ship in Ships)
            {
                if (ship.HP < (maxHp/ 4))
                {
                    ship.HP = 0;
                }
                else
                {
                    alive++;
                }
            }

            if (alive == 0)
            {
                Status = FleetStatus.AnnihilatedThisTurn;
            }
        }

        public void RunPanic(Armada aAlliedArmada, Armada aEnemyArmada)
        {
            
        }

        public void RunFormation(Armada aAlliedArmada, Armada aEnemyArmada)
        {
            Fleet target;

            if (aAlliedArmada.FormationStatus == FormationStatus.None)
            {
                Move(aAlliedArmada.CalculateFormationSpeed());
            }
            else if (aAlliedArmada.FormationStatus == FormationStatus.Disband)
            {
                Command = Command.Free;
            }
            else
            {
                target = FindEnemyInRange();
                if (target != null)
                {
                    Attack(target);
                }

                if (Encountered)
                {
                    target = FindMostDangerousEnemyEncountered();
                    if (target != null)
                    {
                        if (!IsInFiringRange(target, true))
                        {
                            Trace(target);
                        }
                        else
                        {
                            TurnTo(target);
                        }
                    }
                }
                else
                {
                    target = FindClosestEngagedEnemyWithinRange(aEnemyArmada, 2000);
                    if (target != null)
                    {
                        Trace(target);
                    }
                }
            }
        }

        public void RunPenetrate(Armada aAlliedArmada, Armada aEnemyArmada)
        {
            Fleet target = null;

            if (Substatus == FleetSubstatus.Penetration)
            {
                target = FindEnemyInRange();
                if (target != null && !IsCloaked())
                {
                    Attack(target);
                }

                if (CalculatePenetrationRatio(aEnemyArmada) < 80)
                {
                    Move();
                }
                else
                {
                    Substatus = FleetSubstatus.Charge;
                }
            }
            else if (Substatus == FleetSubstatus.Charge)
            {
                target = FindEnemyInRange();
                if (target != null)
                {
                    Attack(target);
                }

                target = FindClosestEnemy(aEnemyArmada);
                if (target != null)
                {
                    if (!IsInFiringRange(target, true))
                    {
                        Trace(target);
                    }
                    else
                    {
                        TurnTo(target);
                    }
                }
                else
                {
                    Move();
                }

                if (PathMeetsBorder(Path))
                {
                    Command = Command.Free;
                }
            }
        }

        public void RunFlank(Armada aAlliedArmada, Armada aEnemyArmada)
        {
            Fleet target = null;

            if (Substatus == FleetSubstatus.MoveStraight)
            {
                Move();
                if (PathMeetsBorder(Path))
                {
                    Substatus = FleetSubstatus.TurnToForward;
                }
            }
            else if (Substatus == FleetSubstatus.TurnToForward)
            {
                Unit forward = new Unit();
                if (Direction > 90 && Direction < 270)
                {
                    forward.SetVector(0, Y, 0);
                }
                else
                {
                    forward.SetVector(10000, Y, 0);
                }

                TurnTo(forward);
                if (IsHeadingTo(forward))
                {
                    Substatus = FleetSubstatus.Penetration;
                }
            }
            else if (Substatus == FleetSubstatus.Penetration)
            {
                target = FindEnemyInRange();
                if (target != null && !target.IsCloaked())
                {
                    Attack(target);
                }

                if (CalculatePenetrationRatio(aEnemyArmada) < 80)
                {
                    Move();
                }
                else
                {
                    Substatus = FleetSubstatus.Charge;
                }
            }
            else if (Substatus == FleetSubstatus.Charge)
            {
                target = FindEnemyInRange();
                if (target != null)
                {
                    Attack(target);
                }

                target = FindClosestEnemy(aEnemyArmada);
                if (target != null)
                {
                    if (!IsInFiringRange(target, true))
                    {
                        Trace(target);
                    }
                    else
                    {
                        TurnTo(target);
                    }
                }
                else
                {
                    Move();
                }

                if (PathMeetsBorder(Path))
                {
                    Command = Command.Free;
                }
            }
        }

        public void RunAssault(Armada aAlliedArmada, Armada aEnemyArmada)
        {
            Fleet target = null;

            if (Substatus == FleetSubstatus.Penetration)
            {
                target = FindEnemyInRange();
                if (target != null)
                {
                    Attack(target);
                }

                if (CalculatePenetrationRatio(aEnemyArmada) < 40)
                {
                    Move();
                }
                else
                {
                    Substatus = FleetSubstatus.Charge;
                }
            }
            else if (Substatus == FleetSubstatus.Charge)
            {
                target = FindEnemyInRange();
                if (target != null)
                {
                    Attack(target);
                }

                target = FindClosestEnemy(aEnemyArmada);
                if (target != null)
                {
                    if (!IsInFiringRange(target, true))
                    {
                        Trace(target);
                    }
                    else
                    {
                        TurnTo(target);
                    }
                }
                else
                {
                    Move();
                }

                if (PathMeetsBorder(Path))
                {
                    Command = Command.Free;
                }
            }
        }

        public void RunStandGround(Armada aAlliedArmada, Armada aEnemyArmada)
        {
            Fleet target = null;

            if (Encountered)
            {
                target = FindEnemyInRange();

                if (target != null)
                {
                    Attack(target);
                }

                target = FindMostDangerousEnemyEncountered();
                if (target != null)
                {
                    TurnTo(target);
                }
            }
            else
            {
                target = FindClosestEngagedEnemyWithinRange(aEnemyArmada, 500);
                if (target != null)
                {
                    Trace(target);
                    Command = Command.Free;
                }
            }
        }

        public void RunNormal(Armada aAlliedArmada, Armada aEnemyArmada)
        {
            if (Encountered || CalculatePenetrationRatio(aEnemyArmada) >= 100 || PathMeetsBorder(Path))
            {
                Command = Command.Free;
            }
            else
            {
                Fleet target = FindClosestEngagedEnemyWithinRange(aEnemyArmada, 1000);
                if (target == null)
                {
                    Move();
                }
                else
                {
                    Trace(target);
                }
            }
        }

        public void RunFree(Armada aAlliedArmada, Armada aEnemyArmada)
        {
            Fleet target = null;
            Unit centre = new Unit(5000, 5000);

            if (Encountered)
            {
                target = FindEnemyInRange();
                if (target != null)
                {
                    Attack(target);
                }

                target = FindMostDangerousEnemyEncountered();
                if (target != null)
                {
                    if (!IsInFiringRange(target, true))
                    {
                        Trace(target);
                    }
                    else
                    {
                        TurnTo(target);
                    }
                }

                Substatus = FleetSubstatus.None;
            }
            else
            {
                target = FindClosestEngagedEnemyWithinRange(aEnemyArmada);
                if (target != null)
                {
                    Trace(target);
                    Substatus = FleetSubstatus.None;
                }
                else if (Substatus == FleetSubstatus.TurningToCentre)
                {
                    TurnTo(centre);

                    if (IsHeadingTo(centre))
                    {
                        Substatus = FleetSubstatus.None;
                        Move();
                    }
                }
                else if (PathMeetsBorder(Path))
                {
                    Substatus = FleetSubstatus.TurningToCentre;
                    TurnTo(centre);
                }
                else
                {
                    Move();
                }
            }
        }

        public void Attack(Fleet aEnemy)
        {
            Decloak();
            aEnemy.Decloak();

            foreach (Turret turret in Turrets)
            {
                if (turret.IsReady())
                {
                    Fire(turret, aEnemy);
                }

                if (aEnemy.Status == FleetStatus.AnnihilatedThisTurn)
                {
                    break;
                }
            }
        }

        public void StartCooldown(Turret aTurret)
        {
            int cooldown = aTurret.CoolingTime;
            cooldown = (int)(cooldown * (100 + Morale / -5 + 10 + Experience / -5 + 10) / 100);

            cooldown = Effects.OfType(FleetEffectType.CoolingTime).CalculateTotalEffect(cooldown, x => x.Amount);

            switch (aTurret.Type)
            {
                case WeaponType.Beam:
                    cooldown = Effects.OfType(FleetEffectType.BeamCoolingTime).CalculateTotalEffect(cooldown, x => x.Amount);
                    break;
                case WeaponType.Missile:
                    cooldown = Effects.OfType(FleetEffectType.MissileCoolingTime).CalculateTotalEffect(cooldown, x => x.Amount);
                    break;
                case WeaponType.Projectile:
                    cooldown = Effects.OfType(FleetEffectType.ProjectileCoolingTime).CalculateTotalEffect(cooldown, x => x.Amount);
                    break;
                default:
                    break;
            }

            aTurret.RemainingCooldown = cooldown;
        }

        public void Fire(Turret aTurret, Fleet aEnemy)
        {
            if (!aTurret.IsReady())
            {
                return;
            }

            Engage();
            aEnemy.Engage();

            int attackRating;
            int defenseRating;
            int hitChance;

            attackRating = CalculateAttackRating(aTurret);
            defenseRating = aEnemy.CalculateDefenseRating(aTurret);

            if (defenseRating > attackRating)
            {
                hitChance = attackRating * 100 / defenseRating / 2;
            }
            else
            {
                hitChance = 100 - (defenseRating * 100 / attackRating / 2);
            }

            if (hitChance < 5)
            {
                hitChance = 5;
            }
            else if (hitChance > 95)
            {
                hitChance = 95;
            }

            Battle.Record.AddFireEvent(this, aEnemy, aTurret, hitChance);

            int critChance = 0;

            double sideDirection = aEnemy.DeltaDirection(this);

            if (sideDirection > 180)
            {
                sideDirection = 360 - sideDirection;
            }

            if (sideDirection >= 0 && sideDirection <= 45)
            {
                critChance = 5;
            }
            else if (sideDirection >45 && sideDirection < 135)
            {
                critChance = 10;
            }
            else
            {
                critChance = 20;
            }

            critChance = aTurret.Effects.OfType(FleetEffectType.ArmorPiercing).CalculateTotalEffect(critChance, x => x.Amount);
            critChance = Effects.OfType(FleetEffectType.CriticalHit).CalculateTotalEffect(critChance, x => x.Amount);
            critChance = Effects.OfType(FleetEffectType.ImpenetrableArmor).CalculateTotalEffect(critChance, x => -x.Amount);

            DamageEnemyResult result = DamageEnemyFleet(aTurret, aEnemy, hitChance, critChance);

            KilledShips += result.SunkenCount;
            if (aEnemy.IsDisabled())
            {
                KilledFleets++;
            }

            Battle.Record.AddHitEvent(this, aEnemy, result.HitCount, result.MissCount, result.TotalDamage, result.SunkenCount);

            StartCooldown(aTurret);
        }

        public bool IsInFiringRange(Fleet aEnemy, bool aIgnoreCooldown = false)
        {
            if (!aEnemy.Detected || aEnemy.IsDisabled())
            {
                return false;
            }
            else
            {
                if (Turrets.Where(x => (x.IsReady() || aIgnoreCooldown) && IsInRange(aEnemy, x.Range, x.AngleOfFire / 2)).Any())
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public DamageEnemyResult DamageEnemyFleet(Turret aTurret, Fleet aEnemy, int aHitChance, int aCritChance)
        {
            int missCount = 0;
            int totalDamage = 0;
            int sunkenCount = 0;
            int hitCount = 0;
            int activeShipCount = ActiveShipCount;
            int numberOfShipsToHit = 0;
            
            if (RandomNumberGenerator.Next(1, Admiral.Efficiency) > 100)
            {
                numberOfShipsToHit = aEnemy.ActiveShipCount / 2;
            }
            else if (RandomNumberGenerator.Next(1, Admiral.Efficiency) > 90)
            {
                numberOfShipsToHit = (int)((double)aEnemy.ActiveShipCount * 0.75);
            }
            else if (RandomNumberGenerator.Next(1, Admiral.Efficiency) > 80)
            {
                numberOfShipsToHit = (int)((double)aEnemy.ActiveShipCount * 0.90);
            }
            else
            {
                numberOfShipsToHit = aEnemy.ActiveShipCount;
            }

            if (numberOfShipsToHit < 1 || numberOfShipsToHit >= aEnemy.ActiveShipCount)
            {
                numberOfShipsToHit = 0;
            }

            for (int i = 0; i < activeShipCount; i++)
            {
                if (aEnemy.IsDisabled())
                {
                    break;
                }

                bool ignorePsi = false;
                if (aEnemy.Effects.OfType(FleetEffectType.PsiNeutralizationField).Any() && aTurret.Effects.OfType(FleetEffectType.Psi).Any())
                {
                    if (RandomNumberGenerator.Next(1, 100) <= aEnemy.Effects.OfType(FleetEffectType.PsiNeutralizationField).CalculateTotalEffect(0, x => x.Amount))
                    {
                        ignorePsi = true;
                    }
                }

                if (RandomNumberGenerator.Next(1, 100) <= aHitChance && ignorePsi == false)
                {
                    int damage = aTurret.Number * RandomNumberGenerator.Dice(aTurret.DamageRoll, aTurret.DamageDice);

                    switch (aTurret.Type)
                    {
                        case WeaponType.Beam:
                            damage = Effects.OfType(FleetEffectType.BeamDamage).CalculateTotalEffect(damage, x => x.Amount);
                            damage = aEnemy.Effects.OfType(FleetEffectType.BeamDefense).CalculateTotalEffect(damage, x => -x.Amount);
                            break;
                        case WeaponType.Missile:
                            damage = Effects.OfType(FleetEffectType.MissileDamage).CalculateTotalEffect(damage, x => x.Amount);
                            damage = aEnemy.Effects.OfType(FleetEffectType.MissileDefense).CalculateTotalEffect(damage, x => -x.Amount);
                            break;
                        case WeaponType.Projectile:
                            damage = Effects.OfType(FleetEffectType.ProjectileDamage).CalculateTotalEffect(damage, x => x.Amount);
                            damage = aEnemy.Effects.OfType(FleetEffectType.ProjectileDefense).CalculateTotalEffect(damage, x => -x.Amount);
                            break;
                    }

                    damage = aEnemy.Effects.OfType(FleetEffectType.GenericDefense).CalculateTotalEffect(damage, x => -x.Amount);
                    damage = aEnemy.Effects.OfType(FleetEffectType.ChainReaction).CalculateTotalEffect(damage, x => x.Amount);

                    if (Owner.Traits.Contains(RacialTrait.EnhancedPsi) && aTurret.Effects.OfType(FleetEffectType.Psi).Any())
                    {
                        damage = (int)(damage * 1.2);
                    }

                    int shieldPierceChance;
                    bool shieldDistorted;
                    bool shieldPierced;

                    shieldPierceChance = aTurret.Effects.OfType(FleetEffectType.ShieldPiercing).CalculateTotalEffect(0, x => x.Amount);
                    shieldPierceChance = aEnemy.Effects.OfType(FleetEffectType.ImpenetrableShield).CalculateTotalEffect(shieldPierceChance, x => -x.Amount);

                    if (RandomNumberGenerator.Next(1, 100) < shieldPierceChance)
                    {
                        shieldPierced = true;
                    }
                    else
                    {
                        shieldPierced = false;
                    }

                    if (RandomNumberGenerator.Next(1, 100) < aTurret.Effects.OfType(FleetEffectType.ShieldDistortion).CalculateTotalEffect(0, x => x.Amount))
                    {
                        shieldDistorted = true;
                    }
                    else
                    {
                        shieldDistorted = false;
                    }

                    int shieldSolidity = aEnemy.CalculateShieldSolidity();
                    if (shieldPierced)
                    {
                        shieldSolidity /= 2;
                    }

                    if (damage <= 0)
                    {
                        missCount++;
                        continue;
                    }

                    // This seems to be way more complicated for what it's trying to do. Prime target for refactor.
                    int index = -1;
                    int pq;

                    index = numberOfShipsToHit;

                    for (pq = numberOfShipsToHit; pq < aEnemy.ActiveShipCount; pq++)
                    {
                        if (index < 1 || index >= aEnemy.ActiveShipCount)
                        {
                            index = 0;
                        }
                        if (aEnemy.Ships[index].HP < 1)
                        {
                            index++;
                        }
                        else
                        {
                            index = pq;
                        }
                    }

                    if (index < 1 || index >= aEnemy.ActiveShipCount)
                    {
                        index = 0;
                    }

                    if (aEnemy.Ships[index].HP < 1)
                    {
                        for (pq = numberOfShipsToHit; pq > 0; pq--)
                        {
                            if (aEnemy.Ships[index].HP < 1)
                            {
                                index--;
                            }
                            else
                            {
                                index = pq;
                            }
                        }
                    }

                    if (index < 1 || index >= aEnemy.ActiveShipCount)
                    {
                        index = 0;
                    }

                    numberOfShipsToHit--;

                    int realDamage = 0;

                    if (shieldDistorted)
                    {
                        int bioBonus = aTurret.Effects.OfType(FleetEffectType.AdditionalDamageToBioArmor).CalculateTotalEffect(0, x => x.Amount);

                        if (aEnemy.Armor.Type == ArmorType.Bio)
                        {
                            damage = (100 + bioBonus) * damage / 100;
                        }

                        if (RandomNumberGenerator.Next(1, 100) < aCritChance)
                        {
                            damage = (int)(2.0 * aEnemy.Armor.HpMultiplier * damage);
                        }

                        if (damage < aEnemy.Ships[index].HP)
                        {
                            totalDamage += damage;
                            realDamage = damage;
                        }
                        else
                        {
                            realDamage = aEnemy.Ships[index].HP;
                            totalDamage += realDamage;
                        }

                        aEnemy.Ships[index].HP -= damage;
                    }
                    else
                    {
                        if (shieldPierced)
                        {
                            damage *= 2;
                        }

                        if (aTurret.Effects.OfType(FleetEffectType.ShieldOverheat).Any())
                        {
                            damage *= 3;
                        }

                        if ((double)aEnemy.Ships[index].ShieldStrength / aEnemy.CalculateMaxShieldStrength() > 0.1)
                        {
                            damage -= (int)(damage * 0.03 * aEnemy.CalculateShieldSolidity());
                        }

                        if (aEnemy.Ships[index].ShieldStrength < damage)
                        {
                            damage -= aEnemy.Ships[index].ShieldStrength;
                            totalDamage += aEnemy.Ships[index].ShieldStrength;
                            aEnemy.Ships[index].ShieldStrength = 0;

                            if (shieldPierced)
                            {
                                damage /= 2;
                            }

                            if (aTurret.Effects.OfType(FleetEffectType.ShieldOverheat).Any())
                            {
                                damage /= 3;
                            }

                            int bioBonus = aTurret.Effects.OfType(FleetEffectType.AdditionalDamageToBioArmor).CalculateTotalEffect(0, x => x.Amount);

                            if (aEnemy.Armor.Type == ArmorType.Bio)
                            {
                                damage = (100 + bioBonus) * damage / 100;
                            }

                            if (RandomNumberGenerator.Next(1, 100) < aCritChance)
                            {
                                damage = (int)(2.0 * aEnemy.Armor.HpMultiplier * damage);
                            }

                            if (damage < aEnemy.Ships[index].HP)
                            {
                                totalDamage += damage;
                                realDamage = damage;
                            }
                            else
                            {
                                realDamage = aEnemy.Ships[index].HP;
                                totalDamage += realDamage;
                            }

                            aEnemy.Ships[index].HP -= damage;
                        }
                        else
                        {
                            totalDamage += damage;
                            aEnemy.Ships[index].ShieldStrength -= damage;
                        }
                    }

                    if (aEnemy.Ships[index].HP <= 0)
                    {
                        long experience = 0;
                        Morale++;
                        aEnemy.Morale--;
                        experience += aEnemy.ShipClass.Class * aEnemy.ShipClass.Class * 3;
                        if (aEnemy.ActiveShipCount <= 0)
                        {
                            experience += aEnemy.Power;
                            aEnemy.Status = FleetStatus.AnnihilatedThisTurn;
                        }

                        if (aEnemy.IsCapital)
                        {
                            experience *= 3;
                        }

                        Admiral.GainedExperience += experience;
                        sunkenCount++;
                    }

                    hitCount++;
                }
                else
                {
                    missCount++;
                }
            }

            double moraleDrop = totalDamage * 100 / aEnemy.CalculateMaxHp();

            if (aTurret.Effects.OfType(FleetEffectType.Psi).Any())
            {
                double psiMoraleDrop = moraleDrop;

                psiMoraleDrop = Effects.OfType(FleetEffectType.PsiAttack).CalculateTotalEffect(psiMoraleDrop, x => x.Amount);
                psiMoraleDrop = aEnemy.Effects.OfType(FleetEffectType.PsiDefense).CalculateTotalEffect(psiMoraleDrop, x => -x.Amount);

                moraleDrop += psiMoraleDrop * 2;
            }

            aEnemy.Morale -= moraleDrop;

            Morale += (totalDamage * 100 / CalculateMaxHp()) / 3;

            return new DamageEnemyResult(totalDamage, hitCount, missCount, sunkenCount);
        }

        public int CalculateAttackRating(Turret aTurret)
        {
            int computerAttackRating = Computer.AttackRating;
            int turretAttackRating = aTurret.AttackRating;

            computerAttackRating = Effects.OfType(FleetEffectType.Computer).CalculateTotalEffect(computerAttackRating, x => x.Amount);
            turretAttackRating = Effects.OfType(FleetEffectType.WeaponAttackRating).CalculateTotalEffect(turretAttackRating, x => x.Amount);

            switch (aTurret.Type)
            {
                case WeaponType.Beam:
                    turretAttackRating = Effects.OfType(FleetEffectType.BeamAttackRating).CalculateTotalEffect(turretAttackRating, x => x.Amount);
                    break;
                case WeaponType.Missile:
                    turretAttackRating = Effects.OfType(FleetEffectType.MissileAttackRating).CalculateTotalEffect(turretAttackRating, x => x.Amount);
                    break;
                case WeaponType.Projectile:
                    turretAttackRating = Effects.OfType(FleetEffectType.ProjectileAttackRating).CalculateTotalEffect(turretAttackRating, x => x.Amount);
                    break;
            }

            int result = turretAttackRating * (100 + computerAttackRating + Experience) / 100;

            result = Effects.OfType(FleetEffectType.AttackRating).CalculateTotalEffect(result, x => x.Amount);

            return result;
        }

        public int CalculateBaseDefenseRating()
        {
            int armorDefenseRating = Armor.DefenseRating;
            int computerDefenseRating = Computer.DefenseRating;
            int mobilityBase = (int)(50.0 * Mobility);

            computerDefenseRating = Effects.OfType(FleetEffectType.Computer).CalculateTotalEffect(computerDefenseRating, x => x.Amount);
            armorDefenseRating = Effects.OfType(FleetEffectType.ArmorDefenseRating).CalculateTotalEffect(armorDefenseRating, x => x.Amount);

            int defenseRating = (mobilityBase + armorDefenseRating) * (100 + computerDefenseRating + Experience) / 100;

            defenseRating = Effects.OfType(FleetEffectType.DefenseRating).CalculateTotalEffect(defenseRating, x => x.Amount);

            return defenseRating;
        }

        public int CalculateDefenseRating(Turret aTurret)
        {
            int defenseRating = CalculateBaseDefenseRating();

            switch (aTurret.Type)
            {
                case WeaponType.Beam:
                    defenseRating = Effects.OfType(FleetEffectType.DefenseRatingAgainstBeam).CalculateTotalEffect(defenseRating, x => x.Amount);
                    break;
                case WeaponType.Missile:
                    defenseRating = Effects.OfType(FleetEffectType.DefenseRatingAgainstMissile).CalculateTotalEffect(defenseRating, x => x.Amount);
                    break;
                case WeaponType.Projectile:
                    defenseRating = Effects.OfType(FleetEffectType.DefenseRatingAgainstProjectile).CalculateTotalEffect(defenseRating, x => x.Amount);
                    break;
            }

            return defenseRating;
        }

        public void Decloak()
        {
            Attributes.Remove(FleetAttribute.CompleteCloaking);
            Attributes.Remove(FleetAttribute.WeakCloaking);
        }

        public Fleet FindEnemyInRange()
        {
            return EncounteredEnemies.Where(x => IsInFiringRange(x)).RandomOrDefault();
        }

        public Fleet FindMostDangerousEnemyEncountered()
        {
            return EncounteredEnemies.OrderByDescending(x => CalculateDangerRating(x)).FirstOrDefault();
        }

        public Fleet FindClosestEnemy(Armada aEnemyArmada)
        {
            return aEnemyArmada.Where(x => !x.IsDisabled()).OrderBy(x => CalculateEffectiveReachTime(x)).FirstOrDefault();
        }

        public Fleet FindClosestEngagedEnemyWithinRange(Armada aEnemyArmada, double aRange = 10000)
        {
            return aEnemyArmada.Where(x => !x.IsDisabled() && x.IsEngaged() && Distance(x) <= aRange).OrderBy(x => CalculateEffectiveReachTime(x)).FirstOrDefault();
        }

        public long CalculateDangerRating(Fleet aEnemy)
        {
            long result = 0;
            int distance = (int)Distance(aEnemy);

            if (distance == 0)
            {
                result = aEnemy.Power;
            }
            else
            {
                result = aEnemy.Power / distance;
            }

            if (result <= 0)
            {
                result = 1;
            }

            if (TargetEnemy == aEnemy || aEnemy.TargetEnemy == this)
            {
                result *= 2;
            }

            int misinterpretChance = aEnemy.Effects.OfType(FleetEffectType.Misinterpret).CalculateTotalEffect(100, x => x.Amount) - 100;

            if (RandomNumberGenerator.Next(1, 100) <= misinterpretChance)
            {
                int severity = 24 + RandomNumberGenerator.Next(1, 376);
                result = result * severity / 100;
            }

            return result;
        }
    }
}
