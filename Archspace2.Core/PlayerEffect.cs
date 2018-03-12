using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archspace2
{
    public enum PlayerEffectSource
    {
        Spy,
        Event,
        Project,
        Admin
    };
    
    public class PlayerEffect : IPlayerEffect
    {
        [JsonProperty("type")]
        public PlayerEffectType Type { get; set; }
        [JsonProperty("modifierType")]
        public ModifierType ModifierType { get; set; }

        [JsonProperty("target")]
        public int Target { get; set; }
        [JsonProperty("argument1")]
        public int Argument1 { get; set; }
        [JsonProperty("argument2")]
        public int Argument2 { get; set; }
        [JsonProperty("controlModelModifier")]
        public ControlModel ControlModelModifier { get; set; }

        [JsonProperty("isInstant")]
        public bool IsInstant { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
