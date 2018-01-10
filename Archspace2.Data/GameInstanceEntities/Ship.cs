using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Archspace2
{
    public class Ship : UniverseEntity
    {
        public int DesignId { get; set; }

        [ForeignKey("DesignId")]
        ShipDesign Design { get; set; }
    }
}
