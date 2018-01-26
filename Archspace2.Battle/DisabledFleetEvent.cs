using Newtonsoft.Json;

namespace Archspace2.Battle
{
    public class DisabledFleetEvent : RecordEvent
    {
        [JsonProperty("DisabledFleetId")]
        public int DisabledFleetId { get; set; }

        public DisabledFleetEvent(int aTurn) : base(aTurn, RecordEventType.DisableFleet)
        {
        }

        public DisabledFleetEvent(int aTurn, Fleet aFleet) : this(aTurn)
        {
            DisabledFleetId = aFleet.Id;
        }
    }
}
