using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class HitEvent : BattleRecordEvent
    {
        [JsonProperty("FiringFleetId")]
        public int FiringFleetId { get; set; }
        [JsonProperty("TargetFleetId")]
        public int TargetFleetId { get; set; }

        public HitEvent()
        {
            Type = BattleRecordEventType.Hit;
        }

        public HitEvent(BattleFleet aFiringFleet, BattleFleet aTargetFleet, int aTotalDamage, int aSunkCount) : this()
        {
            throw new NotImplementedException();
        }
    }
}
