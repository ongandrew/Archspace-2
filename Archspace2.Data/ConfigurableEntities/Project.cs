using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public enum ProjectType
    {
        Planet,
        Fixed,
        Council,
        Ending,
        Secret,
        BlackMarket
    }

    public class Project : Entity
    {
        public Project() : base()
        {
            PlayerEffects = new List<PlayerEffect>();
            Prerequisites = new List<PlayerPrerequisite>();
        }

        [JsonProperty("type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ProjectType Type { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("cost")]
        public int Cost { get; set; }

        [JsonProperty("controlModelModifier")]
        public ControlModel ControlModelModifier { get; set; }
        [JsonProperty("playerEffects")]
        public List<PlayerEffect> PlayerEffects { get; set; }
        
        [JsonProperty("prerequisites")]
        public List<PlayerPrerequisite> Prerequisites { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
