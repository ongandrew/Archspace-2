using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public enum EventEffectApplyType
    {
        Start,
        AnswerYes,
        AnswerNo
    };

    public class EventEffect : IPlayerEffect, IControlModelModifier
    {
        [JsonProperty("Duration")]
        public int Duration { get; set; }
        [JsonProperty("Chance")]
        public int Chance { get; set; }
        [JsonProperty("Type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PlayerEffectType Type { get; set; }
        [JsonProperty("ModifierType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ModifierType ModifierType { get; set; }
        [JsonProperty("Target")]
        public int Target { get; set; }
        [JsonProperty("Argument1")]
        public int Argument1 { get; set; }
        [JsonProperty("Argument2")]
        public int Argument2 { get; set; }

        [JsonProperty("IsInstant")]
        public bool IsInstant { get; set; }
        [JsonProperty("ApplyOn")]
        [JsonConverter(typeof(StringEnumConverter))]
        public EventEffectApplyType ApplyOn { get; set; }

        [JsonProperty("ControlModelModifier")]
        public ControlModel ControlModelModifier { get; set; }

        public EventEffect()
        {
            ApplyOn = EventEffectApplyType.Start;
            IsInstant = false;
            ControlModelModifier = new ControlModel();
        }
    }
}
