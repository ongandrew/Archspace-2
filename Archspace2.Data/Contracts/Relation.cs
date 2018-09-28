using System;

namespace Archspace2
{
    public enum RelationType
    {
        None,
        Subordinary,
        Ally,
        Peace,
        Truce,
        War,
        TotalWar,
        Bounty,
        Hostile
    };

    public abstract class Relation : UniverseEntity
    {
        public int? FromId { get; private set; }
        public int? ToId { get; private set; }

        public RelationType Type { get; set; }
        public int? ExpiryTurn { get; set; }

        public DateTime DateTime { get; private set; }
        public int Turn { get; private set; }

        public bool IsTemporary()
        {
            return ExpiryTurn != null && ExpiryTurn != 0;
        }

        internal Relation() : base()
        {
        }
        public Relation(Universe aUniverse) : base(aUniverse)
        {
            DateTime = DateTime.UtcNow;
            Turn = aUniverse.CurrentTurn;
        }

        public int Significance
        {
            get
            {
                switch(Type)
                {
                    case RelationType.Ally:
                    case RelationType.TotalWar:
                        return 5;
                    case RelationType.Peace:
                    case RelationType.War:
                        return 4;
                    case RelationType.Hostile:
                    case RelationType.Bounty:
                        return 3;
                    case RelationType.Subordinary:
                        return 2;
                    case RelationType.Truce:
                        return 1;
                    default:
                        return 0;
                }
            }
        }
    }
}