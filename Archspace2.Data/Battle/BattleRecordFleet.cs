using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class BattleRecordFleet : Entity
    {
        [JsonProperty("Owner")]
        public Entity Owner { get; set; }

        [JsonProperty("X")]
        public int X { get; set; }
        [JsonProperty("Y")]
        public int Y { get; set; }
        [JsonProperty("Angle")]
        public double Angle { get; set; }

        [JsonProperty("Admiral")]
        public Entity Admiral { get; set; }

        [JsonProperty("Command")]
        [JsonConverter(typeof(StringEnumConverter))]
        public Command Command { get; set; }

        [JsonProperty]
        public bool IsCapital { get; set; }

        [JsonProperty("MaxShips")]
        public int MaxShips { get; set; }

        public BattleRecordFleet()
        {
            Owner = new Entity();
            Admiral = new Entity();
        }

        public BattleRecordFleet(BattleFleet aBattleFleet) : this()
        {
            Id = aBattleFleet.Fleet.Id;
            Name = aBattleFleet.Fleet.Name;

            Admiral.Id = aBattleFleet.Admiral.Id;
            Admiral.Name = aBattleFleet.Admiral.Name;

            Owner.Id = aBattleFleet.Owner.Id;
            Owner.Name = aBattleFleet.Owner.Name;

            X = aBattleFleet.X;
            Y = aBattleFleet.Y;
            Angle = aBattleFleet.Angle;

            Command = aBattleFleet.Command;

            MaxShips = aBattleFleet.MaxShipCount;

            IsCapital = aBattleFleet.IsCapital;
        }
    }
}
