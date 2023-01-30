using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Archspace2
{
    [Table("ShipBuildOrder")]
    public class ShipBuildOrder : UniverseEntity
    {
        public int ShipyardId { get; set; }
        public Shipyard Shipyard { get; set; }

        public DateTime OrderTime { get; set; }
        public long NumberToBuild { get; set; }
        private int ShipDesignId { get; set; }
        [ForeignKey("ShipDesignId")]
        public ShipDesign ShipDesign { get; set; }

		public ShipBuildOrder() 
        {
			OrderTime = DateTime.UtcNow;
		}
		public ShipBuildOrder(Shipyard aShipyard, long aNumberToBuild, ShipDesign aShipDesign) : base(aShipyard.Universe)
        {
            OrderTime = DateTime.UtcNow;
            Shipyard = aShipyard;

            NumberToBuild = aNumberToBuild;
            ShipDesign = aShipDesign;
        }
    }
}
