using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archspace2.Battle
{
    public enum FormationStatus
    {
        None,
        Disband,
        Encounter,
        Reformation
    };

    public class Armada : List<Fleet>
    {
        public Battle Battle { get; set; }
        public Player Owner { get; set; }
        public FormationStatus FormationStatus { get; set; }
        public Side Side { get; set; }

        public Fleet CapitalFleet
        {
            get
            {
                return this.Single(x => x.IsCapital == true);
            }
        }

        public Armada(Player aOwner) : base()
        {
            Owner = aOwner;
        }
        
        public void AutoDeploy(List<Fleet> aFleets)
        {

        }

        public int CalculateFormationSpeed()
        {
            int result = 10000;

            foreach (Fleet fleet in this)
            {
                if (fleet.Command == Command.Formation && !fleet.IsDisabled() && fleet.Speed < result)
                {
                    result = fleet.Speed;
                }
            }

            return result;
        }

        public void RunTurn(Armada aEnemyArmada)
        {
            FormationStatus newStatus = FormationStatus.None;

            foreach (Fleet fleet in this)
            {
                if (fleet.IsDisabled() || fleet.Command != Command.Formation)
                {
                    continue;
                }

                if (fleet.PathMeetsVerticalBorder())
                {
                    newStatus = FormationStatus.Disband;
                    break;
                }
                if (fleet.Encountered)
                {
                    newStatus = FormationStatus.Encounter;
                    break;
                }
            }

            FormationStatus = newStatus;

            foreach (Fleet fleet in this)
            {
                if (fleet.IsDisabled())
                {
                    continue;
                }
                else
                {
                    if (fleet.Status == FleetStatus.Berserk)
                    {
                        fleet.RunBerserk(this, aEnemyArmada);
                    }
                    else if (fleet.Status == FleetStatus.Disorder)
                    {
                        fleet.RunDisorder(this, aEnemyArmada);
                    }
                    else if (fleet.Status == FleetStatus.Panic)
                    {
                        fleet.RunPanic(this, aEnemyArmada);
                    }
                    else if (fleet.Status == FleetStatus.Rout)
                    {
                        fleet.RunRout(this, aEnemyArmada);
                    }
                    else if (fleet.Status == FleetStatus.Retreat)
                    {
                        fleet.RunRetreat(this, aEnemyArmada);
                    }
                    else if (fleet.Command == Command.Formation)
                    {
                        fleet.RunFormation(this, aEnemyArmada);
                    }
                    else if (fleet.Command == Command.Free)
                    {
                        fleet.RunFree(this, aEnemyArmada);
                    }
                    else if (fleet.Command == Command.StandGround)
                    {
                        fleet.RunStandGround(this, aEnemyArmada);
                    }
                    else if (fleet.Command == Command.Assault)
                    {
                        fleet.RunAssault(this, aEnemyArmada);
                    }
                    else if (fleet.Command == Command.Penetrate)
                    {
                        fleet.RunPenetrate(this, aEnemyArmada);
                    }
                    else if (fleet.Command == Command.Flank)
                    {
                        fleet.RunFlank(this, aEnemyArmada);
                    }
                    else if (fleet.Command == Command.Normal)
                    {
                        fleet.RunNormal(this, aEnemyArmada);
                    }
                    else
                    {
                    }
                }

                if (Battle.CurrentTurn % 10 == 0)
                {
                    Battle.Record.AddMovementEvent(fleet);
                }
            }
        }

        public void UpdateMorale(double aMoraleUp, double aCapitalMorale, double aFleetMorale)
        {
            foreach (Fleet fleet in this)
            {
                if (fleet.IsDisabled())
                {
                    continue;
                }

                double moraleUp = aMoraleUp;
                if (fleet.Admiral.RacialAbility == AdmiralRacialAbility.LoneWolf)
                {
                    if (fleet.Admiral.Level <= 12)
                    {
                        moraleUp += (aCapitalMorale / 2) + aFleetMorale;
                    }
                    else if (fleet.Admiral.Level <= 17)
                    {
                        moraleUp += aFleetMorale;
                    }
                }
                else
                {
                    moraleUp += aCapitalMorale + aFleetMorale;
                }

                fleet.Morale += moraleUp;

                FleetMorale previousStatus = fleet.MoraleStatus;

                int weakMoraleBreak = 75 + fleet.MoraleModifier;
                int normalMoraleBreak = 50 + fleet.MoraleModifier;
                int completeMoraleBreak = 25 + fleet.MoraleModifier;

                if (fleet.Morale < completeMoraleBreak)
                {
                    fleet.MoraleStatus = FleetMorale.CompleteBreak;
                    if (previousStatus < FleetMorale.NormalBreak)
                    {
                        if (Battle.Random.Next(1, 100) <= 50)
                        {
                            if (fleet.Effects.OfType(FleetEffectType.NeverRetreatRout).Any())
                            {
                                break;
                            }
                            else
                            {
                                fleet.Status = FleetStatus.Rout;
                            }
                        }
                        else
                        {
                            fleet.Status = FleetStatus.Panic;
                            fleet.StatusTurns = 100 - (int)fleet.Morale;
                        }
                    }
                    else if (previousStatus == FleetMorale.NormalBreak)
                    {
                        if (Battle.Random.Next(1, 100) <= 50)
                        {
                            if (fleet.Effects.OfType(FleetEffectType.NeverRetreatRout).Any())
                            {
                                break;
                            }
                            else
                            {
                                fleet.Status = FleetStatus.Rout;
                            }
                        }
                        else
                        {
                            if (Battle.Random.Next(1, 50) <= 20 + fleet.BerserkModifier)
                            {
                                if (fleet.Effects.OfType(FleetEffectType.NeverBerserk).Any())
                                {
                                    break;
                                }
                                else
                                {
                                    fleet.Status = FleetStatus.Berserk;
                                    fleet.StatusTurns = 100 - (int)fleet.Morale;
                                }
                            }
                            else
                            {
                                fleet.Status = FleetStatus.Disorder;
                                fleet.StatusTurns = 200 - (int)fleet.Morale - fleet.Efficiency;
                            }
                        }
                    }
                }
                else if (fleet.Morale < normalMoraleBreak)
                {
                    fleet.MoraleStatus = FleetMorale.NormalBreak;
                    if (previousStatus == FleetMorale.Normal)
                    {
                        if (Battle.Random.Next(1, 100) <= 50)
                        {
                            fleet.Status = FleetStatus.Panic;
                            fleet.StatusTurns = 100 - (int)fleet.Morale;
                        }
                        else
                        {
                            if (Battle.Random.Next(1, 50) <= 20 + fleet.BerserkModifier)
                            {
                                if (fleet.Effects.OfType(FleetEffectType.NeverBerserk).Any())
                                {
                                    break;
                                }
                                else
                                {
                                    fleet.Status = FleetStatus.Berserk;
                                    fleet.StatusTurns = 100 - (int)fleet.Morale;
                                }
                            }
                            else
                            {
                                fleet.Status = FleetStatus.Disorder;
                                fleet.StatusTurns = 200 - (int)fleet.Morale - fleet.Efficiency;
                            }
                        }
                    }
                    else if (previousStatus == FleetMorale.WeakBreak)
                    {
                        if (Battle.Random.Next(1, 100) <= 40)
                        {
                            if (fleet.Effects.OfType(FleetEffectType.NeverRetreatRout).Any())
                            {
                                break;
                            }
                            else
                            {
                                fleet.Status = FleetStatus.Retreat;
                            }
                        }
                        else
                        {
                            if (Battle.Random.Next(1, 60) <= 10 + fleet.BerserkModifier)
                            {
                                if (fleet.Effects.OfType(FleetEffectType.NeverBerserk).Any())
                                {
                                    break;
                                }
                                else
                                {
                                    fleet.Status = FleetStatus.Berserk;
                                    fleet.StatusTurns = 100 - (int)fleet.Morale;
                                }
                            }
                            else
                            {
                                fleet.Status = FleetStatus.Disorder;
                                fleet.StatusTurns = 200 - (int)fleet.Morale - fleet.Efficiency;
                            }
                        }
                    }
                }
                else if (fleet.Morale < weakMoraleBreak)
                {
                    fleet.MoraleStatus = FleetMorale.WeakBreak;
                    if (previousStatus == FleetMorale.Normal)
                    {
                        if (Battle.Random.Next(1, 100) <= 80)
                        {
                            fleet.Command = Command.Free;
                        }
                        else
                        {
                            fleet.Status = FleetStatus.Disorder;
                            fleet.StatusTurns = 200 - (int)fleet.Morale - fleet.Efficiency;
                        }
                    }
                }
                else
                {
                    fleet.MoraleStatus = FleetMorale.Normal;
                }

                if (fleet.MoraleStatus < previousStatus)
                {
                    fleet.Command = Command.Free;
                    fleet.Status = FleetStatus.None;
                }
            }
        }

        /*
        public void DeployByPlan(DefensePlan aDefensePlan)
        {
            foreach (DefenseDeployment deployment in aDefensePlan.DefenseDeployments)
            {
                Add(new BattleFleet(deployment));
            }

            if (!Validate())
            {
                AutoDeploy(aDefensePlan.Player.Fleets.ToList());
            }
        }

        public void DeployAlliedFleets()
        {
            List<Fleet> alliedFleets = Owner.GetAlliedFleets().Take(Game.Configuration.Battle.MaxAlliedFleets).ToList();

            if (alliedFleets.Any())
            {
                int interval = Game.Configuration.Battle.MaxY / (2 * alliedFleets.Count);

                for (int i = 0; i < alliedFleets.Count; i++)
                {
                    BattleFleet battleFleet = new BattleFleet(Owner, alliedFleets[i]);

                    battleFleet.SetVector((int)(Game.Configuration.Battle.MaxX * 0.92), (Game.Configuration.Battle.MaxY / 4) + interval * (i + 1), 180);

                    Add(battleFleet);
                }
            }
        }
        */
        /*
        public void AddArmadaCommanderBonuses(BattleType aBattleType, Side aSide)
        {
            Admiral capitalAdmiral = this.Where(x => x.IsCapital).Single().Admiral;

            int skill = capitalAdmiral.CalculateArmadaCommanderSkillBonus(aBattleType, aSide);
            int efficiency = capitalAdmiral.ArmadaCommanderEfficiencyBonus;

            foreach (Fleet fleet in this)
            {
                int totalSkill = aSide == Side.Offense ? fleet.Admiral.Attack + skill : fleet.Admiral.Defense + skill;
                int totalEfficiency = efficiency + fleet.Admiral.Efficiency;

                fleet.StaticEffects.Add(new FleetEffect()
                {
                    Type = aSide == Side.Offense ? FleetEffectType.AttackRating : FleetEffectType.DefenseRating,
                    Amount = totalSkill * 3,
                    ModifierType = ModifierType.Proportional
                });

                fleet.StaticEffects.Add(new FleetEffect()
                {
                    Type = FleetEffectType.Efficiency,
                    Amount = totalEfficiency,
                    ModifierType = ModifierType.Absolute
                });

                fleet.StaticEffects.Add(new FleetEffect()
                {
                    Type = FleetEffectType.Speed,
                    Amount = fleet.Admiral.Skills.Maneuver,
                    ModifierType = ModifierType.Proportional
                });

                fleet.StaticEffects.Add(new FleetEffect()
                {
                    Type = FleetEffectType.Mobility,
                    Amount = fleet.Admiral.Skills.Maneuver,
                    ModifierType = ModifierType.Proportional
                });
            }
        }
        */
        /*
        public void DeployStationedFleets(Planet aPlanet)
        {
            throw new NotImplementedException();
        }

        private bool Validate()
        {
            // throw new NotImplementedException();
            return true;
        }
        */
    }
}
