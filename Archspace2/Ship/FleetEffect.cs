using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Archspace2
{
    public enum FleetEffectType
    {
        AttackRating,
        WeaponAttackRating,
        Computer,
        DefenseRating,
        ArmorDefenseRating,
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
        ProjectileAttackRating,
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
        DamageOverTime,
        PsiDamageOverTime,
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
        AreaEffectTargetAll,
        AreaLocalEffect
    };

    public class FleetEffect : IModifier
    {
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FleetEffectType Type { get; set; }
        [JsonProperty("modifierType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ModifierType ModifierType { get; set; }
        [JsonProperty("targetType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FleetEffectTargetType TargetType { get; set; }
        [JsonProperty("amount")]
        public int Amount { get; set; }
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
