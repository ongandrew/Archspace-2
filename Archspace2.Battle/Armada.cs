using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archspace2.Battle
{
    public class Armada : List<Fleet>
    {
        public Player Owner { get; set; }
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
