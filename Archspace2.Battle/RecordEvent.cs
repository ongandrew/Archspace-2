﻿using Newtonsoft.Json;
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
        DisableFleet
    };

    public abstract class RecordEvent
    {
        [JsonProperty("Type")]
        public RecordEventType Type { get; set; }
        [JsonProperty("Turn")]
        public int Turn { get; set; }

        public RecordEvent(int aTurn, RecordEventType aType)
        {
            Turn = aTurn;
            Type = aType;
        }
    }
}