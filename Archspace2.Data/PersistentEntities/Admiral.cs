﻿using Archspace2.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Universal.Common.Extensions;

namespace Archspace2
{
    public enum ArmadaClass
    {
        A = 0,
        B,
        C,
        D
    };

    public enum StartingCircumstance
    {
        Supercommander,
        Excellent,
        VeryGood,
        Good,
        Average,
        Poor,
        Bad,
        VeryBad,
        CannonFodder
    };

    public enum AdmiralSpecialAbility
    {
        EngineeringSpecialist,
        ShieldSystemSpecialist,
        MissileSpecialist,
        BallisticExpert,
        EnergySystemSpecialist
    };

    public enum AdmiralRacialAbility
    {
        IrrationalTactics,
        Intuition,
        LoneWolf,
        DnaPoisonReplicater,
        BreederMale,
        ClonalDouble,
        XenophobicFanatic,
        MentalGiant,
        ArtifactCrystal,
        PsychicProgenitor,
        ArtifactCoolingEngine,
        LyingDormant,
        MissileCraters,
        MeteorDrones,
        CyberScanUnit,
        TrajectoryAugmentation,
        PatternBroadcaster,
        FamousPrivateer,
        CommerceKing,
        RetreatShield,
        GeneticThrowback,
        RigidThinking,
        ManagementProtocol,
        Blitzkreig,
        TacticalGenius,
        ShieldDisrupter,
        DefensiveMatrix
    }

    [Table("Admiral")]
    public class Admiral : UniverseEntity
    {
        public int? PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }

        public int RaceId { get; set; }
        [NotMapped]
        public Race Race
        {
            get
            {
                return Game.Configuration.Races.Single(x => x.Id == RaceId);
            }
            set
            {
                RaceId = value.Id;
            }
        }

        public StartingCircumstance StartingCircumstance { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public AdmiralSpecialAbility SpecialAbility { get; set; }
        public AdmiralRacialAbility RacialAbility { get; set; }

        public ArmadaClass ArmadaClass { get; set; }
        public int BaseFleetCapacity { get; set; }
        public int FleetCapacity
        {
            get
            {
                return BaseFleetCapacity;
            }
        }
        public int BaseEfficiency { get; set; }
        public int Efficiency
        {
            get
            {
                int result = BaseEfficiency;

                if (RacialAbility == AdmiralRacialAbility.ClonalDouble)
                {
                    if (Level <= 7)
                    {
                        result -= 10;
                    }
                    else if (Level <= 13)
                    {
                        result -= 5;
                    }
                }
                else if (RacialAbility == AdmiralRacialAbility.PsychicProgenitor)
                {
                    if (Level <= 12)
                    {
                        result -= 10;
                    }
                    else if (Level <= 17)
                    {
                        result -= 5;
                    }
                }
                else if (RacialAbility == AdmiralRacialAbility.RetreatShield)
                {
                    result -= Level * 2;
                }
                else if (RacialAbility == AdmiralRacialAbility.BreederMale)
                {
                    result += Level / 2;
                }

                if (result > 100)
                {
                    result = 0;
                }

                return result;
            }
        }

        public int ArmadaCommanderEfficiencyBonus
        {
            get
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
        
        public AdmiralSkills BaseSkills { get; set; }
        [NotMapped]
        public AdmiralSkills Skills
        {
            get
            {
                AdmiralSkills result = new AdmiralSkills();
                result.Bind(BaseSkills);

                if (Player != null)
                {
                    #region SiegePlanet
                    if (RacialAbility == AdmiralRacialAbility.XenophobicFanatic)
                    {
                        if (Level <= 4)
                        {
                            result.SiegePlanet++;
                        }
                        else if (Level <= 9)
                        {
                            result.SiegePlanet += 2;
                        }
                        else if (Level <= 14)
                        {
                            result.SiegePlanet += 3;
                        }
                        else if (Level <= 18)
                        {
                            result.SiegePlanet += 4;
                        }
                        else
                        {
                            result.SiegePlanet += 5;
                        }
                    }
                    else if (RacialAbility == AdmiralRacialAbility.BreederMale)
                    {
                        if (Level <= 12)
                        {
                            result.SiegePlanet++;
                        }
                        else
                        {
                            result.SiegePlanet += 2;
                        }
                    }
                    #endregion
                    #region Blockade
                    if (RacialAbility == AdmiralRacialAbility.XenophobicFanatic)
                    {
                        if (Level <= 4)
                        {
                            result.Blockade++;
                        }
                        else if (Level <= 9)
                        {
                            result.Blockade += 2;
                        }
                        else if (Level <= 14)
                        {
                            result.Blockade += 3;
                        }
                        else if (Level <= 18)
                        {
                            result.Blockade += 4;
                        }
                        else
                        {
                            result.Blockade += 5;
                        }
                    }
                    else if (RacialAbility == AdmiralRacialAbility.BreederMale)
                    {
                        if (Level <= 12)
                        {
                            result.Blockade++;
                        }
                        else
                        {
                            result.Blockade += 2;
                        }
                    }
                    #endregion
                    #region Raid
                    if (RacialAbility == AdmiralRacialAbility.XenophobicFanatic)
                    {
                        if (Level <= 4)
                        {
                            result.Raid++;
                        }
                        else if (Level <= 9)
                        {
                            result.Raid += 2;
                        }
                        else if (Level <= 14)
                        {
                            result.Raid += 3;
                        }
                        else if (Level <= 18)
                        {
                            result.Raid += 4;
                        }
                        else
                        {
                            result.Raid += 5;
                        }
                    }
                    else if (RacialAbility == AdmiralRacialAbility.BreederMale)
                    {
                        if (Level <= 12)
                        {
                            result.Raid++;
                        }
                        else
                        {
                            result.Raid += 2;
                        }
                    }
                    #endregion
                    #region Privateer
                    if (RacialAbility == AdmiralRacialAbility.XenophobicFanatic)
                    {
                        if (Level <= 4)
                        {
                            result.Privateer++;
                        }
                        else if (Level <= 9)
                        {
                            result.Privateer += 2;
                        }
                        else if (Level <= 14)
                        {
                            result.Privateer += 3;
                        }
                        else if (Level <= 18)
                        {
                            result.Privateer += 4;
                        }
                        else
                        {
                            result.Privateer += 5;
                        }
                    }
                    else if (RacialAbility == AdmiralRacialAbility.BreederMale)
                    {
                        if (Level <= 12)
                        {
                            result.Privateer++;
                        }
                        else
                        {
                            result.Privateer += 2;
                        }
                    }
                    else if (RacialAbility == AdmiralRacialAbility.LoneWolf)
                    {
                        if (Level <= 4)
                        {
                            result.Privateer++;
                        }
                        else if (Level <= 8)
                        {
                            result.Privateer += 2;
                        }
                        else if (Level <= 12)
                        {
                            result.Privateer += 3;
                        }
                        else if (Level <= 17)
                        {
                            result.Privateer += 4;
                        }
                        else if (Level <= 19)
                        {
                            result.Privateer += 5;
                        }
                        else
                        {
                            result.Privateer += 7;
                        }
                    }
                    else if (RacialAbility == AdmiralRacialAbility.FamousPrivateer)
                    {
                        if (Level <= 6)
                        {
                            result.Privateer += 3;
                        }
                        else if (Level <= 13)
                        {
                            result.Privateer += 4;
                        }
                        else if (Level <= 19)
                        {
                            result.Privateer += 5;
                        }
                        else
                        {
                            result.Privateer += 7;
                        }
                    }
                    #endregion

                    #region SiegeRepel
                    if (RacialAbility == AdmiralRacialAbility.BreederMale)
                    {
                        if (Level <= 12)
                        {
                            result.SiegeRepel++;
                        }
                        else
                        {
                            result.SiegeRepel += 2;
                        }
                    }
                    #endregion
                    #region BreakBlockade
                    if (RacialAbility == AdmiralRacialAbility.BreederMale)
                    {
                        if (Level <= 12)
                        {
                            result.BreakBlockade++;
                        }
                        else
                        {
                            result.BreakBlockade += 2;
                        }
                    }
                    #endregion
                    #region PreventRaid
                    if (RacialAbility == AdmiralRacialAbility.BreederMale)
                    {
                        if (Level <= 12)
                        {
                            result.PreventRaid++;
                        }
                        else
                        {
                            result.PreventRaid += 2;
                        }
                    }
                    #endregion

                    #region Maneuver
                    if (RacialAbility == AdmiralRacialAbility.BreederMale)
                    {
                        if (Level <= 12)
                        {
                            result.Maneuver++;
                        }
                        else
                        {
                            result.Maneuver += 2;
                        }
                    }
                    else if (RacialAbility == AdmiralRacialAbility.IrrationalTactics)
                    {
                        if (Level <= 7)
                        {
                            result.Maneuver -= 2;
                        }
                        else if (Level <= 19)
                        {
                            result.Maneuver--;
                        }
                    }
                    else if (RacialAbility == AdmiralRacialAbility.MeteorDrones)
                    {
                        if (Level <= 7)
                        {
                            result.Maneuver -= 1;
                        }
                        else if (Level <= 13)
                        {
                            result.Maneuver -= 2;
                        }
                        else if (Level <= 19)
                        {
                            result.Maneuver -= 3;
                        }
                        else
                        {
                            result.Maneuver -= 4;
                        }
                    }
                    else if (RacialAbility == AdmiralRacialAbility.Blitzkreig)
                    {
                        if (Level <= 5)
                        {
                            result.Maneuver += 2;
                        }
                        else if (Level <= 9)
                        {
                            result.Maneuver += 3;
                        }
                        else if (Level <= 12)
                        {
                            result.Maneuver += 4;
                        }
                        else if (Level <= 19)
                        {
                            result.Maneuver += 5;
                        }
                        else
                        {
                            result.Maneuver += 7;
                        }
                    }
                    #endregion
                    #region Detection
                    if (RacialAbility == AdmiralRacialAbility.BreederMale)
                    {
                        if (Level <= 12)
                        {
                            result.Detection++;
                        }
                        else
                        {
                            result.Detection += 2;
                        }
                    }
                    else if (RacialAbility == AdmiralRacialAbility.MentalGiant)
                    {
                        if (Level <= 6)
                        {
                            result.Detection += 3;
                        }
                        else if(Level <= 9)
                        {
                            result.Detection += 4;
                        }
                        else if (Level <= 12)
                        {
                            result.Detection += 5;
                        }
                        else if (Level <= 15)
                        {
                            result.Detection += 6;
                        }
                        else if (Level <= 17)
                        {
                            result.Detection += 7;
                        }
                        else if (Level <= 18)
                        {
                            result.Detection += 8;
                        }
                        else if (Level <= 19)
                        {
                            result.Detection += 9;
                        }
                        else
                        {
                            result.Detection += 10;
                        }
                    }
                    else if (RacialAbility == AdmiralRacialAbility.ArtifactCoolingEngine)
                    {
                        if (Level <= 6)
                        {
                            result.Detection--;
                        }
                        else if (Level <= 12)
                        {
                            result.Detection -= 2;
                        }
                        else if (Level <= 18)
                        {
                            result.Detection -= 3;
                        }
                        else if (Level <= 19)
                        {
                            result.Detection -= 4;
                        }
                        else
                        {
                            result.Detection -= 5;
                        }
                    }
                    else if (RacialAbility == AdmiralRacialAbility.CyberScanUnit)
                    {
                        if (Level <= 4)
                        {
                            result.Detection++;
                        }
                        else if (Level <= 8)
                        {
                            result.Detection += 2;
                        }
                        else if (Level <= 12)
                        {
                            result.Detection += 3;
                        }
                        else if (Level <= 15)
                        {
                            result.Detection += 4;
                        }
                        else if (Level <= 17)
                        {
                            result.Detection += 5;
                        }
                        else if (Level <= 19)
                        {
                            result.Detection += 6;
                        }
                        else
                        {
                            result.Detection += 7;
                        }
                    }
                    else if (RacialAbility == AdmiralRacialAbility.Intuition)
                    {
                        if (Level <= 4)
                        {
                            result.Detection++;
                        }
                        else if (Level <= 15)
                        {
                            result.Detection += 2;
                        }
                        else
                        {
                            result.Detection += 3;
                        }
                    }
                    #endregion
                    #region Interpretation
                    if (RacialAbility == AdmiralRacialAbility.BreederMale)
                    {
                        if (Level <= 12)
                        {
                            result.Interpretation++;
                        }
                        else
                        {
                            result.Interpretation += 2;
                        }
                    }
                    else if (RacialAbility == AdmiralRacialAbility.Intuition)
                    {
                        if (Level <= 4)
                        {
                            result.Interpretation += 3;
                        }
                        else if (Level <= 10)
                        {
                            result.Interpretation += 4;
                        }
                        else if (Level <= 14)
                        {
                            result.Interpretation += 5;
                        }
                        else if (Level <= 19)
                        {
                            result.Interpretation += 6;
                        }
                        else
                        {
                            result.Interpretation += 9;
                        }
                    }
                    else if (RacialAbility == AdmiralRacialAbility.ArtifactCoolingEngine)
                    {
                        if (Level <= 6)
                        {
                            result.Interpretation--;
                        }
                        else if (Level <= 12)
                        {
                            result.Interpretation -= 2;
                        }
                        else if (Level <= 18)
                        {
                            result.Interpretation -= 3;
                        }
                        else if (Level <= 19)
                        {
                            result.Interpretation -= 4;
                        }
                        else
                        {
                            result.Interpretation -= 5;
                        }
                    }
                    else if (RacialAbility == AdmiralRacialAbility.CyberScanUnit)
                    {
                        if (Level <= 4)
                        {
                            result.Interpretation++;
                        }
                        else if (Level <= 8)
                        {
                            result.Interpretation += 2;
                        }
                        else if (Level <= 12)
                        {
                            result.Interpretation += 3;
                        }
                        else if (Level <= 15)
                        {
                            result.Interpretation += 4;
                        }
                        else if (Level <= 17)
                        {
                            result.Interpretation += 5;
                        }
                        else if (Level <= 19)
                        {
                            result.Interpretation += 6;
                        }
                        else
                        {
                            result.Interpretation += 7;
                        }
                    }
                    #endregion
                }

                return result;
            }
        }
        public int Attack
        {
            get
            {
                AdmiralSkills baseAmounts = Skills;

                int result = baseAmounts.SiegePlanet + baseAmounts.Blockade + baseAmounts.Raid + baseAmounts.Privateer;

                result /= 8;

                if (Player != null)
                {
                    if (Player.Race.BaseTraits.Contains(RacialTrait.Pacifist))
                    {
                        result -= 3;
                    }

                    if (Player.Race.BaseTraits.Contains(RacialTrait.TacticalMastery))
                    {
                        result += 3;
                    }
                }

                return result;
            }
        }
        public int Defense
        {
            get
            {
                AdmiralSkills baseAmounts = Skills;

                int result = baseAmounts.SiegeRepel + baseAmounts.BreakBlockade + baseAmounts.PreventRaid;

                result /= 6;

                if (Player != null)
                {
                    if (Player.Race.BaseTraits.Contains(RacialTrait.TacticalMastery))
                    {
                        result += 3;
                    }
                }

                return result;
            }
        }

        public int? FleetId { get; set; }
        [ForeignKey("FleetId")]
        public Fleet Fleet { get; set; }

        public Admiral(Universe aUniverse) : base(aUniverse)
        {
            Experience = 0;
            Level = 1;
            BaseSkills = new AdmiralSkills();
            ArmadaClass = Enum.GetValues(typeof(ArmadaClass)).Cast<ArmadaClass>().Random();
            BaseFleetCapacity = Game.Random.Next(1, 6) + 5;
            BaseEfficiency = Game.Random.Next(1, 31) + 24;
        }

        public Admiral AsRandomAdmiral()
        {
            AsRacialAdmiral(Game.Configuration.Races.Random());

            return this;
        }

        public Admiral AsRacialAdmiral(Race aRace)
        {
            Race = aRace;
            Name = GenerateName();
            StartingCircumstance = GenerateStartingCircumstance();
            SpecialAbility = typeof(AdmiralSpecialAbility).GetEnumValues().OfType<AdmiralSpecialAbility>().Random();
            RacialAbility = typeof(AdmiralRacialAbility).GetEnumValues().OfType<AdmiralRacialAbility>().Random();

            AssignStartingStats();

            return this;
        }

        public Admiral AsPlayerAdmiral(Player aPlayer)
        {
            Player = aPlayer;
            AsRacialAdmiral(aPlayer.Race);

            StartingCircumstance = GenerateStartingCircumstance(Player.ControlModel.Genius);

            AssignStartingStats();

            return this;
        }

        private string GenerateName()
        {
            Race race = Race;
            
            if (race.AdmiralNameStyle == AdmiralNameStyle.Xesperados)
            {
                race = Game.Configuration.Races.Where(x => x.AdmiralNameStyle != AdmiralNameStyle.Xesperados).Random();
            }

            StringBuilder stringBuilder = new StringBuilder();

            int numAlpha = Game.Random.Next(1, 4);
            int numNum = Game.Random.Next(1, 4);

            if (race.AdmiralNameStyle == AdmiralNameStyle.Evintos)
            {
                for (int i = 0; i < numAlpha; i++)
                {
                    stringBuilder.Append((char)('A' + Game.Random.Next(0, 25)));
                }
                stringBuilder.Append('-');
                for (int i = 0; i < numNum; i++)
                {
                    stringBuilder.Append((char)('1' + Game.Random.Next(0, 8)));
                }
            }
            else
            {
                stringBuilder.Append(race.AdmiralFirstNames.Random() + (race.AdmiralLastNames.Any() ? $" {race.AdmiralLastNames.Random()}" : string.Empty));
            }

            return stringBuilder.ToString();
        }

        private void UpdateStats()
        {
            BaseFleetCapacity += Game.Configuration.Admiral.LevelSettings[Level].AdditionalFleetCommanding;

            if (RacialAbility == AdmiralRacialAbility.BreederMale)
            {
                if (Level == 2 || Level == 6 || Level == 11 || Level == 16 || Level == 20)
                {
                    BaseFleetCapacity += 1;
                }
            }

            int additional = Enum.GetValues(typeof(StartingCircumstance)).Length - (int)StartingCircumstance;

            BaseSkills.Blockade += Game.Random.Next(1, 100) < 50 + additional ? 0 : 1;
            BaseSkills.BreakBlockade += Game.Random.Next(1, 100) < 50 + additional ? 0 : 1;
            BaseSkills.Detection += Game.Random.Next(1, 100) < 50 + additional ? 0 : 1;
            BaseSkills.Interpretation += Game.Random.Next(1, 100) < 50 + additional ? 0 : 1;
            BaseSkills.Maneuver += Game.Random.Next(1, 100) < 50 + additional ? 0 : 1;
            BaseSkills.Privateer += Game.Random.Next(1, 100) < 50 + additional ? 0 : 1;
            BaseSkills.Raid += Game.Random.Next(1, 100) < 50 + additional ? 0 : 1;
            BaseSkills.SiegePlanet += Game.Random.Next(1, 100) < 50 + additional ? 0 : 1;
            BaseSkills.SiegeRepel += Game.Random.Next(1, 100) < 50 + additional ? 0 : 1;

            switch (StartingCircumstance)
            {
                case StartingCircumstance.Supercommander:
                    BaseEfficiency += Game.Random.Dice(2, 4);
                    break;
                case StartingCircumstance.Excellent:
                    BaseEfficiency += Game.Random.Next(1, 8);
                    break;
                case StartingCircumstance.VeryGood:
                    BaseEfficiency += Game.Random.Dice(2, 3);
                    break;
                case StartingCircumstance.Good:
                    BaseEfficiency += Game.Random.Next(1, 6);
                    break;
                case StartingCircumstance.Average:
                    BaseEfficiency += Game.Random.Dice(2, 2);
                    break;
                case StartingCircumstance.Poor:
                    BaseEfficiency += Game.Random.Dice(1, 4);
                    break;
                default:
                    BaseEfficiency += Game.Random.Dice(1, 3);
                    break;
            }
        }

        public void LevelUp()
        {
            if (Level >= Game.Configuration.Admiral.MaxLevel)
            {
                return;
            }
            else if (Experience < Game.Configuration.Admiral.LevelSettings[Level].RequiredExperience)
            {
                return;
            }

            do
            {
                Level++;
                UpdateStats();
            }
            while (Experience < Game.Configuration.Admiral.LevelSettings[Level].RequiredExperience && Level < Game.Configuration.Admiral.MaxLevel);
        }
        public void GiveLevels(int aLevels)
        {
            for (int i = 0; i < aLevels && Level < Game.Configuration.Admiral.MaxLevel; i++)
            {
                Level++;
                Experience = Game.Configuration.Admiral.LevelSettings[Level].RequiredExperience;
                UpdateStats();
            }
        }

        public void GainExperience(int aExperience)
        {
            Experience += aExperience;
            // throw new NotImplementedException(); Level up logic
        }

        private StartingCircumstance GenerateStartingCircumstance()
        {
            return GenerateStartingCircumstance(Game.Random.Next(-5, 15));
        }

        private StartingCircumstance GenerateStartingCircumstance(int aGenius)
        {
            Dictionary<StartingCircumstance, int> weights = new Dictionary<StartingCircumstance, int>(Game.Configuration.Admiral.StartingCircumstanceWeights);

            List<StartingCircumstance> weightKeys = weights.Keys.ToList();
            foreach (var weightKey in weightKeys)
            {
                weights[weightKey] += (Enum.GetValues(typeof(StartingCircumstance)).Length - (int)(weightKey)) * aGenius;

                if (weights[weightKey] < 0)
                {
                    weights[weightKey] = 0;
                }
            }

            int total = weights.Sum(x => x.Value);
            int random = Game.Random.Next(1, total);
            int current = 0;

            foreach (StartingCircumstance sc in (Enum.GetValues(typeof(StartingCircumstance)).Cast<StartingCircumstance>().OrderByDescending(x => x))) 
            {
                current += weights[sc];
                if (random <= current)
                {
                    return sc;
                }
            }

            return StartingCircumstance.Average;
        }

        private void AssignStartingStats()
        {
            int amount = Enum.GetValues(typeof(StartingCircumstance)).Length - (int)StartingCircumstance + 2;

            BaseSkills.Blockade = -3 + Game.Random.Next(1, amount);
            BaseSkills.BreakBlockade = -3 + Game.Random.Next(1, amount);
            BaseSkills.Detection = -3 + Game.Random.Next(1, amount);
            BaseSkills.Interpretation = -3 + Game.Random.Next(1, amount);
            BaseSkills.Maneuver = -3 + Game.Random.Next(1, amount);
            BaseSkills.Privateer = -3 + Game.Random.Next(1, amount);
            BaseSkills.Raid = -3 + Game.Random.Next(1, amount);
            BaseSkills.SiegePlanet = -3 + Game.Random.Next(1, amount);
            BaseSkills.SiegeRepel = -3 + Game.Random.Next(1, amount);
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
    }
}