using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public enum BattleRecordEventType
    {
        Fire,
        Hit,
        Movement,
        DisableFleet
    };

    public abstract class BattleRecordEvent
    {
        [JsonProperty("Type")]
        public BattleRecordEventType Type { get; set; }
        [JsonProperty("Turn")]
        public int Turn { get; set; }

        public BattleRecordEvent(int aTurn, BattleRecordEventType aType)
        {
            Turn = aTurn;
            Type = aType;
        }
    }
}
