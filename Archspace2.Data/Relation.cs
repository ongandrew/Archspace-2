using System.ComponentModel.DataAnnotations.Schema;

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
        [NotMapped]
        public override string Name { get; set; }

        public RelationType Type { get; set; }
        public int ExpiryTurn { get; set; }

        public bool IsTemporary()
        {
            return ExpiryTurn != 0;
        }

        public Relation(Universe aUniverse) : base(aUniverse)
        {
        }
    }
}