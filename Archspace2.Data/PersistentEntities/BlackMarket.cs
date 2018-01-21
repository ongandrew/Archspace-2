﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Archspace2
{
    [Table("BlackMarket")]
    public class BlackMarket : UniverseEntity
    {
        [NotMapped]
        public override string Name { get; set; }

        public ICollection<BlackMarketItem> BlackMarketItems { get; set; }

        public BlackMarketItem AddListing(Planet aPlanet)
        {
            BlackMarketItem result = CreateBlackMarketItem();
            result.ObjectId = aPlanet.Id;
            result.Type = BlackMarketItemType.Planet;

            BlackMarketItems.Add(result);

            return result;
        }

        public BlackMarket(Universe aUniverse) : base(aUniverse)
        {
            BlackMarketItems = new List<BlackMarketItem>();
        }

        private BlackMarketItem CreateBlackMarketItem()
        {
            BlackMarketItem result = new BlackMarketItem(Universe);
            result.BlackMarket = this;

            return result;
        }
    }
}