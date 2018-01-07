using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class GameConfiguration
    {
        public GameConfiguration()
        {
            Armors = new List<Armor>();
            Computers = new List<Computer>();
            Engines = new List<Engine>();
            Races = new List<Race>();
            Shields = new List<Shield>();
            ShipClasses = new List<ShipClass>();
            Techs = new List<Tech>();
            Weapons = new List<Weapon>();
        }

        [JsonProperty("armors")]
        public List<Armor> Armors { get; set; }
        [JsonProperty("computers")]
        public List<Computer> Computers { get; set; }
        [JsonProperty("devices")]
        public List<Device> Devices { get; set; }
        [JsonProperty("engines")]
        public List<Engine> Engines { get; set; }
        [JsonProperty("races")]
        public List<Race> Races { get; set; }
        [JsonProperty("shields")]
        public List<Shield> Shields { get; set; }
        [JsonProperty("shipClasses")]
        public List<ShipClass> ShipClasses { get; set; }
        [JsonProperty("techs")]
        public List<Tech> Techs { get; set; }
        [JsonProperty("weapons")]
        public List<Weapon> Weapons { get; set; }

        public void UseDefaults()
        {
            UseDefaultArmors();
            UseDefaultComputers();
            UseDefaultDevices();
            UseDefaultEngines();
            UseDefaultRaces();
            UseDefaultShields();
            UseDefaultShipClasses();
            UseDefaultTechs();
        }

        private void UseDefaultArmors()
        {
            Armors = new List<Armor>()
            {
                new Armor()
                {
                    Id = 5101,
                    Name = "Titanium",
                    Description = "This is the basic hull that all ships are built with. It has some benefit for defense, but its main purpose is to keep outside things outside, and inside things in. This is a reasonable goal for any beginning fleet, however, if one truly wants to be powerful, you must have better armor than this.",
                    Type = ArmorType.Normal,
                    TechLevel = 1,
                    DefenseRating = 30,
                    HpMultiplier = 1.0
                },
                new Armor()
                {
                    Id = 5102,
                    Name = "Mithril",
                    Description = "This armor is not made of actual mithril, but it has been named so because of its resemblance to the mythical magical metal. It also appears to have some added benefit in that it appears to be quite effective against psychic attacks. This may also be another reason that the human name has been taken up throughout the Empire.",
                    Type = ArmorType.Normal,
                    TechLevel = 2,
                    DefenseRating = 34,
                    HpMultiplier = 1.35,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1311
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.PsiDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = 15
                        }
                    }
                },
                new Armor()
                {
                    Id = 5103,
                    Name = "Adamantium",
                    Description = "This armor is the next upgrade to the standard armor types. It has no self repair ability, but it does have some other benefits. It is the best armor up to this level for preventing psychic damages. It also a relatively strong armor and can take a good amount of damage before it gives out.",
                    Type = ArmorType.Normal,
                    TechLevel = 3,
                    DefenseRating = 44,
                    HpMultiplier = 1.95,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1325
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.PsiDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = 30
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ProjectileDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = 20
                        }
                    }
                },
                new Armor()
                {
                    Id = 5104,
                    Name = "Neutronium",
                    Description = "One thing can be said of Neutronium. It is tough. This is one of the most durable of armors that is available. Its metallic structure is also quite effective at deflecting beam weaponry. The major drawback to this armor is that it is not easy to work with. This means any repairs made to this armor require almost twice as long to make.",
                    Type = ArmorType.Normal,
                    TechLevel = 4,
                    DefenseRating = 53,
                    HpMultiplier = 3.0,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1329
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.BeamDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = 20
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.RepairSpeed,
                            ModifierType = ModifierType.Proportional,
                            Amount = -50
                        }
                    }
                },
                new Armor()
                {
                    Id = 5105,
                    Name = "Eternium",
                    Description = "The name says it all. This is the strongest and most durable armor available. The main drawback is that it is impossible to repair during combat. However, this armor can take so much abuse, that it is rarely necessary to repair it.",
                    Type = ArmorType.Normal,
                    TechLevel = 5,
                    DefenseRating = 83,
                    HpMultiplier = 3.45,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1333
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ProjectileDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = 20
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.BeamDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = 30
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.PsiDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = 30
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.NonRepairable
                        }
                    }
                },
                new Armor()
                {
                    Id = 5106,
                    Name = "Self-Recovering Crystal",
                    Description = "This is one of the earliest armors to have a self repair faculty. While this can be quite useful, the armor is somewhat weak to missiles. A captain with a ship with SRC must surely pray at night that the repairs can keep up with the missile damage, for if they do, this fine armor can stop almost any other low level weapon.",
                    Type = ArmorType.Normal,
                    TechLevel = 2,
                    DefenseRating = 36,
                    HpMultiplier = 1.2,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1318
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.BeamDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = 10
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ProjectileDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = 20
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.MissileDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = -5
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Repair,
                            ModifierType = ModifierType.Proportional,
                            Amount = 3,
                            Period = 10
                        }
                    }
                },
                new Armor()
                {
                    Id = 5107,
                    Name = "Anti-Matter Crystal",
                    Description = "By combining the ideas of Crystal armor and Anti-matter reactive armor this armor draws some of the benefits from each. Like SRC this armor will repair itself, although not as quickly. As it also uses the reactive techniques of Anti-matter armor, it has overcome all the weaknesses that SRC has.",
                    Type = ArmorType.Bio,
                    TechLevel = 4,
                    DefenseRating = 40,
                    HpMultiplier = 2.4,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1328
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.BeamDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = 10
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.MissileDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = 20
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ProjectileDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = 20
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Repair,
                            ModifierType = ModifierType.Proportional,
                            Amount = 1,
                            Period = 10
                        }
                    }
                },
                new Armor()
                {
                    Id = 5108,
                    Name = "Biocentric",
                    Description = "This is the other early armor which also has the ability to repair itself. This armor is weak to all weapon types except for beams, however, it is over all a stronger armor and can take almost twice as much abuse before giving out. This added ability to take damage, coupled with the repair action make this armor a common choice among many commanders.",
                    Type = ArmorType.Bio,
                    TechLevel = 2,
                    DefenseRating = 30,
                    HpMultiplier = 1.8,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1413
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.PsiDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = -20
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.MissileDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = -10
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ProjectileDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = -10
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Repair,
                            ModifierType = ModifierType.Proportional,
                            Amount = 3,
                            Period = 10
                        }
                    }
                },
                new Armor()
                {
                    Id = 5109,
                    Name = "Organic",
                    Description = "This is the first truly living armor. It even has a limited primitive awareness. The fact that it is alive allows it to heal itself during battles. The drawback is that its awareness makes it slightly more susceptible to psychic attacks.",
                    Type = ArmorType.Bio,
                    TechLevel = 3,
                    DefenseRating = 36,
                    HpMultiplier = 2.4,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1418
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.PsiDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = -20
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ProjectileDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = -20
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Repair,
                            ModifierType = ModifierType.Proportional,
                            Amount = 3,
                            Period = 5
                        }
                    }
                },
                new Armor()
                {
                    Id = 5110,
                    Name = "Energy-Organic Crystal",
                    Description = "This Crystal armor heals faster than other crystal types. It is also the most durable of all the crystal armors. The one drawback that it has is a weakness to projectiles. This may not matter overly much though, as this is the second strongest armor available.",
                    Type = ArmorType.Bio,
                    TechLevel = 5,
                    DefenseRating = 50,
                    HpMultiplier = 4.55,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1418
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ProjectileDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = 40
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Repair,
                            ModifierType = ModifierType.Proportional,
                            Amount = 1,
                            Period = 1
                        }
                    }
                },
                new Armor()
                {
                    Id = 5111,
                    Name = "Anti-Matter Reactive Armor",
                    Description = "This armor uses a tailored form of anti matter to deflect certain types of weaponry. It is particularly effective against missiles. The one drawback to this armor is the fact that it is not as resilient as other armors of the same class.",
                    Type = ArmorType.Reactive,
                    TechLevel = 3,
                    DefenseRating = 30,
                    HpMultiplier = 1.55,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1323
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.MissileDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = 35
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.BeamDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = 10,
                        }
                    }
                },
                new Armor()
                {
                    Id = 5112,
                    Name = "Reflexium",
                    Description = "This armor is not as durable as other armors of its class, however, it is one of the best all around armors for deflecting incoming attacks. Its flexibility and high reflective ness mean that it is particularly good at deflecting any missile or beam type weapons.",
                    Type = ArmorType.Reactive,
                    TechLevel = 4,
                    DefenseRating = 40,
                    HpMultiplier = 2.1,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1323
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.MissileDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = 55
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ProjectileDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = 5,
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.BeamDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = 5,
                        }
                    }
                },
                new Armor()
                {
                    Id = 5113,
                    Name = "Wall of Klein",
                    Description = "This armor is actually designed so that it folds space in odd ways. This allows it to avoid almost all forms of attack. It is most susceptible to projectile weapons, and is almost immune to beam and missile weapons. Its one drawback is that it is not very durable for its class.",
                    Type = ArmorType.Reactive,
                    TechLevel = 5,
                    DefenseRating = 30,
                    HpMultiplier = 2.35,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1323
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.MissileDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = 70
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ProjectileDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = 10,
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.BeamDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = 55,
                        }
                    }
                }
            };
        }
        private void UseDefaultComputers()
        {
            Computers = new List<Computer>()
            {
                new Computer()
                {
                    Id = 5201,
                    Name = "Electronic Computer",
                    Description = "This is the most basic of computers. It is used in the most basic of manufacturing and processing. By using top speed computers of this type it is possible to safely pilot a ship throughout the empire.",
                    TechLevel = 1,
                    AttackRating = 100,
                    DefenseRating = 233,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1201
                        }
                    }
                },
                new Computer()
                {
                    Id = 5202,
                    Name = "Optical Computer",
                    Description = "This is the second level of computers used for space travel. The main innovation of this computer is the use of light particle/waves to enhance processing speed and capability.",
                    TechLevel = 2,
                    AttackRating = 133,
                    DefenseRating = 289,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1208
                        }
                    }
                },
                new Computer()
                {
                    Id = 5203,
                    Name = "Genetic Computer",
                    Description = "This is the third and most common type of ship computer in use in the Empire. Utilizing artificial consciousness, this computer is able to process things as fast as its predecessors, however with its own consciousness, it can occasionally make intuitive judgments, which save time.",
                    TechLevel = 3,
                    AttackRating = 172,
                    DefenseRating = 354,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1213
                        }
                    }
                },
                new Computer()
                {
                    Id = 5204,
                    Name = "Quantum Computer",
                    Description = "This is the fourth level of computer which many races never achieve. By using quantum processors this computer operates much faster than any computer before.",
                    TechLevel = 4,
                    AttackRating = 218,
                    DefenseRating = 429,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1221
                        }
                    }
                },
                new Computer()
                {
                    Id = 5205,
                    Name = "Neuro Computer",
                    Description = "This is the fifth level of computer which is ever used in ships. Using the most self advancing algorithms, this computer is able to evolve itself to fit certain situations. This makes it much more powerful and versatile than any other computers which are small enough to be housed on a ship of any size.",
                    TechLevel = 5,
                    AttackRating = 271,
                    DefenseRating = 518,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1223
                        }
                    }
                }
            };
        }
        private void UseDefaultDevices()
        {
            Devices = new List<Device>()
            {
                new Device()
                {
                    Id = 5501,
                    Name = "Inertia Nullifier",
                    Description = "The Inertial Nullifier is one of the most basic of the devices available. It is however still quite useful. By allowing you to increase the speed with which you decelerate, this device allows your to be much more maneuverable. This aids in movement through the battlefield, as well as allowing you to be more effective at evasive maneuvers.",
                    TechLevel = 1,
                    MinimumClass = 1,
                    MaximumClass = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1312
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.DefenseRatingAgainstProjectile,
                            ModifierType = ModifierType.Proportional,
                            Amount = 5
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.DefenseRatingAgainstBeam,
                            ModifierType = ModifierType.Proportional,
                            Amount = 25
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Mobility,
                            ModifierType = ModifierType.Proportional,
                            Amount = 30
                        }
                    }
                },
                new Device()
                {
                    Id = 5502,
                    Name = "Amplifying Chip",
                    Description = "This chip is not truly a chip at all. It is actually a carefully created crystalline structure. This \"chip\" can then be used to help redirect heat produced during ship operations. This is most noticed in that is aids in cooling weapons down, and also in removing extra energy trapped by shields.",
                    TechLevel = 2,
                    MinimumClass = 1,
                    MaximumClass = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1318
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ShieldSolidity,
                            ModifierType = ModifierType.Absolute,
                            Amount = 2
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ShieldStrength,
                            ModifierType = ModifierType.Proportional,
                            Amount = 30
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.CoolingTime,
                            ModifierType = ModifierType.Proportional,
                            Amount = -5
                        }
                    }
                },
                new Device()
                {
                    Id = 5503,
                    Name = "Psi Drive",
                    Description = "This device is only available to races with Psi ability. It allows the pilot to use its energy to focus and speed the movements of the ship. Such enhancement will aid any fleet, if the pilot is able to use it.",
                    TechLevel = 5,
                    MinimumClass = 1,
                    MaximumClass = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1419
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.RacialTrait,
                            Value = RacialTrait.Psi
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Mobility,
                            ModifierType = ModifierType.Proportional,
                            Amount = 45
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Speed,
                            ModifierType = ModifierType.Proportional,
                            Amount = 45
                        }
                    }
                },
                new Device()
                {
                    Id = 5504,
                    Name = "Force Field Generator",
                    Description = "While the name may be confusing, this device is not in and of itself a shield. It is merely a device that enhances the effectiveness of any shielding you may already possess. This is done by using knowledge of the unifying force behind all energy to strengthen whatever shields you have on your ships.",
                    TechLevel = 1,
                    MinimumClass = 1,
                    MaximumClass = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1314
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ShieldSolidity,
                            ModifierType = ModifierType.Absolute,
                            Amount = 3
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ShieldStrength,
                            ModifierType = ModifierType.Proportional,
                            Amount = 40
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ImpenetrableShield,
                            ModifierType = ModifierType.Absolute,
                            Amount = 15
                        }
                    }
                },
                new Device()
                {
                    Id = 5505,
                    Name = "High Energy Focus",
                    Description = "This device can be fitted to all of your beam weapons. It refines the focus of any beam that uses it allowing greater energy to be focused on your foes. This results in additional damage, if you are able to hit them.",
                    TechLevel = 5,
                    MinimumClass = 5,
                    MaximumClass = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1332
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.BeamDamage,
                            ModifierType = ModifierType.Proportional,
                            Amount = 20
                        }
                    }
                },
                new Device()
                {
                    Id = 5506,
                    Name = "Psi-Control System",
                    Description = "This device is only available to races with Psi ability. It is a special interface for the commander of each ship. This interface allows them additional control of all aspects of their ship. The main benefits however can be seen in the offensive and defensive abilities of the ship's psychic abilities.",
                    TechLevel = 5,
                    MinimumClass = 4,
                    MaximumClass = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1421
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.RacialTrait,
                            Value = RacialTrait.Psi
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.PsiAttack,
                            ModifierType = ModifierType.Proportional,
                            Amount = 60
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.PsiDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = 100
                        }
                    }
                },
                new Device()
                {
                    Id = 5507,
                    Name = "Insanity Field Generator",
                    Description = "This device is only usable by those races with Psi ability. By projecting their hatred into the device, they are able to fashion a field around their fleet, which will inflict a psychic attack to any fleets that enter. This attack will destroy your enemies confidence as it also destroys their ships.",
                    TechLevel = 4,
                    MinimumClass = 8,
                    MaximumClass = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1124
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.RacialTrait,
                            Value = RacialTrait.Psi
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.PsiDamage,
                            ModifierType = ModifierType.Absolute,
                            Amount = 400,
                            Range = 875,
                            TargetType = FleetEffectTargetType.AreaEffectTargetEnemy
                        }
                    }
                },
                new Device()
                {
                    Id = 5508,
                    Name = "Mind Protector",
                    Description = "This device is placed in the center of your ship. It radiates a psychic sense of well-being and calmness. This improves your fleet's ability to resist psychic damages, and also prevents the panic or other states that some crews are occasionally forced into by some of the more horrible psychic attacks.",
                    TechLevel = 4,
                    MinimumClass = 1,
                    MaximumClass = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1420
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.PsiDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = 50
                        }
                    }
                },
                new Device()
                {
                    Id = 5509,
                    Name = "Booster Device",
                    Description = "This component may be added to any type of ship engine. It basically \"boosts\" the output and efficiency of your engines. This allows faster movement and better control. For some ships, this is almost a required addition.",
                    TechLevel = 2,
                    MinimumClass = 1,
                    MaximumClass = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1316
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Mobility,
                            ModifierType = ModifierType.Proportional,
                            Amount = 40
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Speed,
                            ModifierType = ModifierType.Proportional,
                            Amount = 40
                        }
                    }
                },
                new Device()
                {
                    Id = 5510,
                    Name = "Shield Capacitor",
                    Description = "This device is just what its name suggests. It is a capacitor which greatly increases the stability of your shields. It also allows them to rebound faster from any damage that it takes.",
                    TechLevel = 2,
                    MinimumClass = 1,
                    MaximumClass = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1214
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ShieldRechargeRate,
                            ModifierType = ModifierType.Proportional,
                            Amount = 250
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ShieldSolidity,
                            ModifierType = ModifierType.Absolute,
                            Amount = 2
                        }
                    }
                },
                new Device()
                {
                    Id = 5511,
                    Name = "ECM Jammer",
                    Description = "This device is a counter, counter measure. It works by canceling the signals that missiles use to track and move after your ships. It will occasionally allow your ships to escape unscathed from an explosion that could remove them from existence in seconds.",
                    TechLevel = 2,
                    MinimumClass = 1,
                    MaximumClass = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1215
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.DefenseRatingAgainstMissile,
                            ModifierType = ModifierType.Proportional,
                            Amount = 35
                        }
                    }
                },
                new Device()
                {
                    Id = 5512,
                    Name = "Cloaking Field Generator",
                    Description = "This device is prized both by great tacticians and pirates alike. While nowhere near perfect, it will often allow a fleet to go undetected. This device draws on power in a geometric amount. This means that after a ship is a certain size, it is impossible to power the cloaking field.",
                    TechLevel = 4,
                    MinimumClass = 1,
                    MaximumClass = 5,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1217
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Stealth,
                            Amount = 25
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.CompleteCloaking
                        }
                    }
                },
                new Device()
                {
                    Id = 5514,
                    Name = "Auto Repairing Device",
                    Description = "This \"device\" is actually a gigantic herd of miniscule robots. They swarm about your ship making repairs as needed. The only limitation is that they often need more materials than are available on smaller ships. This means that it is only truly useful on ships of a certain size.",
                    TechLevel = 4,
                    MinimumClass = 3,
                    MaximumClass = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1328
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Repair,
                            ModifierType = ModifierType.Proportional,
                            Amount = 1
                        }
                    }
                },
                new Device()
                {
                    Id = 5515,
                    Name = "Train Injector",
                    Description = "This device is a mixture of nanomachines and drugs. It instills your crew with the abilities and heart of battle scarred veterans. This allows even the newest fleet to be more useful than your enemies could expect.",
                    TechLevel = 3,
                    MinimumClass = 1,
                    MaximumClass = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1417
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Trained
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.BerserkModifier,
                            ModifierType = ModifierType.Absolute,
                            Amount = -10
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.PanicModifier,
                            ModifierType = ModifierType.Absolute,
                            Amount = -15
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Efficiency,
                            ModifierType = ModifierType.Absolute,
                            Amount = 10
                        }
                    }
                },
                new Device()
                {
                    Id = 5516,
                    Name = "Reinforced Hull",
                    Description = "This component is actually placed throughout your entire ship. Using microscale robots, you enforce the entire structure of your ships. This increases the effectiveness of your armor. Such effectiveness is often prized during a battle when make the difference between severe damage and a full hull breach.",
                    TechLevel = 2,
                    MinimumClass = 1,
                    MaximumClass = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1317
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Hp,
                            ModifierType = ModifierType.Proportional,
                            Amount = 20
                        }
                    }
                },
                new Device()
                {
                    Id = 5517,
                    Name = "Gyro Deformator",
                    Description = "This device is often the bane of larger ships. By positioning it correctly within your ship, you can extend a field that will cause havoc to any enemy ships directional controls. This field is stable towards the center and will not effect your ship, but outside of that for a certain range, any ships will feel as though \"down\" is constantly changing. This device can only be put on larger ships, as smaller ships lack the mass to make this device effective.",
                    TechLevel = 2,
                    MinimumClass = 1,
                    MaximumClass = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1316
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Mobility,
                            ModifierType = ModifierType.Proportional,
                            Amount = -25,
                            TargetType = FleetEffectTargetType.AreaEffectTargetEnemy
                        }
                    }
                },
                new Device()
                {
                    Id = 5518,
                    Name = "Kwang-11",
                    Description = "This device is one of the more insidious ones available. It will link itself to nearby enemy fleets and bombard their computers with useless and delaying information. This can cripple some fleets computers, if their computer security measures are not advanced enough.",
                    TechLevel = 4,
                    MinimumClass = 1,
                    MaximumClass = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1220
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Computer,
                            ModifierType = ModifierType.Proportional,
                            Amount = -15,
                            Range = 900,
                            TargetType = FleetEffectTargetType.AreaEffectTargetEnemy
                        }
                    }
                },
                new Device()
                {
                    Id = 5519,
                    Name = "Mind Tracker",
                    Description = "This device is aptly named. The one thing it does is tracks minds. This, when paired with the targeting for your psi weapons allows much greater accuracy.",
                    TechLevel = 2,
                    MinimumClass = 1,
                    MaximumClass = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1117
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.PsiAttack,
                            ModifierType = ModifierType.Proportional,
                            Amount = 70
                        }
                    }
                },
                new Device()
                {
                    Id = 5520,
                    Name = "Psi Barrier",
                    Description = "This device is simple enough, once a race understands certain things about how the psyche works. It lessens the focus of any incoming psi attacks. This will increase your chance to avoid damage, but will not lessen the impact if you do succumb to the threat.",
                    TechLevel = 5,
                    MinimumClass = 1,
                    MaximumClass = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1422
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.PsiDefense,
                            ModifierType = ModifierType.Proportional,
                            Amount = 50
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.PsiNeutralizationField,
                            ModifierType = ModifierType.Absolute,
                            Amount = 25
                        }
                    }
                },
                new Device()
                {
                    Id = 5521,
                    Name = "Self-Conscious Fleet",
                    Description = "This is an interesting addition to any fleet. Where some races ships are actual beings, even for them their fleets are not entities unto themselves. The ships are still independent thinkers. This device links the minds controlling the ships into one large being that becomes the fleet. This can increase overall coordination of almost any fleet.",
                    TechLevel = 2,
                    MinimumClass = 1,
                    MaximumClass = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1213
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Efficiency,
                            ModifierType = ModifierType.Absolute,
                            Amount = 35
                        }
                    }
                },
                new Device()
                {
                    Id = 5522,
                    Name = "Coprocessor",
                    Description = "This is a relatively simple device. It is basically a processor, which runs in tandem with your computer. This processor greatly increases the accuracy of your main computer. This increase has many effects as it allows your computer to function faster while maintaining the same accuracy.",
                    TechLevel = 2,
                    MinimumClass = 1,
                    MaximumClass = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1212
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Computer,
                            ModifierType = ModifierType.Proportional,
                            Amount = 15
                        }
                    }
                },
                new Device()
                {
                    Id = 5523,
                    Name = "Escape Pod",
                    Description = "This device takes up quite a bit of space for something that does not help with combat.  Many commanders will refuse to command a fleet without one.  Many rulers actually have them built into their ships because any chance of recovering a trusted, experienced commander is something they like.",
                    TechLevel = 3,
                    MinimumClass = 1,
                    MaximumClass = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1212
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.CommanderSurvival,
                            ModifierType = ModifierType.Absolute,
                            Amount = 20
                        }
                    }
                },
                new Device()
                {
                    Id = 5524,
                    Name = "Homing Beacon",
                    Description = "This device is not large, but the amount of power that it uses is.  It is actually small enough to attach to a standard EVA suit.  For commanders without escape pods, having one of these attached to the suit can offer some chance of rescue.  If one is attached to an escape pod, the combination is even more effective.",
                    TechLevel = 4,
                    MinimumClass = 1,
                    MaximumClass = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1123
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.CommanderSurvival,
                            ModifierType = ModifierType.Absolute,
                            Amount = 20
                        }
                    }
                },
                new Device()
                {
                    Id = 5525,
                    Name = "Space Mining Module",
                    Description = "Asteroids from all around the galaxies contain some valuable trading ressources. The buckaneers have designed this device to collect  those ressources and trading them to make a living with their spaceships.",
                    TechLevel = 1,
                    MinimumClass = 4,
                    MaximumClass = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Race,
                            Value = 3
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.SpaceMining
                        }
                    }
                },
                new Device()
                {
                    Id = 5526,
                    Name = "Anti-PSI Device",
                    Description = "None.",
                    TechLevel = 1,
                    MinimumClass = 1,
                    MaximumClass = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Race,
                            Value = 9
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.PsiNeutralizationField,
                            ModifierType = ModifierType.Absolute,
                            Amount = 65
                        }
                    }
                },
                new Device()
                {
                    Id = 5527,
                    Name = "Ablative Coating",
                    Description = "Ablative Coating is an additional reactive plating that can be added to the hull of any ship. This coating provides a counterforce to incoming ballistic weapons fire by slowing down projectiles with its spongelike density. Under beam weapon fire, this coating will boil away, carrying away a portion of the beam's energy. Ablative Coating's light, airy structure is also a good cushion versus the force of incoming missile blasts.",
                    TechLevel = 1,
                    MinimumClass = 1,
                    MaximumClass = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1328
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ImpenetrableArmor,
                            ModifierType = ModifierType.Absolute,
                            Amount = 15
                        }
                    }
                }
            };
        }
        private void UseDefaultEngines()
        {
            Engines = new List<Engine>()
            {
                new Engine()
                {
                    Id = 5401,
                    Name = "Retro",
                    Description = "This is the basic engine used by all space faring races at one point. It is by no means efficient and is often seen as a sign that a race is primitive. It does accomplish its goal of moving things from one place to another.",
                    TechLevel = 1,
                    BattleSpeed = new Dictionary<int, int>()
                    {
                        [1] = 16,
                        [2] = 15,
                        [3] = 14,
                        [4] = 14,
                        [5] = 13,
                        [6] = 13,
                        [7] = 12,
                        [8] = 11,
                        [9] = 10,
                        [10] = 9
                    },
                    BattleMobility = new Dictionary<int, int>()
                    {
                        [1] = 45,
                        [2] = 41,
                        [3] = 37,
                        [4] = 33,
                        [5] = 30,
                        [6] = 27,
                        [7] = 24,
                        [8] = 21,
                        [9] = 18,
                        [10] = 15
                    }
                },
                new Engine()
                {
                    Id = 5402,
                    Name = "Fusion",
                    Description = "This is the second level of engine. It is considered by many races to be the first true type of engine used by intelligent species. It is a noted improvement on its predecessor, but it is still by no means the best.",
                    TechLevel = 2,
                    BattleSpeed = new Dictionary<int, int>()
                    {
                        [1] = 22,
                        [2] = 21,
                        [3] = 20,
                        [4] = 19,
                        [5] = 18,
                        [6] = 17,
                        [7] = 16,
                        [8] = 15,
                        [9] = 14,
                        [10] = 13
                    },
                    BattleMobility = new Dictionary<int, int>()
                    {
                        [1] = 54,
                        [2] = 49,
                        [3] = 44,
                        [4] = 40,
                        [5] = 36,
                        [6] = 32,
                        [7] = 28,
                        [8] = 24,
                        [9] = 21,
                        [10] = 18
                    },
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1310
                        }
                    }
                },
                new Engine()
                {
                    Id = 5403,
                    Name = "Anti-Matter",
                    Description = "This is the third level of engines. This is in fact the average engine in use throughout the empire. Using the improved power of antimatter reactions, this engine is faster and more useful for maneuvers than its predecessors.",
                    TechLevel = 3,
                    BattleSpeed = new Dictionary<int, int>()
                    {
                        [1] = 31,
                        [2] = 30,
                        [3] = 29,
                        [4] = 27,
                        [5] = 25,
                        [6] = 24,
                        [7] = 23,
                        [8] = 21,
                        [9] = 19,
                        [10] = 17
                    },
                    BattleMobility = new Dictionary<int, int>()
                    {
                        [1] = 65,
                        [2] = 59,
                        [3] = 53,
                        [4] = 48,
                        [5] = 43,
                        [6] = 38,
                        [7] = 34,
                        [8] = 30,
                        [9] = 29,
                        [10] = 13
                    },
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1322
                        }
                    }
                },
                new Engine()
                {
                    Id = 5404,
                    Name = "Impulse",
                    Description = "This is the fourth level of engines and is only utilized by the most advanced races. This Engine is much more efficient than those before as it uses an advanced knowledge of energy-matter science to recapture much of the energy normally misused by engines.",
                    TechLevel = 4,
                    BattleSpeed = new Dictionary<int, int>()
                    {
                        [1] = 42,
                        [2] = 40,
                        [3] = 38,
                        [4] = 36,
                        [5] = 34,
                        [6] = 32,
                        [7] = 30,
                        [8] = 28,
                        [9] = 26,
                        [10] = 24
                    },
                    BattleMobility = new Dictionary<int, int>()
                    {
                        [1] = 78,
                        [2] = 71,
                        [3] = 64,
                        [4] = 58,
                        [5] = 52,
                        [6] = 46,
                        [7] = 41,
                        [8] = 36,
                        [9] = 31,
                        [10] = 26
                    },
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1332
                        }
                    }
                },
                new Engine()
                {
                    Id = 5405,
                    Name = "Phase",
                    Description = "This is the most advanced form of engine in use in the Empire. It used an understanding of the nature of space to move. To observers, the ship appears to simply jump from one place to another. This is inaccurate however as the ship is actually bending portions of the space in front of it into other dimensions, thus reducing the actual distance that it travels.",
                    TechLevel = 1,
                    BattleSpeed = new Dictionary<int, int>()
                    {
                        [1] = 59,
                        [2] = 57,
                        [3] = 54,
                        [4] = 51,
                        [5] = 48,
                        [6] = 46,
                        [7] = 43,
                        [8] = 40,
                        [9] = 37,
                        [10] = 34
                    },
                    BattleMobility = new Dictionary<int, int>()
                    {
                        [1] = 93,
                        [2] = 85,
                        [3] = 77,
                        [4] = 69,
                        [5] = 62,
                        [6] = 55,
                        [7] = 49,
                        [8] = 43,
                        [9] = 37,
                        [10] = 31
                    },
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1334
                        }
                    }
                }
            };
        }
        private void UseDefaultRaces()
        {
            Races = new List<Race>()
            {
                new Race()
                {
                    Id = 1,
                    Name = "Human",
                    Description = "Though humans have a relatively short life span, the population increase rate of humans is nothing short of astonishing.  As a result, the other species have always been wary of the humans' expansion of power in the universe.  However, even the watchful eyes of the other species cannot easily spot the other strong points of the human species, which lie in the philosophical and social sciences, as well as other literary and cultural developments.  Their never-ending desire to seek the ideal has provided a catalyst not found in other species.",
                    SocietyType = SocietyType.Classism,
                    BaseTechs = new List<int>()
                    {
                        1104,
                        1105,
                        1106,
                        1107,
                        1112
                    },
                    BaseControlModel = new ControlModel()
                    {
                        Environment = -1,
                        Growth = 2,
                        Efficiency = -1,
                        Genius = 5,
                        Research = 3
                    },
                    BaseFleetEffects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.CommanderSurvival,
                            Amount = 5
                        }
                    }
                },
                new Race()
                {
                    Id = 2,
                    Name = "Targoid",
                    Description = "The Targoid race maintain a totalitarian society, with all of their species originating from one mother body and being controlled by that mother body.  Targoids efficiently produce every type of their race as needed from one mother body through the programming of the DNA of the unborn Targoid embryo.  This system makes it possible to produce any variety of the species, from mouse-sized micro workers to battle creatures over 2 kilometers in length.  Targoid workers are famous for their efficiency in gathering resources and constructing buildings.",
                    SocietyType = SocietyType.Totalism,
                    BaseTechs = new List<int>()
                    {
                        1404,
                        1405,
                        1406,
                        1408,
                        1413,
                        1603
                    },
                    BaseControlModel = new ControlModel()
                    {
                        Growth = 3,
                        Production = 2,
                        FacilityCost = 2,
                        Research = -3
                    },
                    BaseFleetEffects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.BerserkModifier,
                            Amount = 5
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.CommanderSurvival,
                            Amount = -10
                        }
                    },
                    BaseTraits = new List<RacialTrait>()
                    {
                        RacialTrait.GeneticEngineeringSpecialist,
                        RacialTrait.FragileMindStructure,
                        RacialTrait.GreatSpawningPool
                    }
                },
                new Race()
                {
                    Id = 3,
                    Name = "Buckaneer",
                    Description = "A Buckaneer's spaceship is his home and center of life.  They spend the majority of their life roaming space and finding fortune through trade (and sometimes piracy.)  Because they are accustomed to this type of gypsy roaming life, their fleets move swiftly and cannot be traced easily.  Buckaneer merchants possess information and contacts throughout the universe, which are indispensable aids to commerce, and thus hold the majority of trade and business in the universe.  It is common to see Buckaneer crafts that have duplicator systems for needed items built into the ship's interior design.",
                    SocietyType = SocietyType.Personalism,
                    BaseTechs = new List<int>()
                    {
                        1208,
                        1112,
                        1603
                    },
                    BaseControlModel = new ControlModel()
                    {
                        Growth = -3,
                        Military = -1,
                        Commerce = 4
                    },
                    BaseFleetEffects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Morale,
                            Amount = 5
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.CommanderSurvival,
                            Amount = 5
                        }
                    },
                    BaseTraits = new List<RacialTrait>()
                    {
                        RacialTrait.FastManeuver,
                        RacialTrait.Stealth
                    }
                },
                new Race()
                {
                    Id = 4,
                    Name = "Tecanoid",
                    Description = "The Tecanoids sought to find their key to evolution through attaching computers and bionic machinery to their bodies.  As a result of these experiments, the elite forces of their races have obtained extremely strong physical bodies and extraordinary intellect.  On the other hand, the lowest class of their society did not have an opportunity to receive these gifts, and thus became an unstable supporting pillar of the community.  The Tecanoid effort for evolution brought them optimally advanced data processing skills and electronic infiltration technologies, but the species has ultimately sacrificed their humanity for these machines.",
                    SocietyType = SocietyType.Classism,
                    BaseTechs = new List<int>()
                    {
                        1204,
                        1205,
                        1206
                    },
                    BaseControlModel = new ControlModel()
                    {
                        Environment = 2,
                        Spy = 4
                    },
                    BaseFleetEffects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Morale,
                            Amount = 5
                        }
                    },
                    BaseTraits = new List<RacialTrait>()
                    {
                        RacialTrait.InformationNetworkSpecialist,
                        RacialTrait.Scavenger
                    }
                },
                new Race()
                {
                    Id = 5,
                    Name = "Evintos",
                    Description = "Unlike most of the other races of the galaxy, the Evintos are a non-organic life force whose bodies are composed of silicon and gold.  Their nervous system and metabolisms are also radically different from other inhabitants of the universe.  It is believed that they originate from artificial intelligence creatures created in the far ancient ages of the universe, whose technologies remain primarily lost to the world.  Because of their unusual appearance and makeup, they are often rejected by other species.  They possess a rigid social structure, which makes it hard for technological or social innovations to be implemented.  This structure further adds to their drifting differences between other species.  But these weak points are compensated by their mechanically precise and accurate social structure, which makes for extremely high production and manufacturing within their society.",
                    SocietyType = SocietyType.Totalism,
                    BaseTechs = new List<int>()
                    {
                        1204,
                        1205,
                        1206,
                        1306,
                        1307,
                        1308,
                        1309,
                        1603
                    },
                    BaseControlModel = new ControlModel()
                    {
                        Production = 2,
                        Efficiency = 2,
                        Diplomacy = -4,
                        Research = -4
                    },
                    BaseFleetEffects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Morale,
                            Amount = -5
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.BerserkModifier,
                            Amount = -5
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.CommanderSurvival,
                            Amount = -5
                        }
                    },
                    BaseTraits = new List<RacialTrait>()
                    {
                        RacialTrait.NoBreath,
                        RacialTrait.EfficientInvestment,
                        RacialTrait.DownloadableCommanderExperience
                    }
                },
                new Race()
                {
                    Id = 6,
                    Name = "Agerus",
                    Description = "Even more odd than the Evintos, the Agerus can only be defined as \"planetary life forms.\"  Many scientists doubt their existence, as they have remained largely secluded and withdrawn, having virtually no communication with other species.  Not much is known about this species, whose origin still remains a mystery.  The galaxy battleships that belong to the Agerus are used primarily for defense, and are actually smaller planet forms, which have evolved from spores from the mother planet.  It is not an easy task to classify and differentiate between the children of the Agerus and naturally occurring small planets.",
                    SocietyType = SocietyType.Totalism,
                    BaseTechs = new List<int>()
                    {
                        1404,
                        1405,
                        1406
                    },
                    BaseControlModel = new ControlModel()
                    {
                        Military = 4,
                        Commerce = -3,
                        Diplomacy = -6
                    },
                    BaseFleetEffects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.BerserkModifier,
                            Amount = -5
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.CommanderSurvival,
                            Amount = -5
                        }
                    },
                    BaseTraits = new List<RacialTrait>()
                    {
                        RacialTrait.NoSpy,
                        RacialTrait.AsteroidManagement,
                        RacialTrait.Stealth
                    }
                },
                new Race()
                {
                    Id = 7,
                    Name = "Bosalian",
                    Description = "Bosalians are peace-loving pacifists who hate conflict and battle.  Their noble philosophies and impartiality have settled many a battles between warring races, and their opinions are held in the highest respect by other races.  Though they are pacifists by nature, they are by no means a weak force in the galaxy.  True to their ideology, which states, \"The universe is one with your being, and you are one within the universe,\" Bosalians can freely use psychic powers.  Even races with limited sensory abilities, such as humanoids, can see the brilliance of the psychic aurora emitted by the Bosalians in their attacks.",
                    SocietyType = SocietyType.Personalism,
                    BaseEmpireRelation = 75,
                    BaseTechs = new List<int>()
                    {
                        1419
                    },
                    BaseControlModel = new ControlModel()
                    {
                        Military = -2,
                        Diplomacy = 6,
                        Production = 1,
                        Research = 2
                    },
                    BaseFleetEffects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.BerserkModifier,
                            Amount = -5
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.CommanderSurvival,
                            Amount = 10
                        }
                    },
                    BaseTraits = new List<RacialTrait>()
                    {
                        RacialTrait.Psi,
                        RacialTrait.EnhancedPsi,
                        RacialTrait.Diplomat,
                        RacialTrait.TrainedMind,
                        RacialTrait.Pacifist
                    }
                },
                new Race()
                {
                    Id = 8,
                    Name = "Xeloss",
                    Description = "Much is said about the fanatical religion of the Xeloss, a species that escaped their home planet during the collapse of the Magellan Universe.  The Xeloss are ruthless, and do not hesitate to murder others under their god's name.  Not only do they attack outsiders with their psychic powers, but also they have aptly shown that the individual will sacrifice his basic instinct for survival for the good of their god.  This has added to their already bloody reputation.  No species wishes to readily meet the Xeloss, and they are absolutely correct in their thoughts.",
                    SocietyType = SocietyType.Totalism,
                    BaseEmpireRelation = 20,
                    BaseTechs = new List<int>()
                    {
                        1419
                    },
                    BaseControlModel = new ControlModel()
                    {
                        Military = 2,
                        Diplomacy = -4
                    },
                    BaseFleetEffects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Morale,
                            Amount = -5
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.BerserkModifier,
                            Amount = 5
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.CommanderSurvival,
                            Amount = -10
                        }
                    },
                    BaseTraits = new List<RacialTrait>()
                    {
                        RacialTrait.Psi,
                        RacialTrait.FanaticFleet
                    }
                },
                new Race()
                {
                    Id = 9,
                    Name = "Xerusian",
                    Description = "Xerusians boast an ancient and traditional military tradition.  Though their troops are small in number, they have always remained the utmost elite forces of the galaxy.  In addition, Xerusians have always had great interest in the matter-energy sciences, which are immediately adapted to military weapons and technologies, and have researched these sciences extensively.  The only things that stand between them and the domination of the galaxy are the inefficient workings of their bureaucracy and the excessive amount of energy lost in the internal conflicts within their machinery.  It should be noted that their extensive battle experiences with the Xeloss, has resulted that they are the only race that have a method of stopping the Xeloss psychic attacks.",
                    SocietyType = SocietyType.Totalism,
                    BaseEmpireRelation = 30,
                    BaseTechs = new List<int>()
                    {
                        1306,
                        1307,
                        1308,
                        1309,
                        1310,
                        1311,
                        1313,
                        1314
                    },
                    BaseControlModel = new ControlModel()
                    {
                        Efficiency = -2,
                        Genius = 2,
                        FacilityCost = -1,
                        Research = 1
                    },
                    BaseFleetEffects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Morale,
                            Amount = -5
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.BerserkModifier,
                            Amount = 5
                        }
                    },
                    BaseTraits = new List<RacialTrait>()
                    {
                        RacialTrait.HighMorale,
                        RacialTrait.TacticalMastery
                    }
                },
                new Race()
                {
                    Id = 10,
                    Name = "Xesperados",
                    Description = "Like the Xeloss or Xerusian, the Xesperados race was a group of military species that escaped the Magellan Galaxy during its collapse.  In their wanderings throughout space, they have been joined by other military species and leaders of rebel races, making the Xesperados an impressive force throughout the galaxy.  The merger of many different species is handicapped by potential problems such as the threat of spies from other races and the complex process of expanding life support capable of sustaining the entire group.  But their open minds and universal acceptance has become a great stimulant to the progress of science, and they are continuing to attract talented researchers of all species.",
                    SocietyType = SocietyType.Personalism,
                    BaseEmpireRelation = 30,
                    BaseTechs = new List<int>()
                    {
                        1204
                    },
                    BaseControlModel = new ControlModel()
                    {
                        Spy = -4,
                        Genius = 2,
                        FacilityCost = -3,
                        Research = 3
                    },
                    BaseFleetEffects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Morale,
                            Amount = -10
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.BerserkModifier,
                            Amount = -5
                        }
                    },
                    BaseTraits = new List<RacialTrait>()
                    {
                        RacialTrait.NoBreath
                    }
                }
            };
        }
        private void UseDefaultShields()
        {
            Shields = new List<Shield>()
            {
                new Shield()
                {
                    Id = 5301,
                    Name = "Electromagnetic Shield",
                    Description = "This is the basic shielding that all races develop. This level of shielding is required for a ship to survive the minute amounts of particles that are normally encountered around planets. It also helps somewhat during a battle, but is not that decisive.",
                    TechLevel = 1,
                    Deflection = 1,
                    RechargeRate = new Dictionary<int, int>()
                    {
                        [1] = 2,
                        [2] = 4,
                        [3] = 6,
                        [4] = 8,
                        [5] = 10,
                        [6] = 12,
                        [7] = 14,
                        [8] = 16,
                        [9] = 18,
                        [10] = 20
                    },
                    Strength = new Dictionary<int, int>()
                    {
                        [1] = 30,
                        [2] = 52,
                        [3] = 86,
                        [4] = 148,
                        [5] = 250,
                        [6] = 426,
                        [7] = 724,
                        [8] = 1232,
                        [9] = 2092,
                        [10] = 3558
                    },
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1202
                        }
                    }
                },
                new Shield()
                {
                    Id = 5302,
                    Name = "Graviton Shield",
                    Description = "This is the second level of shielding. In most races it is developed along with core mining techniques. It is needed for this purpose in order to keep the mining equipment safe from the huge pressure and hostile environment found in the cores of planets. The added strength of this shielding makes it more useful for battle.",
                    TechLevel = 2,
                    Deflection = 2,
                    RechargeRate = new Dictionary<int, int>()
                    {
                        [1] = 4,
                        [2] = 8,
                        [3] = 12,
                        [4] = 16,
                        [5] = 20,
                        [6] = 24,
                        [7] = 28,
                        [8] = 32,
                        [9] = 36,
                        [10] = 40
                    },
                    Strength = new Dictionary<int, int>()
                    {
                        [1] = 42,
                        [2] = 72,
                        [3] = 122,
                        [4] = 206,
                        [5] = 350,
                        [6] = 596,
                        [7] = 1014,
                        [8] = 1724,
                        [9] = 2930,
                        [10] = 4980
                    },
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1320
                        }
                    }
                },
                new Shield()
                {
                    Id = 5303,
                    Name = "Deflector",
                    Description = "This is the third level of shielding. Using probability and chaos factors this shielding improves on the Graviton shield by protective energy in areas which are predicted to be most threatened.",
                    TechLevel = 3,
                    Deflection = 3,
                    RechargeRate = new Dictionary<int, int>()
                    {
                        [1] = 6,
                        [2] = 12,
                        [3] = 18,
                        [4] = 24,
                        [5] = 30,
                        [6] = 36,
                        [7] = 42,
                        [8] = 48,
                        [9] = 54,
                        [10] = 60
                    },
                    Strength = new Dictionary<int, int>()
                    {
                        [1] = 58,
                        [2] = 100,
                        [3] = 170,
                        [4] = 288,
                        [5] = 492,
                        [6] = 832,
                        [7] = 1420,
                        [8] = 2412,
                        [9] = 4102,
                        [10] = 6972
                    },
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1219
                        }
                    }
                },
                new Shield()
                {
                    Id = 5304,
                    Name = "Barrier",
                    Description = "This is the fourth level of shielding. This level is not achieved by many races because they never research some of the basic ideas necessary for its development. Basically, this shielding uses even more advanced predictive means to focus and protect.",
                    TechLevel = 4,
                    Deflection = 4,
                    RechargeRate = new Dictionary<int, int>()
                    {
                        [1] = 8,
                        [2] = 16,
                        [3] = 24,
                        [4] = 32,
                        [5] = 40,
                        [6] = 48,
                        [7] = 56,
                        [8] = 64,
                        [9] = 72,
                        [10] = 80
                    },
                    Strength = new Dictionary<int, int>()
                    {
                        [1] = 82,
                        [2] = 140,
                        [3] = 238,
                        [4] = 404,
                        [5] = 688,
                        [6] = 1168,
                        [7] = 1988,
                        [8] = 3378,
                        [9] = 5742,
                        [10] = 9762
                    },
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1223
                        }
                    }
                },
                new Shield()
                {
                    Id = 5305,
                    Name = "Multiphase Shield",
                    Description = "This is the final level of shielding available. It is rarely achieved by most races, but is of great use to those who do. By using energy in different means and transferring some energy through differing dimensions, this shield is sometimes able to in effect reverse the damage done to it as it happens.",
                    TechLevel = 5,
                    Deflection = 5,
                    RechargeRate = new Dictionary<int, int>()
                    {
                        [1] = 10,
                        [2] = 20,
                        [3] = 30,
                        [4] = 40,
                        [5] = 50,
                        [6] = 60,
                        [7] = 70,
                        [8] = 80,
                        [9] = 90,
                        [10] = 100
                    },
                    Strength = new Dictionary<int, int>()
                    {
                        [1] = 116,
                        [2] = 196,
                        [3] = 334,
                        [4] = 566,
                        [5] = 962,
                        [6] = 1636,
                        [7] = 2782,
                        [8] = 4730,
                        [9] = 8040,
                        [10] = 13668
                    },
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1335
                        }
                    }
                }
            };
        }
        private void UseDefaultShipClasses()
        {
            ShipClasses = new List<ShipClass>()
            {
                new ShipClass()
                {
                    Id = 4001,
                    Name = "Gunboat",
                    Description = "Though it is not much more than a single flying weapon outfitted with a bridge and an engine, the Gunboat is a necessary component of any beginning ruler's armada.  While not too intimidating, these ships can nonetheless inflict significant damage.  Also, the Gunboat's small size and lack of devices makes it quick to produce and cost-efficient.  They may be fairly insignificant individually, but many a battle has been decided by a fleet of these little ships attacking an unprepared enemy at just the right time.",
                    Class = 1,
                    BaseHp = 100,
                    Space = 100,
                    WeaponSlotCount = 1,
                    DeviceSlotCount = 0,
                    Cost = 100,
                    Upkeep = 2
                },
                new ShipClass()
                {
                    Id = 4002,
                    Name = "Corvette",
                    Description = "This smaller-class ship is often used as an armed escort for traders and political transports.  The Corvette is significantly smaller than a Destroyer, but can use its speed to evade more powerful enemies. This ship was not designed for a full offensive, but it can do an excellent job at distracting the enemy while other ships move into position or escape, or it can be used in flanking attacks against smaller armadas.  The Corvette is also the largest class ship that players begin with.",
                    Class = 2,
                    BaseHp = 176,
                    Space = 176,
                    WeaponSlotCount = 2,
                    DeviceSlotCount = 0,
                    Cost = 309,
                    Upkeep = 3
                },
                new ShipClass()
                {
                    Id = 4003,
                    Name = "Frigate",
                    Description = "This ship is usually deployed to keep smaller and faster ships away from the less maneuverable Destroyers.  While not as maneuverable as the Corvette, the Frigate has more firepower.  Frigates are also useful in flanking and rear attacks against enemy Cruisers otherwise engaged in combat.  This is also the first ship which can be fitted with enhancing devices; Since these enhancements can perform a variety of functions, the Frigate is the first class of truly versatile ships.",
                    Class = 3,
                    BaseHp = 306,
                    Space = 306,
                    WeaponSlotCount = 2,
                    DeviceSlotCount = 1,
                    Cost = 936,
                    Upkeep = 6
                },
                new ShipClass()
                {
                    Id = 4004,
                    Name = "Destroyer",
                    Description = "This is a small, relatively fast and well-armed warship often used to support larger vessels.  While its armament and enhancing devices are on par with the Frigate, the Destroyer sports a significantly thicker hull.  This means that the loss of some speed and agility as compared to the Frigate is made up for by the ability to take a greater amount of damage before incapacitation.  This allows a fleet of Destroyers to hold fast against smaller numbers of superior class ships.",
                    Class = 4,
                    BaseHp = 537,
                    Space = 537,
                    WeaponSlotCount = 3,
                    DeviceSlotCount = 1,
                    Cost = 2884,
                    Upkeep = 11
                },
                new ShipClass()
                {
                    Id = 4005,
                    Name = "Cruiser",
                    Description = "The Cruiser is large, fast, and moderately armored and gunned.  For several reasons, the Cruiser is the first in a more advanced generation of warships, distinguishing it from the smaller vessels: it has room for two additional devices, can hold some of the more powerful devices, and is the smallest ship which requires off-planet construction (though not necessarily in a vacuum.)  The Cruiser is the pinnacle of elementary spacecraft development, and is often the largest ship that many evolving races have in their armada.  Eventually, though, they are surpassed as the race learns new methods of production.",
                    Class = 5,
                    BaseHp = 940,
                    Space = 940,
                    WeaponSlotCount = 4,
                    DeviceSlotCount = 2,
                    Cost = 8836,
                    Upkeep = 19
                },
                new ShipClass()
                {
                    Id = 4006,
                    Name = "BattleCruiser",
                    Description = "The differences between a Cruiser and the more formidable BattleCruiser are few but significant.  They both have the same number of weapons batteries, but the size of each battery is greatly increased, greatly magnifying the offensive power of each BattleCruiser. This ship is also lighter and faster that the Battleship, while being able to take much more damage than the average Cruiser.  In fact, it is often a perfect combination of the classes above and below it.",
                    Class = 6,
                    BaseHp = 1640,
                    Space = 1640,
                    WeaponSlotCount = 4,
                    DeviceSlotCount = 2,
                    Cost = 26896,
                    Upkeep = 34
                },
                new ShipClass()
                {
                    Id = 4007,
                    Name = "BattleShip",
                    Description = "With the capability of destroying an average space station single-handedly, the Battleship forms the foundation of any serious armada.  It is the largest, most heavily armed and armored type of ship able to be manufactured within gravity.  Its lack of speed demands an array of escorts (Frigate class and smaller) to protect its vulnerable flanks and rear.  Of course, such escorts are usually easily provided by anyone with the power to create a fleet of Battleships.",
                    Class = 7,
                    BaseHp = 2870,
                    Space = 2870,
                    WeaponSlotCount = 5,
                    DeviceSlotCount = 2,
                    Cost = 82369,
                    Upkeep = 61
                },
                new ShipClass()
                {
                    Id = 4008,
                    Name = "Dreadnaught",
                    Description = "This ponderous beast is so large and well armed that it can barely be called a ship, and must be built in orbit.  This super-vessel is bloated with weaponry and armor, and is a menace for any class of opposing forces.  Many an opponent has suddenly realized they were doomed when they saw this powerful creation appear amidst its enemies fleets. Obviously, the Dreadnought requires a full deployment of escorts and support vessels.",
                    Class = 8,
                    BaseHp = 5025,
                    Space = 5025,
                    WeaponSlotCount = 5,
                    DeviceSlotCount = 3,
                    Cost = 252506,
                    Upkeep = 110
                },
                new ShipClass()
                {
                    Id = 4009,
                    Name = "Mobile Fortress",
                    Description = "The Mobile Fortress is so large and so well armed that it can only be classified a space station.  (Though it is reasonably movable, its real function is to destroy any enemies who are foolish enough to approach.)  This titan is large enough that it is a feature itself, around which an armada can station a defense.  It does still retain enough movement though, that it can be brought to bear against any weaponry that your opponent might have which is larger.  The Mobile Fortress is basically an excuse to put many, many guns together in one place.",
                    Class = 9,
                    BaseHp = 8796,
                    Space = 8796,
                    WeaponSlotCount = 6,
                    DeviceSlotCount = 4,
                    Cost = 773696,
                    Upkeep = 198
                },
                new ShipClass()
                {
                    Id = 4010,
                    Name = "Doomstar",
                    Description = "The Doomstar, as its name implies, is nearly a celestial body in its own right.  The pinnacle of space design for any known sentient species, this behemoth is so large that merely trying to land it on a planet would cause an ecological catastrophe.  It is large enough that it generates a large gravitational pull, which would wreak havoc on itself and any planets that it approached.  Fitted with a terrifying array of weaponry, the Doomstar can dispense annihilation upon any nearly any enemy.  Simply put, anyone who tries attacking a Doomstar deserves what they get.",
                    Class = 10,
                    BaseHp = 15400,
                    Space = 15400,
                    WeaponSlotCount = 7,
                    DeviceSlotCount = 4,
                    Cost = 2371600,
                    Upkeep = 357
                },
            };
        }
        private void UseDefaultTechs()
        {
            Techs = new List<Tech>()
            {
                new Tech()
                {
                    Id = 1101,
                    Name = "Metaphysics",
                    Description = "Metaphysics is the study of comprehending the essence which exists beyond the world of experience in its organized final principles and theories.  What one can feel with his/her five senses of sight, touch, smell, hearing, and taste is something is not \"real\" but only \"feels real.\"  The beginning principle of metaphysics is the effort to realize the \"real\" truth beyond the sensory perceptions through thought.  By starting to realize the principles beyond what was perceived, humans began to treat the essence of existence, not mere objects.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Basic,
                    TechLevel = 1
                },
                new Tech()
                {
                    Id = 1102,
                    Name = "Logic",
                    Description = "A cause-and-effect relationship exists in all things.  Logic is the study which seeks to delve into these relationships and arrive at the right conclusions.  This academia is a part of the study of standardization.  Logic seeks to prove the validity of correct reasoning through orderly thought and reasoning procedures.  Through these steps, one can find out errors in one's rationalism, thus coming one step closer to correct reasoning.  Because of this, logic places great importance in the art of expression, and does not make room for ambiguity.  Ambiguity itself is analyzed to the depth of clarity in order to make ideas understood.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Basic,
                    TechLevel = 1
                },
                new Tech()
                {
                    Id = 1103,
                    Name = "Philosophy",
                    Description = "Philosophy refers to thought itself.  That is, the thought of thought creates the base of philosophy.  This study aims to study the method of arriving at knowledge, rather than knowledge itself.  Recognition, interpretation, understanding, and concluding make up the acts of the academia.  Philosophy is a necessary part in the basic understanding of life and the universe.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Basic,
                    TechLevel = 1
                },
                new Tech()
                {
                    Id = 1104,
                    Name = "Religion",
                    Description = "Since the dawn of literature and academics, religion and faith have always been an existing foundation for improvement and advancement.  In an age where there were more things to be understood and discovered than things that were, an ultimate being which stood above all of creation became the simplest answer to impossible phenomenons.  As a result, religion and faith became the strength for humans to escape from their ignorant fears and carry on with their lives.  Religion has carried out its role well and has held onto its ground of influence amongst advancements in thought and culture.  In becoming the model of devotion and will power, the guiding principle of society and its social codes, and the one thing that people sought for their spiritual strength, religion has existed in the past, present, and will carry on to the future.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 2,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1101
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1103
                        }
                    }
                },
                new Tech()
                {
                    Id = 1105,
                    Name = "Planetary Economics",
                    Description = "Economics is the study of calculating how to most effectively utilize limited resources.  Planet Economics seeks to teach how one can effectively divide the given resources of a planet and select the best methods to utilize these resources.  This study has sought to establish basic economic rules, and improve them to seek better results.  However, even with the application of Planetary Economics, not all resources have been effectively utilized, sometimes bringing an overall minus effect.  Thus, Planetary Economics has been concluded to have its limits.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 2,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1102
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1203
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Commerce = 1
                    }
                },
                new Tech()
                {
                    Id = 1106,
                    Name = "Symbolic Logic",
                    Description = "Symbolic Logic is the branch of logistics which specializes in the aspect of symbols.  This branch has succeeded in bringing out the side of symbolism to its extreme limits.  Everyday language is always vague and inaccurate, and bears traces of misunderstanding, making it an inappropriate choice for logical analyzation.  It is almost impossible to fathom the depths of ambiguity, multiple meanings, metaphorical expressions, and ranges of meaning and understanding.  Thus, Symbolic Logic seeks to utilize logical symbols, which are more accurate, to express thoughts and fundamentally quell any sign of ambiguity all together.  Through the development of this branch of logistics, even deductions which required complex thoughts could be simplified to the degree of automation.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 2,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1102
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Efficiency = 1
                    }
                },
                new Tech()
                {
                    Id = 1107,
                    Name = "Psychology",
                    Description = "Psychology deals with the psyche, or simply put, all the realms related to the mind and spirit.  Group mentality and personal psychology both belong to the territory of psychology.  The conscious and the unconscious mind, thought and instinct, survival and defense mechanisms, rest and restlessness, as well as the operating process of the mind, and its background, including the inherent traits of trauma, complexes, according behavior patterns, are all analyzed and interpreted.  In addition, psychology's role and goal are to prepare appropriate responses to to the above.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 2,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1103
                        }
                    }
                },
                new Tech()
                {
                    Id = 1108,
                    Name = "Cosmic Environmentalism",
                    Description = "As society made its progresses, the universe could not escape from the influences of environmental pollution.  The rapid developments exhausted natural resources, and pollution on a planetary scale, as well as satellites and spaceships abaondoned in space, reached dangerous levels, interfering with the normal planet operations.   Electronic radio wave pollutions, the destruction of the natural ecology of the universe, and sabotage and monopoly of ancient cultural relics were criticized as playing major parts in disturbing the natural balance of the universe.  There had been outspoken opinions on the need to preserve the universal environment in the past, but pollution reached dangerous and critial levels before these opinions were recognized and organized into an academia.  The essence of Cosmic Environmentalism focuses on preserving nature and protecting the environment to oversee the ecology of the universe, home to all species.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 3,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1105
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1405
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Diplomacy = 1
                    }
                },
                new Tech()
                {
                    Id = 1109,
                    Name = "Classism Theory",
                    Description = "Classism Theory is the fundamental study of classism ideology, basic concepts, society structure, and behavior patterns.  The class structure bases its foundation on the theory that all organisms are classified into categories from the moment of birth.  There are many standards that one can find guidance in categorizing classes, such as religion, birth, wealth, and poverty, but all of these are similar in that each category is classified by class, and that it is almost impossible to break this system.  The class system is structured in a pyramid structure, with the number of people decreasing with the higher classes, and more power as one moves up the pyramid.  Discontent is always present in this class society.  As a consequence, there are always seeds of discord that may give rise to massive revolutions.  But even if there is no apparent revolution, there are always guerilla wars going on in society.  On the one hand, these wars are destructive in nature, but on the other hand, when applied to productivity, they act as catalysts for social improvements.  Classism Theory also deals with the style of warfare employed by all the classes in their battles and the progressions that these wars have brought to society by closely examining these factors in a scientific manner.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 3,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1104
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Diplomacy = 1
                    }
                },
                new Tech()
                {
                    Id = 1110,
                    Name = "Totalism Theory",
                    Description = "Totalism Theory is the fundamental study of totalism ideology, its social structure, and behavior patterns.  A totalitarian society is one where all individuality is sacrificed for the common good of society.  There are many cases where classes may exist within a totalitarian society, but these classes greatly differ from classist society in that no matter what class a citizen may belong to, he or she has no desires to advance in class, and instead chooses to sacrifice his or herself for the good of the country.  By sacrificing individuality, totalitarian society has succeeded in optimizing the efficiency of the overall society.  However, this society also has its fallacies.  Standardization through elimiation of individual categories has brought on a great dearth of originality.  This has been addressed as the primary problem of totalitarian society.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 3,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1107
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Efficiency = 1
                    }
                },
                new Tech()
                {
                    Id = 1111,
                    Name = "Personalism Theory",
                    Description = "Personalism Theory is the fundamental study of individualist ideology, its social structure, and behavior patterns.  A personalist, or individualist society, is the total opposite of totalitarian society.  In this society, individualism is most valued and respected.  This society cannot imagine risking one's self for the common good of others, or even for society.  Accordingly, all working relationships between individuals are progressed in the form of contract labor.  The success and expansion of society is based on the most productivity possible with maximum freedom.  However, it is extremely difficult to bend individual workers in the direction which society wants to achieve, and consequently, overall productivity is significantly lower than other societies.  Personalism Theory also deals with the position which individuals hold in society, the far reaching effects of these positionings, and examination of social structure.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 3,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1107
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Commerce = 1
                    }
                },
                new Tech()
                {
                    Id = 1112,
                    Name = "Galactic Economics",
                    Description = "Galactic Economics was created to solve the shortcomings of Planetary Economics.  Galactic Economics goes beyond a singular planet to examine the economic flow of the entire galaxy.  The role of Galactic Economics is to study economic issues, phenomena, and patterns on a large scale, in real life (rather than theory), and provide appropriate solutions.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 3,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1105
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1308
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Commerce = 1
                    }
                },
                new Tech()
                {
                    Id = 1113,
                    Name = "Alien Archaeology",
                    Description = "All over the galaxy, discoveries of relics or old ruins are being discovered.  No one knows who created these archaic treasures, or when they were created.  These relics are commonly known to have been part of the ancient galactic empire's treasures.  However, the history of the ancient galactic empire traces so far back as to mythical days, and there are some who believe that these relics are mere products of overactive imaginations.  But these relics, both concrete and tangible, exhibit such superior technology that it leaves no room for doubt that technology and science were both advanced beyond common understanding in the ancient galactic empire.  Alien Archaeology seeks to uncover these ancient works and study their uses.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 4,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1108
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Genius = 1
                    }
                },
                new Tech()
                {
                    Id = 1114,
                    Name = "Green Economics",
                    Description = "Sometimes, it is necessary to stop the progress of the economy for the good of the environment, society, or other more important issues.  But if economic progress is merely halted, its stagnation would lead to a drastic decline in the economic curve.  Green Economics seeks to find the balance between sufficient production and consumption, and maintaining the current status of the environment and resources.  This study can be considered to be the ultimate course in economics, and practices the achievement of balance between supply and demand, and all other social factors.  Green Economics also exercises fluidity by advancing or retreating according to the status of social progress, effectively neutralizing adverse effects and maintaining the perfect balance which it teaches.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 4,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1108
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1402
                        }
                    }
                },
                new Tech()
                {
                    Id = 1115,
                    Name = "Collective Unconsciousness",
                    Description = "All living organisms have an inherent unconscious trait which is universally shared, alongside their individual experiences and feelings.  Without ever having made contact with each other, and living in entirely different environments, all organisms have a common memory with each other.  Collective Unconsciousness seeks to utilize this common memory to fabricate a collective unconsciousness.  The common collective unconsciousness itself is, for the most part, primitive and hard to understand, but possesses a complete sequential system in itself.  By exercising this unconsciousness, it is possible to fabricate a mass common unconsciousness and give it various directions.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 4,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1110
                        }
                    }
                },
                new Tech()
                {
                    Id = 1116,
                    Name = "Advanced Psychology",
                    Description = "Advanced Psychology views psychology as one large consciousness, rather than dividing it into several stages, and seeks to analyze and understand this one large consciousness.  If psychology in the past could be considered analyzation of common traits occurring amongst the different divisions of the conscious mind, advanced psychology can be viewed as analyzation of the divisions as they are.  Recognizing that all organisms possess different conscious minds, and refusing to categorize them, Advanced Psychology can be said to be a product of the personalist society.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 4,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1111
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Genius = 1
                    }
                },
                new Tech()
                {
                    Id = 1117,
                    Name = "Primitive Mind",
                    Description = "All species possess different styles of consciousness.  However, these different conscious thoughts have a common bond with each other, and the Primitive Mind has proven that it is possible to transfer the conscious state of one species to this common trait.  Discovering this common consciousness beyond the unconscious and into the conscious state was a big step towards opening a new chapter in both organized and collective consciousness.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 5,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1115
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1409
                        }
                    }
                },
                new Tech()
                {
                    Id = 1118,
                    Name = "Primitive Language",
                    Description = "All living organisms possess basic information about language from the moment of birth.  Learned language is registered, relying unconsciously, on this primitive information.  Primitive Language is the language which directly utilizes this fundamental language information.  Because primitive language is a fundamental language registered in every individual's brain, it would be possible to overcome most of the language barrier obstacles should an effective way of utilization be found.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 5,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1113
                        }
                    }
                },
                new Tech()
                {
                    Id = 1119,
                    Name = "Advanced Classism Theory",
                    Description = "By minimizing the weak points and maximizing the strong points of classism, an advanced form of classist study, Advanced Classism Theory, was created.  In a classist society, there is a necessary clash at all times between the upper classes, who want to maintain their affluent status, and the lower classes, who want to move up to a higher class.  The core of Advanced Classism Theory centers around the manipulation of these clashes of desire and use that power to increase productivity in society.  The more established a class system is in society, the ability to act against emergencies and change decreases, and chances increase that the discontent of the lower class lead to a revolution.  Accordingly, the base theory of Advanced Classism Theory is based on a moving class system, where status change can occur at any time.  This study has also systematically theorized the method of adjusting class clashes according to the status of society, and consequently changing society's direction.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 6,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1109
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1117
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Efficiency = 1
                    }
                },
                new Tech()
                {
                    Id = 1120,
                    Name = "Advanced Totalism Theory",
                    Description = "By minimizing the weak points and maximizing the strong points of totalism, an advanced form of totalist study, Advanced Totalism Theory, was created.  Members of a highly evolved totalist society possess almost no individual opinions.  All actions are executed through a little instinct and much commands and instructions from above.  Superiors issuing orders may be a class acting as the brain of the society, or may be the conscious opinion of the entire society.  In cases where instinct and orders differ, orders are placed above instinct in priority.  Thus it is common to see a society member volutarilly carrying out kamikaze actions.  Because the whole society moves in unity, production is maximized, and it is possible to apply a set of uniform opinions to relax the rigidity of society.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 6,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1117
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1118
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Diplomacy = 1
                    }
                },
                new Tech()
                {
                    Id = 1121,
                    Name = "Advanced Personalism Theory",
                    Description = "By minimizing the weak points and maximizing the strong points of personalism, an advanced form of individualist study, Advanced Personalism Theory, was created.  The members of a highly evolved individualist society have great pride in themselves, as well as great respect for each other.  Sometimes, this may be regarded as snobbery, as these citizens regard other species as \"primitive\" or \"savage.\"  One can use this pride to temporarily unite these citizens, who owe no allegiance to their country, in times of war.  In addition, by utilizing the creativity and free thoughts abundant to this society, one can greatly speed up the advance in learning.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 6,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1116
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1118
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Commerce = 1
                    }
                },
                new Tech()
                {
                    Id = 1122,
                    Name = "Self-evident Language",
                    Description = "Natural language, with its inherent ambiguity, tends to become more vague and inaccurate as a message is passed on from one person to another.  However, Self-evident Language self-proves its ambiguities and clarifies vague parts, making the message more clear as it is passed on.  Viewed as a combination of evidence collection, the method of proving theories, and the art of expression, the advancement of self-evident language provided yet another topic of debate about the concept of perfection.  Self-evident Language began being regarded as the perfect language, which had surpassed the limits of daily language and symbolism.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 7,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1119
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1216
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Efficiency = 1
                    }
                },
                new Tech()
                {
                    Id = 1123,
                    Name = "Universal Translator",
                    Description = "According to the Primitive Language theory, all organisms possess a common knowledge of language from the moment they are born.  If this language could be developed so that all languages could be translated to this primitive language, it would also be possible to translate and interpret all languages.  The Universal Translator is the product of these ideas.  Through translating all languages into the primitive language, and reinterpreting them into various languages, a complete universal translator was born, and all problems of language communication were solved.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 7,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1121
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1216
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Diplomacy = 1
                    }
                },
                new Tech()
                {
                    Id = 1124,
                    Name = "Collective Mind",
                    Description = "As Primitive Mind theories further developed, a common link between the conscious minds of all organisms could be found.  It was then possible to research a method of manipulating the conscious minds of beings to act as a whole, much like the Collective Unconsciousness had achieved with the unconscious mind.  The result of this research was the Collective Mind, which applies the same method used in the Collective Unconsciousness to the Primitive Mind, making it possible to influence the conscious minds of all beings.  This theory paved the way for totalism society to truly unite the minds and wills of all its members.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 7,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1120
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1418
                        }
                    }
                },
                new Tech()
                {
                    Id = 1125,
                    Name = "Unification",
                    Description = "Unification is the ultimate stage of totalism.  All the thoughts of society's members are gathered into one thought, making the unification society a form of ideal society.  In a unification society, members do not possess any self will.  The entire society moves as one massive organism, and each member exists merely as a cell component of that organism.  Because these members do not possess self-defensive instincts, it is highly debated among other societies as to whether or not each member should be regarded as independent organisms.  Achieving a unified organism structure, a unification society's production is maximized, and cannot be matched by any other society.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 8,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1124
                        }
                    }
                },
                new Tech()
                {
                    Id = 1126,
                    Name = "Imperium",
                    Description = "Imperium is the ultimate stage of classism.  A perfect pyramid class structure is headed by an all-powerful leader, making this system unbreakable and impossible to overthrow.  By going through the correct procedures, one may move up in class rank, but the difference in power between the classes is so great that the desire to advance is the greatest motivator in the development of imperium society.  An imperium society is very flexible and accommodating, but at the same time, the entire society moves under the direction of the leader, making imperium society an interesting hybrid of classism and personalism.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 8,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1122
                        }
                    }
                },
                new Tech()
                {
                    Id = 1127,
                    Name = "Anarchism",
                    Description = "Anarchism is the ultimate stage of personalism.  No government exists, and all citizens move in their desired directions of their own will.  It may appear to be chaotic, but because all people work under a contract for their own profit, it is not a mere disorderly society but rather becomes a form of ideal society.  Because there are no restrictions, an individual's skills can be exercised to the fullest.  There is no other organization which can compare to the speed of creativity produced through anarchism.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 8,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1123
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Genius = 2
                    }
                },
                new Tech()
                {
                    Id = 1128,
                    Name = "Will to Power",
                    Description = "One's will is the true source of power for society and production.  Will to Power is the art of converting will itself into a form of powerful energy.  This status can only be attained when one is above comprehension of one's will, and possesses the power to convert that will into a physical form.  Will to Power is known to be the ultimate stage of existence that an organism can reach.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 8,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1124
                        }
                    }
                },
                new Tech()
                {
                    Id = 1129,
                    Name = "Meaning of Universe",
                    Description = "The area of thought about the meaning of existence grew, expanding to the farthest reaches of the universe.  However, the thoughts that went out into the universe came back as a whole, eventually becoming the conclusion of the meaning of existence.  When one has reached this stage, the all of nature becomes one with one's self, and one can see the truth in the passing wind, and see a flower in front of one's eyes within the meaning of nature.  Encompassing one within the whole, and expressing the whole with one, this meaning cannot be comprehended easily, but once this meaning is realized, one comes yet another step closer to the realm of God.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 9,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1128
                        }
                    }
                },
                new Tech()
                {
                    Id = 1130,
                    Name = "Thought of God",
                    Description = "What are the thoughts of God, and what are God's actions?  If one could understand the thoughts of God, who is beyond the reach of mortals, what would those thoughts be?  The Thought of God theory assumes that if one can comprehend the essence of all things, that person can in turn understand the reverse side of these understandings, the thoughts of God.  To understand comprehend the thoughts of God is to come one step closer to absoluteness and eternity.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1129
                        }
                    }
                },
                new Tech()
                {
                    Id = 1131,
                    Name = "Ultimate Truth",
                    Description = "The Ultimate Truth is the all-encompassing, final answer to all the questions of the universe.  Though the world may change, the Ultimate Truth remains unchanging.  This truth represents the will of God, and is the true power on whom one can rely.  Ultimate Truth is unchanging, yet contains in itself eternity, becoming ever changing.  \"Ever changing\" is not a paradox, but rather an attempt to convey the inexpressible Ultimate Truth.  Thus, Ultimate Truth is a perfect whole which cannot be expressed by words, and can only be comprehended by the enlightened ones.  Ultimate truth can also be said to be the Absolute Being in itself.",
                    Type = TechType.Social,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1130
                        }
                    }
                },
                new Tech()
                {
                    Id = 1201,
                    Name = "Electronic Computer",
                    Description = "This machine enabled work to be digitally converted and electronically processed, increasing calculation speed while allowing various operations and works to be completed in a sequential manner.  The calculation speed itself possessed many advantages.  Its electronic characteristics enabled far more complicated and detailed processing, thus heightening the abilities of the computer, than the normal physical process allowed.  However, even the electronic process had its limits when it came to processing microscopic details.  Electronic computers became the root of automated work processing.",
                    Type = TechType.Information,
                    Attribute = TechAttribute.Basic,
                    TechLevel = 1
                },
                new Tech()
                {
                    Id = 1202,
                    Name = "Linear Programming",
                    Description = "The linear programming process enabled problems to be interpreted linearly and processed.  This simple programming structure was suited for mass production programming.  However, because all problems could only be analyzed in the simplest of structures, this process possessed the weak spot of not being able to be utilized for general purpose work.  The bigger the problem, the more variables occured - and this process required the computer's powers in geometric progressions, or accuracy became less accurate.",
                    Type = TechType.Information,
                    Attribute = TechAttribute.Basic,
                    TechLevel = 1
                },
                new Tech()
                {
                    Id = 1203,
                    Name = "Information Network",
                    Description = "It was this Network that played a pivotal role in optimizing the use of computers.  An infinite number of computers could share information with each other, thus raising the value of their information infinitely.  This network enabled the information to be delivered to each computer, and each computer to possess infinite information.  The most highly evolved stage of trade since the early bartering of the cavemen, this information trade was made possible due to its characteristics of the digitalization of computers, and a network system which was not restricted by space.  As time went on, the Network System began to be used for long distance communication between two parties.",
                    Type = TechType.Information,
                    Attribute = TechAttribute.Basic,
                    TechLevel = 1
                },
                new Tech()
                {
                    Id = 1204,
                    Name = "Chaos Algebra",
                    Description = "This mathematical theory predicted that it was possible to predict the stage of chaos through investigation of infinite variables in the linear stage.  This became possible as the electronic computer used its microelectronic devices to rapidly evolve and widen the range of linear programming.  This prediction program became an invaluable aid in technology dealing with weather, the distance between star clusters, and universal positioning.",
                    Type = TechType.Information,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 2,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1201
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Production = 1
                    }
                },
                new Tech()
                {
                    Id = 1205,
                    Name = "Superconductor Computer",
                    Description = "By using oxidized ceramic substances, such as bismuth, barium, and copper, it was able to utilize electronic currents with virtually no loss.  This became the incentive to rapidly advance the development of the electronic computer's abilities.  Originally, this theory was developed and researched for the purposes of travel using self-levitation, and of particle acceleration.  But when applied to the computer switch unit, it was discovered that great results could be achieved.",
                    Type = TechType.Information,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 2,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1201
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1304
                        }
                    }
                },
                new Tech()
                {
                    Id = 1206,
                    Name = "Planetary Network",
                    Description = "The planetary network was a method of communication between star clusters using fast moving particles between them, such as light.  At first, speed and message delivery accuracy became a problem in solving the problem of distance and isolation between the planets.  Until a faster interplanetary communication device was discovered, the cultural isolation and communication barrier became increasingly worse.",
                    Type = TechType.Information,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 2,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1203
                        }
                    }
                },
                new Tech()
                {
                    Id = 1207,
                    Name = "Non-linear Programming",
                    Description = "When the one-dimensional approach became ineffective against the variables which were multiplying exponentially, a variable approximation principle closest to the non-linear physical world was developed.  This principle could not be produced earlier because of the difficulty in approaching and analyzing non-linear objects.  Until the principle was discovered, much preparation, development in theory, and time was needed.  When this principle could be exercised freely, this non-linear approach method made rapid progress possible in the areas of volume measurement, distance approximation, and observation systems.",
                    Type = TechType.Information,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 3,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1202
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1204
                        }
                    }
                },
                new Tech()
                {
                    Id = 1208,
                    Name = "Non-linear Programming",
                    Description = "As more algebraic algorithms and variables requiring calculation appeared, they required more and more computing power.  The underlying principles of the optical computer were unchanged old principles, but new technology utilizing particles of light to heighten performance and accuracy were added.  Optical science was considered of primary importance in the past, but its reality took time to be realized, due to the characteristics of light particles.  This science contributed largely to the advancement of computer technology.",
                    Type = TechType.Information,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 3,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1205
                        }
                    }
                },
                new Tech()
                {
                    Id = 1209,
                    Name = "Artificial Intelligence",
                    Description = "The purpose of artificial intelligence was as a substitue for human efforts, which could adapt quickly to given conditions.  Artificial intelligence was also started to reduce the volutariness of human intellect.  Ultimately, as humans were regarded as an intellectual species, artificial intelligence was discovered as a existing substitution to human intellect and thought.  However, this technology involved simple electronic computers and a vast network with extensive database and corresponding algorithms, and could only produce logical and orderly conclusions generated on these facts.  This coul not be considered intelligence in the true sense.  Thus, this method soon became developed as a solution system for problem solving, and artificial intelligence broke off to form a specialized self-evolutionary software engineering unit.  Humans will never abandon the quest to create an intellectual object closest to their thoughts.",
                    Type = TechType.Information,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 3,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1205
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Research = 1
                    }
                },
                new Tech()
                {
                    Id = 1210,
                    Name = "Virtual Reality Matrix",
                    Description = "A technology that makes it possible to volutarily experience a virtual reality was discovered.  This technology involved an interface that directly connects to the brain, which controls and oversees all of the sensory experiences.  The virtual reality matrix can be considered the most advanced of the human desire for virtual reality, which begins even from the mother's womb.  But the side effects that followed from this technology were as great as the technologies that produced it.  As the virtual reality matrix was used more and more, mental illness cases increased significantly.",
                    Type = TechType.Information,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 3,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1106
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1206
                        }
                    }
                },
                new Tech()
                {
                    Id = 1211,
                    Name = "Self-Evolutionary S/E",
                    Description = "To artificial intelligence, intellect was confined to information, or database, but to self evolving software, intellect was continual physical improvement through changing one's algorithms and processes.  This was only designated as a program, and was far from human thought and consciousness.  However, it was possible to create a model of human intellect operation through this method.",
                    Type = TechType.Information,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 3,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1209
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1410
                        }
                    }
                },
                new Tech()
                {
                    Id = 1212,
                    Name = "Advanced Encryption Algorithm",
                    Description = "As calculation speed improved at an astonishing rate, inverse functions that seemed virtually impossible to crack with infinite time became a possibility, and an improved security system algorithm was needed.  By utilizing points where inverse functions did not exist, or irregular input keys, it was possible to once again heighten security.  But it was still impossible to disprove the theory that no complete password and security program can exist.",
                    Type = TechType.Information,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 4,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1207
                        }
                    }
                },
                new Tech()
                {
                    Id = 1213,
                    Name = "Artificial Consciousness",
                    Description = "As the study of the human brain progressed in genetics, the knowledge about sensory functions and intellect accumulated to a massive amount, and there was much improvement in the study of human brain imitation.  A theory was reached that machines could duplicate consciousness, which was said before to be impossible to replicate.  Thus, it was possible to plant a great potential in the self evolutionary system progress.  However, it still fell short of the greatness that human potential could be capable of.  As research progressed, the reality of artificial organisms became more clear, and as potential was experimentally planted into a few large systems, artificial consciousness developed to a dangerous point.  A dangerous point signified that the existence of intellect was no longer unique.",
                    Type = TechType.Information,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 5,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1211
                        }
                    }
                },
                new Tech()
                {
                    Id = 1214,
                    Name = "Hyperspace Communication",
                    Description = "Before transport using hyperspace was discovered, a communications systems utilizing hyperspace was developed long ago.  The hyperspace communications device solved many of the isolations problems presented by the standard interplanetary communications device, and its weak points.  However, hyperspace had its flaws.  Problems such as static and unsecured connections to the outside world arose, and the major problems of information and ideas being leaked to the outside world occurred.  Until this technology was created, long and tedious studies were conducted about the n-dimensional technology.",
                    Type = TechType.Information,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 5,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1212
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1308
                        }
                    }
                },
                new Tech()
                {
                    Id = 1215,
                    Name = "Auto-channeling Communication",
                    Description = "This is a communication method that automatically casts the broadcasting channel in order to provide a standard for destination and purpose between the broadcasting communication of two parties.  The auto-channeling communication plays a role in clarifying communication, and has aided in providing more accurate and clear communication delivery systems.",
                    Type = TechType.Information,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 5,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1212
                        }
                    }
                },
                new Tech()
                {
                    Id = 1216,
                    Name = "Universal Communication Protocol",
                    Description = "This protocol was developed in order to communicate between two parties that have different languages or different ways of expressing themselves.  This is different from translation of each other's language.  This can be considered to be the protocol of all protocols, in the fact that all communication is possible between different party protocols.",
                    Type = TechType.Information,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 6,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1214
                        }
                    }
                },
                new Tech()
                {
                    Id = 1217,
                    Name = "N-dimensional Algebra",
                    Description = "The first product of trying to define the physical world through different mathematical theories, n-dimensional algebra made it possible to closely examine the movement of different objects in space, and made great progress in the understanding of hyperspace, which possessed unlimited potential yet to be explored.",
                    Type = TechType.Information,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 6,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1214
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1323
                        }
                    }
                },
                new Tech()
                {
                    Id = 1218,
                    Name = "Ghost Creation",
                    Description = "The adaptation technology of ghost creation makes it possible to create a clone capable of carrying out all duties of one's self, and to actively use this clone in another place via remote control through the network.  At first, the clones that were created were precise mechanical duplicates, but as the development of genetics went on, it was possible to create organic clones.  However, due to the risks posed by this technology, rules have decreed that it can only be used for testing purposes, dangerous situations, wars, or otherwise critical situations that may occur.  Another purpose of the ghost creation technology was to contribute in long-distance communications and negotiations with outside forces.",
                    Type = TechType.Information,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 6,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1213
                        }
                    }
                },
                new Tech()
                {
                    Id = 1219,
                    Name = "Solution of Chaos Equation",
                    Description = "After the discovery of n-dimensional measurement and observation, an equation to control the activity of the n-dimension was discovered.  Accordingly, most of the solutions for physics activities were produced, thanks to the development in computer technology.  This opened the path for freer movement throughout space.",
                    Type = TechType.Information,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 7,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1217
                        }
                    }
                },
                new Tech()
                {
                    Id = 1220,
                    Name = "Self-Evolutionary Network",
                    Description = "In this network, it is possible for the mother network to produce an offspring network.  This began to develop rapidly as solutions for artificial intelligence and thought were discovered.  The self-evolutionary network played a central role in tightly linking the connections between networks.  In addition, the self-evolutionary network made large scale network structures obsolete, and the concept of isolation within one group disappeared all together.",
                    Type = TechType.Information,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 7,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1218
                        }
                    }
                },
                new Tech()
                {
                    Id = 1221,
                    Name = "Quantum Computer",
                    Description = "Information and information processing were designated as a new type when quantum mechanics were applied.  The information processing produced through quantum mechanics produced the greatest power possible by man.  In actuality, a quantum signifies that computing powers can be raised to limitless ranges using unlimited power, but this is unachievable, since combination with other technologies produces a bottleneck state that makes it impossible to obtain unlimited power.",
                    Type = TechType.Information,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 8,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1219
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1221
                        }
                    }
                },
                new Tech()
                {
                    Id = 1222,
                    Name = "Information Network Ecosystem",
                    Description = "A non-existing, self-thinking life form began to appear in the self-evolutionary network system.  These forms were mere by-products of the network organization, but as they grew in size, they began to grow into yet another part of the network.  They further went on to form intellectual spaces as evolved matters.  This was an unprecented event, as before, intellectual spaces centralized around humans.",
                    Type = TechType.Information,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 8,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1220
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1415
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Research = 2
                    }
                },
                new Tech()
                {
                    Id = 1222,
                    Name = "Algorithmic Research Computer",
                    Description = "The algorithmic research computer is a sort of evolutionary system research that studies algorithms to predict and solve problems before they occur.  Through this system, much progress was made in the solving of anticipated problems.  This system also became a milestone in the creation of Oracle Computing.",
                    Type = TechType.Information,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 9,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1221
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1222
                        }
                    }
                },
                new Tech()
                {
                    Id = 1224,
                    Name = "Temporal Probability Computing",
                    Description = "The final corrective mechanics system, the temporal probability computing system calculated minute details, down to the probability of error in minute detail.  This significantly decreased the possibility of error in physics research and adaptative technology, while raising their performance levels.  Because this computing system is highly adaptive, it can be used in many situations.  The temporal probability computing system became the mandatory study required to make basic assumptions about time machine research, the general flow of the galaxy and space, and the flow of time.",
                    Type = TechType.Information,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1223
                        }
                    }
                },
                new Tech()
                {
                    Id = 1225,
                    Name = "Oracle Computing",
                    Description = "Oracle Computing is a high end goal of Information Science, and holds the key to all problems.  The Oracle Computer acts as if an answer exists for any question, even questions that are posed for the first time.  The computing system also maintains a strict cycle of evolution, preservation, improvement, organization, and information rotation in order to prevent its demise.  It is said that the Oracle Computing System has evolved to the point of absolute accuracy, and cannot be challenged.  It is also said that the system still maintains all of the solutions, procedures, and protocols needed for outside contact reception.  However, all of the above statements rest on the premise that the system \"still\" has all the answers, meaning that the statements can be disproved at any time.  It must never be forgotten that the Oracle Computing System is that much more dangerous because it is an indestructible object, and in the case that the system is destroyed or deviates off its course, it may become an unstoppable force.",
                    Type = TechType.Information,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 11,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1224
                        }
                    }
                },
                new Tech()
                {
                    Id = 1301,
                    Name = "Nuclear Power",
                    Description = "Progress in quantum mechanics and the Theory of Relativity led to the discovery of a power which, for the first time, was stronger than the forces of nature - nuclear power.  This nuclear power settled the problem of energy and resource exhaustion, and was regarded as a gift from the heavens.  However, nuclear power was a two-sided coin, and brought the creation of the first mass killing weapon capable of destruction on a planet-scale along with its good uses.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Basic,
                    TechLevel = 1
                },
                new Tech()
                {
                    Id = 1302,
                    Name = "Laser",
                    Description = "The technology of emitting directional electrical pulses of identical wavelengths.  Unlike other fundamental utilities, laser technology maximizes the effect of energy, and is used widely in every day use as well as warfare.  The exact description of this technology is a device that utilizes the replicative inductive characteristics of atomic particles in their unstable state to produce light waves.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Basic,
                    TechLevel = 1
                },
                new Tech()
                {
                    Id = 1303,
                    Name = "Hyperspace Physics",
                    Description = "Initial space travel relied solely on the performance of the spacecraft's engine.  The universe is too vast and life too short to explore space with a spacecraft alone.  However, by bending space & time in the third and fourth dimensions, it was discovered that time travel was possible by jump-gating through the bent third dimension surfaces within a fourth dimension reality. Rapid progress in the field of space travel was made possible through the discovery of hyperspace physics navigation.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Basic,
                    TechLevel = 1
                },
                new Tech()
                {
                    Id = 1304,
                    Name = "High-temperature Superconductor",
                    Description = "A method to produce magnetic fields or electrical currents without using any energy was found.  This was achieved by creating a zero electrical resistance environment.  This superconductor enabled the storage of energy, as well as long distance energy transportation without energy loss.  Utilization of the superconductor made it possible for self-levitation transportation.  The superconductor went on to become more than a mere tool for zero resistance electricity, and became the predecessor of technologies utilizing Superconducting Tunneling effects similar to the Josephson Junction, such as the micro polar magnetic field sensor SQUID (Superconducting Quantum Interference Device) and high speed ultra computing technologies. Agressively occuring in Perovskite based acids, high-temperature superconductors can actually be considered advancements following the advance in Quantum Physics.  One can say that the phenomenon of Superconduction in itself is a manifistation of the macro advancements in Quantum Physics.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Basic,
                    TechLevel = 1
                },
                new Tech()
                {
                    Id = 1305,
                    Name = "Quantum Physics",
                    Description = "Scientists realized that most of the everyday scientific knowledge surrounding them had never been established on a micro level.  On a final microlevel, they realized that time and space were quantized and had discrete differentiations, and as a result, time and space on a microscopic level had particles similar to pulse waves, and existed in a miniature universe where the law of probability ruled.  Scientists named this theory Quantum Physics.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Basic,
                    TechLevel = 1
                },
                new Tech()
                {
                    Id = 1306,
                    Name = "Fusion Power",
                    Description = "The technology of fusing the atomic nuclei and deriving energy from the fusion.  A higher level of technology than Nuclear Power. Permanent stars, such as the sun, generate energy through fusion power.  This energy is many times stronger than nuclear fission, and the discovery of Fusion Power signaled the beginning of man's time in space.  As rapid progress was made in Fusion Technology, the Age of Space also stepped up its progress, and in addition, all battles grew to become conducted on planetary scales.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 2,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1301
                        }
                    }
                },
                new Tech()
                {
                    Id = 1307,
                    Name = "Fusion Power",
                    Description = "Matters exist not only as solids, liquids, and gases, but as plasma.  In its plasma state, matters exhibit characteristics not portrayed in its other states.  Understanding these characteristics and manipulating plasma for useful purposes is the role of plasma mechanics.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 2,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1302
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1304
                        }
                    }
                },
                new Tech()
                {
                    Id = 1308,
                    Name = "Ultralight Communication",
                    Description = "Scientists discovered the existence of Tachion particles, which surpassed the speed of light, thought to be the ultimate in speed.  They were successful in creating Ultralight Communication utilizing these Tachion waves.  Thus it was possible to support communication in hyperspace travel utilizing warp-drive engines.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 2,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1305
                        }
                    }
                },
                new Tech()
                {
                    Id = 1309,
                    Name = "Gravitation Physics",
                    Description = "Scientists discovered Graviton particles which cause gravity, a weak force.  Through this discovery, true understanding of gravity and gravitational force was made possible.  As research in Anti-Graviton and Anti-Gravity actively developed, this was linked to Gravity Control.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 2,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1303
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1305
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Production = 1
                    }
                },
                new Tech()
                {
                    Id = 1310,
                    Name = "Cold Fusion",
                    Description = "Fusion is possible only in extreme temperatures, such as the ones on fixed stars.  Fusion generates an enormous amount of energy, but since an extreme temperature environment must first be created, much energy is required to produce fusion.  Cold Fusion does not require extreme temperatures, and can be produced as a small-scale chemical reaction.  Because of its safe, effect and clean method of producing energy, it becomes the first method of nonpolluting unlimited energy production in history.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 3,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1306
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1307
                        }
                    }
                },
                new Tech()
                {
                    Id = 1311,
                    Name = "Advanced Metallurgy",
                    Description = "The technology of alloying untempered metals and the fabrication of metal crystallization brought astounding progress to flight and space engineering areas of technology.  Through Advanced Metallurgy, it was possible to construct stronger spaceships for space travel.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 3,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1307
                        }
                    }
                },
                new Tech()
                {
                    Id = 1312,
                    Name = "Inertia Control",
                    Description = "Sometimes, in order for a goal to be reached, a minor step must first be taken.  With the development of new engines and new energies, the speed of spaceships increased, but in order to reach a target location, one must be able to control the movements of spaceships in movement.  The development in Inertia Control technologies made it possible to freely manipulate the movement of high speed spaceships.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 3,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1309
                        }
                    }
                },
                new Tech()
                {
                    Id = 1313,
                    Name = "Molecular Manipulation",
                    Description = "Technology capable of creating an object possessing manipulation qualities on a molecular level. The development of nano-technology progressed to the final point of being able to create a structure capable of manipulation on a molecular level.  This made it possible for micro machining and single electron computing possessing ultra high density, which lead to the age of a new Industrial Revolution.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 3,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1302
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1404
                        }
                    }
                },
                new Tech()
                {
                    Id = 1314,
                    Name = "Unified Field Theory",
                    Description = "Scientists developed a Field Theory which unites all the basic forces of the universe (strong force, electro-magnetic force, weak force, gravitational force).  With this, a basic fundamental in understanding all forces and pulses was established, and this provided an important key to the understanding of the mysteries of the universe.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 3,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1204
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1309
                        }
                    }
                },
                new Tech()
                {
                    Id = 1315,
                    Name = "Robotic Manufacturing",
                    Description = "As robotic technology developed, facilities utilizing advanced robotic mechanics for the production of artificial intelligence were created.  The use of robotics enabled more detailed and accurate work to be done, as well as significantly decreasing the risk of work hazards, and manufacturing advanced to a new level.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 4,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1209
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1311
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        FacilityCost = 1
                    }
                },
                new Tech()
                {
                    Id = 1316,
                    Name = "Gravity Control",
                    Description = "The discovery of Anti-Graviton and Anti-Gravity following in the discovery of Gravitons led to the eventual development of being freed from a planet's gravitational force, and making it possible for the effortless transportation of heavy objects.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 4,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1312
                        }
                    }
                },
                new Tech()
                {
                    Id = 1317,
                    Name = "Microscale Robot",
                    Description = "The development of Robotic engineering and Molecular Manipulation led to the construction of a Microscale Robot.  These robots were utilized in many areas of industry, and made it possible for mass production of ultra precise products.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 4,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1208
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1313
                        }
                    }
                },
                new Tech()
                {
                    Id = 1318,
                    Name = "Crystal Manipulation",
                    Description = "Identical objects possess different characteristics according to their manipulations.  Crystal Manipulation is the method of artificially creating these manipulations to form new objects.  Through this technology, many new substances were developed, and production of basic substances were facilitated, bringing rapid progress to the material industry.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 4,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1311
                        }
                    }
                },
                new Tech()
                {
                    Id = 1319,
                    Name = "Atomic Manipulation",
                    Description = "Facility capable of creating objects capable of manipulation on an atomic level. A step up from Molecular Manipulation, this technology enabled manipulation at an atomic particle level, leading to a greater accuracy in electromagnetic particle proximities.  Atomic Manipulation also made it possible for electromagnetic devices utilizing quantum electrodynamics.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 4,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1310
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1314
                        }
                    }
                },
                new Tech()
                {
                    Id = 1319,
                    Name = "Core Mining",
                    Description = "Mining, which had previously involved obtaining needed minerals and metals from near the planet's surface, made rapid advancements to enable mining of staple ores such as Fe and Mg possible from the core of the planet.  Though the risks are high, this solved the problem of the lack of minerals on the planet's barren surface.  However, the tragedy of 0-sider planet, which had disappeared due to the extensive mining of its core, was a sad reminder that too much greed leads to self-destruction.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 5,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1315
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1316
                        }
                    }
                },
                new Tech()
                {
                    Id = 1321,
                    Name = "Solar Manipulation",
                    Description = "The technology controlling solar temperatures and size. The possession of vast energy resources and the advancements in Cosmic Science made it possible to utilize a planet's core temperature and size to manipulate environmental reactions to induce terraforming.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 5,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1316
                        }
                    }
                },
                new Tech()
                {
                    Id = 1322,
                    Name = "Anti-Matter Power",
                    Description = "As it became possible for the creation of anti-matter, the equal and opposing force of matter, an incredibly power source of energy was made possible.  The gigantic source of energy that is created when matter reacts with anti-matter is similar to the Big Bang, which is the beginning of the universe\'s creation.  As it became possible to control this reaction, the anti-matter engine became a reality.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 5,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1319
                        }
                    }
                },
                new Tech()
                {
                    Id = 1323,
                    Name = "Temporal Mechanics",
                    Description = "The rate of understanding about time and space developed to the extreme point of being able to control the flow of time itself within a restricted area.  But due to the enormous amount of energy required, and the side effect of time and space distortion, this technology was severely limited in its use.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 5,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1314
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1319
                        }
                    }
                },
                new Tech()
                {
                    Id = 1325,
                    Name = "Quark Manipulation",
                    Description = "Quark Manipulation is the technology of freely combining and controlling any sub-atomic particle, or quark, to desired needs.  Sub-atomic particles (quarks) have an extremely high penetration level, enabling weapons created with the quark technology to pierce armor which had been previously impenetrable with existing beam weaponary.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 6,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1322
                        }
                    }
                },
                new Tech()
                {
                    Id = 1326,
                    Name = "Singularity Physics",
                    Description = "The completion of research on the universal singularity phenomena, such as black holes and white holes, led to the active development of research of unlimited mass, unlimited or static time, and warped-space.  This research made it possible for Advanced Spaceflight, and influenced the entire area of Space-time Physics, opening a new page in modern science.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 6,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1321
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1323
                        }
                    }
                },
                new Tech()
                {
                    Id = 1327,
                    Name = "Novarization",
                    Description = "The final stage of evolution for stars with mass 10 times greater than the sun is a supernova.  After the explosion of a supernova, neutron stars, pulsars, and black holes are generated.  The universe chronicles its life cycle in this method : the birth of such a star, its evolution, supernova explosion, and the recomposition of elements thereafter, return to interstellar gases, and the reformation of a star.  The fundamental idea of a supernova is nuclear fusion, and as nuclear power technology developed, advancements were made to the point of being able to force a planet or star into its supernova state.  Like the nuclear bomb of the 20th century, the universe had just encountered a potentially deadly technological breakthrough.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 7,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1326
                        }
                    }
                },
                new Tech()
                {
                    Id = 1328,
                    Name = "Nanoscale Robot",
                    Description = "Technology capable of creating nano-scale robots and utilize it in the industry scene. The development of the field of micro technology led to the leaping progress in nano electronics and atomic manipulation.  These two fields were then utilized as components for the Nano Robot, bringing an innovative turning point to the micro manufacturing industry.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 7,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1317
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1325
                        }
                    }
                },
                new Tech()
                {
                    Id = 1329,
                    Name = "Matter Compression",
                    Description = "Matter Compression is the technology of compressing matter to the density of a neutron.  Matter compressed in this matter possesses the weight and solidity of a neutron star.  As this type of matter became available, a revolutionary change occurred in armory and weaponry technology.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 7,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1325
                        }
                    }
                },
                new Tech()
                {
                    Id = 1330,
                    Name = "Artificial Singularity",
                    Description = "The progress of Singularity Physics made it possible to artificially create and maintain objects in desired locations and in their desired states.  This made it possible to utilize warp-drive engines for jump gates at two set locations, setting a new milestone in space travel.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 8,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1327
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1329
                        }
                    }
                },
                new Tech()
                {
                    Id = 1331,
                    Name = "Probability Mechanics",
                    Description = "Einstein's theory, that there is no such thing as chance, was proved to be correct.  A pattern was discovered among randomness and possibilities of quantum mechanics, and through this discovery, it became possible to anticipate and predict future outcomes.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 8,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1219
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1326
                        }
                    }
                },
                new Tech()
                {
                    Id = 1332,
                    Name = "Energy-Matter Synthesis",
                    Description = "The abundant knowledge in the energy sciences made it possible to create matter and objects of complex structures in their desired states.  However, a massive amount of energy was required in order to change energy into matter, and this technology was restricted in its use to rapidly creating relatively small amounts of matter.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 8,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1329
                        }
                    }
                },
                new Tech()
                {
                    Id = 1333,
                    Name = "Timespace Manipulation",
                    Description = "Basic space travel depends heavily on the element of Singularity.  Timespace Manipulation, an extremely developed technology, made it possible for free warping through timespace without the existence of singularity.  Though this could be considered a finality of science, this technology required vast amounts of energy.  In addition, should space and time be bent to the point where recovery was not possible, this would also permanently affect the surrounding planets of that peculiar time and space.  This technology was thus serverly restricted in use, being utilized only on extreme long distance travel outside of star clusters.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 9,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1330
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1331
                        }
                    }
                },
                new Tech()
                {
                    Id = 1334,
                    Name = "Dimension Manipulation",
                    Description = "Dimension Manipulation is the ability to freely bend the surrounding space.  By utilizing this technology, it became possible to isolate a certain targeted area from its surroundings for a limited period of time.  With the development of dimension manipulation technology, new tools previously unimaginable were developed, and various war strategies were developed as a result.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1333
                        }
                    }
                },
                new Tech()
                {
                    Id = 1335,
                    Name = "Reverse Entropy",
                    Description = "Matter and energy do not occur spontaneously, nor do they disappear at the blink of an eye.  In contrast, entropy occurs in this sort of manufacturing activity.  As long as this manufacturing activity continues, entropy constanty occurs and accumulates.  In primitive ages, this entropy had been in balance with nature, and enabled the preservation of environmental conditions, but with the continued industrial revolutions, the environment was slowly destroyed.  This technology of reversing the effects of entropy came about as a result of re-interpreting the Chaos Theory and working in a different dimension.  Through this, nature and life found a new balance in their existence.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 11,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1334
                        }
                    }
                },
                new Tech()
                {
                    Id = 1336,
                    Name = "Permanent Engine",
                    Description = "A Permanent Engine is a formidable power-driven engine with the capability to work forever.  The Law of Conservation of Energy argues that a Permanent Engine cannot exist, as the total amount of energy does not change, no matter what occurs in the natural world.  However, by applying the theory of the Permanent Engine in the 4th, rather than the 3rd, dimension, it was possible to create an engine which was not perfectly permanent but came extremely close.  By initially storing large amounts of energy before starting the energy cycling, a new permanent engine was developed, and used in space travel and the manufacturing industry, which required large amounts of energy.",
                    Type = TechType.MatterEnergy,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 12,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1335
                        }
                    }
                },
                new Tech()
                {
                    Id = 1401,
                    Name = "Genetic Science",
                    Description = "As physics had approached the micro level through quantum physics, biological sciences began to pay closer attention to the micro level of life, genetics.  As the small universe of this world was slowly discovered, some mysteries of life were solved, and at the same time, many new mysteries presented themselves.  The most basic genetic research became the foundations for new areas of study, such as genetic engineering, genetic theraphy, and the genetic computer.  Genetics also influenced other parts of biology, and as the field of genetics developed further, so did the other fields of life sciences.",
                    Type = TechType.Life,
                    Attribute = TechAttribute.Basic,
                    TechLevel = 1
                },
                new Tech()
                {
                    Id = 1402,
                    Name = "Terraforming",
                    Description = "Unlike other lifeforms which had adapted to the environment and ultimately perished from changes in nature, species that possessed technology were able to develop the environment to suit their needs.  On planets where the population had become too vast, or necessity required the colonization of a new planet, these species used terraforming technology to change the surroundings.  With this technology, it was possible to transform even the most hostile of environments to living conditions.  Terraforming bases its foundations in planet physics, and utilizes the basic planet science skills of temperature, earth creation, gravitation force control, and decrease in spaceships to change the formation of land and living conditions, creating a suitable ecosystem.",
                    Type = TechType.Life,
                    Attribute = TechAttribute.Basic,
                    TechLevel = 1
                },
                new Tech()
                {
                    Id = 1403,
                    Name = "Molecular Biology",
                    Description = "There are many directions of molecular biology research, which aims to develop many different cases of life conditions at the molecular level.  These researches include molecular structure of living organisms, organic cell metabolism, effects on genetic development, and the genetic characteristic mechanisms possessed at the molecular level.  Rapidly developing with the progress of genetic science, these two fields greatly influenced each other, and eventually became inseparable.  Molecular biology greatly influenced the applications of genetic sciences, such as genetic engineering and genetic theraphy.  With the development of molecular biology, quantum biology also began to make progress.  With the aid of quantum biology, life forms could be understood on a quantum level, and made great contributions to the understanding of life itself.",
                    Type = TechType.Life,
                    Attribute = TechAttribute.Basic,
                    TechLevel = 1
                },
                new Tech()
                {
                    Id = 1404,
                    Name = "Genetic Engineering",
                    Description = "Following the advancements in genetic science, an application of genetics, genetic engineering, began to rapidly develop.  Utilizing the functions, applications, and control of genes discovered in genetic science research as its base, genetic manipulation technology such as the re-compunding of genes and gene cloning were used to make life science manufacturing more efficient, and this in turn led to the increase in manufacturing of breeding techniques.  Genetic engineering also led to the development of medical science, and many chronic diseases are cured using genetic engineering technology.",
                    Type = TechType.Life,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 2,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1401
                        }
                    }
                },
                new Tech()
                {
                    Id = 1405,
                    Name = "Hydrocarbon Ecology",
                    Description = "All life forms in space can be classified into two large groups: organic and inorganic.  Organic life form tissue is structured with organic matter.  Organic matter itself means that its structures are composed of hydrocarbons, and hydrocarbon ecology can be regarded as the continued lifecycle of hydrocarbons.  When an organic life form dies, another life form decomposes its physical body, and broken down, these elements are reabsorbed into the bodies of other living organisms to complete one full circle of the cycle.  Each individual life form not only possesses part of their own planet, but a bit of the history of the entire universe.",
                    Type = TechType.Life,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 2,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1402
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1403
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Environment = 1
                    }
                },
                new Tech()
                {
                    Id = 1406,
                    Name = "Bionic Chip",
                    Description = "The bionic chip combines the smallest electro circuit with an individual cell, and exists in the micrometer scale.  Attached to a particular part of the body and administered electric shock through a computer, the bionic chip can control the cellular activity of the affected area.  This bionic chip is not semi-permanent like gene injections, and can be removed after sufficient use.  The cost is also significantly lower and the application easier, and bionic chip technology begins to be widely used.",
                    Type = TechType.Life,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 2,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1401
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1403
                        }
                    }
                },
                new Tech()
                {
                    Id = 1408,
                    Name = "Genetic Therapy",
                    Description = "As atoms and molecules contain the peculiar characteristics of that matter, genes contain the characteristics of that individual.  By utilizing genetic therapy, it is possible to cure all inherited diseases and genetic flaws, as well as prevent any of the illnesses that an individual will experience in his/her lifetime.  In addition, genetic therapy can prevent the occurances of any new illnesses which may occur as space expansion continues, as well as aid in adapting life forms for space travel.",
                    Type = TechType.Life,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 3,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1404
                        }
                    }
                },
                new Tech()
                {
                    Id = 1409,
                    Name = "Secret of Brain Structure",
                    Description = "It is said that a life form will only use a fraction of its brain's abilities in his/her lifetime.  To explore the hidden powers of life forms, and for curiosity's sake, research is started to uncover the secrets of the brain.  The most important breakthrough came with the mapping of brain functions in each related brain area.  According to this map, the brain contains matter on a nano-scale relevant to self-consciousness.  In addition, the brain's 3 dimensional structure is related to psychic power.",
                    Type = TechType.Life,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 3,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1405
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1406
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Growth = 1
                    }
                },
                new Tech()
                {
                    Id = 1410,
                    Name = "Genetic Computer",
                    Description = "Each species constructed a new computer based on the operation principles of gene coding.  A basic computer utilizes a binary system of memory using 0's and 1's.  The genetic computer utilizes the various codes of genes to compress and display data, and its data handling and memory capacity are far superior to the conventional computer.  Genetic computers could work at a much faster, efficient speed to process more data in less time, and made a vast contribution to the beginning of space travel.",
                    Type = TechType.Life,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 3,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1406
                        }
                    }
                },
                new Tech()
                {
                    Id = 1411,
                    Name = "Cloning",
                    Description = "Each species had different laws about cloning, (of course, the majority did not allow cloning), but as population rapidly decreased following rapid space colonization, areas requiring population increases developed, causing the species of the universe to change their thoughts about cloning and dedicating themselves to this research.  There was much opposition from ethics and religious scholars, but these were denounced in favor of the increasing necessity for population, and cloning research is completed.  Thereafter, many individuals seek a method to live forever by injecting their thoughts and brain neural scannings into their clones.",
                    Type = TechType.Life,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 4,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1408
                        }
                    }
                },
                new Tech()
                {
                    Id = 1412,
                    Name = "Neural Scanning",
                    Description = "Having discovered at least some parts of the brain's structure and functions, species now completely understand the memory structure of the brain, and succeed at creating a data base of the brain's memory.  It was proven that memory is not a chemical reaction but rather a result of the physical connectivity of brain neurons.  With this discovery, it was possible to store memory by scanning an individual's brain and saving its structural contents.  An individual's memory was then acknowledged as being part of intellectual property with patent and trademark rights, and laws regulating the illegal scanning and sale of others' memories were strengthened.  In addition, a new culture developed as species shifted their focus from virtual reality to creating a virtual world from their own reality.",
                    Type = TechType.Life,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 4,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1409
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Spy = 1
                    }
                },
                new Tech()
                {
                    Id = 1413,
                    Name = "Artificial Gene",
                    Description = "Having uncovered the entire genetic structure, species shifted their attention to the Argema project (Artificial gene manipulation).  The goal of each species was to artificially create skills not inherent in them, and inject these genes into their own bodies.  At the beginning of the project, these artificial genes showed normal patterns in the laboratory, but violently reacted with the other genes within a physical body, bringing a long trek of trial and error to the research.  However, this provided the foundation for understanding the secret of life.  Eventually, each species devoted much time and resources to succeed at artificial gene technology.  This technology went on to become the fundamental grounding idea for artificial brain cell manufacturing, and each species soon became successful in the production of artificial brain cell production.",
                    Type = TechType.Life,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 4,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1313
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1408
                        }
                    }
                },
                new Tech()
                {
                    Id = 1414,
                    Name = "Mind Injection",
                    Description = "After succeeding in creating a database of the brain's memory structures, a research was started to explore the possibility of injecting new memories into the brain's memory structure.  Because memory is a physical structure, mind injection enables the brain structure to shift and change to a desired position.  Therefore, when a mind injection is performed, the personality and/or skills of an individual may change.  The mind injection research was kept top-secret.  It is said that the highest form of punishment of the Empire is rebirth by mind injection.",
                    Type = TechType.Life,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 5,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1313
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1408
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Spy = 1
                    }
                },
                new Tech()
                {
                    Id = 1415,
                    Name = "Artificial Neuron",
                    Description = "As the Argema project made significant progress, some scholars began to plan for a new Arnema (Artificial Neuron manipulation) project.  These scholars utilized the knowledge of brain structures and advanced cell manufacturing technology to produce artificial neurons, and succeeded in creating new neuron abilities.  Injected into the desired region of the brain, this artificial neuron made possible brain functions previously unavailable, or optimized the various functions of the brain.",
                    Type = TechType.Life,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 5,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1412
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1413
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Environment = 1
                    }
                },
                new Tech()
                {
                    Id = 1416,
                    Name = "Artificial Life",
                    Description = "Having succeeded with the cloning technology, Artificial Life was a direct challenge to the gods themselves, by attempting to create organic life from inorganic matter.  Life begins with a single organic cell, but Artificial Life begins in the glass petrie dishes of the laboratory.  Each species poured their entire resources into this project in order to create needed life forms.  In that process, researchers perfected their artificial evolution technology, and ultimately succeeded in creating artificial life.  In the age of space expansion, many of the needed organisms were created in the labs.",
                    Type = TechType.Life,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 5,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1411
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1413
                        }
                    }
                },
                new Tech()
                {
                    Id = 1417,
                    Name = "Mind-Machine Interface",
                    Description = "As the brain's structure and construction were revealed, and the database creation of brain structures were possible, it was finally possible to synchronize organic life forms with computers.  After injecting artificial neurons into the brain, researchers needed only to connect the brain with a genetic computer in order to share thoughts between the life form and the computer.  By connecting to the computer, one could experience cyberspace with his/her five senses through the sensors connected on the body, and prevously difficult tasks could be performed with the utmost ease.",
                    Type = TechType.Life,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 6,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1213
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1414
                        }
                    }
                },
                new Tech()
                {
                    Id = 1418,
                    Name = "Artificial Evolution",
                    Description = "As streams come together to form a mighty river, accumulated knowledge in genetic fabrication and manufacturing, as well as the success of creating organic life forms from inorganic matter made it possible for Artificial Evolution technology.  It was now possible to change the physical body structure of a life form to his/her will, and each species began to evolve their bodies to desired physical forms.  This technology makes it possible for swift adaptation to any atmosphere in the universe.",
                    Type = TechType.Life,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 6,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1416
                        }
                    }
                },
                new Tech()
                {
                    Id = 1419,
                    Name = "Psionic Power",
                    Description = "The distinction between life forms and machines became indistinguishable, and the functions of life forms were stored in databases.  As machines began to possess genetic structures, scientists began to slowly discover the existence of the psyche, which had always remained a scientific mystery.  The psyche, made of the wave fluctuations of the alpha and beta brain waves, was a multi-dimensional power.  Though research has not made significant progress, this signified a change in the thoughts of species, to once again intrude on the realm of the gods.",
                    Type = TechType.Life,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 7,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1116
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1417
                        }
                    }
                },
                new Tech()
                {
                    Id = 1420,
                    Name = "Telepathic Training",
                    Description = "Telepathy requires no other tools than the mind for its communications.  Telepathic level and ability differs from species to species.  There are some species who are born with an innate gift for telepathy, while others must devote their lifetimes to arduous training in order to gain telepathic abilities.  Telepathy can be considered to be the most superior method of communication, and requires no resources, making it an extremely economical means of relaying thought.  Because telepathy is not affected by time differences, it is a very useful skill, and many species train their people in this skill.",
                    Type = TechType.Life,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 7,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1417
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1418
                        }
                    }
                },
                new Tech()
                {
                    Id = 1421,
                    Name = "Psionic : Life",
                    Description = "This skill utilizes power to change a life form according to will.  From mere perception of a life form to the creation of a new life, there are various degrees of this skill.  Most importantly, this skill affects any form of life, and can be utilized in terraforming technology or against previously unknown life forms.",
                    Type = TechType.Life,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 8,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1419
                        }
                    }
                },
                new Tech()
                {
                    Id = 1422,
                    Name = "Psionic : Energy",
                    Description = "Reaction of matter always leads to entropy, and entropy is always a (-) sign.  Psionic : Energy is the mental capability to manipulate and change this amount of entropy.  When entropy levels are increased or decreased according to will, a desired energy effect occurs.",
                    Type = TechType.Life,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 8,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1419
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1420
                        }
                    }
                },
                new Tech()
                {
                    Id = 1423,
                    Name = "Psionic : Space",
                    Description = "A three dimensional ability, the easiest of this skill ranges from the most simple perception of space to the manipulating surroundings to desired forms.  The highest form of this skill is creating an entirely new space.  Because one possesses ultimate power inside the space that he/she creates, one can utilize all things within that space to produce beneficial results.  However, this skill is useless against life forms that possess higher psychic powers.",
                    Type = TechType.Life,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 9,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1422
                        }
                    }
                },
                new Tech()
                {
                    Id = 1424,
                    Name = "Psionic : Matter",
                    Description = "Psionic : Matter is the ability to change matter into physical or chemical reactions, or create an entirely new matter.   Because this ability has much in common with mental psychic powers, it is not rare to see an individual who possesses both abilities.  Levels of ability range from creating a new atom to simply affecting the characteristics of an existing matter.  High level individuals have the capability of moving elementary particles at the speed of light by utilizing energy.",
                    Type = TechType.Life,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 9,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1332
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1422
                        }
                    }
                },
                new Tech()
                {
                    Id = 1425,
                    Name = "Secret of Life",
                    Description = "As science developed, there were many opinions that a life form is like a machinery with many parts, and that each part of an organism is like the nuts and bolts of machinery parts.  But with the development of artificial gene injection came the discovery of unique new elements which make up life.  In the case of life forms, 1+1 does not equal 2, but rather, + .  This force, stemming from the three dimensional structure of genes, is deduced to be more than a power existing in the three dimensional realm, and is thought to be the base for psychic abilities in the fourth dimension.  That is, secrets lay not only in the genetic structure of genes, but in the space itself that surrounds them.  Though many questions were answered with this discovery, even more new questions arose.",
                    Type = TechType.Life,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 9,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1128
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1420
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Research = 1
                    }
                },
                new Tech()
                {
                    Id = 1426,
                    Name = "Secret of Life",
                    Description = "Generally speaking, Ascension is the state of enlightenment in which an individual discovers the truth of the universe, and ascends to a level of understanding beyond normal comprehension.  By realizing the fundamental universal truth, one becomes one step closer to the gods, and becomes capable of understanding the existence of all things.  Through this realization, one completely understands what absolute truth is, and this state can be considered ascension.  This state cannot be explained with mere words, and can only be understood by those who have reached the ascension level.  Each species had their own views about the enlightenments for Ascension, and have their according good or evil desires about Ascension, living their whole lives for this purpose.",
                    Type = TechType.Life,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1425
                        }
                    }
                },
                new Tech()
                {
                    Id = 1427,
                    Name = "True Enlightenment",
                    Description = "True Enlightenment- the final stage of ascension and achieved by few. Many have gone through trials and tribulations, devoting life to prayer, meditation, non-violence and life to charity. True Enlightenment is achieved by dropping all of ones burdens, finding the zone between peace and strife, leaving the cares of mortal life behind and focusing on self-reflection.",
                    Type = TechType.Life,
                    Attribute = TechAttribute.Normal,
                    TechLevel = 11,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1426
                        }
                    }
                }
            };
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
