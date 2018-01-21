using System;
using System.Collections.Generic;
using System.Linq;
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
        public Formation Formation { get; set; }

        public int Power
        {
            get
            {
                return this.Sum(x => x.Power);
            }
        }

        public BattleGroup(Player aOwner, Side aSide) : base()
        {
            Side = aSide;
            Owner = aOwner;
            Formation = new Formation();
        }
    }
}
