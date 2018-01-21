using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class MovementEvent : BattleRecordEvent
    {
        [JsonProperty("FleetId")]
        public int FleetId { get; set; }

        [JsonProperty("X")]
        public int X { get; set; }
        [JsonProperty("Y")]
        public int Y { get; set; }

        [JsonProperty("Angle")]
        public double Angle { get; set; }

        [JsonProperty("Command")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Command Command { get; set; }

        [JsonProperty("Status")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BattleFleetStatus Status { get; set; }
        [JsonProperty("Substatus")]
        [JsonConverter(typeof(StringEnumConverter))]
        public BattleFleetSubstatus Substatus { get; set; }

        [JsonProperty("RemainingShips")]
        public int RemainingShips { get; set; }

        public MovementEvent(int aTurn) : base(aTurn, BattleRecordEventType.Movement)
        {
        }

        public MovementEvent(int aTurn, BattleFleet aBattleFleet) : this(aTurn)
        {
            X = aBattleFleet.X;
            Y = aBattleFleet.Y;
            Angle = aBattleFleet.Angle;
            Command = aBattleFleet.Command;

            Status = aBattleFleet.Status;
            Substatus = aBattleFleet.Substatus;

            RemainingShips = aBattleFleet.ActiveShipCount;
        }
    }
}
