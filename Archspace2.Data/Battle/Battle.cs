using System;
using System.Collections.Generic;
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

        public Battle(BattleType aBattleType, Player aAttacker, Player aDefender, Planet aBattlefield)
        {
            Turn = 0;
            Type = aBattleType;
            Attacker = aAttacker;
            Defender = aDefender;

            Battlefield = aBattlefield;

            AttackingFleets = new BattleGroup(aAttacker, Side.Offense);
            DefendingFleets = new BattleGroup(aDefender, Side.Defense);

            Record = new BattleRecord(Attacker, Defender, Type, Battlefield);
        }

        public void InitializeBattleFleets(DefensePlan aOffensePlan, DefensePlan aDefensePlan)
        {
            if (aOffensePlan == null)
            {

            }

        }

        public void AutoDeploy(List<Fleet> aFleets, Side aSide)
        {
            int fleetCount = 0;
            foreach (Fleet fleet in aFleets)
            {
                if (fleetCount >= 20)
                {
                    break;
                }

                if (fleet.Status != FleetStatus.StandBy)
                {
                    continue;
                }
            }
        }
    }
}
