using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Archspace2.Battle
{
    public class MovementEvent : RecordEvent, IPosition, IDirection
    {
        [JsonProperty("fleetId")]
        public int FleetId { get; set; }

        [JsonProperty("x")]
        public double X { get; set; }
        [JsonProperty("y")]
        public double Y { get; set; }

        [JsonProperty("direction")]
        public double Direction { get; set; }

        [JsonProperty("command")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Command Command { get; set; }

        [JsonProperty("status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FleetStatus Status { get; set; }
        [JsonProperty("substatus")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FleetSubstatus Substatus { get; set; }

        [JsonProperty("remainingShips")]
        public int RemainingShips { get; set; }

        public MovementEvent(int aTurn) : base(aTurn, RecordEventType.Movement)
        {
        }

        public MovementEvent(int aTurn, Fleet aFleet) : this(aTurn)
        {
            X = aFleet.X;
            Y = aFleet.Y;
            Direction = aFleet.Direction;
            Command = aFleet.Command;

            Status = aFleet.Status;
            Substatus = aFleet.Substatus;

            RemainingShips = aFleet.ActiveShipCount;
        }
    }
}
