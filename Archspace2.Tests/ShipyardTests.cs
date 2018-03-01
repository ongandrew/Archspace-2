using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Universal.Common.Extensions;

namespace Archspace2
{
    [TestClass]
    [TestCategory("Shipyard")]
    public class ShipyardTests
    {
        [TestMethod]
        public async Task CanQueueShips()
        {
            User user = await Game.CreateNewUserAsync();

            Assert.IsNotNull(user);

            Race race = Game.Configuration.Races.Random();
            Player player = user.CreatePlayer("Shipqueuer", race);

            Assert.IsNotNull(player);

            int shipQueueLength = player.Shipyard.ShipBuildOrders.Count;

            using (DatabaseContext context = Game.Context)
            {
                context.Attach(player);

                player.Shipyard.PlaceBuildOrder(5, player.ShipDesigns.Random());

                Assert.IsTrue(shipQueueLength < player.Shipyard.ShipBuildOrders.Count);

                await context.SaveChangesAsync();
            }
        }

        [TestMethod]
        public async Task CannotQueueInvalidNumberOfShips()
        {
            User user = await Game.CreateNewUserAsync();

            Assert.IsNotNull(user);

            Race race = Game.Configuration.Races.Random();
            Player player = user.CreatePlayer("Shipqueuer 2", race);

            Assert.IsNotNull(player);

            int shipQueueLength = player.Shipyard.ShipBuildOrders.Count;
            
            player.Shipyard.PlaceBuildOrder(-5, player.ShipDesigns.Random());
            Assert.AreEqual(shipQueueLength, player.Shipyard.ShipBuildOrders.Count);

            player.Shipyard.PlaceBuildOrder(0, player.ShipDesigns.Random());
            Assert.AreEqual(shipQueueLength, player.Shipyard.ShipBuildOrders.Count);
        }

        [TestMethod]
        public async Task CanChangeDockedShips()
        {
            User user = await Game.CreateNewUserAsync();

            Assert.IsNotNull(user);

            Race race = Game.Configuration.Races.Random();
            Player player = user.CreatePlayer("Ship Giver", race);

            Assert.IsNotNull(player);

            using (DatabaseContext context = Game.Context)
            {
                context.Attach(player);

                ShipDesign design = player.ShipDesigns.Random();

                Assert.AreEqual(0, player.Shipyard.GetDockedShipCount(design));

                player.Shipyard.ChangeDockedShip(design, 7);

                Assert.AreEqual(7, player.Shipyard.GetDockedShipCount(design));

                player.Shipyard.ChangeDockedShip(design, -7);

                Assert.AreEqual(0, player.Shipyard.GetDockedShipCount(design));

                await context.SaveChangesAsync();
            }
        }

        [TestMethod]
        public async Task CanScrapDockedShips()
        {
            User user = await Game.CreateNewUserAsync();

            Assert.IsNotNull(user);

            Race race = Game.Configuration.Races.Random();
            Player player = user.CreatePlayer("Ship Scrapper", race);

            Assert.IsNotNull(player);

            using (DatabaseContext context = Game.Context)
            {
                context.Attach(player);

                ShipDesign design = player.ShipDesigns.Random();

                Assert.AreEqual(0, player.Shipyard.GetDockedShipCount(design));

                player.Shipyard.ChangeDockedShip(design, 7);

                Assert.AreEqual(7, player.Shipyard.GetDockedShipCount(design));

                long previousPP = player.Resource.ProductionPoint;

                player.Shipyard.ScrapDockedShip(design, 3);

                Assert.AreEqual(4, player.Shipyard.GetDockedShipCount(design));
                Assert.IsTrue(player.Resource.ProductionPoint > previousPP);

                await context.SaveChangesAsync();
            }
        }

        [TestMethod]
        public async Task QueuedShipsAreBuilt()
        {
            User user = await Game.CreateNewUserAsync();

            Assert.IsNotNull(user);

            Race race = Game.Configuration.Races.Random();
            Player player = user.CreatePlayer("Ship Builder", race);

            Assert.IsNotNull(player);

            int shipQueueLength = player.Shipyard.ShipBuildOrders.Count;

            using (DatabaseContext context = Game.Context)
            {
                context.Attach(player);

                ShipDesign design = player.ShipDesigns.Random();
                player.Shipyard.PlaceBuildOrder(3, design);

                Assert.AreEqual(0, player.Shipyard.GetDockedShipCount(design));

                Assert.IsTrue(shipQueueLength < player.Shipyard.ShipBuildOrders.Count);

                int totalUpdatedTurns = 0;
                while (player.Shipyard.ShipBuildOrders.Any() && totalUpdatedTurns < 100)
                {
                    player.UpdateTurn();
                    totalUpdatedTurns++;
                }

                Assert.AreNotEqual(100, totalUpdatedTurns, "Could not build ships.");
                Assert.AreEqual(3, player.Shipyard.GetDockedShipCount(design));

                await context.SaveChangesAsync();
            }

            
        }
    }
}
