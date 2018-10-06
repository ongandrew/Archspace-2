using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Archspace2
{
    [TestClass]
    [TestCategory("Configuration")]
    public class ConfigurationTests
    {
        [TestMethod]
        public void DefaultsValid()
        {
            ConfigurationBuilder configurationBuilder = new ConfigurationBuilder().UseDefaults();

            Configuration configuration = configurationBuilder.Build();
            Assert.AreNotEqual(0, configuration.Armors.Count);
            Assert.AreNotEqual(0, configuration.Computers.Count);
            Assert.AreNotEqual(0, configuration.Devices.Count);
            Assert.AreNotEqual(0, configuration.Engines.Count);
            Assert.AreNotEqual(0, configuration.Events.Count);
            Assert.AreNotEqual(0, configuration.PlanetAttributes.Count);
            Assert.AreNotEqual(0, configuration.Projects.Count);
            Assert.AreNotEqual(0, configuration.Races.Count);
            Assert.AreNotEqual(0, configuration.Shields.Count);
            Assert.AreNotEqual(0, configuration.ShipClasses.Count);
            Assert.AreNotEqual(0, configuration.SpyActions.Count);
            Assert.AreNotEqual(0, configuration.Techs.Count);
            Assert.AreNotEqual(0, configuration.Weapons.Count);
        }
    }
}
