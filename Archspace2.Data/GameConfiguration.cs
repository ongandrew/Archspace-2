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
            UseDefaultShields();
            UseDefaultShipClasses();
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
                            ModifierType = FleetEffectModifierType.Proportional,
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
                            ModifierType = FleetEffectModifierType.Proportional,
                            Amount = 30
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ProjectileDefense,
                            ModifierType = FleetEffectModifierType.Proportional,
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
                            ModifierType = FleetEffectModifierType.Proportional,
                            Amount = 20
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.RepairSpeed,
                            ModifierType = FleetEffectModifierType.Proportional,
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
                            ModifierType = FleetEffectModifierType.Proportional,
                            Amount = 20
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.BeamDefense,
                            ModifierType = FleetEffectModifierType.Proportional,
                            Amount = 30
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.PsiDefense,
                            ModifierType = FleetEffectModifierType.Proportional,
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
                            ModifierType = FleetEffectModifierType.Proportional,
                            Amount = 10
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ProjectileDefense,
                            ModifierType = FleetEffectModifierType.Proportional,
                            Amount = 20
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.MissileDefense,
                            ModifierType = FleetEffectModifierType.Proportional,
                            Amount = -5
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Repair,
                            ModifierType = FleetEffectModifierType.Proportional,
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
                            ModifierType = FleetEffectModifierType.Proportional,
                            Amount = 10
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.MissileDefense,
                            ModifierType = FleetEffectModifierType.Proportional,
                            Amount = 20
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ProjectileDefense,
                            ModifierType = FleetEffectModifierType.Proportional,
                            Amount = 20
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Repair,
                            ModifierType = FleetEffectModifierType.Proportional,
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
                            ModifierType = FleetEffectModifierType.Proportional,
                            Amount = -20
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.MissileDefense,
                            ModifierType = FleetEffectModifierType.Proportional,
                            Amount = -10
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ProjectileDefense,
                            ModifierType = FleetEffectModifierType.Proportional,
                            Amount = -10
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Repair,
                            ModifierType = FleetEffectModifierType.Proportional,
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
                            ModifierType = FleetEffectModifierType.Proportional,
                            Amount = -20
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ProjectileDefense,
                            ModifierType = FleetEffectModifierType.Proportional,
                            Amount = -20
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Repair,
                            ModifierType = FleetEffectModifierType.Proportional,
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
                            ModifierType = FleetEffectModifierType.Proportional,
                            Amount = 40
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Repair,
                            ModifierType = FleetEffectModifierType.Proportional,
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
                            ModifierType = FleetEffectModifierType.Proportional,
                            Amount = 35
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.BeamDefense,
                            ModifierType = FleetEffectModifierType.Proportional,
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
                            ModifierType = FleetEffectModifierType.Proportional,
                            Amount = 55
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ProjectileDefense,
                            ModifierType = FleetEffectModifierType.Proportional,
                            Amount = 5,
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.BeamDefense,
                            ModifierType = FleetEffectModifierType.Proportional,
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
                            ModifierType = FleetEffectModifierType.Proportional,
                            Amount = 70
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ProjectileDefense,
                            ModifierType = FleetEffectModifierType.Proportional,
                            Amount = 10,
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.BeamDefense,
                            ModifierType = FleetEffectModifierType.Proportional,
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

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
