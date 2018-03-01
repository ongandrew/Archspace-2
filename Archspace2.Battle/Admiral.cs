using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2.Battle
{
    public class Admiral : NamedEntity
    {
        public int Level { get; set; }
        public AdmiralSpecialAbility SpecialAbility { get; set; }
        public AdmiralRacialAbility RacialAbility { get; set; }
        public ArmadaClass ArmadaClass { get; set; }
        public int Efficiency { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }
        public AdmiralSkills Skills { get; set; }

        public long GainedExperience { get; set; }

        public Admiral(int aId, string aName, int aLevel, AdmiralSpecialAbility aSpecialAbility, AdmiralRacialAbility aRacialAbility, int aEfficiency, int aAttack, int aDefense, AdmiralSkills aSkills, ArmadaClass aArmadaClass)
        {
            Id = aId;
            Name = aName;
            Level = aLevel;
            SpecialAbility = aSpecialAbility;
            RacialAbility = aRacialAbility;
            Efficiency = aEfficiency;
            Attack = aAttack;
            Defense = aDefense;
            Skills = aSkills;

            GainedExperience = 0;
        }

        public int CalculateArmadaCommanderSkillBonus(BattleType aBattleType, Side aSide)
        {
            switch (aSide)
            {
                case Side.Offense:
                    {
                        switch (aBattleType)
                        {
                            case BattleType.Siege:
                                return Skills.SiegePlanet / 4;
                            case BattleType.Blockade:
                                return Skills.Blockade / 4;
                            case BattleType.Privateer:
                                return Skills.Privateer / 4;
                            case BattleType.Raid:
                                return Skills.Raid / 4;
                            default:
                                return 0;
                        }
                    }
                case Side.Defense:
                    {
                        switch (aBattleType)
                        {
                            case BattleType.Siege:
                                return Skills.SiegeRepel / 4;
                            case BattleType.Blockade:
                                return Skills.BreakBlockade / 4;
                            case BattleType.Privateer:
                                return Skills.Privateer / 4;
                            case BattleType.Raid:
                                return Skills.PreventRaid / 4;
                            default:
                                return 0;
                        }
                    }
                default:
                    return 0;
            }
        }

        public int CalculateArmadaCommanderEfficiencyBonus()
        {
            switch (ArmadaClass)
            {
                case ArmadaClass.A:
                    return 10;
                case ArmadaClass.B:
                    return 5;
                case ArmadaClass.C:
                    return 0;
                case ArmadaClass.D:
                    return -5;
                default:
                    return -10;
            }
        }
    }
}
