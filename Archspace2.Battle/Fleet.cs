using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Universal.Common.Extensions;

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
        public Player Owner { get; set; }
        public List<Ship> Ships { get; set; }

        public Command Command { get; set; }
        public FleetStatus Status { get; set; }
        public FleetSubstatus Substatus { get; set; }
        public FleetMorale MoraleStatus { get; set; }
        
        public double Morale { get; set; }
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

        public Admiral Admiral { get; set; }
        public ShipClass ShipClass { get; set; }

        public Armor Armor { get; set; }
        public Engine Engine { get; set; }
        public Computer Computer { get; set; }
        public Shield Shield { get; set; }
        public List<Device> Devices { get; set; }
        public List<Turret> Turrets { get; set; }

        public int ShieldSolidity { get; set; }

        public bool Detected { get; set; }
        protected int mEngagementTimer { get; set; }
        public bool Encountered { get; set; }

        public Fleet TargetEnemy { get; set; }
        public List<Fleet> EncounteredEnemies { get; set; }
        
        public HashSet<FleetAttribute> Attributes { get; set; }

        public int HP
        {
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

                return result / 5000.0;
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
            count = count * 100 / (MaxShipCount * CalculateMaxHp());

            return count;
        }

        public int Power { get; set; }

        public bool IsCapital { get; set; }

        public List<FleetEffect> StaticEffects { get; set; }
        public List<FleetEffect> DynamicsEffects { get; set; }

        public Fleet()
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

            StatusTurns = 0;

            IsCapital = false;
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

        public int CalculatePenetrationRatio(Armada aEnemyArmada)
        {
            int penetratedPower = 0; 
            int unpenetratedPower = 0;

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
            if (StaticEffects.Union(DynamicsEffects).Any(x => x.Type ==  FleetEffectType.NonRepairable))
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

        public int CalculateShieldRechargeRate()
        {
            int rechargeAmount = Shield.RechargeRate[ShipClass.Class];
            rechargeAmount = StaticEffects.Union(DynamicsEffects).Where(x => x.Type == FleetEffectType.ShieldRechargeRate).CalculateTotalEffect(rechargeAmount, x => x.Amount.Value);

            return rechargeAmount;
        }

        public int CalculateMaxShieldStrength()
        {
            int maxShieldStrength = Shield.Strength[ShipClass.Class];
            maxShieldStrength = StaticEffects.Union(DynamicsEffects).Where(x => x.Type == FleetEffectType.ShieldStrength).CalculateTotalEffect(maxShieldStrength, x => x.Amount.Value);

            return maxShieldStrength;
        }

        public int CalculateMaxHp()
        {
            int baseHp = (int)(ShipClass.BaseHp * Armor.HpMultiplier);

            return StaticEffects.Union(DynamicsEffects).Where(x => x.Type == FleetEffectType.Hp).CalculateTotalEffect(baseHp, x => x.Amount.Value);
        }

        public int CalculateRepairRate()
        {
            int baseRepair = StaticEffects.Union(DynamicsEffects).Where(x => x.Type == FleetEffectType.Repair).CalculateTotalEffect(CalculateMaxHp(), x => x.Amount.Value) - CalculateMaxHp();

            return StaticEffects.Union(DynamicsEffects).Where(x => x.Type == FleetEffectType.RepairSpeed).CalculateTotalEffect(baseRepair, x => x.Amount.Value);
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
                    double effectiveDistance = distance * (100 + (5 - enemyFleet.ShipClass.Class * 5) / 100);

                    effectiveDistance = enemyFleet.StaticEffects.Union(enemyFleet.DynamicsEffects).Where(x => x.Type == FleetEffectType.Stealth).CalculateTotalEffect((int)effectiveDistance, x => x.Amount.Value);

                    if (effectiveDistance < DetectionRange)
                    {
                        enemyFleet.Detected = true;
                    }

                    if (OnPath(enemyFleet))
                    {
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

        public bool OnPath(Unit aUnit)
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

            return OnPath(aUnit, leftX, rightX, topY, bottomY);
        }

        public bool TakeDamage(int aDamageAmount, bool aIsPsi = false, DamageDistribution aDistribution = DamageDistribution.First)
        {
            int damage = aDamageAmount;
            if (aIsPsi)
            {
                damage = StaticEffects.Union(DynamicsEffects).Where(x => x.Type == FleetEffectType.PsiDefense).CalculateTotalEffect(damage, x => x.Amount.Value);
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
                    psiMoraleDrop = psiMoraleDrop + StaticEffects.Union(DynamicsEffects).Where(x => x.Type == FleetEffectType.PsiDefense).CalculateTotalEffect(-psiMoraleDrop, x => x.Amount.Value);

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

        public RecordFleet ToRecordFleet()
        {
            RecordFleet result = new RecordFleet();

            return result;
        }
    }
}
