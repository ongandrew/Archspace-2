using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Archspace2.Battle
{
    [TestClass]
    [TestCategory("Battle")]
    public class BattleTests
    {
        [TestMethod]
        public void CanCreateNewSimulation()
        {
            Player attacker = new Player(0, "Attacking Player", Game.Configuration.Races.Single(x => x.Id == (int)RaceType.Human), new List<RacialTrait>(Game.Configuration.Races.Single(x => x.Id == (int)RaceType.Human).BaseTraits));

            Player defender = new Player(1, "Defending Player", Game.Configuration.Races.Single(x => x.Id == (int)RaceType.Xeloss), new List<RacialTrait>(Game.Configuration.Races.Single(x => x.Id == (int)RaceType.Xeloss).BaseTraits));

            Battlefield battlefield = new Battlefield(0, "Test Battlefield");

            Armada attackingArmada = new Armada(attacker);

            attackingArmada.Add(
                new Fleet(
                    0,
                    "0th Fleet",
                    attacker,
                    Game.Configuration.ShipClasses.Single(x => x.Name == "Gunboat"),
                    Game.Configuration.Armors.Single(x => x.Name == "Titanium"),
                    Game.Configuration.Computers.Single(x => x.Name == "Electronic Computer"),
                    Game.Configuration.Engines.Single(x => x.Name == "Retro"),
                    Game.Configuration.Shields.Single(x => x.Name == "Electromagnetic Shield"),
                    new List<Device>(),
                    new List<Weapon>()
                    {
                        Game.Configuration.Weapons.Single(x => x.Name == "Laser")
                    },
                    new Admiral(0, "Attacking Admiral", 1, AdmiralSpecialAbility.EnergySystemSpecialist, AdmiralRacialAbility.ArtifactCoolingEngine, 100, new AdmiralSkills()),
                    6,
                    600
                    )
                );

            Armada defendingArmada = new Armada(defender);

            defendingArmada.Add(
                new Fleet(
                    1,
                    "1st Fleet",
                    attacker,
                    Game.Configuration.ShipClasses.Single(x => x.Name == "Gunboat"),
                    Game.Configuration.Armors.Single(x => x.Name == "Titanium"),
                    Game.Configuration.Computers.Single(x => x.Name == "Electronic Computer"),
                    Game.Configuration.Engines.Single(x => x.Name == "Retro"),
                    Game.Configuration.Shields.Single(x => x.Name == "Electromagnetic Shield"),
                    new List<Device>(),
                    new List<Weapon>()
                    {
                        Game.Configuration.Weapons.Single(x => x.Name == "Laser")
                    },
                    new Admiral(1, "Defending Admiral", 1, AdmiralSpecialAbility.EnergySystemSpecialist, AdmiralRacialAbility.ArtifactCoolingEngine, 100, new AdmiralSkills()),
                    6,
                    600
                    )
            );


            Battle simulation = new Battle(BattleType.Siege, attacker, defender, battlefield, attackingArmada, defendingArmada);
        }

        [TestMethod]
        public void FleetCopyConstructorWorks()
        {
            Player attacker = new Player(0, "Attacking Player", Game.Configuration.Races.Single(x => x.Id == (int)RaceType.Human), new List<RacialTrait>(Game.Configuration.Races.Single(x => x.Id == (int)RaceType.Human).BaseTraits));

            Fleet fleet = new Fleet(
                0,
                "0th Fleet",
                attacker,
                Game.Configuration.ShipClasses.Single(x => x.Name == "Gunboat"),
                Game.Configuration.Armors.Single(x => x.Name == "Titanium"),
                Game.Configuration.Computers.Single(x => x.Name == "Electronic Computer"),
                Game.Configuration.Engines.Single(x => x.Name == "Retro"),
                Game.Configuration.Shields.Single(x => x.Name == "Electromagnetic Shield"),
                new List<Device>(),
                new List<Weapon>()
                {
                                    Game.Configuration.Weapons.Single(x => x.Name == "Laser")
                },
                new Admiral(0, "Attacking Admiral", 1, AdmiralSpecialAbility.EnergySystemSpecialist, AdmiralRacialAbility.ArtifactCoolingEngine, 100, new AdmiralSkills()),
                6,
                600
                );

            Fleet another = new Fleet(fleet);

            Assert.AreEqual(0, another.Id);
            Assert.AreEqual("0th Fleet", another.Name);
            Assert.AreEqual(fleet.Admiral, another.Admiral);
            Assert.AreEqual(fleet.Mobility, another.Mobility);
        }

        [TestMethod]
        public void BasicBattleWorks()
        {
            Player attacker = new Player(0, "Attacking Player", Game.Configuration.Races.Single(x => x.Id == (int)RaceType.Human), new List<RacialTrait>(Game.Configuration.Races.Single(x => x.Id == (int)RaceType.Human).BaseTraits));

            Player defender = new Player(1, "Defending Player", Game.Configuration.Races.Single(x => x.Id == (int)RaceType.Xeloss), new List<RacialTrait>(Game.Configuration.Races.Single(x => x.Id == (int)RaceType.Xeloss).BaseTraits));

            Battlefield battlefield = new Battlefield(0, "Test Battlefield");

            Armada attackingArmada = new Armada(attacker);

            attackingArmada.Add(
                new Fleet(
                    0,
                    "0th Fleet",
                    attacker,
                    Game.Configuration.ShipClasses.Single(x => x.Name == "Gunboat"),
                    Game.Configuration.Armors.Single(x => x.Name == "Titanium"),
                    Game.Configuration.Computers.Single(x => x.Name == "Electronic Computer"),
                    Game.Configuration.Engines.Single(x => x.Name == "Retro"),
                    Game.Configuration.Shields.Single(x => x.Name == "Electromagnetic Shield"),
                    new List<Device>(),
                    new List<Weapon>()
                    {
                        Game.Configuration.Weapons.Single(x => x.Name == "Laser")
                    },
                    new Admiral(0, "Attacking Admiral", 1, AdmiralSpecialAbility.EnergySystemSpecialist, AdmiralRacialAbility.ArtifactCoolingEngine, 100, new AdmiralSkills()),
                    6,
                    600,
                    true
                    )
                );

            attackingArmada.First().Deploy(2500, 5000, 0, Command.Normal);

            Armada defendingArmada = new Armada(defender);

            defendingArmada.Add(
                new Fleet(
                    1,
                    "1st Fleet",
                    attacker,
                    Game.Configuration.ShipClasses.Single(x => x.Name == "Gunboat"),
                    Game.Configuration.Armors.Single(x => x.Name == "Titanium"),
                    Game.Configuration.Computers.Single(x => x.Name == "Electronic Computer"),
                    Game.Configuration.Engines.Single(x => x.Name == "Retro"),
                    Game.Configuration.Shields.Single(x => x.Name == "Electromagnetic Shield"),
                    new List<Device>(),
                    new List<Weapon>()
                    {
                        Game.Configuration.Weapons.Single(x => x.Name == "Laser")
                    },
                    new Admiral(1, "Defending Admiral", 1, AdmiralSpecialAbility.EnergySystemSpecialist, AdmiralRacialAbility.ArtifactCoolingEngine, 100, new AdmiralSkills()),
                    6,
                    600,
                    true
                    )
            );

            defendingArmada.First().Deploy(7500, 5000, 180, Command.Normal);

            Battle simulation = new Battle(BattleType.Siege, attacker, defender, battlefield, attackingArmada, defendingArmada);

            simulation.Run();

            Assert.IsTrue(simulation.IsComplete());
            Assert.IsTrue(simulation.Record.BattleOccurred);
            Assert.IsTrue(simulation.Record.Events.Any());
            Assert.IsTrue(simulation.Record.Events.Any(x => x.Type == RecordEventType.Movement), "No fleets moved.");
            Assert.IsTrue(simulation.Record.Events.Any(x => x.Type == RecordEventType.Fire), "No fleets fired despite battle conditions.");
            Assert.IsTrue(simulation.Record.Events.Any(x => x.Type == RecordEventType.Hit), "No fleets were hit despite battle conditions.");
        }
    }
}
