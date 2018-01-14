using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Archspace2
{
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

    public enum PlayerEffectSource
    {
        Spy,
        Event,
        Project,
        Admin
    };

    [Table("PlayerEffect")]
    public class PlayerEffect : UniverseEntity, IPlayerEffect
    {
        public int PlayerId { get; set; }
        [ForeignKey("PlayerId")]
        public Player Player { get; set; }

        public ModifierType ModifierType { get; set; }

        public int Target { get; set; }

        public int Life { get; set; }
        public PlayerEffectType Type { get; set; }
        public PlayerEffectSource SourceType { get; set; }
        public int SourceId { get; set; }

        public int Argument1 { get; set; }
        public int Argument2 { get; set; }

        public PlayerEffect(Universe aUniverse) : base(aUniverse)
        {
        }
    }
}
