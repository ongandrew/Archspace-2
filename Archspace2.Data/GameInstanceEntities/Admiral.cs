using System;
using System.Collections.Generic;
using System.Text;

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
        public Player Owner { get; set; }

        public AdmiralSpecialAbility SpecialAbility { get; set; }
        public AdmiralRacialAbility RacialAbility { get; set; }
        public int AttackRating { get; set; }
        public int DefenseRating { get; set; }
        public int DetectionRating { get; set; }
    }
}
