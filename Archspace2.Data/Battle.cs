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

    public class Battle
    {
        public int Turns { get; set; }
        public BattleType Type { get; set; }

        public Player Attacker { get; set; }
        public Player Defender { get; set; }

        public List<BattleFleet> AttackingFleets { get; set; }
        public List<BattleFleet> DefendingFleets { get; set; }

        public Planet Battlefield { get; set; }

        public void InitializeBattleFleets(DefensePlan aOffensePlan, DefensePlan aDefensePlan)
        {
            if (aOffensePlan == null)
            {

            }

        }
    }
}
