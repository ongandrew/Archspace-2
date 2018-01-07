using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class Engine : ShipComponent
    {
        public Engine() : base(ComponentCategory.Engine)
        {
            BattleSpeed = new Dictionary<int, int>();
            BattleMobility = new Dictionary<int, int>();
        }

        public Dictionary<int, int> BattleSpeed { get; set; }
        public Dictionary<int, int> BattleMobility { get; set; }
    }
}
