using Microsoft.EntityFrameworkCore;
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
            Player player = user.CreatePlayer("Tester", race);

            Assert.IsNotNull(player);
            Assert.AreEqual("Tester", player.Name);
            Assert.AreEqual(race.Id, player.RaceId);
        }

        [TestMethod]
        public async Task CanUpdateTurn()
        {
            User user = await Game.CreateNewUserAsync();

            Assert.IsNotNull(user);

            Race race = Game.Configuration.Races.Random();
            Player player = user.CreatePlayer("TurnTest", race);

            Assert.IsNotNull(player);

            using (DatabaseContext context = Game.Context)
            {
                context.Attach(player);

                await Task.Run(() =>
                player.UpdateTurn());

                await context.SaveChangesAsync();
            }
        }

        [TestMethod]
        public async Task CanResearchTech()
        {
            User user = await Game.CreateNewUserAsync();

            Assert.IsNotNull(user);

            Race race = Game.Configuration.Races.Random();
            Player player = user.CreatePlayer("ResearchTechTest", race);

            Assert.IsNotNull(player);

            player.Resource.ResearchPoint = 1000000;
            List<Tech> before = player.Techs.ToList();

            using (DatabaseContext context = Game.Context)
            {
                context.Attach(player);

                await Task.Run(() =>
                player.UpdateTurn());

                List<Tech> after = player.Techs.ToList();

                await context.SaveChangesAsync();

                Assert.IsTrue(after.Count > before.Count, "Could not research tech despite having enough RP.");
            }
        }

        [TestMethod]
        public async Task CanDiscoverTech()
        {
            User user = await Game.CreateNewUserAsync();

            Assert.IsNotNull(user);

            Race race = Game.Configuration.Races.Random();
            Player player = user.CreatePlayer("DiscoverTech", race);

            Assert.IsNotNull(player);

            using (DatabaseContext context = Game.Context)
            {
                context.Attach(player);

                await Task.Run(() =>
                player.UpdateTurn());
                player.DiscoverTech(Game.Configuration.Techs.Single(x => x.Id == 1335));

                List<Tech> after = player.Techs.ToList();

                await context.SaveChangesAsync();

                Assert.IsTrue(player.Techs.Any(x => x.Id == 1335));

                player = await context.Players.Where(x => x.Name == "DiscoverTech").SingleOrDefaultAsync();

                Assert.IsNotNull(player);

                Assert.IsTrue(player.Techs.Any(x => x.Id == 1335));
            }
        }
    }
}
