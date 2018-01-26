using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    public class Fleet : Unit
    {
        public Player Owner { get; set; }
        public List<Ship> Ships { get; set; }

        public Command Command { get; set; }
        public FleetStatus Status { get; set; }
        public FleetSubstatus Substatus { get; set; }
        public FleetMorale MoraleStatus { get; set; }

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

        public Admiral Admiral { get; set; }
        public ShipClass ShipClass { get; set; }

        public Armor Armor { get; set; }
        public Engine Engine { get; set; }
        public Computer Computer { get; set; }
        public Shield Shield { get; set; }
        public List<Device> Devices { get; set; }
        public List<Turret> Turrets { get; set; }

        public int RedZoneRadius { get; set; }

        public int ShieldSolidity { get; set; }
        public int MaxShieldStrength { get; set; }
        public int ShieldRechargeRate { get; set; }

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
            count = count * 100 / (MaxShipCount * MaxHP);

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
            RedZoneRadius = 0;

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
            }
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

        public RecordFleet ToRecordFleet()
        {
            RecordFleet result = new RecordFleet();

            return result;
        }
    }
}
