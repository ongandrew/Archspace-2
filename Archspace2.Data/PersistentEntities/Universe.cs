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
            Empire = new Empire(this);
            BlackMarket = new BlackMarket(this);

            Clusters = new List<Cluster>();
            Councils = new List<Council>();
            Players = new List<Player>();
        }

        public Universe(DateTime aFromDate, DateTime? aToDate = null) : this()
        {
            FromDate = aFromDate;
            ToDate = aToDate;
        }

        public int? EmpireId { get; set; }
        [ForeignKey("EmpireId")]
        public Empire Empire { get; set; }

        public int? BlackMarketId { get; set; }
        [ForeignKey("BlackMarketId")]
        public BlackMarket BlackMarket { get; set; }

        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public int CurrentTurn { get; set; }

        public ICollection<Cluster> Clusters { get; set; }
        public ICollection<Council> Councils { get; set; }
        public ICollection<Player> Players { get; set; }

        public void Initialize()
        {
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
                CreateBot();
            }
        }

        public Player CreateBot()
        {
            Player bot = CreatePlayer(Game.Configuration.Universe.BotNames.Random(), Game.Configuration.Races.Random());

            bot.Type = PlayerType.Bot;

            return bot;
        }

        public Player CreatePlayer(string aName, Race aRace)
        {
            Player player = new Player(this);

            player.Turn = CurrentTurn;

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
                player.SpawnAdmiral();
            }

            Admiral admiral1 = player.CreateAdmiral().AsPlayerAdmiral(player);
            Fleet fleet1 = player.CreateFleet(1, "Royal Guard Fleet", admiral1, shipDesign1, 6);

            Admiral admiral2 = player.CreateAdmiral().AsPlayerAdmiral(player);
            Fleet fleet2 = player.CreateFleet(2, "Royal Guard Fleet", admiral2, shipDesign1, 6);

            Admiral admiral3 = player.CreateAdmiral().AsPlayerAdmiral(player);
            Fleet fleet3 = player.CreateFleet(3, "Royal Guard Fleet", admiral3, shipDesign2, 6);

            Players.Add(player);

            return player;
        }

        public List<Council> GetCouncilsWithCapacity()
        {
            return Councils.ToList();
        }
        
        public void Update()
        {
            foreach (Player player in Players)
            {
                player.Update();
            }
        }

        public void UpdateTurn()
        {
            CurrentTurn++;
            Update();
        }
    }
}
