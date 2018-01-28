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
                    new Admiral(0, "Attacking Admiral", 1, AdmiralSpecialAbility.EnergySystemSpecialist, AdmiralRacialAbility.ArtifactCoolingEngine, 100, 5, 5, new AdmiralSkills(), ArmadaClass.C),
                    6,
                    600,
                    true
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
                    new Admiral(1, "Defending Admiral", 1, AdmiralSpecialAbility.EnergySystemSpecialist, AdmiralRacialAbility.ArtifactCoolingEngine, 100, 5, 5, new AdmiralSkills(), ArmadaClass.C),
                    6,
                    600,
                    true
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
                new Admiral(0, "Attacking Admiral", 1, AdmiralSpecialAbility.EnergySystemSpecialist, AdmiralRacialAbility.ArtifactCoolingEngine, 100, 5, 5, new AdmiralSkills(), ArmadaClass.C),
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
                    new Admiral(0, "Attacking Admiral", 1, AdmiralSpecialAbility.EnergySystemSpecialist, AdmiralRacialAbility.ArtifactCoolingEngine, 100, 5, 5, new AdmiralSkills(), ArmadaClass.C),
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
                    new Admiral(1, "Defending Admiral", 1, AdmiralSpecialAbility.EnergySystemSpecialist, AdmiralRacialAbility.ArtifactCoolingEngine, 100, 5, 5, new AdmiralSkills(), ArmadaClass.C),
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

        [TestMethod]
        public void LargerBattleWorks()
        {
            Player attacker = new Player(0, "Attacking Player", Game.Configuration.Races.Single(x => x.Id == (int)RaceType.Human), new List<RacialTrait>(Game.Configuration.Races.Single(x => x.Id == (int)RaceType.Human).BaseTraits));

            Player defender = new Player(1, "Defending Player", Game.Configuration.Races.Single(x => x.Id == (int)RaceType.Xeloss), new List<RacialTrait>(Game.Configuration.Races.Single(x => x.Id == (int)RaceType.Xeloss).BaseTraits));

            Battlefield battlefield = new Battlefield(0, "Test Battlefield");

            Armada attackingArmada = new Armada(attacker);

            Fleet fleetA1 = new Fleet(
                    1,
                    "Attacker 1st Fleet",
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
                    new Admiral(1, "Attacking Admiral", 1, AdmiralSpecialAbility.EnergySystemSpecialist, AdmiralRacialAbility.ArtifactCoolingEngine, 100, 5, 5, new AdmiralSkills(), ArmadaClass.C),
                    6,
                    600,
                    true
                    );
            Fleet fleetA2 = new Fleet(
                    2,
                    "Attacker 2nd Fleet",
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
                    new Admiral(2, "Attacking Admiral", 1, AdmiralSpecialAbility.EnergySystemSpecialist, AdmiralRacialAbility.ArtifactCoolingEngine, 100, 5, 5, new AdmiralSkills(), ArmadaClass.C),
                    6,
                    600
                    );
            Fleet fleetA3 = new Fleet(
                    3,
                    "Attacker 3rd Fleet",
                    attacker,
                    Game.Configuration.ShipClasses.Single(x => x.Name == "Doomstar"),
                    Game.Configuration.Armors.Single(x => x.Name == "Titanium"),
                    Game.Configuration.Computers.Single(x => x.Name == "Neuro Computer"),
                    Game.Configuration.Engines.Single(x => x.Name == "Phase"),
                    Game.Configuration.Shields.Single(x => x.Name == "Electromagnetic Shield"),
                    new List<Device>(),
                    new List<Weapon>()
                    {
                        Game.Configuration.Weapons.Single(x => x.Name == "Homing Black Hole"),
                        Game.Configuration.Weapons.Single(x => x.Name == "Homing Black Hole"),
                        Game.Configuration.Weapons.Single(x => x.Name == "Homing Black Hole"),
                        Game.Configuration.Weapons.Single(x => x.Name == "Homing Black Hole"),
                        Game.Configuration.Weapons.Single(x => x.Name == "Homing Black Hole"),
                        Game.Configuration.Weapons.Single(x => x.Name == "Homing Black Hole"),
                        Game.Configuration.Weapons.Single(x => x.Name == "Homing Black Hole")
                    },
                    new Admiral(3, "Attacking Admiral", 1, AdmiralSpecialAbility.MissileSpecialist, AdmiralRacialAbility.ArtifactCoolingEngine, 100, 5, 5, new AdmiralSkills(), ArmadaClass.C),
                    6,
                    600
                    );
            attackingArmada.Add(fleetA1);
            attackingArmada.Add(fleetA2);
            attackingArmada.Add(fleetA3);

            fleetA1.Deploy(2500, 5000, 0, Command.Formation);
            fleetA2.Deploy(2500, 5010, 0, Command.Formation);
            fleetA3.Deploy(2500, 4990, 0, Command.Formation);

            Armada defendingArmada = new Armada(defender);

            Fleet fleetD1 = new Fleet(
                    4,
                    "Defender 1st Fleet",
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
                    new Admiral(4, "Defending Admiral", 1, AdmiralSpecialAbility.EnergySystemSpecialist, AdmiralRacialAbility.ArtifactCoolingEngine, 100, 5, 5, new AdmiralSkills(), ArmadaClass.C),
                    6,
                    600
                    );
            Fleet fleetD2 = new Fleet(
                    5,
                    "Defender 2nd Fleet",
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
                    new Admiral(5, "Defending Admiral", 1, AdmiralSpecialAbility.EnergySystemSpecialist, AdmiralRacialAbility.ArtifactCoolingEngine, 100, 5, 5, new AdmiralSkills(), ArmadaClass.C),
                    6,
                    600
                    );
            Fleet fleetD3 = new Fleet(
                    6,
                    "Defender 3rd Fleet",
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
                    new Admiral(6, "Defending Admiral", 1, AdmiralSpecialAbility.EnergySystemSpecialist, AdmiralRacialAbility.ArtifactCoolingEngine, 100, 5, 5, new AdmiralSkills(), ArmadaClass.C),
                    6,
                    600,
                    true
                    );

            defendingArmada.Add(fleetD1);
            defendingArmada.Add(fleetD2);
            defendingArmada.Add(fleetD3);

            fleetD1.Deploy(7500, 5000, 180, Command.Formation);
            fleetD2.Deploy(7500, 5010, 180, Command.Formation);
            fleetD3.Deploy(7500, 4990, 180, Command.Formation);

            Battle simulation = new Battle(BattleType.Siege, attacker, defender, battlefield, attackingArmada, defendingArmada);

            simulation.Run();

            Assert.IsTrue(simulation.IsComplete());
            Assert.IsTrue(simulation.Record.BattleOccurred);
            Assert.IsTrue(simulation.Record.Events.Any());
            Assert.IsTrue(simulation.Record.Events.Any(x => x.Type == RecordEventType.Movement), "No fleets moved.");
            Assert.IsTrue(simulation.Record.Events.Any(x => x.Type == RecordEventType.Fire), "No fleets fired despite battle conditions.");
            Assert.IsTrue(simulation.Record.Events.Any(x => x.Type == RecordEventType.Hit), "No fleets were hit despite battle conditions.");
            Assert.IsTrue(simulation.Record.Events.Any(x => x.Type == RecordEventType.FleetDisabled), "No fleets were disabled despite battle conditions.");
        }
    }
}
