using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
    }
}
