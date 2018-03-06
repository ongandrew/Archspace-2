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

            using (DatabaseContext context = Game.GetContext())
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

            using (DatabaseContext context = Game.GetContext())
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

            using (DatabaseContext context = Game.GetContext())
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

        [TestMethod]
        public async Task NewPlayerHasNoDuplicateAdmirals()
        {
            User user = await Game.CreateNewUserAsync();
            Race race = Game.Configuration.Races.Random();
            Player player = user.CreatePlayer("Admiral Spawner", race);

            using (DatabaseContext context = Game.GetContext())
            {
                context.Attach(Game.Universe);

                for (int i = 0; i < 100; i++)
                {
                    player.SpawnAdmiral();
                }

                await context.SaveChangesAsync();

                Assert.AreEqual(player.Admirals.Select(x => x.Id).Distinct().Count(), player.Admirals.Count);
            }
        }

        [TestMethod]
        public async Task ExpeditionFindsPlanet()
        {
            User user = await Game.CreateNewUserAsync();

            Assert.IsNotNull(user);

            Race race = Game.Configuration.Races.Random();
            Player player = user.CreatePlayer("Expedition Test", race);

            Assert.IsNotNull(player);

            using (DatabaseContext context = Game.GetContext())
            {
                context.Attach(player);

                await context.SaveChangesAsync();

                Fleet fleet = player.Fleets.Random();

                player.SendExpedition(fleet.Id);

                Assert.AreEqual(FleetStatus.UnderMission, fleet.Status);
                Assert.AreEqual(MissionType.Expedition, fleet.Mission.Type);

                await context.SaveChangesAsync();

                for (int i = 0; i < 100; i++)
                {
                    await Task.Run(() => Game.Universe.UpdateTurn());
                }

                await context.SaveChangesAsync();

                Assert.AreEqual(2, player.Planets.Count);
                Assert.AreEqual(FleetStatus.UnderMission, fleet.Status);
                Assert.AreEqual(MissionType.ReturningWithPlanet, fleet.Mission.Type);

                for (int i = 0; i < 100; i++)
                {
                    await Task.Run(() => Game.Universe.UpdateTurn());
                }

                Assert.AreEqual(FleetStatus.StandBy, fleet.Status);
                Assert.AreEqual(MissionType.None, fleet.Mission.Type);
                await context.SaveChangesAsync();
            }

            
        }
    }
}
