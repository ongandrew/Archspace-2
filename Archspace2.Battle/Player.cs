using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2.Battle
{
    public class Player : NamedEntity
    {
        public Race Race { get; set; }
        public List<RacialTrait> Traits { get; set; }

        public Player()
        {
            Traits = new List<RacialTrait>();
        }
    }
}
