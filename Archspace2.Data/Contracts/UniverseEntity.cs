using System.ComponentModel.DataAnnotations.Schema;

namespace Archspace2
{
    public abstract class UniverseEntity : Entity
    {
        public int UniverseId { get; set; }
        [NotMapped]
        [ForeignKey("UniverseId")]
        public Universe Universe
        {
            get
            {
                return Game.Universe.Id == UniverseId ? Game.Universe : null;
            }
            set
            {
                UniverseId = value.Id;
            }
        }

        public UniverseEntity() : base()
        {
        }
        public UniverseEntity(Universe aUniverse) : this()
        {
            Universe = aUniverse;
        }
    }
}
