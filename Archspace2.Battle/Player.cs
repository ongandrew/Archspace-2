using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2.Battle
{
    public class Player : NamedEntity
    {
        public Race Race { get; set; }
        public List<RacialTrait> Traits { get; set; }

        internal Player()
        {
            Traits = new List<RacialTrait>();
        }

        public Player(int aId, string aName, Race aRace, List<RacialTrait> aTraits)
        {
            Id = aId;
            Name = aName;
            Race = aRace;
            Traits = aTraits;
        }
    }
}
