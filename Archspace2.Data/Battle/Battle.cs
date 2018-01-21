using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archspace2
{
    public enum BattleType
    {
        Siege,
        Privateer,
        Raid,
        Blockade,

        Magistrate,
        MagistrateCounterattack,
        EmpirePlanet,
        EmpirePlanetCounterattack,
        Fortress,
        EmpireCapitalPlanet
    };

    public enum Side
    {
        Offense,
        Defense
    };

    public class Battle
    {
        public int Turn { get; set; }
        public BattleType Type { get; set; }

        public Player Attacker { get; set; }
        public Player Defender { get; set; }

        public BattleGroup AttackingFleets { get; set; }
        public BattleGroup DefendingFleets { get; set; }

        public Planet Battlefield { get; set; }

        public BattleRecord Record { get; set; }

        public Battle(BattleType aBattleType, Player aAttacker, Player aDefender, Planet aBattlefield, DefensePlan aOffensePlan, DefensePlan aDefensePlan)
        {
            Turn = 0;
            Type = aBattleType;
            Attacker = aAttacker;
            Defender = aDefender;

            Battlefield = aBattlefield;

            AttackingFleets = new BattleGroup(aAttacker, Side.Offense);
            DefendingFleets = new BattleGroup(aDefender, Side.Defense);

            InitializeBattleFleets(aOffensePlan, aDefensePlan);

            Record = new BattleRecord(Attacker, Defender, Type, Battlefield, AttackingFleets, DefendingFleets);
        }

        public void InitializeBattleFleets(DefensePlan aOffensePlan, DefensePlan aDefensePlan)
        {
            if (aOffensePlan == null)
            {
                AttackingFleets.AutoDeploy(aOffensePlan.Player.Fleets.ToList());
            }
            else
            {
                AttackingFleets.DeployByPlan(aOffensePlan);
            }

            if (aDefensePlan == null)
            {
                DefendingFleets.AutoDeploy(aDefensePlan.Player.Fleets.ToList());
            }
            else
            {
                DefendingFleets.DeployByPlan(aDefensePlan);
            }

            if (DefendingFleets.Any())
            {
                DefendingFleets.DeployAlliedFleets();
                //DefendingFleets.DeployStationedFleets(Battlefield);
            }
            else
            {
                return;
            }

            AttackingFleets.InitializeBonuses(Type, Side.Offense);
            DefendingFleets.InitializeBonuses(Type, Side.Defense);
        }

        private void AddAdmiralBonuses()
        {

        }
    }
}
