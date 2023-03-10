using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Archspace2
{
    [TestClass]
    [TestCategory("Universe")]
    public class UniverseTests
    {
        [TestMethod]
        public async Task CanCreateNewUniverse()
        {
            Universe currentUniverse = Game.Universe;

            Assert.IsNotNull(currentUniverse);

            await Game.CreateNewUniverseAsync(DateTime.UtcNow, DateTime.UtcNow.AddYears(1));

            Universe newUniverse = Game.Universe;

            Assert.IsNotNull(newUniverse);

            Assert.AreNotEqual(currentUniverse.Id, newUniverse.Id);
        }

        [TestMethod]
        public async Task NoDuplicatePlayersInUniverse()
        {
            Universe currentUniverse = Game.Universe;

            Assert.IsNotNull(currentUniverse);

            await Game.CreateNewUniverseAsync(DateTime.UtcNow, DateTime.UtcNow.AddYears(1));

            Universe newUniverse = Game.Universe;

            Assert.IsNotNull(newUniverse);

            Assert.AreNotEqual(currentUniverse.Id, newUniverse.Id);

            Assert.AreEqual(Game.Universe.Players.Select(x => x.Id).Distinct().Count(), Game.Universe.Players.Count);
        }

        [TestMethod]
        public async Task CanUpdateManyTurns()
        {
            using (DatabaseContext context = Game.GetContext())
            {
                context.Attach(Game.Universe);

                for (int i = 0; i < 10000; i++)
                {
                    Game.Universe.UpdateTurn();
                }

                await context.SaveChangesAsync();
            }
            
        }
    }
}
