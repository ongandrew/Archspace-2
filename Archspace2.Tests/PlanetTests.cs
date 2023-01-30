using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Threading.Tasks;
using Universal.Common;

namespace Archspace2
{
    [TestClass]
    [TestCategory("Planet")]
    public class PlanetTests
    {
        [TestMethod]
        public void HomePlanetSettingsAreCorrect()
        {
            Player player = new Player(Game.Universe);
            player.Race = Game.Configuration.Races.Single(x => x.Id == (int)RaceType.Human);
            Planet planet = new Planet(Game.Universe);
            
            planet.AsHomePlanet(player);

            Assert.AreEqual(PlanetResource.Normal, planet.Resource);
            Assert.AreEqual(PlanetSize.Medium, planet.Size);
            Assert.AreEqual(planet.Population, 50000);

            Assert.AreEqual(planet.Atmosphere.CH4, player.Race.HomeAtmosphere.CH4);
            Assert.AreEqual(planet.Atmosphere.Cl2, player.Race.HomeAtmosphere.Cl2);
            Assert.AreEqual(planet.Atmosphere.CO2, player.Race.HomeAtmosphere.CO2);
            Assert.AreEqual(planet.Atmosphere.H2, player.Race.HomeAtmosphere.H2);
            Assert.AreEqual(planet.Atmosphere.H2O, player.Race.HomeAtmosphere.H2O);
            Assert.AreEqual(planet.Atmosphere.N2, player.Race.HomeAtmosphere.N2);
            Assert.AreEqual(planet.Atmosphere.O2, player.Race.HomeAtmosphere.O2);

            Assert.AreEqual(planet.Gravity, player.Race.HomeGravity);
            Assert.AreEqual(planet.Investment, 0);
            Assert.AreEqual(planet.Infrastructure.Factory, 30);
            Assert.AreEqual(planet.Infrastructure.ResearchLab, 10);
            Assert.AreEqual(planet.Infrastructure.MilitaryBase, 10);
        }

        [TestMethod]
        public void InvestingProducesCorrectResults()
        {
            Player player = new Player(Game.Universe);
            player.Race = Game.Configuration.Races.Single(x => x.Id == (int)RaceType.Human);
            Planet planet = new Planet(Game.Universe);

            planet.AsHomePlanet(player);

            Assert.AreEqual(0, planet.Investment);
            planet.Investment += 20000;
            Assert.AreEqual(20000, planet.Investment);
        }

        [TestMethod]
        public async Task CanCalculateVariousQuantities()
        {
            User user = await Game.CreateNewUserAsync();

            Assert.IsNotNull(user);

            Race race = Game.Configuration.Races.Random();
            Player player = user.CreatePlayer("Calculator", race);

            Assert.IsNotNull(player);
            Planet planet = new Planet(Game.Universe);

            planet.AsHomePlanet(player);

            planet.CalculateUsableInvestment();

            planet.CalculateInvestRate();

            planet.CalculateMaxInvestmentProudction();
        }
    }
}
