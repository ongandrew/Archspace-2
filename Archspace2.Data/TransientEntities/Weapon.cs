using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public enum WeaponType
    {
        Beam,
        Missile,
        Projectile,
        Fighter
    }

    public class Weapon : ShipComponent
    {
        public Weapon() : base(ComponentCategory.Weapon)
        {
        }

        [JsonProperty("Type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public WeaponType Type { get; set; }
        [JsonProperty("AttackRating")]
        public int AttackRating { get; set; }
        [JsonProperty("DamageRoll")]
        public int DamageRoll { get; set; }
        [JsonProperty("DamageDice")]
        public int DamageDice { get; set; }
        [JsonProperty("Space")]
        public int Space { get; set; }
        [JsonProperty("CoolingTime")]
        public int CoolingTime { get; set; }
        [JsonProperty("Range")]
        public int Range { get; set; }
        [JsonProperty("AngleOfFire")]
        public int AngleOfFire { get; set; }
        [JsonProperty("Speed")]
        public int Speed { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
