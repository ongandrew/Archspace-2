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

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public WeaponType Type { get; set; }
        [JsonProperty("attackRating")]
        public int AttackRating { get; set; }
        [JsonProperty("damageRoll")]
        public int DamageRoll { get; set; }
        [JsonProperty("damageDice")]
        public int DamageDice { get; set; }
        [JsonProperty("space")]
        public int Space { get; set; }
        [JsonProperty("coolingTime")]
        public int CoolingTime { get; set; }
        [JsonProperty("range")]
        public int Range { get; set; }
        [JsonProperty("angleOfFire")]
        public int AngleOfFire { get; set; }
        [JsonProperty("speed")]
        public int Speed { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
