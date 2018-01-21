using Newtonsoft.Json;

namespace Archspace2
{
    public class HitEvent : BattleRecordEvent
    {
        [JsonProperty("FiringFleetId")]
        public int FiringFleetId { get; set; }
        [JsonProperty("TargetFleetId")]
        public int TargetFleetId { get; set; }

        [JsonProperty("TotalDamage")]
        public int TotalDamage { get; set; }
        [JsonProperty("SunkCount")]
        public int SunkCount { get; set; }

        public HitEvent(int aTurn) : base(aTurn, BattleRecordEventType.Hit)
        {
        }

        public HitEvent(int aTurn, BattleFleet aFiringFleet, BattleFleet aTargetFleet, int aTotalDamage, int aSunkCount) : this(aTurn)
        {
            Turn = aTurn;
        }
    }
}
