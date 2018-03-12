using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2.Battle
{
    public enum RecordEventType
    {
        Fire,
        Hit,
        Movement,
        FleetDisabled
    };

    public abstract class RecordEvent
    {
        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public RecordEventType Type { get; set; }
        [JsonProperty("turn")]
        public int Turn { get; set; }

        public RecordEvent(int aTurn, RecordEventType aType)
        {
            Turn = aTurn;
            Type = aType;
        }
    }
}
