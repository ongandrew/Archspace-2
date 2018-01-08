using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Universal.Common.Extensions;

namespace Archspace2
{
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

    public class Admiral : GameInstanceEntity
    {
        public int? OwnerId { get; set; }
        [ForeignKey("OwnerId")]
        public Player Owner { get; set; }

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

        public int Experience { get; set; }
        public AdmiralSpecialAbility SpecialAbility { get; set; }
        public AdmiralRacialAbility RacialAbility { get; set; }
        public int AttackRating { get; set; }
        public int DefenseRating { get; set; }
        public int DetectionRating { get; set; }

        public Admiral()
        {
            Race = Game.Configuration.Races.Random();
            Experience = 0;

            Name = GenerateName();
        }

        private string GenerateName()
        {
            Race race = Race;

            if (race.AdmiralNameStyle == AdmiralNameStyle.Xesperados)
            {
                race = Game.Configuration.Races.Where(x => x.AdmiralNameStyle != AdmiralNameStyle.Xesperados).Random();
            }

            StringBuilder stringBuilder = new StringBuilder();

            if (race.AdmiralNameStyle == AdmiralNameStyle.Evintos)
            {
                Random random = new Random();
                for (int i = 0; i < random.Next(4); i++)
                {
                    stringBuilder.Append((char)('A' + random.Next(26) - 1));
                }
                stringBuilder.Append('-');
                for (int i = 0; i < random.Next(4); i++)
                {
                    stringBuilder.Append((char)('1' + random.Next(9) - 1));
                }
            }
            else
            {
                stringBuilder.Append(race.AdmiralFirstNames.Random() + (race.AdmiralLastNames.Any() ? $" {race.AdmiralLastNames.Random()}" : string.Empty));
            }

            return stringBuilder.ToString();
        }
    }
}
