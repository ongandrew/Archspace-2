using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public enum TechType
    {
        Information,
        Life,
        MatterEnergy,
        Social
    };

    public enum TechAttribute
    {
        Basic,
        Normal,
        Innate
    }

    public class Tech : Entity
    {
        public Tech()
        {
            Prerequisites = new List<PlayerPrerequisite>();
            PlayerEffects = new List<PlayerEffect>();
        }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TechType Type { get; set; }
        [JsonProperty("attribute")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TechAttribute Attribute { get; set; }

        [JsonProperty("controlModelModifier")]
        public ControlModel ControlModelModifier { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("techLevel")]
        public int TechLevel { get; set; }
        [JsonProperty("prerequisites")]
        public List<PlayerPrerequisite> Prerequisites { get; set; }
        [JsonProperty("playerEffects")]
        public List<PlayerEffect> PlayerEffects { get; set; }
    }
}
