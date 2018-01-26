using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2.Battle
{
    public class RecordPlayer : NamedEntity
    {
        public RaceType Race { get; set; }

        public List<RecordFleet> Fleets { get; set; }

        public RecordPlayer()
        {
            Fleets = new List<RecordFleet>();
        }
    }
}
