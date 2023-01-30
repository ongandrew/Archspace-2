using Archspace2.Battle.Simulator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Archspace2
{
    [TestClass]
    [TestCategory("Simulator")]
    public class SimulatorTests
    {
        [TestMethod]
        public void SetupCreatesCorrectInstruments()
        {
            Simulator simulator = new Simulator(options =>
            {
                options.UseDefaults();
            });

            Simulation simulation = simulator.CreateSimulation()
                .SetBattlefield(new Battlefield() { Name = "Battlefield" })
                .SetAttacker(
                    new Battle.Simulator.Player()
                    {
                        Id = 1,
                        Name = "Attacker",
                        Race = Races.Human
                    },
                    new Armada()
                    {
                        Deployments = new List<Deployment>()
                        {
                            new Deployment()
                            {
                                IsCapital = true,
                                Direction = 0,
                                X = 2000,
                                Y = 5000,
                                Fleet = new Battle.Simulator.Fleet()
                                {
                                    Id = 1,
                                    Name = "1st Attacker Fleet",
                                    ShipCount = 6,
                                    Admiral = new Battle.Simulator.Admiral()
                                    {
                                        Level = 20,
                                        SpecialAbility = (int)AdmiralSpecialAbility.MissileSpecialist,
                                        RacialAbility = (int)AdmiralRacialAbility.ArtifactCrystal
                                    },
                                    Design = new Design()
                                    {
                                        ShipClass = ShipClasses.Gunboat,
                                        Armor = Armors.Titanium,
                                        Computer = Computers.ElectronicComputer,
                                        Engine = Engines.Retro,
                                        Shield = Shields.ElectromagneticShield,
                                        Weapons = new List<int>()
                                        {
                                            Weapons.Laser
                                        }
                                    }
                                },
                                Command = (int)Command.Formation
                            }
                        }
                    }
                )
                .SetDefender(
                    new Battle.Simulator.Player()
                    {
                        Id = 2,
                        Name = "Defender",
                        Race = Races.Human
                    },
                    new Armada()
                    {
                        Deployments = new List<Deployment>()
                        {
                            new Deployment()
                            {
                                IsCapital = true,
                                Direction = 180,
                                X = 8000,
                                Y = 5000,
                                Fleet = new Battle.Simulator.Fleet()
                                {
                                    Id = 2,
                                    Name = "1st Defender Fleet",
                                    ShipCount = 6,
                                    Admiral = new Battle.Simulator.Admiral()
                                    {
                                        Level = 20,
                                        SpecialAbility = (int)AdmiralSpecialAbility.MissileSpecialist,
                                        RacialAbility = (int)AdmiralRacialAbility.ArtifactCrystal
                                    },
                                    Design = new Design()
                                    {
                                        ShipClass = ShipClasses.Gunboat,
                                        Armor = Armors.Titanium,
                                        Computer = Computers.ElectronicComputer,
                                        Engine = Engines.Retro,
                                        Shield = Shields.ElectromagneticShield,
                                        Weapons = new List<int>()
                                        {
                                            Weapons.Laser
                                        }
                                    }
                                },
                                Command = (int)Command.Formation
                            }
                        }
                    }
                );

            Battle.Battle battle = simulation.Build();
            Assert.AreEqual(1, battle.Attacker.Id, "Attacker ID wrong.");
            Assert.AreEqual("Attacker", battle.Attacker.Name, "Attacker name wrong.");
            Assert.AreEqual(2, battle.Defender.Id, "Defender ID wrong.");
            Assert.AreEqual("Defender", battle.Defender.Name, "Defender name wrong.");
            Assert.AreEqual(Races.Human, battle.Attacker.Race.Id, "Wrong attacker race.");
            Assert.AreEqual(Races.Human, battle.Defender.Race.Id, "Wrong defender race.");

            Assert.AreEqual(1, battle.AttackingFleets.Count, "Wrong number of attacking fleets.");
            Assert.AreEqual(1, battle.DefendingFleets.Count, "Wrong number of defending fleets.");

            Battle.Fleet fleet = battle.AttackingFleets.First();
            Assert.AreEqual(2000, fleet.X, "Fleet position X wrong.");
            Assert.AreEqual(5000, fleet.Y, "Fleet position Y wrong.");
            Assert.AreEqual(0, fleet.Direction, "Fleet direction wrong.");
            Assert.AreEqual(Command.Formation, fleet.Command, "Fleet command wrong.");
            Assert.AreEqual(Armors.Titanium, fleet.Armor.Id, "Fleet armor wrong.");
            Assert.AreEqual(Computers.ElectronicComputer, fleet.Computer.Id, "Fleet computer wrong.");
            Assert.AreEqual(AdmiralSpecialAbility.MissileSpecialist, fleet.Admiral.SpecialAbility, "Fleet admiral special skill wrong wrong.");
            Assert.AreEqual(AdmiralRacialAbility.ArtifactCrystal, fleet.Admiral.RacialAbility, "Fleet admiral racial skill wrong wrong.");
            Assert.AreEqual(Weapons.Laser, fleet.Turrets.First().Id, "Fleet weapon wrong.");
            Assert.AreEqual(1, fleet.Turrets.Count, "Fleet weapon count wrong.");
            Assert.AreEqual(0, fleet.Devices.Count, "Fleet devices count wrong.");
        }

        [TestMethod]
        public void CanCreateAndRunBasicSimulation()
        {
            Simulator simulator = new Simulator(options =>
            {
                options.UseDefaults();
            });

            Simulation simulation = simulator.CreateSimulation()
                .SetBattlefield(new Battlefield() { Name = "Battlefield" })
                .SetAttacker(
                    new Battle.Simulator.Player()
                    {
                        Id = 1,
                        Name = "Attacker",
                        Race = Races.Human
                    },
                    new Armada()
                    {
                        Deployments = new List<Deployment>()
                        {
                            new Deployment()
                            {
                                IsCapital = true,
                                Direction = 0,
                                X = 2000,
                                Y = 5000,
                                Fleet = new Battle.Simulator.Fleet()
                                {
                                    Id = 1,
                                    Name = "1st Attacker Fleet",
                                    ShipCount = 6,
                                    Admiral = new Battle.Simulator.Admiral()
                                    {
                                        Level = 20,
                                        SpecialAbility = (int)AdmiralSpecialAbility.MissileSpecialist,
                                        RacialAbility = (int)AdmiralRacialAbility.ArtifactCrystal
                                    },
                                    Design = new Design()
                                    {
                                        ShipClass = ShipClasses.Gunboat,
                                        Armor = Armors.Titanium,
                                        Computer = Computers.ElectronicComputer,
                                        Engine = Engines.Retro,
                                        Shield = Shields.ElectromagneticShield,
                                        Weapons = new List<int>()
                                        {
                                            Weapons.Laser
                                        }
                                    }
                                },
                                Command = (int)Command.Formation
                            }
                        }
                    }
                )
                .SetDefender(
                    new Battle.Simulator.Player()
                    {
                        Id = 2,
                        Name = "Defender",
                        Race = Races.Xeloss
                    },
                    new Armada()
                    {
                        Deployments = new List<Deployment>()
                        {
                            new Deployment()
                            {
                                IsCapital = true,
                                Direction = 180,
                                X = 8000,
                                Y = 5000,
                                Fleet = new Battle.Simulator.Fleet()
                                {
                                    Id = 2,
                                    Name = "1st Defender Fleet",
                                    ShipCount = 6,
                                    Admiral = new Battle.Simulator.Admiral()
                                    {
                                        Level = 20,
                                        SpecialAbility = (int)AdmiralSpecialAbility.MissileSpecialist,
                                        RacialAbility = (int)AdmiralRacialAbility.ArtifactCrystal
                                    },
                                    Design = new Design()
                                    {
                                        ShipClass = ShipClasses.Gunboat,
                                        Armor = Armors.Titanium,
                                        Computer = Computers.ElectronicComputer,
                                        Engine = Engines.Retro,
                                        Shield = Shields.ElectromagneticShield,
                                        Weapons = new List<int>()
                                        {
                                            Weapons.Laser
                                        }
                                    }
                                },
                                Command = (int)Command.Formation
                            }
                        }
                    }
                );

            Battle.Battle battle = simulation.Build();
            battle.Run();

            Assert.IsTrue(battle.IsComplete);
        }

        [TestMethod]
        public void CanCreateAndRunFullFledgedSimulation()
        {
            Simulator simulator = new Simulator(options =>
            {
                options.UseDefaults();
            });

            Simulation simulation = simulator.CreateSimulation()
                .SetBattlefield(new Battlefield() { Name = "Battlefield" });

            Armada attackingArmada = new Armada();

            int i = 0;
            for (int x = 2000; x <= 2600; x += 200)
            {
                for (int y = 4600; y <= 5400; y += 200)
                {
                    attackingArmada.Add(new Deployment()
                    {
                        IsCapital = x == 2000 && y == 5000,
                        Direction = 0,
                        X = x,
                        Y = y,
                        Fleet = new Battle.Simulator.Fleet()
                        {
                            Id = i,
                            Name = "Attacking Fleet",
                            ShipCount = 40,
                            Admiral = new Battle.Simulator.Admiral()
                            {
                                Level = 20,
                                SpecialAbility = (int)AdmiralSpecialAbility.MissileSpecialist,
                                RacialAbility = (int)AdmiralRacialAbility.Intuition
                            },
                            Design = new Design()
                            {
                                ShipClass = ShipClasses.Doomstar,
                                Armor = Armors.WallofKlein,
                                Computer = Computers.NeuroComputer,
                                Engine = Engines.Phase,
                                Shield = Shields.MultiphaseShield,
                                Weapons = new List<int>()
                                {
                                    Weapons.HomingBlackHole,
                                    Weapons.HomingBlackHole,
                                    Weapons.HomingBlackHole,
                                    Weapons.HomingBlackHole,
                                    Weapons.HomingBlackHole,
                                    Weapons.HomingBlackHole,
                                    Weapons.HomingBlackHole
                                },
                                Devices = new List<int>()
                                {
                                    Devices.Coprocessor,
                                    Devices.ReinforcedHull,
                                    Devices.ECMJammer,
                                    Devices.AmplifyingChip,
                                    Devices.AblativeCoating
                                }
                            }
                        },
                        Command = (int)Command.Formation
                    });

                    i++;
                }
            }

            Armada defendingArmada = new Armada();
            
            for (int x = 7400; x <= 8000; x += 200)
            {
                for (int y = 4600; y <= 5400; y += 200)
                {
                    defendingArmada.Add(new Deployment()
                    {
                        IsCapital = x == 8000 && y == 5000,
                        Direction = 180,
                        X = x,
                        Y = y,
                        Fleet = new Battle.Simulator.Fleet()
                        {
                            Id = i,
                            Name = "Defending Fleet",
                            ShipCount = 40,
                            Admiral = new Battle.Simulator.Admiral()
                            {
                                Level = 20,
                                SpecialAbility = (int)AdmiralSpecialAbility.MissileSpecialist,
                                RacialAbility = (int)AdmiralRacialAbility.MentalGiant
                            },
                            Design = new Design()
                            {
                                ShipClass = ShipClasses.Doomstar,
                                Armor = Armors.WallofKlein,
                                Computer = Computers.NeuroComputer,
                                Engine = Engines.Phase,
                                Shield = Shields.MultiphaseShield,
                                Weapons = new List<int>()
                                {
                                    Weapons.HomingBlackHole,
                                    Weapons.HomingBlackHole,
                                    Weapons.HomingBlackHole,
                                    Weapons.HomingBlackHole,
                                    Weapons.HomingBlackHole,
                                    Weapons.HomingBlackHole,
                                    Weapons.HomingBlackHole
                                },
                                Devices = new List<int>()
                                {
                                    Devices.Coprocessor,
                                    Devices.ReinforcedHull,
                                    Devices.ECMJammer,
                                    Devices.AmplifyingChip,
                                    Devices.AblativeCoating
                                }
                            }
                        },
                        Command = (int)Command.Formation
                    });

                    i++;
                }
            }

            simulation
                .SetAttacker(
                    new Battle.Simulator.Player()
                    {
                        Id = 1,
                        Name = "Attacker",
                        Race = Races.Human
                    },
                    attackingArmada)
                .SetDefender(new Battle.Simulator.Player()
                    {
                        Id = 2,
                        Name = "Defender",
                        Race = Races.Xeloss
                    },
                    defendingArmada);

            Battle.Battle battle = simulation.Build();

            battle.Run();
            File.WriteAllText("simulation.json", simulation.ToString());
            File.WriteAllText("battle.json", battle.Record.ToString());
        }
    }
}
