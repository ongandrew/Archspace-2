using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2.Battle
{
    public class MovementEvent : RecordEvent, IPosition, IDirection
    {
        [JsonProperty("FleetId")]
        public int FleetId { get; set; }

        [JsonProperty("X")]
        public double X { get; set; }
        [JsonProperty("Y")]
        public double Y { get; set; }

        [JsonProperty("Direction")]
        public double Direction { get; set; }

        [JsonProperty("Command")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Command Command { get; set; }

        [JsonProperty("Status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FleetStatus Status { get; set; }
        [JsonProperty("Substatus")]
        [JsonConverter(typeof(StringEnumConverter))]
        public FleetSubstatus Substatus { get; set; }

        [JsonProperty("RemainingShips")]
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
