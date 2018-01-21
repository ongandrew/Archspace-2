using Newtonsoft.Json;

namespace Archspace2
{
    public class DisabledFleetEvent : BattleRecordEvent
    {
        [JsonProperty("DisabledFleetId")]
        public int DisabledFleetId { get; set; }

        public DisabledFleetEvent(int aTurn) : base(aTurn, BattleRecordEventType.DisableFleet)
        {
        }

        public DisabledFleetEvent(int aTurn, BattleFleet aBattleFleet) : this(aTurn)
        {
            DisabledFleetId = aBattleFleet.Fleet.Id;
        }
    }
}
