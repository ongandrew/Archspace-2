using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public enum FleetEffectType
    {
        AttackRating,
        WeaponAttackRating,
        Computer,
        DefenseRating,
        Mobility,
        Speed,
        Hp,
        DetectionRange,
        Morale,
        CompleteCloaking,
        WeakCloaking,
        CompleteCloakingDetection,
        WeakCloakingDetection,
        ShieldSolidity,
        ShieldStrength,
        ImpenetrableShield,
        ImpenetrableArmor,
        ShieldRechargeRate,
        BeamDamage,
        BeamAttackRating,
        DefenseRatingAgainstBeam,
        BeamDefense,
        MissileDamage,
        MissileAttackRating,
        DefenseRatingAgainstMissile,
        MissileDefense,
        ProjectileDamage,
        DefenseRatingAgainstProjectile,
        ProjectileDefense,
        PsiAttack,
        PsiDefense,
        GenericDefense,
        ChainReaction,
        Repair,
        RepairSpeed,
        NonRepairable,
        CoolingTime,
        BeamCoolingTime,
        MissileCoolingTime,
        ProjectileCoolingTime,
        Stealth,
        MoraleModifier,
        BerserkModifier,
        Efficiency,
        Trained,
        Damage,
        PsiDamage,
        PsiStaticDamage,
        CriticalHit,
        Misinterpret,
        CommanderSurvival,
        NeverBerserk,
        NeverRetreatRout,
        DefenseRatingAgainstPsi,
        PsiNeutralizationField,
        SpaceMining,

        ShieldPiercing,
        ArmorPiercing,
        AdditionalDamageToBioArmor,
        ShieldDistortion,
        ShieldOverheat,
        Corrosivity,
        Psi,
        PsiEmpower,
        PanicModifier
    };
    public enum ModifierType
    {
        Absolute,
        Proportional
    };
    public enum FleetEffectTargetType
    {
        LocalEffect,
        AreaEffectTargetAlly,
        AreaEffectTargetEnemy,
        AreaEffectTargetAll
    }

    public class FleetEffect
    {
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FleetEffectType Type { get; set; }
        [JsonProperty("modifierType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ModifierType? ModifierType { get; set; }
        [JsonProperty("targetType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FleetEffectTargetType? TargetType { get; set; }
        [JsonProperty("amount")]
        public int? Amount { get; set; }
        [JsonProperty("period")]
        public int? Period { get; set; }
        [JsonProperty("range")]
        public int? Range { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
