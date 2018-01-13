using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
