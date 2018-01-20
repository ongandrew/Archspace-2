using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public enum FormationStatus
    {
        None,
        Disband,
        Encounter,
        Reformation
    };

    public class BattleGroup : List<BattleFleet>
    {
        public Player Owner { get; set; }
        public Side Side { get; set; }
        public FormationStatus Status { get; set; }
        public int Speed { get; set; }

        public BattleFleet CapitalFleet { get; set; }

        public BattleGroup() : base()
        {
        }
    }
}
