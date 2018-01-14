using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

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

        protected UniverseEntity() : base()
        {
        }
        public UniverseEntity(Universe aUniverse) : this()
        {
            Universe = aUniverse;
        }
    }
}
