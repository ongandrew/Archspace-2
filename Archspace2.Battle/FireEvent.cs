using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2.Battle
{
    public class FireEvent : RecordEvent
    {
        [JsonProperty("FiringFleetId")]
        public int FiringFleetId { get; set; }
        [JsonProperty("TargetFleetId")]
        public int TargetFleetId { get; set; }

        [JsonProperty("Weapon")]
        public string Weapon { get; set; }
        [JsonProperty("WeaponType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public WeaponType WeaponType { get; set; }
        [JsonProperty("Quantity")]
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
