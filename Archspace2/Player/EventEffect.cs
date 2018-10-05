using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
        [JsonProperty("duration")]
        public int Duration { get; set; }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public PlayerEffectType Type { get; set; }
        [JsonProperty("modifierType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ModifierType ModifierType { get; set; }
        [JsonProperty("target")]
        public int Target { get; set; }
        [JsonProperty("argument1")]
        public int Argument1 { get; set; }
        [JsonProperty("argument2")]
        public int Argument2 { get; set; }

        [JsonProperty("isInstant")]
        public bool IsInstant { get; set; }
        [JsonProperty("applyOn")]
        [JsonConverter(typeof(StringEnumConverter))]
        public EventEffectApplyType ApplyOn { get; set; }

        [JsonProperty("controlModelModifier")]
        public ControlModel ControlModelModifier { get; set; }

        public EventEffect()
        {
            ApplyOn = EventEffectApplyType.Start;
            IsInstant = false;
            ControlModelModifier = new ControlModel();
        }
    }
}
