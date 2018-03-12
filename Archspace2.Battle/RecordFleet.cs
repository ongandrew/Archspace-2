using Newtonsoft.Json;

namespace Archspace2.Battle
{
    public class RecordFleet : NamedEntity
    {
        [JsonProperty("isCapital")]
        public bool IsCapital { get; set; }

        public RecordFleet(int aId, string aName, bool aIsCapital)
        {
            Id = aId;
            Name = aName;
            IsCapital = aIsCapital;
        }

        public RecordFleet(Fleet aFleet) : this(aFleet.Id, aFleet.Name, aFleet.IsCapital)
        {
        }
    }
}
