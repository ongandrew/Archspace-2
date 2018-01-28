using Newtonsoft.Json;

namespace Archspace2.Battle
{
    public class FleetDisabledEvent : RecordEvent
    {
        [JsonProperty("DisabledFleetId")]
        public int DisabledFleetId { get; set; }

        public FleetDisabledEvent(int aTurn) : base(aTurn, RecordEventType.FleetDisabled)
        {
        }

        public FleetDisabledEvent(int aTurn, Fleet aFleet) : this(aTurn)
        {
            DisabledFleetId = aFleet.Id;
        }
    }
}
