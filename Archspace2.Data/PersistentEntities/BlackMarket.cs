using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Archspace2
{
    [Table("BlackMarket")]
    public class BlackMarket : UniverseEntity
    {
        public BlackMarket(Universe aUniverse) : base(aUniverse)
        {
        }

        public ICollection<BlackMarketItem> BlackMarketItems { get; set; }
    }
}
