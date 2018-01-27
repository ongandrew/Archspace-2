using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
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
            Player attacker = new Player()
            {
                Id = 0,
                Name = "Attacking Player",
                Race = Game.Configuration.Races.Single(x => x.Id == (int)RaceType.Human),
                Traits = new List<RacialTrait>(Game.Configuration.Races.Single(x => x.Id == (int)RaceType.Human).BaseTraits)
            };

            Player defender = new Player()
            {
                Id = 1,
                Name = "Defending Player",
                Race = Game.Configuration.Races.Single(x => x.Id == (int)RaceType.Targoid),
                Traits = new List<RacialTrait>(Game.Configuration.Races.Single(x => x.Id == (int)RaceType.Targoid).BaseTraits)
            };

            Battlefield battlefield = new Battlefield()
            {
                Id = 0,
                Name = "Test Battlefield"
            };

            Armada attackingArmada = new Armada(attacker);

            attackingArmada.Add(
                new Fleet()
                {
                    Id = 0,
                    Name = "0th Fleet",
                    Admiral = new Admiral()
                    {
                        Id = 0,
                        Name = "Attacking Admiral",
                        Skills = new AdmiralSkills()
                    },
                    Armor = Game.Configuration.Armors.Single(x => x.Name == "Titanium"),
                    Computer = Game.Configuration.Computers.Single(x => x.Name == "Electronic Computer"),
                    Engine = Game.Configuration.Engines.Single(x => x.Name == "Retro"),
                    IsCapital = true,
                    MaxShipCount = 6,
                    Owner = attacker,
                    Power = 100,
                    Shield = Game.Configuration.Shields.Single(x => x.Name == "Electromagnetic Shield"),
                    ShipClass = Game.Configuration.ShipClasses.Single(x => x.Name == "Gunboat"),
                    Turrets = new List<Turret>()
                    {
                        new Turret(Game.Configuration.Weapons.Single(x => x.Name == "Laser"), 1)
                    }
                }
                );

            for (int i = 0; i < 6; i++)
            {
                attackingArmada.First().Ships.Add(
                    new Ship()
                    {
                        HP = 100,
                        ShieldStrength = 100
                    });
            }

            Armada defendingArmada = new Armada(defender);

            defendingArmada.Add(
                new Fleet()
                {
                    Id = 1,
                    Name = "1st Fleet",
                    Admiral = new Admiral()
                    {
                        Id = 1,
                        Name = "Defending Admiral",
                        Skills = new AdmiralSkills()
                    },
                    Armor = Game.Configuration.Armors.Single(x => x.Name == "Titanium"),
                    Computer = Game.Configuration.Computers.Single(x => x.Name == "Electronic Computer"),
                    Engine = Game.Configuration.Engines.Single(x => x.Name == "Retro"),
                    IsCapital = true,
                    MaxShipCount = 6,
                    Owner = attacker,
                    Power = 100,
                    Shield = Game.Configuration.Shields.Single(x => x.Name == "Electromagnetic Shield"),
                    ShipClass = Game.Configuration.ShipClasses.Single(x => x.Name == "Gunboat"),
                    Turrets = new List<Turret>()
                    {
                                    new Turret(Game.Configuration.Weapons.Single(x => x.Name == "Laser"), 1)
                    }
                }
            );

            for (int i = 0; i < 6; i++)
            {
                defendingArmada.First().Ships.Add(
                    new Ship()
                    {
                        HP = 100,
                        ShieldStrength = 100
                    });
            }


            Simulation simulation = new Simulation(BattleType.Siege, attacker, defender, battlefield, attackingArmada, defendingArmada);
        }
    }
}
