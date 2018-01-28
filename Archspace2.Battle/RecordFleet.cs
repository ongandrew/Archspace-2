using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2.Battle
{
    public class RecordFleet : NamedEntity
    {
        public bool IsCapital { get; set; }

        public RecordFleet(int aId, string aName, bool aIsCapital)
        {
            Id = aId;
            Name = aName;
            IsCapital = aIsCapital;
        }

        public RecordFleet(Fleet aFleet) : this(aFleet.Id, aFleet.Name, aFleet.IsCapital)
        {
        }
    }
}
