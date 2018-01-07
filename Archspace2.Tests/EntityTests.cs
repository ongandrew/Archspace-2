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
            GameConfiguration gameConfiguration = new GameConfiguration();
            gameConfiguration.UseDefaults();

            Console.WriteLine(gameConfiguration);
        }
    }
}
