using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class BlackMarket : UniverseEntity
    {
        protected BlackMarket()
        {
        }

        public ICollection<BlackMarketItem> BlackMarketItems { get; set; }
    }
}
