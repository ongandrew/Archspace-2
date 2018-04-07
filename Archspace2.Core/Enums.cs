namespace Archspace2
{
    public enum Side
    {
        Offense,
        Defense
    };

    public enum Command
    {
        Normal,
        Formation,
        Penetrate,
        Flank,
        Reserve,
        Free,
        Assault,
        StandGround
    };

    public enum ListType
    {
        Inclusion,
        Exclusion
    };

    public enum ModifierType
    {
        Absolute,
        Proportional
    };

    public enum PlayerEffectType
    {
        ChangePlanetResource,
        SwitchPlanetOrder,
        SetFleetMission,
        LosePlanet,
        DestroyAllDockedShip,
        WinAndGainPlanet,
        LoseTech,
        LoseProject,
        PlanetLostBuilding,
        SkipTurn,
        ChangeControlModel,
        ChangeProduction,
        ChangeAllCommanderAbility,
        GainTech,
        GainFleet,
        GainProject,
        GainSecretProject,
        GuardedByImperialFleet,
        ChangeEmpireRelation,
        CommanderLevelUp,
        GrantBoon,
        LoseCommander,
        ChangePlanetControlModel,
        ChangePlanetPopulation,
        LosePlanetGravityControl,
        GainPlanetGravityControl,
        ChangeShipAbilityOnPlanet,
        ChangeFleetReturnTime,
        DamageFleet,
        ChangeHonor,
        ShowPlayer,
        CouncilDeclareTotalWar,
        WinOrLosePlanet,
        ChangeConcentrationMode,
        RecruitEnemyCommander,
        KillCommanderAndDisbandFleet,
        InvasionFromEmpire,
        GainCommander,
        ParalyzePlanet,
        ProduceMpPerTurn,
        ProduceRpPerTurn,
        ConsumePpPerTurn,
        ImperialRetribution,
        EnemyMoraleModifier,
        GainAbility,
        LoseAbility
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
    };
    
    public enum AdmiralSpecialAbility
    {
        EngineeringSpecialist,
        ShieldSystemSpecialist,
        MissileSpecialist,
        BallisticExpert,
        EnergySystemSpecialist
    };
    
    public enum FleetAttribute
    {
        WeakCloaking = 1,
        CompleteCloaking,
        WeakCloakingDetection,
        CompleteCloakingDetection,
        PsiRace,
        EnhancedPsi,
        EnhancedMobility
    };

    public enum ArmadaClass
    {
        A = 0,
        B,
        C,
        D
    };

    public enum ResultType
    {
        Success,
        Failure
    };
}
