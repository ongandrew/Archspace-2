using Archspace2.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Universal.Common;

namespace Archspace2
{
    [TestClass]
    [TestCategory("General")]
    public class GeneralTests
    {
        public enum TestEnum
        {
            Test = -3,
            Hello = 2,
            Cypherus = 4,
            Hi = -6
        };

        [TestMethod]
        public void EnumMinMaxGenerateCorrectValues()
        {
            int min = typeof(TestEnum).Min();
            TestEnum minEnum = typeof(TestEnum).Min<TestEnum>();

            Assert.AreEqual(-6, min);
            Assert.AreEqual(TestEnum.Hi, minEnum);

            int max = typeof(TestEnum).Max();
            TestEnum maxEnum = typeof(TestEnum).Max<TestEnum>();

            Assert.AreEqual(4, max);
            Assert.AreEqual(TestEnum.Cypherus, maxEnum);
        }

        [TestMethod]
        public void EnumModifyByIntIsCorrect()
        {
            TestEnum firstTest = TestEnum.Hello.ModifyByInt(2);

            Assert.AreEqual(TestEnum.Cypherus, firstTest);

            TestEnum secondTest = TestEnum.Hi.ModifyByInt(4);

            Assert.AreEqual(TestEnum.Test, secondTest);

            TestEnum thirdTest = TestEnum.Cypherus.ModifyByInt(-8);

            Assert.AreEqual(TestEnum.Test, thirdTest);
        }

        [TestMethod]
        public void CalculateTotalEffectReturnsCorrectValue()
        {
            List<FleetEffect> effects = new List<FleetEffect>()
            {
                new FleetEffect()
                {
                    Type = FleetEffectType.AttackRating,
                    Amount = 20
                },
                new FleetEffect()
                {
                    Type = FleetEffectType.AttackRating,
                    Amount = 20,
                    ModifierType = ModifierType.Proportional
                }
            };

            int test1 = effects.Where(x => x.Type == FleetEffectType.AttackRating).CalculateTotalEffect(0, x => x.Amount);

            Assert.AreEqual(20, test1, "Wrong amount calculated when there is not based effect.");

            int test2 = effects.Where(x => x.Type == FleetEffectType.AttackRating).CalculateTotalEffect(20, x => x.Amount);

            Assert.AreEqual(44, test2, "Wrong amount calculated when a base value is non-zero.");

            effects.Clear();

            int test3 = effects.Where(x => x.Type == FleetEffectType.AttackRating).CalculateTotalEffect(0, x => x.Amount);

            Assert.AreEqual(0, test3, "Wrong amount calculated when the list is empty.");

            int test4 = effects.Where(x => x.Type == FleetEffectType.AttackRating).CalculateTotalEffect(20, x => x.Amount);

            Assert.AreEqual(20, test4, "Wrong amount calculated when the list is empty.");
        }

        [TestMethod]
        public async Task EntitesAreNotPersistedIfNotAttachedToContext()
        {
            Council council = new Council(Game.Universe);

            User user2 = await Game.CreateNewUserAsync();
            Race race2 = Game.Configuration.Races.Random();
            Player player2 = user2.CreatePlayer("Persistence Tester", race2);

            Assert.AreEqual(0, player2.DefensePlans.Count);

            using (DatabaseContext context = Game.GetContext())
            {
                context.Attach(Game.Universe);

                await context.SaveChangesAsync();
            }

            DefensePlan defensePlan = player2.CreateDefensePlan();
            Assert.AreEqual(0, player2.DefensePlans.Count);

            using (DatabaseContext context = Game.GetContext())
            {
                context.Attach(Game.Universe);

                await context.SaveChangesAsync();
            }

            Assert.AreEqual(0, player2.DefensePlans.Count);

            using (DatabaseContext context = Game.GetContext())
            {
                context.Attach(Game.Universe);

                DefensePlan defensePlan2 = player2.CreateDefensePlan();

                await context.SaveChangesAsync();

                Assert.AreEqual(0, player2.DefensePlans.Count);
            }
        }
    }
}
