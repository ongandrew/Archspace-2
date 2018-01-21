using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archspace2
{
    public enum FormationStatus
    {
        None,
        Disband,
        Encounter,
        Reformation
    };

    public class BattleGroup : List<BattleFleet>
    {
        public Player Owner { get; set; }
        public Side Side { get; set; }
        public FormationStatus Status { get; set; }
        public int Speed { get; set; }

        public BattleFleet CapitalFleet { get; set; }
        public Formation Formation { get; set; }

        public int Power
        {
            get
            {
                return this.Sum(x => x.Power);
            }
        }

        public BattleGroup(Player aOwner, Side aSide) : base()
        {
            Side = aSide;
            Owner = aOwner;
            Formation = new Formation();
        }

        public void AutoDeploy(List<Fleet> aFleets)
        {

        }

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

        public void InitializeBonuses(BattleType aBattleType, Side aSide)
        {
            Admiral capitalAdmiral = this.Where(x => x.IsCapital).Single().Admiral;

            int skill = capitalAdmiral.CalculateArmadaCommanderSkillBonus(aBattleType, aSide);
            int efficiency = capitalAdmiral.ArmadaCommanderEfficiencyBonus;

            foreach (BattleFleet fleet in this)
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

        public void DeployStationedFleets(Planet aPlanet)
        {
            throw new NotImplementedException();
        }

        private bool Validate()
        {
            // throw new NotImplementedException();
            return true;
        }
    }
}
