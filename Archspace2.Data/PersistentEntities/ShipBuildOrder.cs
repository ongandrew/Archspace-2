using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Archspace2
{
    [Table("ShipBuildOrder")]
    public class ShipBuildOrder : UniverseEntity
    {
        public int ShipyardId { get; set; }
        public Shipyard Shipyard { get; set; }

        public DateTime OrderTime { get; set; }
        public int NumberToBuild { get; set; }
        private int ShipDesignId { get; set; }
        [ForeignKey("ShipDesignId")]
        public ShipDesign ShipDesign { get; set; }

        public ShipBuildOrder(Shipyard aShipyard, int aNumberToBuild, ShipDesign aShipDesign) : base(aShipyard.Universe)
        {
            OrderTime = DateTime.UtcNow;
            Shipyard = aShipyard;

            NumberToBuild = aNumberToBuild;
            ShipDesign = aShipDesign;
        }
    }
}
