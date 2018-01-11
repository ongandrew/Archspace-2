using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Archspace2
{
    [TestClass]
    [TestCategory("Game")]
    public class GameTests
    {
        [TestMethod]
        public void DefaultGameConfigurationSuccessfullyValidates()
        {
            ValidateResult result = GameConfiguration.CreateDefault().Validate();
            Assert.IsTrue(result.IsPassResult(), string.Join("\n", result.Items.Select(x => $"{x.Severity.ToString()}: {x.Message}")));

            if (result.Items.Any())
            {
                Console.WriteLine(string.Join("\n", result.Items.Select(x => $"{x.Severity.ToString()}: {x.Message}")));
            }
        }

        [TestMethod]
        public void DuplicateIdsWithinSameClassFailsValidation()
        {
            GameConfiguration configuration = new GameConfiguration();
            configuration.Armors = new List<Armor>()
            {
                new Armor()
                {
                    Id = 1
                },
                new Armor()
                {
                    Id = 1
                }
            };
            
            ValidateResult result = configuration.Validate();
            Assert.IsFalse(result.IsPassResult(), "Repeated Id within category is not identified by validation.");
        }

        [TestMethod]
        public void NonExistentTechRequirementFailsValidation()
        {
            GameConfiguration configuration = new GameConfiguration();
            configuration.Armors = new List<Armor>()
            {
                new Armor()
                {
                    Id = 1,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1000
                        }
                    }
                }
            };

            ValidateResult result = configuration.Validate();
            Assert.IsFalse(result.IsPassResult(), "Non-existent tech requirement error not identified by validation.");
        }
    }
}
