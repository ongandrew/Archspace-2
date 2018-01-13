using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class BlackMarket : UniverseEntity
    {
        public BlackMarket(Universe aUniverse) : base(aUniverse)
        {
        }

        public ICollection<BlackMarketItem> BlackMarketItems { get; set; }
    }
}
