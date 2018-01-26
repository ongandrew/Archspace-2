using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2.Battle
{
    // The plan is to redo the battle system so that this is autonomous and tracked on a real per-ship basis.
    public class Ship
    {
        public int HP { get; set; }
        public int ShieldStrength { get; set; }
    }
}
