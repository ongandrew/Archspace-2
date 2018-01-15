using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using Universal.Common.Extensions;

namespace Archspace2
{
    [Table("Universe")]
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

        public int CurrentTurn { get; set; }
        
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
            player.Techs = Game.Configuration.Techs.Where(x => x.Attribute == TechAttribute.Basic).ToList();
            player.Techs.AddRange(Game.Configuration.Techs.Where(x => player.Race.BaseTechs.Contains(x.Id)));
            player.ConcentrationMode = ConcentrationMode.Balanced;
            player.Traits.AddRange(aRace.BaseTraits);

            player.Planets.Add(Clusters.Random().CreatePlanet().AsHomePlanet(player));

            ShipDesign shipDesign1 = player.CreateShipDesign().AsInitialShipDesign(0);
            ShipDesign shipDesign2 = player.CreateShipDesign().AsInitialShipDesign(1);
            player.ShipDesigns.Add(shipDesign1);
            player.ShipDesigns.Add(shipDesign2);

            for (int i = 0; i < Game.Configuration.Player.StartingAdmiralCount; i++)
            {
                player.Admirals.Add(player.CreateAdmiral().AsPlayerAdmiral(player));
            }

            Fleet fleet1 = player.CreateFleet();
            fleet1.Name = "1st Royal Guard Fleet";
            fleet1.Admiral = player.GetAdmiralPool().Random();
            fleet1.ShipDesign = shipDesign1;
            fleet1.CurrentShipCount = 6;
            fleet1.MaxShipCount = 6;

            Fleet fleet2 = player.CreateFleet();
            fleet2.Name = "2nd Royal Guard Fleet";
            fleet2.Admiral = player.GetAdmiralPool().Random();
            fleet2.ShipDesign = shipDesign1;
            fleet2.CurrentShipCount = 6;
            fleet2.MaxShipCount = 6;

            Fleet fleet3 = player.CreateFleet();
            fleet3.Name = "3rd Royal Guard Fleet";
            fleet3.Admiral = player.GetAdmiralPool().Random();
            fleet3.ShipDesign = shipDesign2;
            fleet3.CurrentShipCount = 6;
            fleet3.MaxShipCount = 6;

            player.Fleets.Add(fleet1);
            player.Fleets.Add(fleet2);
            player.Fleets.Add(fleet3);

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
