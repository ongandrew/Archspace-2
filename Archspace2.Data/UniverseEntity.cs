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
        [ForeignKey("UniverseId")]
        public Universe Universe { get; set; }

        protected UniverseEntity() : base()
        {
        }
        public UniverseEntity(Universe aUniverse) : this()
        {
            Universe = aUniverse;
        }
    }
}
