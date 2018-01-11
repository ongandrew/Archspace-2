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

        [JsonProperty("Type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TechType Type { get; set; }
        [JsonProperty("Attribute")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TechAttribute Attribute { get; set; }

        [JsonProperty("ControlModelModifier")]
        public ControlModel ControlModelModifier { get; set; }
        [JsonProperty("Description")]
        public string Description { get; set; }
        [JsonProperty("TechLevel")]
        public int TechLevel { get; set; }
        [JsonProperty("Prerequisites")]
        public List<PlayerPrerequisite> Prerequisites { get; set; }
        [JsonProperty("PlayerEffects")]
        public List<PlayerEffect> PlayerEffects { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
