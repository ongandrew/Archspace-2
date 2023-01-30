using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Universal.Common;

namespace Archspace2
{
    [TestClass]
    [TestCategory("Fleet")]
    public class FleetTests
    {
        [TestMethod]
        public async Task AllFleetsHaveAdmirals()
        {
            User user = await Game.CreateNewUserAsync();

            Assert.IsNotNull(user);

            Race race = Game.Configuration.Races.Random();
            Player player = user.CreatePlayer("FleetTester", race);

            Assert.IsNotNull(player);
            Assert.AreEqual("FleetTester", player.Name);
            Assert.AreEqual(race.Id, player.RaceId);

            List<Fleet> fleets = Game.Universe.Players.SelectMany(x => x.Fleets).ToList();

            foreach (Fleet fleet in fleets)
            {
                Assert.IsNotNull(fleet.Admiral, $"Fleet {fleet.Id} has no admiral.");
            }
        }
    }
}
