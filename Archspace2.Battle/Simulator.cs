using System;
using System.Collections.Generic;

namespace Archspace2.Battle
{
    public enum Side
    {
        Offense,
        Defense
    };

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

    public class Simulator
    {
        protected int Turn { get; set; }
        public BattleType Type { get; set; }

        public Player Attacker { get; set; }
        public Player Defender { get; set; }

        public Armada AttackingFleets { get; set; }
        public Armada DefendingFleets { get; set; }

        public Battlefield Battlefield { get; set; }

        public Record Record { get; set; }

        public Simulator(BattleType aBattleType, Player aAttacker, Player aDefender, Battlefield aBattlefield, List<Deployment> aAttackerInitialDeployments, List<Deployment> aDefenderInitialDeployment)
        {
            Turn = 0;
            Type = aBattleType;
            Attacker = aAttacker;
            Defender = aDefender;

            Battlefield = aBattlefield;

            AttackingFleets = new Armada(aAttacker, Side.Offense);
            DefendingFleets = new Armada(aDefender, Side.Defense);

            InitializeBattleFleets(aAttackerInitialDeployments, aDefenderInitialDeployment);

            Record = new Record(Attacker, Defender, Type, Battlefield, AttackingFleets, DefendingFleets);
        }
        
        public void InitializeBattleFleets(List<Deployment> aAttackerDeployments, List<Deployment> aDefenderDeployments)
        {
            foreach (Deployment deployment in aAttackerDeployments)
            {
                deployment.Deploy();
            }

            foreach (Deployment deployment in aDefenderDeployments)
            {
                deployment.Deploy();
            }

            //AttackingFleets.InitializeBonuses(Type, Side.Offense);
            //DefendingFleets.InitializeBonuses(Type, Side.Defense);
        }
    }
}
