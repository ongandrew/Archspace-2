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

            using (DatabaseContext context = Game.Context)
            {
                context.Attach(player);

                await Task.Run(() =>
                player.Shipyard.PlaceBuildOrder(5, player.ShipDesigns.Random()));

                await context.SaveChangesAsync();
            }
        }
    }
}
