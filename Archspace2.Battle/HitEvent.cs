using Newtonsoft.Json;

namespace Archspace2.Battle
{
    public class HitEvent : RecordEvent
    {
        [JsonProperty("FiringFleetId")]
        public int FiringFleetId { get; set; }
        [JsonProperty("TargetFleetId")]
        public int TargetFleetId { get; set; }

        [JsonProperty("TotalDamage")]
        public int TotalDamage { get; set; }
        [JsonProperty("SunkCount")]
        public int SunkCount { get; set; }

        public HitEvent(int aTurn) : base(aTurn, RecordEventType.Hit)
        {
        }

        public HitEvent(int aTurn, Fleet aFiringFleet, Fleet aTargetFleet, int aTotalDamage, int aSunkCount) : this(aTurn)
        {
            Turn = aTurn;
            FiringFleetId = aFiringFleet.Id;
            TargetFleetId = aTargetFleet.Id;
            TotalDamage = aTotalDamage;
            SunkCount = aSunkCount;
        }
    }
}
