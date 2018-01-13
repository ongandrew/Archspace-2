using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Universal.Common.Extensions;

namespace Archspace2
{
    public sealed class Universe : Entity
    {
        private Universe() : base()
        {
            Clusters = new List<Cluster>();
            Councils = new List<Council>();
            Players = new List<Player>();
        }

        public Universe(DateTime aFromDate, DateTime? aToDate = null) : this()
        {
            FromDate = aFromDate;
            ToDate = aToDate;
        }

        public int? BlackMarketId { get; set; }
        [ForeignKey("BlackMarketId")]
        public BlackMarket BlackMarket { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public void Initialize()
        {
            BlackMarket = CreateBlackMarket();

            for (int i = 0; i < Game.Configuration.Universe.StartingClusterCount; i++)
            {
                Clusters.Add(new Cluster(this));
            }

            for (int i = 0; i < Game.Configuration.Universe.StartingCouncilCount; i++)
            {
                Councils.Add(new Council(this));
            }

            for (int i = 0; i < Game.Configuration.Universe.StartingBotCount; i++)
            {
                Players.Add(CreateBot());
            }
        }

        private void SetUniverse(UniverseEntity aUniverseEntity)
        {
            aUniverseEntity.Universe = this;
        }

        private BlackMarket CreateBlackMarket()
        {
            BlackMarket blackMarket = new BlackMarket(this);

            return blackMarket;
        }

        private Player CreateBot()
        {
            Player bot = new Player(this);
            SetUniverse(bot);

            bot.Type = PlayerType.Bot;
            bot.Name = Game.Configuration.Universe.BotNames.Random();
            bot.Council = GetCouncilsWithCapacity().Random();

            return bot;
        }

        public Player CreatePlayer(string aName, Race aRace)
        {
            Player player = new Player(this);
            SetUniverse(player);

            player.Name = aName;
            player.Race = aRace;
            player.Council = GetCouncilsWithCapacity().Random();

            Players.Add(player);

            return player;
        }

        public List<Council> GetCouncilsWithCapacity()
        {
            return Councils.ToList();
        }
        
        public ICollection<Cluster> Clusters { get; set; }
        public ICollection<Council> Councils { get; set; }
        public ICollection<Player> Players { get; set; }
    }
}
