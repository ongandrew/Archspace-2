using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Archspace2
{
    [TestClass]
    [TestCategory("Entity")]
    public class EntityTests
    {
        [TestMethod]
        public void GameConfigurationDefaultsAreCorrect()
        {
            Console.WriteLine(Game.Configuration);
        }
    }
}
