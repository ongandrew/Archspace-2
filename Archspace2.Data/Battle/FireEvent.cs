using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class FireEvent : BattleRecordEvent
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

        public FireEvent(int aTurn) : base(aTurn, BattleRecordEventType.Fire)
        {
        }

        public FireEvent(int aTurn, BattleFleet aFiringFleet, BattleFleet aTargetFleet, Turret aTurret) : this(aTurn)
        {
            FiringFleetId = aFiringFleet.Fleet.Id;
            TargetFleetId = aTargetFleet.Fleet.Id;

            Weapon = aTurret.Name;
            WeaponType = aTurret.Type;

            Quantity = aTurret.Number * aFiringFleet.ActiveShipCount;
        }
    }
}
