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
    [TestCategory("Player")]
    public class PlayerTests
    {
        [TestMethod]
        public async Task CanCreateNewPlayer()
        {
            User user = await Game.CreateNewUserAsync();

            Assert.IsNotNull(user);

            Race race = Game.Configuration.Races.Random();
            Player player = await user.CreatePlayerAsync("Tester", race);

            Assert.IsNotNull(player);
            Assert.AreEqual("Tester", player.Name);
            Assert.AreEqual(race.Id, player.RaceId);
        }
    }
}
