using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Archspace2
{
    [Table("Empire")]
    public class Empire : UniverseEntity
    {
        public Empire(Universe aUniverse) : base(aUniverse)
        {
        }
    }
}
