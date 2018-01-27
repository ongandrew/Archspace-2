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
        protected bool mCompleted;
        public int CurrentTurn { get; protected set; }
        
        public BattleType Type { get; set; }

        public Player Attacker { get; set; }
        public Player Defender { get; set; }

        public Armada AttackingFleets { get; set; }
        public Armada DefendingFleets { get; set; }

        public Battlefield Battlefield { get; set; }

        public Record Record { get; set; }
        
        public Simulation(BattleType aBattleType, Player aAttacker, Player aDefender, Battlefield aBattlefield, Armada aAttaackingFleets, Armada aDefendingFleets)
        {
            CurrentTurn = 0;
            Type = aBattleType;
            Attacker = aAttacker;
            Defender = aDefender;

            Battlefield = aBattlefield;

            AttackingFleets = aAttaackingFleets;
            DefendingFleets = aDefendingFleets;

            Record = new Record(Attacker, Defender, Type, Battlefield, AttackingFleets, DefendingFleets);
        }

        public bool IsComplete()
        {
            return mCompleted;
        }

        public void Run()
        {
            while (!mCompleted)
            {
                RunTurn();
            }
        }

        public void RunTurn()
        {
            if (CurrentTurn > 1800 || AttackingFleets.TrueForAll(x => x.IsDisabled()) || DefendingFleets.TrueForAll(x => x.IsDisabled()))
            {
                mCompleted = true;
            }

            Record.BattleOccurred = true;
            
            foreach (Fleet fleet in AttackingFleets.Union(DefendingFleets))
            {
                fleet.DynamicsEffects.Clear();
            }

            foreach (Fleet fleet in AttackingFleets)
            {
                fleet.ApplyDynamicEffects(AttackingFleets);
                fleet.ApplyAreaEffects(AttackingFleets, DefendingFleets);
            }

            foreach (Fleet fleet in DefendingFleets)
            {
                fleet.ApplyDynamicEffects(DefendingFleets);
                fleet.ApplyAreaEffects(DefendingFleets, AttackingFleets);
            }

            foreach (Fleet fleet in AttackingFleets)
            {
                fleet.EncounterEnemyFleets(DefendingFleets);
            }

            foreach (Fleet fleet in DefendingFleets)
            {
                fleet.EncounterEnemyFleets(AttackingFleets);
            }

            foreach (Fleet fleet in AttackingFleets.Union(DefendingFleets))
            {
                int damage = fleet.StaticEffects.Union(fleet.DynamicsEffects).Where(x => x.Type == FleetEffectType.DamageOverTime).CalculateTotalEffect(0, x => x.Amount.Value);
                int psiDamage = fleet.StaticEffects.Union(fleet.DynamicsEffects).Where(x => x.Type == FleetEffectType.PsiDamageOverTime).CalculateTotalEffect(0, x => x.Amount.Value);

                if (damage > 0)
                {
                    fleet.TakeDamage(damage, false, DamageDistribution.Random);
                }
                if (psiDamage > 0)
                {
                    fleet.TakeDamage(psiDamage, true, DamageDistribution.Random);
                }
            }
        }
    }
}
