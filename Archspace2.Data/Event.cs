using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace Archspace2
{
    public enum EventType
    {
        System,
        Council,
        Cluster,
        Magistrate,
        Empire,
        Galactic,
        Racial,
        Major
    };

    public class Event : NamedEntity
    {
        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("Type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public EventType Type { get; set; }
        [JsonProperty("RaceListType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ListType RaceListType { get; set; }
        [JsonProperty("RaceList")]
        public List<int> RaceList { get; set; }

        [JsonProperty("MinDuration")]
        public int MinDuration { get; set; }
        [JsonProperty("MaxDuration")]
        public int MaxDuration { get; set; }
        [JsonProperty("Duration")]
        public int Duration
        {
            get
            {
                if (MinDuration == MaxDuration)
                {
                    return MinDuration;
                }
                else
                {
                    return MinDuration - 1 + Game.Random.Next(1, MaxDuration - MinDuration);
                }
            }
            set
            {
                MinDuration = value;
                MaxDuration = value;
            }
        }
        [JsonProperty("MinHonor")]
        public int MinHonor { get; set; }
        [JsonProperty("MaxHonor")]
        public int MaxHonor { get; set; }

        [JsonProperty("RequiresResponse")]
        public bool RequiresResponse { get; set; }

        public List<EventEffect> Effects { get; set; }

        public Event()
        {
            MinHonor = 0;
            MaxHonor = 100;
            RequiresResponse = false;
            Effects = new List<EventEffect>();
            RaceList = new List<int>();
        }
    }
}
