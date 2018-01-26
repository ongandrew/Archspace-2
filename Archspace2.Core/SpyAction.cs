using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public enum SpyType
    {
        Acceptable,
        Ordinary,
        Hostile,
        Atrocious
    }

    public class SpyAction : NamedEntity, IPlayerUnlockable
    {
        public SpyAction()
        {
            Prerequisites = new List<PlayerPrerequisite>();
        }

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Difficulty")]
        public int Difficulty { get; set; }
        [JsonProperty("Cost")]
        public int Cost { get; set; }

        [JsonProperty("Type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public SpyType Type { get; set; }

        [JsonProperty("Prerequisites")]
        public List<PlayerPrerequisite> Prerequisites { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
