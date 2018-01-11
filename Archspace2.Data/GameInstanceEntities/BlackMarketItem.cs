using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Archspace2
{
    public enum BlackMarketItemType
    {
        Tech,
        Fleet,
        Admiral,
        Project,
        Planet
    };

    public class BlackMarketItem : UniverseEntity
    {
        public int ObjectId { get; set; }
        public BlackMarketItemType Type { get; set; }

        public int CurrentBid { get; set; }

        public int? BidderId { get; set; }
        [ForeignKey("BidderId")]
        public Player Bidder { get; set; }

        public async Task<Admiral> AsAdmiralAsync()
        {
            if (Type == BlackMarketItemType.Fleet)
            {
                return await Game.Context.Admirals.SingleAsync(x => x.Id == ObjectId);
            }
            else
            {
                throw new InvalidOperationException("Item is not an admiral.");
            }
        }

        public async Task<Fleet> AsFleetAsync()
        {
            if (Type == BlackMarketItemType.Fleet)
            {
                return await Game.Context.Fleets.SingleAsync(x => x.Id == ObjectId);
            }
            else
            {
                throw new InvalidOperationException("Item is not a fleet.");
            }
        }

        public async Task<Planet> AsPlanetAsync()
        {
            if (Type == BlackMarketItemType.Planet)
            {
                return await Game.Context.Planets.SingleAsync(x => x.Id == ObjectId);
            }
            else
            {
                throw new InvalidOperationException("Item is not a planet.");
            }
        }

        public async Task<Project> AsProjectAsync()
        {
            if (Type == BlackMarketItemType.Project)
            {
                return Game.Configuration.Projects.Single(x => x.Id == ObjectId);
            }
            else
            {
                throw new InvalidOperationException("Item is not a planet.");
            }
        }

        public async Task<Tech> AsTechAsync()
        {
            if (Type == BlackMarketItemType.Tech)
            {
                return Game.Configuration.Techs.Single(x => x.Id == ObjectId);
            }
            else
            {
                throw new InvalidOperationException("Item is not a tech.");
            }
        }
    }
}
