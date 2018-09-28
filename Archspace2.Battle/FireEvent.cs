using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Archspace2.Battle
{
    public class FireEvent : RecordEvent
    {
        [JsonProperty("firingFleetId")]
        public int FiringFleetId { get; set; }
        [JsonProperty("targetFleetId")]
        public int TargetFleetId { get; set; }

        [JsonProperty("weapon")]
        public string Weapon { get; set; }
        [JsonProperty("weaponType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public WeaponType WeaponType { get; set; }
        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        public FireEvent(int aTurn) : base(aTurn, RecordEventType.Fire)
        {
        }

        public FireEvent(int aTurn, Fleet aFiringFleet, Fleet aTargetFleet, Turret aTurret) : this(aTurn)
        {
            FiringFleetId = aFiringFleet.Id;
            TargetFleetId = aTargetFleet.Id;

            Weapon = aTurret.Name;
            WeaponType = aTurret.Type;

            Quantity = aTurret.Number * aFiringFleet.ActiveShipCount;
        }
    }
}
