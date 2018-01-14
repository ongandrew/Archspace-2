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
        PanicModifier,
        ShieldPiercing,
        ArmorPiercing,
        AdditionalDamageToBioArmor,
        ShieldDistortion,
        ShieldOverheat,
        Corrosivity,
        Psi,
        PsiEmpower
    };

    public enum FleetEffectTargetType
    {
        LocalEffect,
        AreaEffectTargetAlly,
        AreaEffectTargetEnemy,
        AreaEffectTargetAll
    };

    public class FleetEffect : IModifier
    {
        [JsonProperty("Type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FleetEffectType Type { get; set; }
        [JsonProperty("ModifierType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ModifierType ModifierType { get; set; }
        [JsonProperty("TargetType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FleetEffectTargetType? TargetType { get; set; }
        [JsonProperty("Amount")]
        public int? Amount { get; set; }
        [JsonProperty("Period")]
        public int? Period { get; set; }
        [JsonProperty("Range")]
        public int? Range { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.None, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
