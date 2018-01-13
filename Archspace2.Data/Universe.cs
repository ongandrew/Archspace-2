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

        private BlackMarket CreateBlackMarket()
        {
            BlackMarket blackMarket = new BlackMarket(this);

            return blackMarket;
        }

        private Player CreateBot()
        {
            Player bot = CreatePlayer(Game.Configuration.Universe.BotNames.Random(), Game.Configuration.Races.Random());

            bot.Type = PlayerType.Bot;

            return bot;
        }

        public Player CreatePlayer(string aName, Race aRace)
        {
            Player player = new Player(this);

            player.Name = aName;
            player.Race = aRace;
            player.Council = GetCouncilsWithCapacity().Random();
            player.ProductionPoint = 50000;
            player.Techs = Game.Configuration.Techs.Where(x => x.Attribute == TechAttribute.Basic).ToList();
            player.Techs.AddRange(Game.Configuration.Techs.Where(x => player.Race.BaseTechs.Contains(x.Id)));
            player.ConcentrationMode = ConcentrationMode.Balanced;

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
