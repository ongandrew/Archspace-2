using System;
using System.Collections.Generic;
using System.Linq;

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

    public class Simulation
    {
        protected int Turn { get; set; }
        public BattleType Type { get; set; }

        public Player Attacker { get; set; }
        public Player Defender { get; set; }

        public Armada AttackingFleets { get; set; }
        public Armada DefendingFleets { get; set; }

        public Battlefield Battlefield { get; set; }

        public Record Record { get; set; }

        public Simulation(BattleType aBattleType, Player aAttacker, Player aDefender, Battlefield aBattlefield, List<Deployment> aAttackerInitialDeployments, List<Deployment> aDefenderInitialDeployment)
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
                deployment.Deploy(this);
            }

            foreach (Deployment deployment in aDefenderDeployments)
            {
                deployment.Deploy(this);
            }

            //AttackingFleets.InitializeBonuses(Type, Side.Offense);
            //DefendingFleets.InitializeBonuses(Type, Side.Defense);
        }

        public void Run()
        {


        }

        private void RunTurn()
        {
            if (Turn > 1800 || AttackingFleets.TrueForAll(x => x.IsDisabled()) || DefendingFleets.TrueForAll(x => x.IsDisabled()))
            {

            }

            Record.BattleOccurred = true;
            
            foreach (Fleet fleet in AttackingFleets.Union(DefendingFleets))
            {
                fleet.DynamicsEffects.Clear();
            }

            foreach (Fleet fleet in AttackingFleets.Union(DefendingFleets))
            {
                fleet.ApplyDynamicEffects(AttackingFleets, DefendingFleets);
            }
        }
    }
}
