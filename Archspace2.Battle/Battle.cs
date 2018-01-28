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

    public class Battle
    {
        private static Random mRandom;

        static Battle()
        {
            mRandom = new Random();
        }

        public static Random Random { get => mRandom; }

        protected bool mCompleted;
        public int CurrentTurn { get; protected set; }
        
        public BattleType Type { get; set; }

        public Player Attacker { get; set; }
        public Player Defender { get; set; }

        public Armada AttackingFleets { get; protected set; }
        public Armada DefendingFleets { get; protected set; }

        public Battlefield Battlefield { get; protected set; }

        public Record Record { get; set; }
        
        public Battle(BattleType aBattleType, Player aAttacker, Player aDefender, Battlefield aBattlefield, Armada aAttackingFleets, Armada aDefendingFleets)
        {
            CurrentTurn = 0;
            Type = aBattleType;
            Attacker = aAttacker;
            Defender = aDefender;

            Battlefield = aBattlefield;
            
            AttackingFleets = aAttackingFleets;
            DefendingFleets = aDefendingFleets;

            if (Battlefield != null)
            {
                Battlefield.Battle = this;
            }

            if (AttackingFleets != null)
            {
                AttackingFleets.Battle = this;
                AttackingFleets.Side = Side.Offense;
                foreach (Fleet fleet in AttackingFleets)
                {
                    fleet.Battle = this;
                    fleet.Armada = AttackingFleets;
                }

                AttackingFleets.ApplyArmadaStaticEffects();
            }

            if (DefendingFleets != null)
            {
                DefendingFleets.Battle = this;
                DefendingFleets.Side = Side.Defense;
                foreach (Fleet fleet in DefendingFleets)
                {
                    fleet.Battle = this;
                    fleet.Armada = DefendingFleets;
                }

                DefendingFleets.ApplyArmadaStaticEffects();
            }

            Record = new Record(this, Attacker, Defender, Type, Battlefield, AttackingFleets, DefendingFleets);
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
                fleet.RunTurn();
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
                int damage = fleet.StaticEffects.Union(fleet.DynamicsEffects).Where(x => x.Type == FleetEffectType.DamageOverTime).CalculateTotalEffect(0, x => x.Amount);
                int psiDamage = fleet.StaticEffects.Union(fleet.DynamicsEffects).Where(x => x.Type == FleetEffectType.PsiDamageOverTime).CalculateTotalEffect(0, x => x.Amount);

                if (damage > 0)
                {
                    fleet.TakeDamage(damage, false, DamageDistribution.Random);
                }
                if (psiDamage > 0)
                {
                    fleet.TakeDamage(psiDamage, true, DamageDistribution.Random);
                }
            }

            AttackingFleets.RunTurn(DefendingFleets);
            DefendingFleets.RunTurn(AttackingFleets);

            int attackerMoraleUp = 0;
            int defenderMoraleUp = 0;
            int attackerMoraleCapitalDown = 0;
            int defenderMoraleCapitalDown = 0;
            int attackerMoraleFleetDown = 0;
            int defenderMoraleFleetDown = 0;

            foreach (Fleet fleet in AttackingFleets)
            {
                int moraleDown = 0;
                int moraleUp = 0;

                if (fleet.Status == FleetStatus.AnnihilatedThisTurn || fleet.Status == FleetStatus.RetreatedThisTurn)
                {
                    if (fleet.IsCapital)
                    {
                        attackerMoraleCapitalDown = -25;
                        moraleUp = 15;
                    }
                    else
                    {
                        int fleetPower = fleet.Power;
                        int totalPower = AttackingFleets.Sum(x => x.Power);
                        moraleDown = -((100 * fleetPower / totalPower));
                        moraleUp = -(moraleDown / 2);

                        if (moraleDown < -15)
                        {
                            moraleDown = -15;
                        }
                        if (moraleUp > 10)
                        {
                            moraleUp = 10;
                        }
                    }

                    if (fleet.Status == FleetStatus.AnnihilatedThisTurn)
                    {
                        fleet.Status = FleetStatus.Annihilated;
                    }
                    else
                    {
                        fleet.Status = FleetStatus.Retreated;
                    }

                    Record.AddFleetDisabledEvent(fleet);

                    defenderMoraleUp += moraleUp;
                    attackerMoraleFleetDown += moraleDown;
                }
            }

            foreach (Fleet fleet in DefendingFleets)
            {
                int moraleDown = 0;
                int moraleUp = 0;

                if (fleet.Status == FleetStatus.AnnihilatedThisTurn || fleet.Status == FleetStatus.RetreatedThisTurn)
                {
                    if (fleet.IsCapital)
                    {
                        defenderMoraleCapitalDown = -25;
                        moraleUp = 15;
                    }
                    else
                    {
                        int fleetPower = fleet.Power;
                        int totalPower = DefendingFleets.Sum(x => x.Power);
                        moraleDown = -((100 * fleetPower / totalPower));
                        moraleUp = -(moraleDown / 2);

                        if (moraleDown < -15)
                        {
                            moraleDown = -15;
                        }
                        if (moraleUp > 10)
                        {
                            moraleUp = 10;
                        }
                    }

                    if (fleet.Status == FleetStatus.AnnihilatedThisTurn)
                    {
                        fleet.Status = FleetStatus.Annihilated;
                    }
                    else
                    {
                        fleet.Status = FleetStatus.Retreated;
                    }

                    Record.AddFleetDisabledEvent(fleet);

                    attackerMoraleUp += moraleUp;
                    defenderMoraleFleetDown += moraleDown;
                }
            }

            AttackingFleets.UpdateMorale(attackerMoraleUp, attackerMoraleCapitalDown, attackerMoraleFleetDown);
            DefendingFleets.UpdateMorale(defenderMoraleUp, defenderMoraleCapitalDown, defenderMoraleFleetDown);

            CurrentTurn++;
        }
    }
}
