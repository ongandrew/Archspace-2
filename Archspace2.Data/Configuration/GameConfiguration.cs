using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Archspace2
{
    public enum Language
    {
        English,
        Korean
    };

    public class GameConfiguration : IValidatable
    {
        public GameConfiguration()
        {
            Action = new ActionSettings();
            BlackMarket = new BlackMarketSettings();
            Mission = new MissionSettings();
            Planet = new PlanetSettings();
            Player = new PlayerSettings();
            Universe = new UniverseSettings();

            Armors = new List<Armor>();
            Computers = new List<Computer>();
            Devices = new List<Device>();
            Engines = new List<Engine>();
            Events = new List<Event>();
            PlanetAttributes = new List<PlanetAttribute>();
            Projects = new List<Project>();
            Races = new List<Race>();
            Shields = new List<Shield>();
            ShipClasses = new List<ShipClass>();
            SpyActions = new List<SpyAction>();
            Techs = new List<Tech>();
            Weapons = new List<Weapon>();
        }
        
        [JsonProperty("Language")]
        public Language Language { get; set; }
        [JsonProperty("SecondsPerTurn")]
        public int SecondsPerTurn { get; set; }
        [JsonProperty("MaxUsers")]
        public int MaxUsers { get; set; }
        [JsonProperty("TechRateModifier")]
        public double TechRateModifier { get; set; }

        [JsonProperty("Action")]
        public ActionSettings Action { get; set; }
        [JsonProperty("Admiral")]
        public AdmiralSettings Admiral { get; set; }
        [JsonProperty("Battle")]
        public BattleSettings Battle { get; set; }
        [JsonProperty("BlackMarket")]
        public BlackMarketSettings BlackMarket { get; set; }
        [JsonProperty("Mission")]
        public MissionSettings Mission { get; set; }
        [JsonProperty("Planet")]
        public PlanetSettings Planet { get; set; }
        [JsonProperty("Player")]
        public PlayerSettings Player { get; set; }
        [JsonProperty("Universe")]
        public UniverseSettings Universe { get; set; }

        [JsonProperty("Armors")]
        public List<Armor> Armors { get; set; }
        [JsonProperty("Computers")]
        public List<Computer> Computers { get; set; }
        [JsonProperty("Devices")]
        public List<Device> Devices { get; set; }
        [JsonProperty("Engines")]
        public List<Engine> Engines { get; set; }
        [JsonProperty("Events")]
        public List<Event> Events { get; set; }
        [JsonProperty("PlanetAttributes")]
        public List<PlanetAttribute> PlanetAttributes { get; set; }
        [JsonProperty("Projects")]
        public List<Project> Projects { get; set; }
        [JsonProperty("Races")]
        public List<Race> Races { get; set; }
        [JsonProperty("Shields")]
        public List<Shield> Shields { get; set; }
        [JsonProperty("ShipClasses")]
        public List<ShipClass> ShipClasses { get; set; }
        [JsonProperty("SpyActions")]
        public List<SpyAction> SpyActions { get; set; }
        [JsonProperty("Techs")]
        public List<Tech> Techs { get; set; }
        [JsonProperty("Weapons")]
        public List<Weapon> Weapons { get; set; }

        public static GameConfiguration CreateDefault()
        {
            GameConfiguration result = new GameConfiguration();
            result.UseDefaults();

            return result;
        }

        private void UseDefaults()
        {
            UseDefaultSettings();

            UseDefaultArmors();
            UseDefaultComputers();
            UseDefaultDevices();
            UseDefaultEngines();
            UseDefaultEvents();
            UseDefaultRaces();
            UseDefaultPlanetAttributes();
            UseDefaultProjects();
            UseDefaultShields();
            UseDefaultShipClasses();
            UseDefaultSpyActions();
            UseDefaultTechs();
            UseDefaultWeapons();
        }

        private void UseDefaultSettings()
        {
            Language = Language.English;
            SecondsPerTurn = 300;
            MaxUsers = 10000;
            TechRateModifier = 1;
            
            Action = ActionSettings.CreateDefault();
            Admiral = AdmiralSettings.CreateDefault();
            Battle = BattleSettings.CreateDefault();
            BlackMarket = BlackMarketSettings.CreateDefault();
            Mission = MissionSettings.CreateDefault();
            Planet = PlanetSettings.CreateDefault();
            Player = PlayerSettings.CreateDefault();
            Universe = UniverseSettings.CreateDefault();
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
                            Type = FleetEffectType.PsiDamageOverTime,
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
                    TechLevel = 5,
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
        private void UseDefaultEvents()
        {
            Events = new List<Event>()
            {
                new Event()
                {
                    Id = 1200,
                    Type = EventType.Major,
                    Name = "Scientific Mishap",
                    Description = "One of your scientists was certain that they had found a way to replicate matter and increase the resources of your planets.  Unfortunately, they were wrong and the experiment had a major impact on the planet they were testing it on.",
                    Duration = 36,
                    MinHonor = 0,
                    MaxHonor = 40,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangePlanetResource,
                            Argument1 = -2,
                            IsInstant = true
                        }
                    }
                },
                new Event()
                {
                    Id = 1202,
                    Type = EventType.Major,
                    Name = "Attempted Military Coup",
                    Description = "The on planet military on one of your planets attempts to take the planet from you.  You are able to put the revolt down yourself, but you must divert critical resources to the effort.",
                    Duration = 6,
                    MinHonor = 0,
                    MaxHonor = 30,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.SetFleetMission,
                            Argument1 = 8,
                            Argument2 = 7200,
                            Duration = 6
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.SetFleetMission,
                            Argument1 = 8,
                            Argument2 = 7200,
                            Duration = 6
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.SetFleetMission,
                            Argument1 = 8,
                            Argument2 = 7200,
                            Duration = 6
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.SetFleetMission,
                            Argument1 = 8,
                            Argument2 = 7200,
                            Duration = 6
                        }
                    }
                },
                new Event()
                {
                    Id = 1203,
                    Type = EventType.Major,
                    Name = "Succession",
                    Description = "One of your planets has rebelled and succeeded from your empire!",
                    Duration = 36,
                    MinHonor = 0,
                    MaxHonor = 40,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.LosePlanet,
                            IsInstant = true
                        }
                    }
                },
                new Event()
                {
                    Id = 1204,
                    Type = EventType.Major,
                    Name = "Dockside Sabotage",
                    Description = "Somehow, your ship storage docks have been sabotaged.  None of the ships in your pool survived the explosion.",
                    Duration = 36,
                    MinHonor = 0,
                    MaxHonor = 20,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.DestroyAllDockedShip,
                            IsInstant = true
                        }
                    }
                },
                new Event()
                {
                    Id = 1206,
                    Type = EventType.Major,
                    Name = "Racial Amnesia",
                    Description = "When those cosmic radiation storms came through, you didn't think it caused any damage.  Unfortunately, that is because you forgot.  In fact, you forgot quite a bit.",
                    Duration = 36,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.LoseTech,
                            IsInstant = true
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.LoseProject,
                            IsInstant = true
                        }
                    }
                },
                new Event()
                {
                    Id = 1209,
                    Type = EventType.Racial,
                    RaceListType = ListType.Inclusion,
                    RaceList = new List<RaceType>() { RaceType.Human }.Cast<int>().ToList(),
                    Name = "Governmental Collapse",
                    Description = "The current form of government has been removed through a coup.  For a while, things are rough, but after people adapt, things seem better.  For a while at least.",
                    Duration = 288,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 144,
                            ControlModelModifier = new ControlModel()
                            {
                                                                                                    FacilityCost = -4,
                                Research = -4,
                                Production = -4
                            }
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 288,
                            ControlModelModifier = new ControlModel()
                            {
                                FacilityCost = 2,
                                Research = 2,
                                Production = 2
                            }
                        }
                    }
                },
                new Event()
                {
                    Id = 1210,
                    Type = EventType.Racial,
                    RaceList = new List<RaceType>() { RaceType.Targoid }.Cast<int>().ToList(),
                    RaceListType = ListType.Inclusion,
                    Name = "Colony Lost/Refound",
                    Description = "One of your research colonies was given too much freedom by the parent mind, and all contact was lost.  This colony was on its own for a while.  When it was regained, the parent mind decided to incorporate all its new knowledge and resources.",
                    Duration = 288,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeProduction,
                            ModifierType = ModifierType.Absolute,
                            Argument1 = 50000000,
                            IsInstant = true
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 288,
                            ControlModelModifier = new ControlModel()
                            {
                                Research = 2
                            }
                        }
                    }
                },
                new Event()
                {
                    Id = 1211,
                    Type = EventType.Racial,
                    RaceList = new List<RaceType>() { RaceType.Xeloss }.Cast<int>().ToList(),
                    RaceListType = ListType.Inclusion,
                    Name = "Mystic Vision",
                    Description = "One of your High politicians has been given a vision.  This vision inspires your people and they work with greater ferocity for a while.",
                    Duration = 288,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeAllCommanderAbility,
                            Target = 0,
                            Argument1 = 2,
                            Duration = 288
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeAllCommanderAbility,
                            Target = 1,
                            Argument1 = 2,
                            Duration = 288
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeAllCommanderAbility,
                            Target = 2,
                            Argument1 = 2,
                            Duration = 288
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeAllCommanderAbility,
                            Target = 3,
                            Argument1 = 2,
                            Duration = 288
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 288,
                            ControlModelModifier = new ControlModel()
                            {
                                Production = 2
                            }
                        }
                    }
                },
                new Event()
                {
                    Id = 1212,
                    Type = EventType.Racial,
                    RaceList = new List<RaceType>() { RaceType.Xesperados }.Cast<int>().ToList(),
                    RaceListType = ListType.Inclusion,
                    Name = "New Race Joins Up",
                    Description = "The remains of another race which survived the M-13 collapse join you.  They bring a new technology with them and new ways of thinking.",
                    Duration = 288,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.GainTech,
                            IsInstant = true
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 288,
                            ControlModelModifier = new ControlModel()
                            {
                                Research = 2
                            }
                        }
                    }
                },
                new Event()
                {
                    Id = 1213,
                    Type = EventType.Racial,
                    RaceList = new List<RaceType>() { RaceType.Agerus }.Cast<int>().ToList(),
                    RaceListType = ListType.Inclusion,
                    Name = "New Race Joins Up",
                    Description = "In your wanderings you encounter a rogue spawn of young Agerus. You bring them under your control and add them to your fleet.",
                    Duration = 288,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.GainCommander,
                            Argument1 = 12,
                            IsInstant = true
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.GainCommander,
                            Argument1 = 12,
                            IsInstant = true
                        }
                    }
                },
                new Event()
                {
                    Id = 1214,
                    Type = EventType.Racial,
                    RaceList = new List<RaceType>() { RaceType.Tecanoid }.Cast<int>().ToList(),
                    RaceListType = ListType.Inclusion,
                    Name = "Successful Evintos Infiltration",
                    Description = "Some of your spies have been able to infiltrate the Evintos in hopes of gaining even more knowledge of how to evolve to a pure machine existence.",
                    Duration = 288,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 288,
                            ControlModelModifier = new ControlModel()
                            {
                                Production = 3,
                                Efficiency = 3
                            }
                        }
                    }
                },
                new Event()
                {
                    Id = 1215,
                    Type = EventType.Racial,
                    RaceList = new List<RaceType>() { RaceType.Buckaneer }.Cast<int>().ToList(),
                    RaceListType = ListType.Inclusion,
                    Name = "Ransom a Famous Pirate",
                    Description = "You are lucky enough to be in the right place at the right time.  You are able to pay the ransom for a famous pirate who agrees to work for you in repayment.",
                    Duration = 36,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeProduction,
                            ModifierType = ModifierType.Absolute,
                            Argument1 = -200000,
                            IsInstant = true
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 288,
                            ControlModelModifier = new ControlModel()
                            {
                                Commerce = 3
                            }
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.GainCommander,
                            Argument1 = 20,
                            IsInstant = true
                        }
                    }
                },
                new Event()
                {
                    Id = 1216,
                    Type = EventType.Racial,
                    RaceList = new List<RaceType>() { RaceType.Bosalian }.Cast<int>().ToList(),
                    RaceListType = ListType.Inclusion,
                    Name = "Sabotage Discovered",
                    Description = "Your scientists after hundreds of years of study are certain they have proof of an important Truth.  They believe they have proven that the reason your people did not Ascend was that there was sabotage from another race.",
                    Duration = 288,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeAllCommanderAbility,
                            Target = 0,
                            Argument1 = 5,
                            Duration = 288
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeAllCommanderAbility,
                            Target = 1,
                            Argument1 = 5,
                            Duration = 288
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeAllCommanderAbility,
                            Target = 2,
                            Argument1 = 5,
                            Duration = 288
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeAllCommanderAbility,
                            Target = 3,
                            Argument1 = 5,
                            Duration = 288
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 288,
                            ControlModelModifier = new ControlModel()
                            {
                                Military = 4
                            }
                        }
                    }
                },
                new Event()
                {
                    Id = 1217,
                    Type = EventType.Racial,
                    RaceList = new List<RaceType>() { RaceType.Evintos }.Cast<int>().ToList(),
                    RaceListType = ListType.Inclusion,
                    Name = "Past Memory Core Found",
                    Description = "An old memory core from a lost colony is found.  This core has the knowledge of ages past.",
                    Duration = 288,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.GainTech,
                            IsInstant = true
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 144,
                            ControlModelModifier = new ControlModel()
                            {
                                Research = 1
                            }
                        }
                    }
                },
                new Event()
                {
                    Id = 1218,
                    Type = EventType.Racial,
                    RaceList = new List<RaceType>() { RaceType.Xerusian }.Cast<int>().ToList(),
                    RaceListType = ListType.Inclusion,
                    Name = "Military Genius Trained",
                    Description = "Your military college has produced one of those genius tacticians that are only born once a millennium.  This person becomes the best professor your academies have ever seen.",
                    Duration = 288,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 288,
                            ControlModelModifier = new ControlModel()
                            {
                                Genius = 7
                            }
                        }
                    }
                },
                new Event()
                {
                    Id = 1221,
                    Type = EventType.System,
                    Name = "Imperial Investigator",
                    Description = "The Empire has sent an investigator to determine if the Magistrate has been giving accurate reports.  He reports to the Empire on your loyalty and the Empire decides that the magistrate has been reporting accurately.",
                    Duration = 36
                },
                new Event()
                {
                    Id = 1222,
                    Type = EventType.System,
                    Name = "Imperial Investigator",
                    Description = "The Empire has sent an investigator to determine if the Magistrate has been giving accurate reports.  He reports to the Empire on your loyalty and the Empire decides that the magistrate has been too humble on your behalf.",
                    Duration = 36,
                    MinHonor = 40,
                    MaxHonor = 100,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeProduction,
                            ModifierType = ModifierType.Absolute,
                            Argument1 = 1000000,
                            IsInstant = true
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeEmpireRelation,
                            Argument1 = 5,
                            IsInstant = true
                        }
                    }
                },
                new Event()
                {
                    Id = 1223,
                    Type = EventType.System,
                    Name = "Imperial Investigator",
                    Description = "The Empire has sent an investigator to determine if the Magistrate has been giving accurate reports.  He reports to the Empire on your loyalty and the Empire decides that the magistrate has been too kind for one as evil as you.",
                    Duration = 36,
                    MinHonor = 0,
                    MaxHonor = 40,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeEmpireRelation,
                            Argument1 = -30,
                            IsInstant = true
                        }
                    }
                },
                new Event()
                {
                    Id = 1224,
                    Type = EventType.System,
                    Name = "Heroic Rescue",
                    Description = "One of your commanders succeeds in rescuing a kidnapped child of your magistrate.",
                    Duration = 36,
                    MinHonor = 50,
                    MaxHonor = 100,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeEmpireRelation,
                            Argument1 = 30,
                            IsInstant = true
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.CommanderLevelUp,
                            Argument1 = 1,
                            IsInstant = true
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeHonor,
                            Argument1 = 5,
                            IsInstant = true
                        }
                    }
                },
                new Event()
                {
                    Id = 1225,
                    Type = EventType.System,
                    Name = "Imperial Gift",
                    Description = "The Emperor, or the Magistrate in his place, has deemed you worth of a gift.",
                    Duration = 36,
                    MinHonor = 40,
                    MaxHonor = 100,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.GrantBoon,
                            IsInstant = true
                        }
                    }
                },
                new Event()
                {
                    Id = 1226,
                    Type = EventType.System,
                    Name = "Imperial Levies",
                    Description = "The Emperor has decided to improve the fleets of the Magistrate.  To do this, he is calling for all systems to send warriors.  From your system he demands commanders.",
                    Duration = 36,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.LoseCommander,
                            ModifierType = ModifierType.Proportional,
                            Argument1 = 25,
                            IsInstant = true
                        }
                    }
                },
                new Event()
                {
                    Id = 1227,
                    Type = EventType.System,
                    Name = "Population Explosion",
                    Description = "One of your planets has had a massive population surge.",
                    Duration = 96,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangePlanetControlModel,
                            Duration = 96,
                            ControlModelModifier = new ControlModel()
                            {
                                Growth = 5
                            }
                        }
                    }
                },
                new Event()
                {
                    Id = 1228,
                    Type = EventType.System,
                    Name = "Workers Stay Overtime",
                    Description = "Your workers have been inspired by something... no-one is sure what... but they have been working extra lately.",
                    Duration = 96,
                    MinHonor = 50,
                    MaxHonor = 100,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangePlanetControlModel,
                            Duration = 96,
                            ControlModelModifier = new ControlModel()
                            {
                                Production = 5
                            }
                        }
                    }
                },
                new Event()
                {
                    Id = 1229,
                    Type = EventType.System,
                    Name = "New Resources Discovered",
                    Description = "Just as you thought that you had exhausted the remains of the main resource of the planet, your scientists discover a large amount of another useful resource.",
                    Duration = 36,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangePlanetResource,
                            Argument1 = 1,
                            Duration = 0,
                            IsInstant = true
                        }
                    }
                },
                new Event()
                {
                    Id = 1230,
                    Type = EventType.System,
                    RaceList = new List<RaceType>() { RaceType.Agerus }.Cast<int>().ToList(),
                    RaceListType = ListType.Exclusion,
                    Name = "Rampant Epidemic",
                    Description = "One of your planets is struck by a meteor which contains the DNA coding for a horrible virus.  This greatly harms your population.",
                    Duration = 96,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangePlanetPopulation,
                            ModifierType = ModifierType.Proportional,
                            Argument1 = -33,
                            IsInstant = true
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangePlanetControlModel,
                            Duration = 96,
                            ControlModelModifier = new ControlModel()
                            {
                                Growth = -7
                            }
                        }
                    }
                },
                new Event()
                {
                    Id = 1231,
                    Type = EventType.System,
                    RaceList = new List<RaceType>() { RaceType.Agerus }.Cast<int>().ToList(),
                    RaceListType = ListType.Exclusion,
                    Name = "Natural Tectonic Disaster",
                    Description = "One of your major cities experiences major tectonic shifting resulting in earthquakes and minor volcanic eruptions.  Your scientists are able to warn your population in time, but there is major damage to your buildings.",
                    Duration = 96,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.PlanetLostBuilding,
                            ModifierType = ModifierType.Proportional,
                            Argument1 = -20,
                            Argument2 = -1,
                            IsInstant = true
                        }
                    }
                },
                new Event()
                {
                    Id = 1232,
                    Type = EventType.System,
                    RaceList = new List<RaceType>() { RaceType.Agerus }.Cast<int>().ToList(),
                    RaceListType = ListType.Exclusion,
                    Name = "Freakish Weather",
                    Description = "One of your planets suffers major storms such as tornadoes and hurricanes.  This wrecks some buildings, kills some people, and reduces production.",
                    Duration = 3,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangePlanetPopulation,
                            ModifierType = ModifierType.Proportional,
                            Argument1 = -2,
                            Duration = 3
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangePlanetControlModel,
                            Duration = 3,
                            ControlModelModifier = new ControlModel()
                            {
                                Production = -2
                            }
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.PlanetLostBuilding,
                            ModifierType = ModifierType.Proportional,
                            Argument1 = -2,
                            Argument2 = -1,
                            Duration = 3
                        }
                    }
                },
                new Event()
                {
                    Id = 1233,
                    Type = EventType.System,
                    RaceList = new List<RaceType>() { RaceType.Agerus }.Cast<int>().ToList(),
                    RaceListType = ListType.Exclusion,
                    Name = "Radical Terrorists",
                    Description = "These terrorists have decided that altering the environment is evil, even if it is necessary to live.  To prove they are dedicated, they blow up your gravity control center.",
                    Duration = 36,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.LosePlanetGravityControl,
                            IsInstant = true
                        }
                    }
                },
                new Event()
                {
                    Id = 1234,
                    Type = EventType.System,
                    Name = "Agerus Spawning Frenzy",
                    Description = "Your system is filled with millions of tiny Agerus spawn.  They are too small to recognize as Agerus, but are in search of a good asteroid belt to colonize.  They clog all ship traffic and their hard bodies constantly strain ship shields.",
                    Duration = 96,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeShipAbilityOnPlanet,
                            ModifierType = ModifierType.Proportional,
                            Argument1 = 15,
                            Argument2 = -50,
                            Duration = 96
                        }
                    }
                },
                new Event()
                {
                    Id = 1235,
                    Type = EventType.System,
                    Name = "Interstellar Dust Cloud",
                    Description = "Your system is filled with a fine particulate cloud which moves seemingly of its own will. This cloud has the effect of dispersing beams fired in it.",
                    Duration = 96,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeShipAbilityOnPlanet,
                            ModifierType = ModifierType.Proportional,
                            Argument1 = 19,
                            Argument2 = -50,
                            Duration = 96
                        }
                    }
                },
                new Event()
                {
                    Id = 1236,
                    Type = EventType.System,
                    Name = "Discrimination Demonstrations",
                    Description = "A segment of you population demands equal treatment.  They complain for weeks and finally you give in to their demands.  You will let them work, just like all your other subjects. After a while, they decide they don't like work much after all and quit.",
                    Duration = 120,
                    MinHonor = 0,
                    MaxHonor = 50,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 144,
                            ControlModelModifier = new ControlModel()
                            {
                                Production = -3
                            }
                        }
                    }
                },
                new Event()
                {
                    Id = 1237,
                    Type = EventType.System,
                    Name = "Temporary Wormhole",
                    Description = "You have found a temporary wormhole.  With you current technology, you can even make it open where you want.  This helps reduce the time it takes for your attacking fleets to travel.",
                    Duration = 144,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeFleetReturnTime,
                            Argument1 = -50,
                            Duration = 144
                        }
                    }
                },
                new Event()
                {
                    Id = 1238,
                    Type = EventType.System,
                    Name = "Silly Old Game",
                    Description = "Your people discover an old game from the 'web' days called 'Ultra-Wizard'.  This seems to help morale a lot, but a while later it seems to be affecting your commanders as they all seem to think that a new strategy called 'stacking' would definitely help win battles.",
                    Duration = 144,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 144,
                            ControlModelModifier = new ControlModel()
                            {
                                Production = 3
                            }
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeAllCommanderAbility,
                            Target = 9,
                            Argument1 = -3,
                            Duration = 144
                        }
                    }
                },
                new Event()
                {
                    Id = 1239,
                    Type = EventType.System,
                    Name = "Insane Mental Giant",
                    Description = "Somewhere in your system, someone or something with enormous psychic powers has gone insane.  It has been broadcasting non-stop on all psychic wavelengths.  All you know is that it is reducing the effects of all psychic attacks in your system, and he keeps repeating something about a being known as 'Dora'.",
                    Duration = 96,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeShipAbilityOnPlanet,
                            ModifierType = ModifierType.Proportional,
                            Argument1 = 32,
                            Argument2 = 50,
                            Duration = 96
                        }
                    }
                },
                new Event()
                {
                    Id = 1240,
                    Type = EventType.System,
                    Name = "Carbon Cloud",
                    Description = "A large dark cloud rolls into your system one day.  Any ships, which travel through the cloud, build up a large deposit of carbon on their hulls.  This greatly slows them down, but ballistic weapons have less effect as this coating absorbs their force.",
                    Duration = 96,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeShipAbilityOnPlanet,
                            ModifierType = ModifierType.Proportional,
                            Argument1 = 30,
                            Argument2 = 20,
                            Duration = 96
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeShipAbilityOnPlanet,
                            ModifierType = ModifierType.Proportional,
                            Argument1 = 5,
                            Argument2 = -20,
                            Duration = 96
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeShipAbilityOnPlanet,
                            ModifierType = ModifierType.Proportional,
                            Argument1 = 6,
                            Argument2 = -20,
                            Duration = 96
                        }
                    }
                },
                new Event()
                {
                    Id = 1241,
                    Type = EventType.System,
                    Name = "Solar Blackout",
                    Description = "The sun just seemed to disappear one day.  When your scientists went to study why, they found that there were billions of small creatures hurling themselves into the star.  The scientists assume the light from the star will be allowed through in a couple of days.  During the meantime, your planets can survive on their reserve energy.  In fact, the lack of solar rays is allowing some excellent viewings of the galaxy because of a lack of solar interference.",
                    Duration = 24,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangePlanetControlModel,
                            Duration = 24,
                            ControlModelModifier = new ControlModel()
                            {
                                Production = -1
                            }
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeShipAbilityOnPlanet,
                            ModifierType = ModifierType.Proportional,
                            Argument1 = 20,
                            Argument2 = 20,
                            Duration = 24
                        }
                    }
                },
                new Event()
                {
                    Id = 1242,
                    Type = EventType.System,
                    Name = "Industrial Nanite Failure",
                    Description = "These nanites were originally produced in order to help reduce waste.  There was an error in their programming somewhere.  They have been weakening the end product of many of your critical processes.  This is adding major excess waste.",
                    Duration = 72,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 72,
                            ControlModelModifier = new ControlModel()
                            {
                                Efficiency = -2
                            }
                        }
                    }
                },
                new Event()
                {
                    Id = 1243,
                    Type = EventType.System,
                    Name = "Experiment Gone Wrong",
                    Description = "Your scientists were studying how your brain works and they made a horrible error.  They accidentally irradiated their own brains too much.  Your research capacity drops alarmingly until you can train some new scientists.",
                    Duration = 72,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 72,
                            ControlModelModifier = new ControlModel()
                            {
                                Research = -5
                            }
                        }
                    }
                },
                new Event()
                {
                    Id = 1244,
                    Type = EventType.System,
                    Name = "Angry Entertainment",
                    Description = "The young on your planets have started enjoying a new violent form of entertainment.  There are many accidents and many of them seem to be ignoring education.",
                    Duration = 144,
                    MinHonor = 0,
                    MaxHonor = 60,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 144,
                            ControlModelModifier = new ControlModel()
                            {
                                Growth = -3,
                                Research = -3,
                                Military = 1
                            }
                        }
                    }
                },
                new Event()
                {
                    Id = 1245,
                    Type = EventType.System,
                    Name = "Religious Revolution",
                    Description = "The religion of your systems has been greatly altered by recent events.  The people are in a very good mood for a while and willing to ignore problems with the planet around them.  However, this is because they are now blaming their troubles on those in other systems.",
                    Duration = 96,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 96,
                            ControlModelModifier = new ControlModel()
                            {
                                Environment = 3,
                                Diplomacy = -3
                            }
                        }
                    }
                },
                new Event()
                {
                    Id = 1246,
                    Type = EventType.System,
                    Name = "Mass Hysteria",
                    Description = "Your people are convinced that they are being infiltrated by some alien race.  Suspicion is everywhere and people no longer trust anything.",
                    Duration = 96,
                    MinHonor = 0,
                    MaxHonor = 70,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Target = 3,
                            Argument1 = -3,
                            Duration = 96,
                            ControlModelModifier = new ControlModel()
                            {
                                Production = -3,
                                Diplomacy = -3
                            }
                        }
                    }
                },
                new Event()
                {
                    Id = 1247,
                    Type = EventType.System,
                    Name = "Computer Virus",
                    Description = "Your computer system has been corrupted by a presentient form of AI.  This AI is drastically slowing your development progress.",
                    Duration = 72,
                    MinHonor = 0,
                    MaxHonor = 70,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 72,
                            ControlModelModifier = new ControlModel()
                            {
                                Research = -3
                            }
                        }
                    }
                },
                new Event()
                {
                    Id = 1248,
                    Type = EventType.System,
                    Name = "New Tech Discovered",
                    Description = "Your scout ships find a relic from earlier in the Empire, and thinking to salvage the materials, return with it.  Upon looting the remains, your engineers discover valuable information in the computers.",
                    Duration = 36,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.GainTech,
                            IsInstant = true
                        }
                    }
                },
                new Event()
                {
                    Id = 1249,
                    Type = EventType.System,
                    Name = "Bribe from a Wealthy Merchant",
                    Description = "A wealthy trading baron asks to open trade with your planets. In exchange he is offering to help start the local economy by giving you 500k PP.",
                    Duration = 36,
                    MinHonor = 0,
                    MaxHonor = 60,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeProduction,
                            ModifierType = ModifierType.Absolute,
                            Argument1 = 500000,
                            IsInstant = true
                        }
                    }
                },
                new Event()
                {
                    Id = 1250,
                    Type = EventType.System,
                    Name = "Pirates in Your Trading Lanes",
                    Description = "There have been pirates sighted in your system. Your scout ships detected a band of roving pirates in your trade lanes.  After dispatching them, you gained 800k PP reward for their deaths.",
                    Duration = 36,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeProduction,
                            ModifierType = ModifierType.Absolute,
                            Argument1 = 800000,
                            IsInstant = true
                        }
                    }
                },
                new Event()
                {
                    Id = 1251,
                    Type = EventType.System,
                    Name = "Pirates in Your Trading Lanes",
                    Description = "There have been pirates sighted in your system. Your trading lanes are under attack by pirates.  All commerce you are under is reduced.",
                    Duration = 48,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 48,
                            ControlModelModifier = new ControlModel()
                            {
                                Commerce = -4
                            }
                        }
                    }
                },
                new Event()
                {
                    Id = 1252,
                    Type = EventType.System,
                    Name = "Supercommander Offers You his Help",
                    Description = "A roving commander with a heroic reputation offers to help you in your cause.",
                    Duration = 36,
                    MinHonor = 50,
                    MaxHonor = 100,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.GainCommander,
                            Argument1 = 20,
                            IsInstant = true
                        }
                    }
                },
                new Event()
                {
                    Id = 1253,
                    Type = EventType.System,
                    Name = "Assassination",
                    Description = "One of your active commanders is assassinated.",
                    Duration = 36,
                    MinHonor = 0,
                    MaxHonor = 50,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.LoseCommander,
                            ModifierType = ModifierType.Absolute,
                            Argument1 = 1,
                            IsInstant = true
                        }
                    }
                },
                new Event()
                {
                    Id = 1254,
                    Type = EventType.System,
                    Name = "Assassination",
                    Description = "One of your fleets has been sabotaged.  All of the ships in this fleet take damage and need repairs.",
                    Duration = 36,
                    MinHonor = 0,
                    MaxHonor = 60,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.DamageFleet,
                            ModifierType = ModifierType.Proportional,
                            Argument1 = 15,
                            IsInstant = true
                        }
                    }
                },
                new Event()
                {
                    Id = 1255,
                    Type = EventType.System,
                    RaceList = new List<RaceType>()
                    {
                        RaceType.Agerus
                    }.Cast<int>().ToList(),
                    RaceListType = ListType.Exclusion,
                    Name = "Previous Civilization Discovered",
                    Description = "Your archaeologists discover the remains of a highly advanced civilization on one of your planets.",
                    Duration = 48,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.GainTech,
                            IsInstant = true
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 48,
                            ControlModelModifier = new ControlModel()
                            {
                                Research = 2
                            }
                        }
                    }
                },
                new Event()
                {
                    Id = 1256,
                    Type = EventType.System,
                    RaceList = new List<RaceType>()
                    {
                        RaceType.Agerus
                    }.Cast<int>().ToList(),
                    RaceListType = ListType.Exclusion,
                    Name = "Silicon Insect Swarm",
                    Description = "These insects fly in one day on the dark side of your planet.  You are afraid at first what damage they might cause.  Then you realize that all they are eating is scrap material.  When they leave weeks later, your planet has undergone a great change.",
                    Duration = 36,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangePlanetResource,
                            Argument1 = 1,
                            IsInstant = true
                        }
                    }
                },
                new Event()
                {
                    Id = 1258,
                    Type = EventType.System,
                    RaceList = new List<RaceType>()
                    {
                        RaceType.Agerus
                    }.Cast<int>().ToList(),
                    RaceListType = ListType.Exclusion,
                    Name = "Commerce Title Offered",
                    Description = "You are elected as the guardian of free commerce by the galaxy commerce guild. All you have to do is pay 1,000,000 PP, and you'll have the title.",
                    Duration = 36,
                    RequiresResponse = true,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeProduction,
                            ModifierType = ModifierType.Absolute,
                            Argument1 = -1000000,
                            IsInstant = true,
                            ApplyOn = EventEffectApplyType.AnswerYes
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 1440,
                            ApplyOn = EventEffectApplyType.AnswerYes,
                            ControlModelModifier = new ControlModel()
                            {
                                Commerce = 1
                            }
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 1152,
                            ApplyOn = EventEffectApplyType.AnswerYes,
                            ControlModelModifier = new ControlModel()
                            {
                                Commerce = 1
                            }
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 864,
                            ApplyOn = EventEffectApplyType.AnswerYes,
                            ControlModelModifier = new ControlModel()
                            {
                                Commerce = 1
                            }
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 576,
                            ApplyOn = EventEffectApplyType.AnswerYes,
                            ControlModelModifier = new ControlModel()
                            {
                                Commerce = 1
                            }
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 288,
                            ApplyOn = EventEffectApplyType.AnswerYes,
                            ControlModelModifier = new ControlModel()
                            {
                                Commerce = 1
                            }
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeHonor,
                            Argument1 = 10,
                            IsInstant = true,
                            ApplyOn = EventEffectApplyType.AnswerYes
                        }
                    }
                },
                new Event()
                {
                    Id = 1262,
                    Type = EventType.System,
                    Name = "Whispers of the Ascended",
                    Description = "From somewhere, the whispering of those who have ascended is being broadcast to your people's minds. None of your people can understand what the voices are whispering, nor can they determine where the voices are speaking from.  The effect is critical however, because everyone is starting to day-dream, and all industry is stopping, All your fleets are sitting idle.",
                    Duration = 72,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 72,
                            ControlModelModifier = new ControlModel()
                            {
                                Production = -5,
                                Military = -5
                            }
                        }
                    }
                },
                new Event()
                {
                    Id = 1263,
                    Type = EventType.System,
                    Name = "Information Routing Error",
                    Description = "Your main coordination system had a horrible malfunction.  You learned about it in time to avoid it destroying your empire, but it did cause some problems.",
                    Duration = 36,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeConcentrationMode,
                            IsInstant = true
                        }
                    }
                },
                new Event()
                {
                    Id = 1264,
                    Type = EventType.System,
                    Name = "Horrible Accusations",
                    Description = "Some unknown party has implicated you in an attempt to remove the magistrate from the Emperor's favor. The Magistrate trusted the party and you could feel his scorn response.",
                    Duration = 36,
                    MinHonor = 0,
                    MaxHonor = 60,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeEmpireRelation,
                            Argument1 = -15,
                            IsInstant = true
                        }
                    }
                },
                new Event()
                {
                    Id = 1265,
                    Type = EventType.System,
                    Name = "Horrible Accusations",
                    Description = "Some unknown party has implicated you in an attempt to remove the magistrate from the Emperor's favor. However, the Magistrate trusted you.",
                    Duration = 36,
                    MinHonor = 40,
                    MaxHonor = 100,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeEmpireRelation,
                            Argument1 = 5,
                            IsInstant = true
                        }
                    }
                },
                new Event()
                {
                    Id = 1266,
                    Type = EventType.System,
                    Name = "Traitorous Commander",
                    Description = "One of your commanders has gotten bored with being passed by for promotion.  He has left your system, and takes some money and a ship with him.",
                    Duration = 36,
                    MinHonor = 0,
                    MaxHonor = 40,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.LoseCommander,
                            ModifierType = ModifierType.Absolute,
                            Argument1 = 1,
                            IsInstant = true
                        }
                    }
                },
                new Event()
                {
                    Id = 1268,
                    Type = EventType.System,
                    Name = "Energy Leech",
                    Description = "This odd being latches on to one of your fleet groups and drains all ship energy.  Before it can be destroyed, all hands aboard die.  This includes some of your commanders unfortunately.",
                    Duration = 36,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.KillCommanderAndDisbandFleet,
                            IsInstant = true
                        }
                    }
                },
                new Event()
                {
                    Id = 1269,
                    Type = EventType.System,
                    Name = "Archeological Find",
                    Description = "Your scientists unearth a strange device one day.  Only after intense study do they realize what it is.  This device will help to moderate this planets gravity.",
                    Duration = 36,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.GainPlanetGravityControl,
                            IsInstant = true
                        }
                    }
                },
                new Event()
                {
                    Id = 1270,
                    Type = EventType.System,
                    Name = "Drug Craze",
                    Description = "New drug craze sweeps your population. People refuse to learn or work, they just want to have sex all the time.",
                    Duration = 72,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Target = 1,
                            Argument1 = 2,
                            Duration = 72,
                            ControlModelModifier = new ControlModel()
                            {
                                Growth = 1,
                                Research = -3,
                                Production = -3,
                                Genius = -3
                            }
                        }
                    }
                },
                new Event()
                {
                    Id = 1271,
                    Type = EventType.System,
                    Name = "Slaves Revolt",
                    Description = "Slaves revolt in almost all of your planets. They are mad and refuse to surrender, so you have no choice but to kill them all. It will take some time before you can rebuild your precious workforce.",
                    MinHonor = 0,
                    MaxHonor = 30,
                    Duration = 72,
                    Effects = new List<EventEffect>()
                    {
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 36,
                            ControlModelModifier = new ControlModel()
                            {
                                Production = -1
                            }
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 72,
                            ControlModelModifier = new ControlModel()
                            {
                                Production = -1
                            }
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 144,
                            ControlModelModifier = new ControlModel()
                            {
                                Production = -1
                            }
                        },
                        new EventEffect()
                        {
                            Type = PlayerEffectType.ChangeControlModel,
                            Duration = 288,
                            ControlModelModifier = new ControlModel()
                            {
                                Production = -1
                            }
                        }
                    }
                }
            };
        }
        private void UseDefaultPlanetAttributes()
        {
            PlanetAttributes = new List<PlanetAttribute>()
            {
                new PlanetAttribute()
                {
                    Id = 10000,
                    Name = "Artifact",
                    Type = PlanetAttributeType.Artifact,
                    ControlModelModifier = new ControlModel()
                    {
                        Research = 10
                    }
                },
                new PlanetAttribute()
                {
                    Id = 10001,
                    Name = "Massive Artifact",
                    Type = PlanetAttributeType.MassiveArtifact,
                    ControlModelModifier = new ControlModel()
                    {
                        Research = 20
                    }
                },
                new PlanetAttribute()
                {
                    Id = 10002,
                    Name = "Asteroid",
                    Type = PlanetAttributeType.Asteroid,
                    ControlModelModifier = new ControlModel()
                    {
                        Production = 1
                    }
                },
                new PlanetAttribute()
                {
                    Id = 10003,
                    Name = "Moon",
                    Type = PlanetAttributeType.Moon,
                    ControlModelModifier = new ControlModel()
                    {
                        Military = 2,
                        Growth = 3,
                        Commerce = 1
                    }
                },
                new PlanetAttribute()
                {
                    Id = 10004,
                    Name = "Radiation",
                    Type = PlanetAttributeType.Radiation,
                    ControlModelModifier = new ControlModel()
                    {
                        Environment = - 1
                    }
                },
                new PlanetAttribute()
                {
                    Id = 10005,
                    Name = "Severe Radiation",
                    Type = PlanetAttributeType.SevereRadiation,
                    ControlModelModifier = new ControlModel()
                    {
                        Environment = -2
                    }
                },
                new PlanetAttribute()
                {
                    Id = 10006,
                    Name = "Hostile Monster",
                    Type = PlanetAttributeType.HostileMonster,
                    ControlModelModifier = new ControlModel()
                    {
                        Environment = -1
                    }
                },
                new PlanetAttribute()
                {
                    Id = 10007,
                    Name = "Obstinate Microbe",
                    Type = PlanetAttributeType.ObstinateMicrobe,
                    ControlModelModifier = new ControlModel()
                    {
                        Environment = -2
                    }
                },
                new PlanetAttribute()
                {
                    Id = 10008,
                    Name = "Beautiful Landscape",
                    Type = PlanetAttributeType.BeautifulLandscape,
                    ControlModelModifier = new ControlModel()
                    {
                        Commerce = 2,
                        Spy = -1
                    }
                },
                new PlanetAttribute()
                {
                    Id = 10009,
                    Name = "Black Hole",
                    Type = PlanetAttributeType.BlackHole,
                    ControlModelModifier = new ControlModel()
                    {
                        Commerce = -2,
                        Environment = -2,
                        Production = -1
                    }
                },
                new PlanetAttribute()
                {
                    Id = 10010,
                    Name = "Nebula",
                    Type = PlanetAttributeType.Nebula
                },
                new PlanetAttribute()
                {
                    Id = 10011,
                    Name = "Dark Nebula",
                    Type = PlanetAttributeType.DarkNebula,
                    ControlModelModifier = new ControlModel()
                    {
                        Environment = -2,
                        Commerce = -1
                    }
                },
                new PlanetAttribute()
                {
                    Id = 10012,
                    Name = "Volcanic Activity",
                    Type = PlanetAttributeType.VolcanicActivity,
                    ControlModelModifier = new ControlModel()
                    {
                        Environment = -1,
                        Production = 2
                    }
                },
                new PlanetAttribute()
                {
                    Id = 10013,
                    Name = "Intense Volcanic Activity",
                    Type = PlanetAttributeType.IntenseVolcanicActivity,
                    ControlModelModifier = new ControlModel()
                    {
                        Environment = -2,
                        Production = 5
                    }
                },
                new PlanetAttribute()
                {
                    Id = 10014,
                    Name = "Ocean",
                    Type = PlanetAttributeType.Ocean,
                    ControlModelModifier = new ControlModel()
                    {
                        Environment = 1,
                        Growth = 3,
                        FacilityCost = -5
                    }
                },
                new PlanetAttribute()
                {
                    Id = 10015,
                    Name = "Irregular Climate",
                    Type = PlanetAttributeType.IrregularClimate,
                    ControlModelModifier = new ControlModel()
                    {
                        Environment = -2
                    }
                },
                new PlanetAttribute()
                {
                    Id = 10016,
                    Name = "Major Space Route",
                    Type = PlanetAttributeType.MajorSpaceRoute,
                    ControlModelModifier = new ControlModel()
                    {
                        Commerce = 2
                    }
                },
                new PlanetAttribute()
                {
                    Id = 10017,
                    Name = "Major Space Crossroute",
                    Type = PlanetAttributeType.MajorSpaceCrossroute,
                    ControlModelModifier = new ControlModel()
                    {
                        Commerce = 5
                    }
                },
                new PlanetAttribute()
                {
                    Id = 10018,
                    Name = "Frontier Area",
                    Type = PlanetAttributeType.FrontierArea,
                    ControlModelModifier = new ControlModel()
                    {
                        Commerce = -2
                    }
                }
            };
        }
        private void UseDefaultProjects()
        {
            Projects = new List<Project>()
            {
                new Project()
                {
                    Id = 7002,
                    Name = "Earth Elevator",
                    Description = "Passing through the entire planet, the Earth Elevator utilizes gravitational energy to move from one end of the planet to the other.  Though its rapid speed makes it difficult for the transportation of live beings, the transportation of objects is greatly simplified, contributing to the increase in efficiency of manufacturing.",
                    Type = ProjectType.Planet,
                    Cost = 35000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1320
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Production = 1
                    }
                },
                new Project()
                {
                    Id = 7004,
                    Name = "Solar Control System",
                    Description = "Long ago, during the expansion of the universe, the technology most needed was the construction of a system to control the characteristics of the solar environment.  As generations progressed, and many sacrifices later, this technology was perfected with the completion of the solar control system.  With the passage of time, a star ages to become a super nova or a white dwarf star, and the surrounding planets also reach their ends.  But the utilization of the solar control system can prevent the aging of a star, as well as making it possible to partially control the environment of surrounding planets that are under the star's influence.",
                    Type = ProjectType.Planet,
                    Cost = 20000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1321
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Environment = 1
                    }
                },
                new Project()
                {
                    Id = 7005,
                    Name = "Orbital Space Elevator",
                    Description = "An incredible amount of energy is required for a spaceship to escape a planet's atmosphere.  This energy can be conserved through constructing and launching space fleets from orbital stations.  The orbital space elevator was used extensively to transport material from the planet's surface to these stations in orbit around the planet.",
                    Type = ProjectType.Planet,
                    Cost = 70000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1328
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Military = 1,
                        Production = 1
                    }
                },
                new Project()
                {
                    Id = 7006,
                    Name = "Mechanic Bard",
                    Description = "Well-constructed words have always been one of the most powerful forces in history.  A bard, who combines this powerful language with another power, music, can be considered to possess the ultimate power.  A mechanic bard is an artificially created bard that remembers all forms and history of poetry and song for every language and species.  The mechanic bard also carries out the task of incorporating all this knowledge to create a new language of song.  The mechanic bard is created with the goal of \"stability.\" Everywhere it goes, it will sing songs that will bring stability to each of the regions.  Wherever a mechanic bard exists, life is stabilized, and advancements are brought about faster than other regions.",
                    Type = ProjectType.Planet,
                    Cost = 35000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1328
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Growth = 1
                    }
                },
                new Project()
                {
                    Id = 7008,
                    Name = "Mind Control Center",
                    Description = "This facility is installed per planet, and controls the minds of inhabitants in its own way. The inhabitants strive for completion of the given task without realizing they are controlled. The society is extremely well under control, and the inhabitants would even die for the cause.",
                    Type = ProjectType.Planet,
                    Cost = 50000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1119
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Society,
                            Value = SocietyType.Classism
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Military = 1,
                        Efficiency = 1
                    }
                },
                new Project()
                {
                    Id = 7009,
                    Name = "Planet Environment Stabilizer",
                    Description = "This system is based on the idea that life is present within every matter in the universe. This system also enhances overall environment and adjusts it for the race, but it's different from terra-forming in general. While terra forming physically changes the terrain of the planet, this system works on the lives on the planet, changing the environment and proliferating the race indirectly.",
                    Type = ProjectType.Planet,
                    Cost = 30000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1421
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Growth = 1,
                        Environment = 2
                    }
                },
                new Project()
                {
                    Id = 7010,
                    Name = "Clone Family",
                    Description = "A cluster of clones based on one single organism is called 'Clone Family.' They share the same attributes, outlook and abilities. Grown under the same environment, they manifest their full potential when they are together. They always move together, think together and work together; the whole cluster of clones consider themselves a single entity. Its prowess is unequalled - especially when compared to another organism - but loss of a single clone could well render the whole family useless.",
                    Type = ProjectType.Planet,
                    Cost = 30000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1120
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Society,
                            Value = SocietyType.Totalism
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Production = 2,
                        FacilityCost = 2,
                        Genius = -1,
                        Research = -1
                    }
                },
                new Project()
                {
                    Id = 7011,
                    Name = "Memory Rewinder",
                    Description = "Memories fade away; it's not that the memories are physically erased from the brain, but that it becomes harder to recollect them after a while, once the memory is stored somewhere in the brain. Memory rewinder is a tool that \"replays\" the memories that were once considered 'lost.' Through the help of this device, anybody can replay the necessary memory logged in the brain; and they can be of immense help for many researchers.",
                    Type = ProjectType.Fixed,
                    Cost = 20000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1211
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Research = 1
                    }
                },
                new Project()
                {
                    Id = 7013,
                    Name = "Mind Passway",
                    Description = "There has been many attempts to find the safest method of data transmission. So far, the safest method known to the universe is the mind passway. This method was brought about by merging the psionic power and enciphering of daily transmissions. Barring the limitations due to the distance, it is the safest transmission method.",
                    Type = ProjectType.Fixed,
                    Cost = 150000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1420
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.RacialTrait,
                            Value = RacialTrait.Psi
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Spy = 2
                    }
                },
                new Project()
                {
                    Id = 7016,
                    Name = "Perfect Secretary",
                    Description = "This device is the best 'secretary' an intelligence has ever created. Its overall shape and attributes differs from race to race, but its function is about the same for all. It functions as the translator for all languages. It can summarize the statements from the other party, or vice versa. It can also log all the dialogues in the past, then recall the relevant part in need in times of need.",
                    Type = ProjectType.Fixed,
                    Cost = 100000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1417
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Spy = 1,
                        Commerce = 1
                    }
                },
                new Project()
                {
                    Id = 7017,
                    Name = "Dream Maker",
                    Description = "This device utilizes the psionic power within the materialistic world. It analyzes a person's will, then absorbs his psionic power to transform that energy and will into a concrete material. Once considered an invaluable machine, it lost its luster in that it takes immense mental control as well as massive resources and energy.",
                    Type = ProjectType.Fixed,
                    Cost = 600000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1424
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.RacialTrait,
                            Value = RacialTrait.Psi
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Genius = 2,
                        Production = 1,
                        FacilityCost = -1
                    }
                },
                new Project()
                {
                    Id = 7018,
                    Name = "Saga Archive",
                    Description = "This archive contains all the heroics, saga and many other memorable events. It is open to public, to allow open research of the war history, legends and hidden history. Installation of this facility has caused, directly and indirectly, the growth of heroes in all generations.",
                    Type = ProjectType.Planet,
                    Cost = 10000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1121
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Society,
                            Value = SocietyType.Personalism
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Genius = 2,
                        Diplomacy = 1
                    }
                },
                new Project()
                {
                    Id = 7020,
                    Name = "University of Past, Present and Future",
                    Description = "Past, present and future - the flow of time and the secret behind. The Space - the meaning hidden behind. They're the primordial source of question that bogged down any intelligence ever since the start of the time. This institute was established to study exclusively the meaning and secrets of our time and space.",
                    Type = ProjectType.Fixed,
                    Cost = 300000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1129
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Research = 3,
                        Diplomacy = 2
                    }
                },
                new Project()
                {
                    Id = 7022,
                    Name = "Fallen Head",
                    Description = "This device was called 'Head of Lucifer' once it was first developed. It could exhibit the maximum efficiency under any circumstances, and its abilities were well beyond comparison to other artificial machinaries. To some, it even inspired awe in their heart, and when it was finally committed to the battle, it was named 'Fallen Head.'",
                    Type = ProjectType.Council,
                    Cost = 2000000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1130
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Diplomacy = 3,
                        Efficiency = 2,
                        Commerce = 1,
                        Military = 2,
                        Growth = -1,
                        Environment = -2
                    }
                },
                new Project()
                {
                    Id = 7024,
                    Name = "Nova Plant",
                    Description = "This plant is run by the enormous energy released from Nova. Productivity of the system increases by mutiple fold, but the environment suffers, due to the harmful radiation from the Nova. Ships from the outside tends shun from this system, consequently. This in turn results in contraction of inter-planetary commerce. This is an oppressive project that maximizes the productivity at the cost of the inhabitants' well being.",
                    Type = ProjectType.Council,
                    Cost = 2000000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1327
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Production = 3,
                        Environment = -1,
                        Commerce = -1
                    }
                },
                new Project()
                {
                    Id = 7025,
                    Name = "Archmage",
                    Description = "This is a cyber warfare game on Matrix. This game is highly addictive too. This game is used to train the establishment of strategy and tactics in military academies. Also, through the training with this game, each individual learns how to work together and exist together; consequently this enhances the cohesion among individuals.",
                    Type = ProjectType.Council,
                    Cost = 50000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1213
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Genius = 1
                    }
                },
                new Project()
                {
                    Id = 7026,
                    Name = "Humanoid Plant",
                    Description = "Out of the endless desire toward the throne that only belonged to the deity, humans have eventually created a being that looks same as humans themselves, but possesses much superior talents. This creature, named 'Replicant' has both mental and physical talent that could reach a few hundred fold of human's talent. However, they have a very short life span, and are programmed not to hurt humans. Creation of 'humans without human right' also have waged intense opposition among humans later.",
                    Type = ProjectType.Fixed,
                    Cost = 600000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1425
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Race,
                            Value = 1
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        FacilityCost = 2,
                        Production = 2,
                        Diplomacy = -1
                    }
                },
                new Project()
                {
                    Id = 7027,
                    Name = "Thinker Ball",
                    Description = "No one knows where the name 'Thinker Ball' originated from. This system works like an Oracle, but its much capricious behavior rendered its nickname 'fairy.' At times this device presents helpful ideas but it consumes massive amount of energy, shutting down the nearby facilities at times.",
                    Type = ProjectType.Council,
                    Cost = 2000000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1224
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Military = 3,
                        Commerce = 2,
                        Efficiency = 2,
                        FacilityCost = -2
                    }
                },
                new Project()
                {
                    Id = 7030,
                    Name = "AI Bill of Rights",
                    Description = "Development of AI has brought up many questions about the rights of the AI, resulting in the declaration of AI Bill of Rights eventually. This at first caused overall agitation in the society, which also raised the cost of maintanence of the society in the course. Especially, this had a significant effect on the military AI's, but in the end, this has contributed in the wholesomeness and individual autonomy in the society.",
                    Type = ProjectType.Council,
                    Cost = 200000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1220
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Military = -1,
                        Efficiency = 4,
                        FacilityCost = -1
                    }
                },
                new Project()
                {
                    Id = 7031,
                    Name = "Truth Tester",
                    Description = "Discovery of Self-evident Language has brought answers to many statements that were once questioned or considered inconclusive. By transforming and manipulating with this self-evident language, anyone could figure out whether the given statement was true or not. This process is called 'Truth Testing.' Implementation of automated truth tester has brought about many change and influence over the society.",
                    Type = ProjectType.Council,
                    Cost = 225000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1122
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Research = 1,
                        Spy = 1,
                        Diplomacy = 1
                    }
                },
                new Project()
                {
                    Id = 7032,
                    Name = "Imperial Palace",
                    Description = "As the Empire grew in strength, it became necessary to make a place where overall domestic and diplomatic matters could be discussed. The necessity for the Imperial Palace was manisfested, through the need to proclaim the dignity of the Emperor to its domain in beyond. With the Imperial Palace, the diplomats work with higher degree of pride, and the people would support the military with more enthusiasm. But its maintenance is highly costly; it takes a lot to rear the dignity all over the space.",
                    Type = ProjectType.Fixed,
                    Cost = 300000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1126
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Society,
                            Value = SocietyType.Classism
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Diplomacy = 2,
                        Military = 2,
                        Production = 1,
                        FacilityCost = -1
                    }
                },
                new Project()
                {
                    Id = 7033,
                    Name = "Symbol of Liberation",
                    Description = "Symbol of Liberation is a monolithic monument, which stands for the aspiration for the liberation of all races in the whole galaxy and beyond. This monument will inspire the people and instill individual autonomy and responsibility, aspiring them for higher ambition. Yet this may turn out to be a setback in that the people may fall a prey to the external manipulation of information.",
                    Type = ProjectType.Fixed,
                    Cost = 300000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1127
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Society,
                            Value = SocietyType.Classism
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Genius = 2,
                        Production = 1,
                        Commerce = 1,
                        Research = 1,
                        Spy = -2
                    }
                },
                new Project()
                {
                    Id = 7034,
                    Name = "The One Unified Mind",
                    Description = "You have merged the minds of the race into a new mind. Your own mind and existence have been absorbed into this 'unified mind' and you are satisfied with the race organizing and moving with unified mind. The whole race is working with higher degree of efficiency, but at times you, the overlord, find it cumbersome at times to control the race minion by minion.",
                    Type = ProjectType.Fixed,
                    Cost = 300000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1125
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Society,
                            Value = SocietyType.Totalism
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Production = 2,
                        Growth = 2,
                        Efficiency = 1,
                        Spy = -1,
                        Diplomacy = -1
                    }
                },
                new Project()
                {
                    Id = 7036,
                    Name = "Organic Plant",
                    Description = "Industrial growth has always involved consumption of more energy. The need for energy continuously grew for higher efficiency and more production in less time. Organic plant is the answer scientists have been looking for, as the answer to the current problem. This scientific revolution is made of a live organic entity, and its efficiency is simply unequalled. This living plant is capable of supplying more energy to the industry constantly at less cost.",
                    Type = ProjectType.Planet,
                    Cost = 60000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1411
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Race,
                            Value = 2
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Growth = 1,
                        Production = 1,
                        FacilityCost = 2
                    }
                },
                new Project()
                {
                    Id = 7037,
                    Name = "Desire Amplifier",
                    Description = "Individual development and growth is closely related to the desire toward power and resource. The history is the manisfestation of how the people have fought, lived and betrayed for the power and resources. This device, called 'Desire Amplifier,' amplifies the desire of individuals to the extreme, using that desire as the driving force to move the society. This amplified desire is seen through the thirst for more power and resource, instigating an extreme competition. Such a society grows faster, and produces more resources but it tends to lose its flexibility and overall control of such a society becomes hard. Yet great many of the developing society use this device, for this ensures a fast growth in the early stage.",
                    Type = ProjectType.Planet,
                    Cost = 3000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1107
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Race,
                            Value = 1
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Growth = 1,
                        Production = 1,
                        Efficiency = -1,
                        Research = -1
                    }
                },
                new Project()
                {
                    Id = 7038,
                    Name = "Will of the Righteous",
                    Description = "Even before the discovery of religion, it was so easy to deceive yourself by you. With religion, it's a lot easier to deceive yourself by you. This is a kind of the national wide hypnosis. You are never wrong. You cannot be wrong. If something is wrong, It's your opponent's fault. You make for righteousness and your will itself is the will of the Righteous.",
                    Type = ProjectType.Fixed,
                    Cost = 3000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1104
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Society,
                            Value = SocietyType.Classism
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Military = 1,
                        Production = 1,
                        Commerce = -1,
                        Diplomacy = -1
                    }
                },
                new Project()
                {
                    Id = 7040,
                    Name = "Galactic Liberalism Movement",
                    Description = "As the pan-galactic liberalism movement starts spreading, everybody starts acting on his own accord. This results in loss of order in major part and decrease in industrial output. Researchers are free to research his area of interest, however, and this results in many magnificent feats. Also, inter-galactic commerce is vitalized through the promotion of free trade.",
                    Type = ProjectType.Fixed,
                    Cost = 100000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1111
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Society,
                            Value = SocietyType.Personalism
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Production = -1,
                        Research = 3,
                        Commerce = 3
                    }
                },
                new Project()
                {
                    Id = 7041,
                    Name = "Advocacy of Green Round",
                    Description = "Under the advocacy, the council declares that it will administer environment-friendly policies, and regulates excess development of the planet. Through the exhaustive research and study of the past and the lost civilizations, these cluster of sages have come to claim that excess consumption of resources and irresponsible production of waste have caused the decline and fall of past civilization. This policy reflects such an opinion. Under this policy, the council makes every effort to preserve the environment, but this also results in impediment to the overall industrial development.",
                    Type = ProjectType.Council,
                    Cost = 30000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1114
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Environment = 2,
                        Efficiency = 1,
                        FacilityCost = -1
                    }
                },
                new Project()
                {
                    Id = 7042,
                    Name = "Neutral Power Declaration",
                    Description = "Under such a declaration, the council renounces its military ambitions, and turns to pacifism. The excess resources from the military accelerates the overall development of the society. Also, it facilitates all kinds of intelligence activities, but the lack of military power undermines the voice.",
                    Type = ProjectType.Council,
                    Cost = 500000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1128
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Military = -2,
                        Diplomacy = -1,
                        Commerce = 2,
                        Production = 2,
                        Spy = 1,
                        Research = 2
                    }
                },
                new Project()
                {
                    Id = 7043,
                    Name = "Galactic Agreement of Free Commerce",
                    Description = "Galactic Agreement of Free Commerce, a pan-council agreement contributes in vitalization of inter-council commerces, which in turn promotes commerce from the external sources. Traders travel to the farthest sectors, and all regions overflow with liveliness and energy. At the same time, it becomes harder to regulate informations, and this in turn jeopardizes the information security.",
                    Type = ProjectType.Council,
                    Cost = 15000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1112
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Commerce = 2
                    }
                },
                new Project()
                {
                    Id = 7045,
                    Name = "Propaganda Entertainment",
                    Description = "This project involves putting together the perfect show to inspire your people. It causes them to put thier best effort into all they do and fuels their desire to conquer the enemy. Unfortunately, it also takes up time when they could be doing things, and decreases originality.",
                    Type = ProjectType.Planet,
                    Cost = 20000,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1116
                        }
                    },
                    ControlModelModifier = new ControlModel()
                    {
                        Military = 1,
                        Production = -1,
                        Research = -1,
                        Efficiency = 1,
                        Growth = 1
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
                    Id = (int)RaceType.Human,
                    Name = "Human",
                    Description = "Though humans have a relatively short life span, the population increase rate of humans is nothing short of astonishing.  As a result, the other species have always been wary of the humans' expansion of power in the universe.  However, even the watchful eyes of the other species cannot easily spot the other strong points of the human species, which lie in the philosophical and social sciences, as well as other literary and cultural developments.  Their never-ending desire to seek the ideal has provided a catalyst not found in other species.",
                    SocietyType = SocietyType.Classism,
                    HomeAtmosphere = new Atmosphere()
                    {
                        H2 = 0,
                        Cl2 = 0,
                        CO2 = 0,
                        O2 = 1,
                        N2 = 4,
                        CH4 = 0,
                        H2O = 0
                    },
                    HomeGravity = 1,
                    HomeTemperature = 300,
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
                    },
                    AdmiralAbilities = new List<AdmiralRacialAbility>()
                    {
                        AdmiralRacialAbility.IrrationalTactics,
                        AdmiralRacialAbility.Intuition,
                        AdmiralRacialAbility.LoneWolf
                    },
                    AdmiralFirstNames = new List<string>()
                    {
                        "Tania",
                        "David",
                        "Hal",
                        "Norman",
                        "Taorin",
                        "Molly",
                        "Ivan",
                        "Mellany",
                        "Taryn",
                        "Rock",
                        "Serra",
                        "Delilah",
                        "Zorra",
                        "Seria",
                        "Brian",
                        "Kay",
                        "Clair",
                        "Jean",
                        "Mike",
                        "Miguel",
                        "Pablo",
                        "Dora",
                        "Johnny",
                        "Arthur",
                        "Luke"
                    },
                    AdmiralLastNames = new List<string>()
                    {
                        "Trokov",
                        "Shang",
                        "Struve",
                        "Kessler",
                        "Stendel",
                        "Champion",
                        "Chefman",
                        "Carter",
                        "Cervani",
                        "Ashray",
                        "Noran",
                        "Sergei",
                        "Forgraves",
                        "McKintire",
                        "Parker",
                        "Baker",
                        "Dupin",
                        "Vandike",
                        "Drought",
                        "Drake"
                    },
                    Color = Color.FromArgb(42, 23, 69)
                },
                new Race()
                {
                    Id = (int)RaceType.Targoid,
                    Name = "Targoid",
                    Description = "The Targoid race maintain a totalitarian society, with all of their species originating from one mother body and being controlled by that mother body.  Targoids efficiently produce every type of their race as needed from one mother body through the programming of the DNA of the unborn Targoid embryo.  This system makes it possible to produce any variety of the species, from mouse-sized micro workers to battle creatures over 2 kilometers in length.  Targoid workers are famous for their efficiency in gathering resources and constructing buildings.",
                    SocietyType = SocietyType.Totalism,
                    HomeAtmosphere = new Atmosphere()
                    {
                        H2 = 0,
                        Cl2 = 0,
                        CO2 = 1,
                        O2 = 0,
                        N2 = 3,
                        CH4 = 1,
                        H2O = 0
                    },
                    HomeGravity = 1.2,
                    HomeTemperature = 340,
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
                    },
                    AdmiralAbilities = new List<AdmiralRacialAbility>()
                    {
                        AdmiralRacialAbility.DnaPoisonReplicater,
                        AdmiralRacialAbility.BreederMale,
                        AdmiralRacialAbility.ClonalDouble
                    },
                    AdmiralFirstNames = new List<string>()
                    {
                        "Urrhka-truqh",
                        "Orhmmvorrhaqhu",
                        "Krhrhuukhahath",
                        "Orumh",
                        "Rhudorrhak",
                        "Zhoelahazekuth",
                        "Thunzthammharhudz",
                        "Khohorrdadz",
                        "Thrildikadz",
                        "Kvardanz",
                        "Zdilpotek",
                        "Mortaksh",
                        "Kahhruth",
                        "Arrhundazthallamahak",
                        "Mmohoth",
                        "Bintokashredd",
                        "Mmorrhukrrah"
                    },
                    AdmiralLastNames = new List<string>()
                    {
                        "Thrrorhkuhatz",
                        "Bmataurhammazikhhathahat",
                        "Kvorhhiamadz",
                        "Zzomdhullahhodaqh",
                        "Khoshvaprrhothoqh",
                        "Walrorhkshafkoh",
                        "Fahqtrapohradz",
                        "Bahkrrhoatrroph",
                        "Torriashstoh",
                        "Zihaarrhud",
                        "Tworrkatth"
                    },
                    Color = Color.FromArgb(15, 50, 0)
                },
                new Race()
                {
                    Id = (int)RaceType.Buckaneer,
                    Name = "Buckaneer",
                    Description = "A Buckaneer's spaceship is his home and center of life.  They spend the majority of their life roaming space and finding fortune through trade (and sometimes piracy.)  Because they are accustomed to this type of gypsy roaming life, their fleets move swiftly and cannot be traced easily.  Buckaneer merchants possess information and contacts throughout the universe, which are indispensable aids to commerce, and thus hold the majority of trade and business in the universe.  It is common to see Buckaneer crafts that have duplicator systems for needed items built into the ship's interior design.",
                    SocietyType = SocietyType.Personalism,
                    HomeAtmosphere = new Atmosphere()
                    {
                        H2 = 0,
                        Cl2 = 2,
                        CO2 = 0,
                        O2 = 0,
                        N2 = 3,
                        CH4 = 0,
                        H2O = 0
                    },
                    HomeGravity = 0.7,
                    HomeTemperature = 270,
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
                    },
                    AdmiralAbilities = new List<AdmiralRacialAbility>()
                    {
                        AdmiralRacialAbility.ShieldDisrupter,
                        AdmiralRacialAbility.FamousPrivateer,
                        AdmiralRacialAbility.CommerceKing
                    },
                    AdmiralFirstNames = new List<string>()
                    {
                        "Navarre",
                        "Neffes",
                        "Gaviria",
                        "Fuerzas",
                        "Carletti",
                        "Veroniz",
                        "Montoya",
                        "Nouvouz",
                        "Naridad",
                        "Cervantes",
                        "Turleza",
                        "Vazar",
                        "Fernali",
                        "Montanya",
                        "Rodhorigue",
                        "Tiranos",
                        "Valdes",
                        "Esciante"
                    },
                    AdmiralLastNames = new List<string>()
                    {
                        "Vivouz",
                        "Mostilloz",
                        "Vienvenuez",
                        "Rhomuliges",
                        "Monjaros",
                        "Vernardi",
                        "Jertrali",
                        "Rozal",
                        "Janares",
                        "Toumarillos",
                        "Nouvau",
                        "Plait",
                        "Mosles"
                    },
                    Color = Color.FromArgb(70, 39, 0)
                },
                new Race()
                {
                    Id = (int)RaceType.Tecanoid,
                    Name = "Tecanoid",
                    Description = "The Tecanoids sought to find their key to evolution through attaching computers and bionic machinery to their bodies.  As a result of these experiments, the elite forces of their races have obtained extremely strong physical bodies and extraordinary intellect.  On the other hand, the lowest class of their society did not have an opportunity to receive these gifts, and thus became an unstable supporting pillar of the community.  The Tecanoid effort for evolution brought them optimally advanced data processing skills and electronic infiltration technologies, but the species has ultimately sacrificed their humanity for these machines.",
                    SocietyType = SocietyType.Classism,
                    HomeAtmosphere = new Atmosphere()
                    {
                        H2 = 0,
                        Cl2 = 0,
                        CO2 = 2,
                        O2 = 1,
                        N2 = 2,
                        CH4 = 0,
                        H2O = 0
                    },
                    HomeGravity = 1.3,
                    HomeTemperature = 240,
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
                    },
                    AdmiralAbilities = new List<AdmiralRacialAbility>()
                    {
                        AdmiralRacialAbility.CyberScanUnit,
                        AdmiralRacialAbility.TrajectoryAugmentation,
                        AdmiralRacialAbility.PatternBroadcaster
                    },
                    AdmiralFirstNames = new List<string>()
                    {
                        "Krom-ashur",
                        "jezrael",
                        "Skutharka",
                        "Dahtuashur",
                        "Noritohs",
                        "Saglazael",
                        "Zeanuxh",
                        "Tangael",
                        "Solsh-alush",
                        "Ashutharka",
                        "Thratch",
                        "Zinatt",
                        "Vees",
                        "Konte",
                        "Kothlosh",
                        "Ahkubilis",
                        "Variablis",
                        "Stathush",
                        "Aeteriash",
                        "Donablis",
                        "Luxhratch",
                        "Velutash",
                        "Semperixh",
                        "Krethaxhash",
                        "Rothaxh",
                        "Veripton",
                        "Esplosh",
                        "Thrizriash",
                        "Nerixhash",
                        "Lantarixh",
                        "Surizael",
                        "Aixh-agla",
                        "Laeratch",
                        "Lashloxh",
                        "Silutash",
                        "Iblis",
                        "Temnash",
                        "Maerixh",
                        "Menelublis",
                        "Aglaratch",
                        "Palankuxh",
                        "Druxh",
                        "Jeblis",
                        "Volublis",
                        "Nunkaxh",
                        "Jushtoh",
                        "Obthuratch",
                        "Thunkuxh",
                        "Rudhomente",
                        "Axiemlosh",
                        "Elenathriz",
                        "Pennalis",
                        "Aglar-sagla",
                        "Nachaearesh",
                        "Voraxh",
                        "Aurumntoh",
                        "Natohs",
                        "Niruxh",
                        "Dahntohs",
                        "Ubbo-sagla",
                        "Solsh-imarixh",
                        "Inarixh",
                        "Nektublis",
                        "Ishtlosh"
                    },
                    Color = Color.FromArgb(0, 58, 72)
                },
                new Race()
                {
                    Id = (int)RaceType.Evintos,
                    Name = "Evintos",
                    Description = "Unlike most of the other races of the galaxy, the Evintos are a non-organic life force whose bodies are composed of silicon and gold.  Their nervous system and metabolisms are also radically different from other inhabitants of the universe.  It is believed that they originate from artificial intelligence creatures created in the far ancient ages of the universe, whose technologies remain primarily lost to the world.  Because of their unusual appearance and makeup, they are often rejected by other species.  They possess a rigid social structure, which makes it hard for technological or social innovations to be implemented.  This structure further adds to their drifting differences between other species.  But these weak points are compensated by their mechanically precise and accurate social structure, which makes for extremely high production and manufacturing within their society.",
                    SocietyType = SocietyType.Totalism,
                    HomeAtmosphere = new Atmosphere()
                    {
                        H2 = 0,
                        Cl2 = 0,
                        CO2 = 0,
                        O2 = 0,
                        N2 = 0,
                        CH4 = 0,
                        H2O = 0
                    },
                    HomeGravity = 0.8,
                    HomeTemperature = 300,
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
                    },
                    AdmiralAbilities = new List<AdmiralRacialAbility>()
                    {
                        AdmiralRacialAbility.CyberScanUnit,
                        AdmiralRacialAbility.RigidThinking,
                        AdmiralRacialAbility.ManagementProtocol
                    },
                    AdmiralNameStyle = AdmiralNameStyle.Evintos,
                    Color = Color.FromArgb(49, 49, 49)
                },
                new Race()
                {
                    Id = (int)RaceType.Agerus,
                    Name = "Agerus",
                    Description = "Even more odd than the Evintos, the Agerus can only be defined as \"planetary life forms.\"  Many scientists doubt their existence, as they have remained largely secluded and withdrawn, having virtually no communication with other species.  Not much is known about this species, whose origin still remains a mystery.  The galaxy battleships that belong to the Agerus are used primarily for defense, and are actually smaller planet forms, which have evolved from spores from the mother planet.  It is not an easy task to classify and differentiate between the children of the Agerus and naturally occurring small planets.",
                    SocietyType = SocietyType.Totalism,
                    HomeAtmosphere = new Atmosphere()
                    {
                        H2 = 3,
                        Cl2 = 0,
                        CO2 = 0,
                        O2 = 0,
                        N2 = 2,
                        CH4 = 0,
                        H2O = 0
                    },
                    HomeGravity = 1.5,
                    HomeTemperature = 370,
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
                    },
                    AdmiralAbilities = new List<AdmiralRacialAbility>()
                    {
                        AdmiralRacialAbility.LyingDormant,
                        AdmiralRacialAbility.MissileCraters,
                        AdmiralRacialAbility.MeteorDrones
                    },
                    AdmiralFirstNames = new List<string>()
                    {
                        "Urm",
                        "Ro",
                        "Mmu",
                        "Rhmmu",
                        "Yorr",
                        "Mer",
                        "Rogg",
                        "Ttem",
                        "Rhe",
                        "Rouh",
                        "Hymh",
                        "Hyl",
                        "Nish",
                        "Lig",
                        "Ohr",
                        "Yim",
                        "Feh",
                        "She",
                        "Loum",
                        "Tylh",
                        "Yag",
                        "Dush",
                        "Har",
                        "Mmon",
                        "Nohr",
                        "Gosh",
                        "Rhuo",
                        "Mau",
                        "Dahl",
                        "Morr",
                        "Bovr",
                        "Dirr",
                        "Mula",
                        "Norr",
                        "Nil",
                        "Mou",
                        "Hodd",
                        "Houl",
                        "Gou",
                        "Naa",
                        "Geem",
                        "Kuu",
                        "Chah",
                        "Mutho",
                        "Torh",
                        "Tla",
                        "Taar",
                        "Rhhu",
                        "Gaah",
                        "Rhga",
                        "Haa",
                        "Lorh",
                        "Nee",
                        "Rhoa",
                        "Seak",
                        "Thorr",
                        "Baal"
                    },
                    Color = Color.FromArgb(84, 30, 30)
                },
                new Race()
                {
                    Id = (int)RaceType.Bosalian,
                    Name = "Bosalian",
                    Description = "Bosalians are peace-loving pacifists who hate conflict and battle.  Their noble philosophies and impartiality have settled many a battles between warring races, and their opinions are held in the highest respect by other races.  Though they are pacifists by nature, they are by no means a weak force in the galaxy.  True to their ideology, which states, \"The universe is one with your being, and you are one within the universe,\" Bosalians can freely use psychic powers.  Even races with limited sensory abilities, such as humanoids, can see the brilliance of the psychic aurora emitted by the Bosalians in their attacks.",
                    SocietyType = SocietyType.Personalism,
                    HomeAtmosphere = new Atmosphere()
                    {
                        H2 = 0,
                        Cl2 = 0,
                        CO2 = 0,
                        O2 = 1,
                        N2 = 3,
                        CH4 = 0,
                        H2O = 1
                    },
                    HomeGravity = 2,
                    HomeTemperature = 350,
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
                    },
                    AdmiralAbilities = new List<AdmiralRacialAbility>()
                    {
                        AdmiralRacialAbility.MentalGiant,
                        AdmiralRacialAbility.RetreatShield,
                        AdmiralRacialAbility.GeneticThrowback
                    },
                    AdmiralFirstNames = new List<string>()
                    {
                        "Vidhyarthi",
                        "Prakrit",
                        "Mahavanyak",
                        "Siddhanta-Chakravarti",
                        "Prabhupada",
                        "Vaisnava",
                        "Sarasvati",
                        "Samhita",
                        "Bhartiya",
                        "Siksastakam",
                        "Vijayanagara",
                        "Rashtrakuta",
                        "Kakatiya",
                        "Chalukya",
                        "Hoysala",
                        "Nirvana",
                        "Bhudavis",
                        "Ghina",
                        "Shivalaya",
                        "Aitlaya",
                        "Prasad",
                        "Subramanyam",
                        "Lalvwani",
                        "Mahavir",
                        "Kundakunda",
                        "Umaswati",
                        "Ajivikas",
                        "Parasanghate",
                        "Acharya",
                        "Yashovijay"
                    },
                    AdmiralLastNames = new List<string>()
                    {
                        "Paraghate",
                        "Bodhisvaha",
                        "Paramiddha",
                        "Visyar-Maitreya",
                        "Moksha-Sanyas",
                        "Daivasur-Sampdvibhag",
                        "Shradhatray",
                        "Puranas",
                        "Chakravarti",
                        "Upadhye",
                        "Sutrakrtangsutram",
                        "Isibhasiyaim",
                        "Uttarajjhaya",
                        "Dasaveyaliya",
                        "Suyadanga",
                        "Ayaranga",
                        "Cirantanacarya",
                        "Acarangsutram",
                        "Mahasvati",
                        "Jambuvijayji",
                        "Bhadrabahuswami",
                        "Padmarajiah",
                        "Sahitya",
                        "Gyanpith",
                        "Samaysara",
                        "Dhyanastav",
                        "Dharanendra"
                    },
                    Color = Color.FromArgb(72, 54, 0)
                },
                new Race()
                {
                    Id = (int)RaceType.Xeloss,
                    Name = "Xeloss",
                    Description = "Much is said about the fanatical religion of the Xeloss, a species that escaped their home planet during the collapse of the Magellan Universe.  The Xeloss are ruthless, and do not hesitate to murder others under their god's name.  Not only do they attack outsiders with their psychic powers, but also they have aptly shown that the individual will sacrifice his basic instinct for survival for the good of their god.  This has added to their already bloody reputation.  No species wishes to readily meet the Xeloss, and they are absolutely correct in their thoughts.",
                    SocietyType = SocietyType.Totalism,
                    HomeAtmosphere = new Atmosphere()
                    {
                        H2 = 0,
                        Cl2 = 1,
                        CO2 = 1,
                        O2 = 0,
                        N2 = 3,
                        CH4 = 0,
                        H2O = 0
                    },
                    HomeGravity = 2,
                    HomeTemperature = 230,
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
                    },
                    AdmiralAbilities = new List<AdmiralRacialAbility>()
                    {
                        AdmiralRacialAbility.XenophobicFanatic,
                        AdmiralRacialAbility.MentalGiant,
                        AdmiralRacialAbility.IrrationalTactics
                    },
                    AdmiralFirstNames = new List<string>()
                    {
                        "Anedjib",
                        "Bakare",
                        "Djoser",
                        "Khaba",
                        "Khefren",
                        "Anedjibka",
                        "Anubis",
                        "Nebka",
                        "Niuserre",
                        "Sekhen",
                        "Thoth",
                        "Unas",
                        "Ptah",
                        "Geb",
                        "Nut",
                        "Shu",
                        "Horus",
                        "Osiris",
                        "Isis",
                        "Nepthys",
                        "Seth",
                        "Uto",
                        "Amun",
                        "Teshut",
                        "Re",
                        "Apophis",
                        "Djedefhor",
                        "Usermaatre",
                        "Seputula",
                        "Muthed",
                        "Hederased",
                        "Rakhbat",
                        "Qeteth",
                        "Vitesh",
                        "Menkauhor"
                    },
                    AdmiralLastNames = new List<string>()
                    {
                        "Selket",
                        "Wepwawet",
                        "Sekhbat-Atheb-Sed",
                        "Merenre",
                        "Sekhmosis",
                        "Thutmosis",
                        "Hatshepsut",
                        "Qesatchela",
                        "Setepenre",
                        "Kheops",
                        "Seshat",
                        "Yinepu",
                        "Serekh",
                        "Selkheops",
                        "Nekhbet",
                        "Setem",
                        "Djehuty",
                        "Atman",
                        "Harsomtus",
                        "Heb-Sed",
                        "Hetepheres",
                        "Netjer",
                        "Tephnut",
                        "Maat",
                        "Taweret",
                        "Qetesh"
                    },
                    Color = Color.FromArgb(90, 0, 0)
                },
                new Race()
                {
                    Id = (int)RaceType.Xerusian,
                    Name = "Xerusian",
                    Description = "Xerusians boast an ancient and traditional military tradition.  Though their troops are small in number, they have always remained the utmost elite forces of the galaxy.  In addition, Xerusians have always had great interest in the matter-energy sciences, which are immediately adapted to military weapons and technologies, and have researched these sciences extensively.  The only things that stand between them and the domination of the galaxy are the inefficient workings of their bureaucracy and the excessive amount of energy lost in the internal conflicts within their machinery.  It should be noted that their extensive battle experiences with the Xeloss, has resulted that they are the only race that have a method of stopping the Xeloss psychic attacks.",
                    SocietyType = SocietyType.Totalism,
                    HomeAtmosphere = new Atmosphere()
                    {
                        H2 = 0,
                        Cl2 = 1,
                        CO2 = 0,
                        O2 = 0,
                        N2 = 4,
                        CH4 = 0,
                        H2O = 0
                    },
                    HomeGravity = 1.4,
                    HomeTemperature = 280,
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
                    },
                    AdmiralAbilities = new List<AdmiralRacialAbility>()
                    {
                        AdmiralRacialAbility.ArtifactCrystal,
                        AdmiralRacialAbility.TacticalGenius,
                        AdmiralRacialAbility.Blitzkreig
                    },
                    AdmiralFirstNames = new List<string>()
                    {
                        "Ja'rod",
                        "P'Etor",
                        "Dubelmo",
                        "Tullj",
                        "Toq",
                        "Zu'basha",
                        "B'etlr",
                        "Kahlest",
                        "Mel'zek",
                        "Nok'zek'bash",
                        "Nok'tam",
                        "Be'lesh",
                        "Kaetak",
                        "Nil'bash",
                        "DubelmoHchugh",
                        "Hogh",
                        "Qu'",
                        "Ba'el",
                        "NgoDHommey",
                        "Lursa",
                        "Ja'bash",
                        "Valkris",
                        "Tel'Ehleyr",
                        "MajDaq",
                        "Khon",
                        "Kow'Hel",
                        "Korgh"
                    },
                    AdmiralLastNames = new List<string>()
                    {
                        "Turas",
                        "MajQa'",
                        "Ku'",
                        "Pehturas'",
                        "HeHDaq",
                        "HemHesH",
                        "QongmeH",
                        "HeghmeH",
                        "SoQlIj",
                        "Sagh",
                        "Kowron",
                        "Kell",
                        "Azetdur",
                        "Gorkon",
                        "K'Ehleyr",
                        "Khon'Tihl",
                        "Tel'Peh",
                        "J'Dtan",
                        "K'mpec"
                    },
                    Color = Color.FromArgb(0, 52, 46)
                },
                new Race()
                {
                    Id = (int)RaceType.Xesperados,
                    Name = "Xesperados",
                    Description = "Like the Xeloss or Xerusian, the Xesperados race was a group of military species that escaped the Magellan Galaxy during its collapse.  In their wanderings throughout space, they have been joined by other military species and leaders of rebel races, making the Xesperados an impressive force throughout the galaxy.  The merger of many different species is handicapped by potential problems such as the threat of spies from other races and the complex process of expanding life support capable of sustaining the entire group.  But their open minds and universal acceptance has become a great stimulant to the progress of science, and they are continuing to attract talented researchers of all species.",
                    SocietyType = SocietyType.Personalism,
                    HomeAtmosphere = new Atmosphere()
                    {
                        H2 = 0,
                        Cl2 = 0,
                        CO2 = 0,
                        O2 = 0,
                        N2 = 0,
                        CH4 = 0,
                        H2O = 0
                    },
                    HomeGravity = 1,
                    HomeTemperature = 300,
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
                    },
                    AdmiralAbilities = new List<AdmiralRacialAbility>()
                    {
                        AdmiralRacialAbility.DefensiveMatrix,
                        AdmiralRacialAbility.PsychicProgenitor,
                        AdmiralRacialAbility.ArtifactCoolingEngine
                    },
                    AdmiralNameStyle = AdmiralNameStyle.Xesperados,
                    Color = Color.FromArgb(62, 62, 0)
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
                    Upkeep = 1.8
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
                    Upkeep = 3.24
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
                    Upkeep = 5.83
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
                    Upkeep = 10.50
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
                    Upkeep = 18.90
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
                    Upkeep = 34.01
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
                    Upkeep = 61.22
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
                    Upkeep = 110.20
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
                    Upkeep = 198.36
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
                    Upkeep = 357.05
                },
            };
        }
        private void UseDefaultSpyActions()
        {
            SpyActions = new List<SpyAction>()
            {
                new SpyAction()
                {
                    Id = (int)SpyId.GeneralInformationGathering,
                    Name = "General Information Gathering",
                    Description = "The goal of this mission is to collect general information. You can easily conduct number of operations such as the collection of basic statistics or analysis of the status overall without creating any disturbance. This level of information gathering is often considered common and innocuous, thus it rarely becomes a target of anti-espionage organizations.",
                    Type = SpyType.Acceptable,
                    Cost = 1000,
                    Difficulty = 0
                },
                new SpyAction()
                {
                    Id = (int)SpyId.DetailedInformationGathering,
                    Name = "Detailed Information Gathering",
                    Description = "Processing is what makes the collected information useful. By analyzing large amount of information and extracting useful ones out of the mass, the processed data becomes readily useful.",
                    Type = SpyType.Acceptable,
                    Cost = 50000,
                    Difficulty = 0,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1210
                        }
                    }
                },
                new SpyAction()
                {
                    Id = (int)SpyId.StealSecretInfo,
                    Name = "Steal Secret Info",
                    Description = "This operation focuses on information that are usually hard to obtain by ordinary means. It involves many different methods and means to obtain such classified information, and risky choices are employed at times as well. It can cause diplomatic disturbances, should the operation fail and go detected - rarely it becomes an issue for an elongated period, however.",
                    Type = SpyType.Ordinary,
                    Cost = 100000,
                    Difficulty = 200,
                   Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1116
                        }
                    }
                },
                new SpyAction()
                {
                    Id = (int)SpyId.ComputerVirusInfiltration,
                    Name = "Computer Virus Infiltration",
                    Description = "This operation involves infiltrating hostile computer virus in the enemy computer network system. It will bring about huge damage, should it succeed.",
                    Type = SpyType.Ordinary,
                    Cost = 100000,
                    Difficulty = 50,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1116
                        }
                    }
                },
                new SpyAction()
                {
                    Id = (int)SpyId.DevastatingNetworkWorm,
                    Name = "Devastating Network Worm",
                    Description = "This operation involves infiltrating devastating network worm in enemy network system. If the target network system has inferior Information Network Ecosystem than yours, this worm can cripple the system overall and cause chronic damage as well.",
                    Type = SpyType.Hostile,
                    Cost = 100000,
                    Difficulty = 150,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1222
                        }
                    }
                },
                new SpyAction()
                {
                    Id = (int)SpyId.Sabotage,
                    Name = "Sabotage",
                    Description = "Your spy will incite sabotage in the industries of the target. Upon success, the enemy will have to spend massive resource only to build up the lost industries, during which the loss of productive force is crippling.",
                    Type = SpyType.Hostile,
                    Cost = 30000,
                    Difficulty = 100
                },
                new SpyAction()
                {
                    Id = (int)SpyId.InciteRiot,
                    Name = "Incite Riot",
                    Description = "Your spy will attempt a powerful psi attack upon the population of target planet. With this psi attack, your spy will cause chaos and disturbance. Upon success, this will inflict heavier damage than Sabotage Operation. The real beauty of this operation is that you are manipulating your enemy to attack the very same one.",
                    Type = SpyType.Hostile,
                    Cost = 70000,
                    Difficulty = 140,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1414
                        }
                    }
                },
                new SpyAction()
                {
                    Id = (int)SpyId.StealCommonTechnology,
                    Name = "Steal Technology",
                    Description = "This operation focuses on stealing the cutting-edge technology that you don't currently own. This won't be easy but it's one of the operations that focuses on fortifying yourself than undermining others.",
                    Type = SpyType.Ordinary,
                    Cost = 50000,
                    Difficulty = 150,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1214
                        }
                    }
                },
                new SpyAction()
                {
                    Id = (int)SpyId.ArtificialDisease,
                    Name = "Artificial Disease",
                    Description = "Your spy will spread the new microbe secretly developed by your toxicologists. This will inflict crippling damage to any planet, and it is one of the most effective methods to devastate enemy. When it is detected, however, the employment of such tactic along with the development of such microbe will cause severe diplomatic problem. For this reason, this weapon should be reserved to for the last resort.",
                    Type = SpyType.Atrocious,
                    Cost = 100000,
                    Difficulty = 100,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1416
                        }
                    }
                },
                new SpyAction()
                {
                    Id = (int)SpyId.RedDeath,
                    Name = "Red Death",
                    Description = "This is by far the most damaging microbe ever created. This already deadly artificial germ has been cultured in the harshest environment for accelerated evolution. The end product far out-damages the Artificial Disease. This can effectively neutralize any major planet, inflicting deadly damage.",
                    Type = SpyType.Atrocious,
                    Cost = 200000,
                    Difficulty = 150,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1418
                        }
                    }
                },
                new SpyAction()
                {
                    Id = (int)SpyId.StrikeBase,
                    Name = "Strike Base",
                    Description = "You attempt to destroy or neutralize the enemy military base by infiltrating saboteur or commandos. Usually this operation precedes full-scale planetary invasion. Even with such a low success ratio, this is one of the most attractive means available due to the high cost-efficiency.",
                    Type = SpyType.Hostile,
                    Cost = 70000,
                    Difficulty = 150
                },
                new SpyAction()
                {
                    Id = (int)SpyId.MeteorStrike,
                    Name = "Meteor Strike",
                    Description = "You deflect one of the asteroids out of the orbit, and cause that asteroid to crash on enemy planet. This method is relatively safe, especially for a devastating attack as this. Still, many planets have planetary defense system on respective orbits, which will intercept the free-falling meteor. This attack will prove but futile against well defended planet for this reason.",
                    Type = SpyType.Hostile,
                    Cost = 20000,
                    Difficulty = 50,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1316
                        }
                    }
                },
                new SpyAction()
                {
                    Id = (int)SpyId.EmpStorm,
                    Name = "EMP Storm",
                    Description = "You send out a satelite equipped with an anti-matter warhead, which can cause massive EMP storm on the ionosphere of the target planet. Upon the explosion of the warhead, every electronic device breaks down for a certain period of time, practically paralyzing the whole planet for the duration.",
                    Type = SpyType.Hostile,
                    Cost = 100000,
                    Difficulty = 200,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1322
                        }
                    }
                },
                new SpyAction()
                {
                    Id = (int)SpyId.StellarBombardment,
                    Name = "Stellar Bombardment",
                    Description = "This is Meteor Strike on a larger scale. This massive attack can send a crippling blow to a planet with weak defensive measures. Even the best-defended planet will have hard time defending against the number of asteroids dashing at them full speed ahead.",
                    Type = SpyType.Hostile,
                    Cost = 300000,
                    Difficulty = 150,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1323
                        }
                    }
                },
                new SpyAction()
                {
                    Id = (int)SpyId.Assassination,
                    Name = "Assassination",
                    Description = "You send out an agent to assassinate important figure in the enemy planet. As an old adage has it, an army of lions led by a lamb never defeats an army of lambs led by a lion. Such is the power of leadership, and disposing a capable leader can be more devastating than decimating the whole fleet of battleships.",
                    Type = SpyType.Hostile,
                    Cost = 50000,
                    Difficulty = 200
                }
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
                    TechLevel = 11,
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
                    Name = "Optical Computer",
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
                    Id = 1223,
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
                    Name = "Plasma Mechanics",
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
                    Id = 1320,
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
                    Description = "As physics had approached the micro level through quantum physics, biological sciences began to pay closer attention to the micro level of life, genetics.  As the small universe of this world was slowly discovered, some mysteries of life were solved, and at the same time, many new mysteries presented themselves.  The most basic genetic research became the foundations for new areas of study, such as genetic engineering, genetic therapy, and the genetic computer.  Genetics also influenced other parts of biology, and as the field of genetics developed further, so did the other fields of life sciences.",
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
                    Name = "Ascension",
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
                }
            };
        }
        private void UseDefaultWeapons()
        {
            Weapons = new List<Weapon>()
            {
                new Weapon()
                {
                    Id = 6101,
                    Name = "Laser",
                    Description = "The Laser is the basic idea behind all beam weapons. The focusing of light through the correct crystal matrix can make it into an efficient tool or a deadly weapon. This is another of the weapons that all races in the Empire seem to possess. Although not as powerful as some later weapons, the laser is an important weapon for all starting rulers.",
                    TechLevel = 1,
                    Type = WeaponType.Beam,
                    AttackRating = 100,
                    DamageRoll = 2,
                    DamageDice = 4,
                    Space = 25,
                    CoolingTime = 50,
                    Range = 1000,
                    AngleOfFire = 60,
                    Speed = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1302
                        }
                    }
                },
                new Weapon()
                {
                    Id = 6102,
                    Name = "Plasma Beam",
                    Description = "The Plasma Beam is a uses the your knowledge of the differing states of matter to form and project a focused ray of plasma at your enemies. When this matter reaches its target, the reaction is quite deadly. This makes the plasma beam quite effective against enemies who have not yet developed technology which protects from this type of attack.",
                    TechLevel = 2,
                    Type = WeaponType.Beam,
                    AttackRating = 120,
                    DamageRoll = 3,
                    DamageDice = 4,
                    Space = 28,
                    CoolingTime = 70,
                    Range = 1000,
                    AngleOfFire = 60,
                    Speed = 15,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1307
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ShieldOverheat
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.AdditionalDamageToBioArmor,
                            ModifierType = ModifierType.Proportional,
                            Amount = 175
                        }
                    }
                },
                new Weapon()
                {
                    Id = 6103,
                    Name = "Crystal Laser",
                    Description = "The Crystal Laser uses your knowledge of crystal manipulation to purify the focusing component in your laser weaponry. This newly focused beam is more deadly and more accurate than its predecessor. This new lens does not cool as fast as the originals though, which means you cannot fire as often without risking dangerous temperatures.",
                    TechLevel = 2,
                    Type = WeaponType.Beam,
                    AttackRating = 120,
                    DamageRoll = 4,
                    DamageDice = 4,
                    Space = 30,
                    CoolingTime = 60,
                    Range = 1000,
                    AngleOfFire = 60,
                    Speed = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1318
                        }
                    }
                },
                new Weapon()
                {
                    Id = 6104,
                    Name = "Graviton Beam",
                    Description = "The Graviton Beam is actually a focused projection of many graviton particles. As this projection moves it gathers both speed and mass through attraction to objects in its path. This means that when it hits it has much speed and mass, this, along with the actual effect of the particles on the target itself allow this beam to crush your enemies.",
                    TechLevel = 3,
                    Type = WeaponType.Beam,
                    AttackRating = 144,
                    DamageRoll = 4,
                    DamageDice = 4,
                    Space = 29,
                    CoolingTime = 55,
                    Range = 1000,
                    AngleOfFire = 60,
                    Speed = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1320
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ShieldPiercing,
                            ModifierType = ModifierType.Absolute,
                            Amount = 25
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ArmorPiercing,
                            ModifierType = ModifierType.Absolute,
                            Amount = 25
                        }
                    }
                },
                new Weapon()
                {
                    Id = 6105,
                    Name = "Psi Blaster",
                    Description = "The Psi Blaster is a weapon that can go from merely dangerous to outright deadly if used by the correct races. Powered by either a races own ability or by machines cleverly created for the purpose, the psi blaster destroys your enemies with forces that cannot be seen but only felt.",
                    TechLevel = 3,
                    Type = WeaponType.Beam,
                    AttackRating = 144,
                    DamageRoll = 4,
                    DamageDice = 7,
                    Space = 35,
                    CoolingTime = 72,
                    Range = 1000,
                    AngleOfFire = 60,
                    Speed = 15,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1419
                        },
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Race,
                            Value = 7
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Psi
                        }
                    }
                },
                new Weapon()
                {
                    Id = 6106,
                    Name = "Neutron Blaster",
                    Description = "The Neutron Blaster uses the understanding of sub atomic physics to assemble neutrons which are tailored to rip through hull components. These are then projected along the path of a guiding laser to the enemy. Upon contact, they interact with the hull and can often cripple if not destroy an enemy ship.",
                    TechLevel = 3,
                    Type = WeaponType.Beam,
                    AttackRating = 144,
                    DamageRoll = 5,
                    DamageDice = 5,
                    Space = 38,
                    CoolingTime = 60,
                    Range = 1000,
                    AngleOfFire = 60,
                    Speed = 15,
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
                            Type = FleetEffectType.AdditionalDamageToBioArmor,
                            ModifierType = ModifierType.Proportional,
                            Amount = 200
                        }
                    }
                },
                new Weapon()
                {
                    Id = 6107,
                    Name = "Phasor",
                    Description = "The Phasor is a deadly weapon that can alter the way in which space-time and the target interact. This can cause a high range of effects, from almost no damage, to complete destruction/dissolution of the component materials of the effected target. The phasor is perhaps on of the better known weapons in the current fighting happening within the Empire.",
                    TechLevel = 5,
                    Type = WeaponType.Beam,
                    AttackRating = 250,
                    DamageRoll = 12,
                    DamageDice = 5,
                    Space = 50,
                    CoolingTime = 40,
                    Range = 1000,
                    AngleOfFire = 180,
                    Speed = 5,
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
                            Type = FleetEffectType.ShieldOverheat
                        }
                    }
                },
                new Weapon()
                {
                    Id = 6108,
                    Name = "Tachyon Beam",
                    Description = "The Tachyon Beam is a force which alters the probability of the target structure. The desired effect is to cause all of the particles of the structure to \"randomly\" repel each other. If this can be achieved for even an instant it can cause the effected area to explode as all of the particles attempt to put distance between their neighbors.",
                    TechLevel = 4,
                    Type = WeaponType.Beam,
                    AttackRating = 273,
                    DamageRoll = 5,
                    DamageDice = 6,
                    Space = 40,
                    CoolingTime = 54,
                    Range = 1000,
                    AngleOfFire = 60,
                    Speed = 10,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1219
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ArmorPiercing,
                            ModifierType = ModifierType.Absolute,
                            Amount = 45
                        }
                    }
                },
                new Weapon()
                {
                    Id = 6109,
                    Name = "Oscillation of Dissonance",
                    Description = "The Oscillation of Dissonance is the most deadly Beam weapon to be invented. It has the power to rip ships to pieces faster than any other beam weapon. One of the more deadly aspects of this weapon is its ability to ignore almost all armor that your enemies may have on their ships.",
                    TechLevel = 5,
                    Type = WeaponType.Beam,
                    AttackRating = 250,
                    DamageRoll = 6,
                    DamageDice = 8,
                    Space = 50,
                    CoolingTime = 80,
                    Range = 1000,
                    AngleOfFire = 360,
                    Speed = 15,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1224
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ArmorPiercing,
                            ModifierType = ModifierType.Absolute,
                            Amount = 95
                        }
                    }
                },
                new Weapon()
                {
                    Id = 6110,
                    Name = "Psi Storm Launcher",
                    Description = "The Psi Storm Launcher is a weapon often feared not just for the obvious damage it can do to matter, but also for the effects upon minds that survive it. This weapon creates a temporary vortex to the fourth dimension allowing psionic apparitions to manifest in our physical space. They do not always choose to do great damage, but the mental storms this living thoughts create can cause the bravest commanders to retreat.",
                    TechLevel = 5,
                    Type = WeaponType.Beam,
                    AttackRating = 200,
                    DamageRoll = 7,
                    DamageDice = 8,
                    Space = 38,
                    CoolingTime = 72,
                    Range = 1200,
                    AngleOfFire = 120,
                    Speed = 15,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1424
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
                            Type = FleetEffectType.Psi
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ShieldDistortion,
                            ModifierType = ModifierType.Absolute,
                            Amount = 25
                        }
                    }
                },
                new Weapon()
                {
                    Id = 6201,
                    Name = "Nuclear Missile",
                    Description = "The Nuclear Missile was in the past one of the most feared weapons known. However, as races leave their first planet, they inevitably discover more powerful means of destruction. The nuclear missile is still a great force for destruction, and is in fact the one missile type that all races seem to have when they first reach the stars.",
                    TechLevel = 1,
                    Type = WeaponType.Missile,
                    AttackRating = 100,
                    DamageRoll = 5,
                    DamageDice = 5,
                    Space = 20,
                    CoolingTime = 200,
                    Range = 1500,
                    AngleOfFire = 180,
                    Speed = 20,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1301
                        }
                    }
                },
                new Weapon()
                {
                    Id = 6202,
                    Name = "Fusion Missile",
                    Description = "The Fusion Missile is a logical progression from the nuclear missile. As a race refines their science, they are able to truly utilize fusion power efficiently. This power can be used for many things. One of these uses is to make more deadly weapons. This is the second type of missile that many races use as its power is a simple upgrade to that of the nuclear missile that proves so effective early in a powers career.",
                    TechLevel = 2,
                    Type = WeaponType.Missile,
                    AttackRating = 120,
                    DamageRoll = 8,
                    DamageDice = 10,
                    Space = 33,
                    CoolingTime = 300,
                    Range = 1500,
                    AngleOfFire = 180,
                    Speed = 20,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1306
                        }
                    }
                },
                new Weapon()
                {
                    Id = 6203,
                    Name = "Nova Torpedo",
                    Description = "The Nova Torpedo is the final stage of the evolution of the nuclear missile. This missile takes initial science of fusion to a new height. With the use of each of these missiles a temporary star is created out of the component atoms of the target. This missile has the capacity for more damage than all but two other weapons. This force can be of great help in a battle, but be warned - the weapon takes a long time to prepare before it can be used again.",
                    TechLevel = 4,
                    Type = WeaponType.Missile,
                    AttackRating = 172,
                    DamageRoll = 18,
                    DamageDice = 20,
                    Space = 40,
                    CoolingTime = 450,
                    Range = 1500,
                    AngleOfFire = 180,
                    Speed = 20,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1327
                        }
                    }
                },
                new Weapon()
                {
                    Id = 6204,
                    Name = "Kamikaze Conscious Torpedo",
                    Description = "The Kamikaze Torpedo has caused demonstrations upon numerous words. This weapon uses the very force that lies behind all living things to track your enemy. Of course, this link is rarely one way, so when this weapon is activated, those who can notice are often said to cringe at the shock this device sends thru the unconsciousness. Some people have begun to argue that this abuse will have repercussions, but few rulers head this. The weapon is too efficient to avoid using it because it gives some sensitive people headaches.",
                    TechLevel = 3,
                    Type = WeaponType.Missile,
                    AttackRating = 200,
                    DamageRoll = 8,
                    DamageDice = 10,
                    Space = 28,
                    CoolingTime = 330,
                    Range = 1500,
                    AngleOfFire = 180,
                    Speed = 20,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1115
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.Psi
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.AdditionalDamageToBioArmor,
                            ModifierType = ModifierType.Proportional,
                            Amount = 200
                        }
                    }
                },
                new Weapon()
                {
                    Id = 6205,
                    Name = "Death Spore",
                    Description = "The Death Spore might not sound like the most threatening weapon. However, this device has it uses. In this time when more and more races are turning to artificial life forms for many things, the death spore is merely the destructive use of this trend. This weapon is tailored to damage your enemies, and woe be to the enemy who tries to use any sort of living shield to protect themselves, as this life form will use such shielding to fuel itself to even greater destructive heights.",
                    TechLevel = 3,
                    Type = WeaponType.Missile,
                    AttackRating = 172,
                    DamageRoll = 7,
                    DamageDice = 7,
                    Space = 30,
                    CoolingTime = 280,
                    Range = 1500,
                    AngleOfFire = 180,
                    Speed = 20,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1416
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.AdditionalDamageToBioArmor,
                            ModifierType = ModifierType.Proportional,
                            Amount = 700
                        }
                    }
                },
                new Weapon()
                {
                    Id = 6206,
                    Name = "Anti-Matter Missile",
                    Description = "The Antimatter Missile seems simple enough. As one developer said while working on this project, \"just imagine what this material will do when it comes into contact with regular matter, without the proper controls. In fact this scientist witnessed it first hand when a containment mistake was made during prototype construction. It goes without saying that there was not enough of him for a proper burial.",
                    TechLevel = 3,
                    Type = WeaponType.Missile,
                    AttackRating = 144,
                    DamageRoll = 10,
                    DamageDice = 20,
                    Space = 38,
                    CoolingTime = 400,
                    Range = 1500,
                    AngleOfFire = 180,
                    Speed = 20,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1322
                        }
                    }
                },
                new Weapon()
                {
                    Id = 6207,
                    Name = "Reflexium Missile",
                    Description = "The Reflexium Missile is the use of an interesting property of reflexium. For some reason any interaction between this metal and most shielding causes great stress to the powering component of shields. This makes this weapon quite effective against those who utilize shielding to a great extent. Of course, the effect of a strong material impacting an object at high speed is nothing to laugh at either.",
                    TechLevel = 4,
                    Type = WeaponType.Missile,
                    AttackRating = 200,
                    DamageRoll = 12,
                    DamageDice = 20,
                    Space = 40,
                    CoolingTime = 355,
                    Range = 1500,
                    AngleOfFire = 180,
                    Speed = 20,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1330
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ShieldDistortion,
                            ModifierType = ModifierType.Absolute,
                            Amount = 100
                        }
                    }
                },
                new Weapon()
                {
                    Id = 6208,
                    Name = "Psionic Pulse Shocker",
                    Description = "The Psionic Pulse Shocker a deadly method of using psionic forces. This missile is in fact a focusing device for the psionic energies of your crew. They then use their abilities to hasten the entropy within your enemies to dangerous levels. If the enemy is unprepared, this can easily be their undoing.",
                    TechLevel = 4,
                    Type = WeaponType.Missile,
                    AttackRating = 172,
                    DamageRoll = 10,
                    DamageDice = 14,
                    Space = 32,
                    CoolingTime = 240,
                    Range = 1700,
                    AngleOfFire = 180,
                    Speed = 20,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1422
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
                            Type = FleetEffectType.Psi
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ShieldDistortion,
                            ModifierType = ModifierType.Absolute,
                            Amount = 75
                        }
                    }
                },
                new Weapon()
                {
                    Id = 6209,
                    Name = "Time-Wake Homing Missile",
                    Description = "The Time-Wake Homing Missile is a tricky weapon to figure out. Whether it is in fact tracking its enemies future path, or if it tracks its own future to determine the best path, this missile can be quite deadly and effective. There are even reports of it relocating itself through space/time and thus penetrating enemy shields and armor.",
                    TechLevel = 5,
                    Type = WeaponType.Missile,
                    AttackRating = 250,
                    DamageRoll = 8,
                    DamageDice = 21,
                    Space = 50,
                    CoolingTime = 275,
                    Range = 1500,
                    AngleOfFire = 180,
                    Speed = 20,
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
                            Type = FleetEffectType.ArmorPiercing,
                            ModifierType = ModifierType.Absolute,
                            Amount = 30
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ShieldDistortion,
                            ModifierType = ModifierType.Absolute,
                            Amount = 75
                        }
                    }
                },
                new Weapon()
                {
                    Id = 6210,
                    Name = "Homing Black Hole",
                    Description = "The Homing Black Hole is easily the single most powerful weapon ever created. This missile calls into being a temporary and small black hole, which can destroy fleets of weak vessels and cripple some of the largest ships in existence. The only drawback to this weapon is that it takes a long time to bring the next shot online.",
                    TechLevel = 5,
                    Type = WeaponType.Missile,
                    AttackRating = 250,
                    DamageRoll = 40,
                    DamageDice = 36,
                    Space = 100,
                    CoolingTime = 500,
                    Range = 1500,
                    AngleOfFire = 180,
                    Speed = 20,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1335
                        }
                    }
                },
                new Weapon()
                {
                    Id = 6301,
                    Name = "Rail Gun",
                    Description = "The Rail Gun uses the basic principle that species have been using for thousands of Millennia. If you throw something at your enemies fast enough, it will hurt them. While this principle has been refined in other ways, the rail gun is still an effective way to harm your enemy. The fact that you are only accelerating simple matter and not charging it means that this type of weapon is especially useful because it can pass through almost any shields that your enemy may use.",
                    TechLevel = 1,
                    Type = WeaponType.Projectile,
                    AttackRating = 50,
                    DamageRoll = 2,
                    DamageDice = 4,
                    Space = 25,
                    CoolingTime = 30,
                    Range = 800,
                    AngleOfFire = 60,
                    Speed = 5,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1305
                        }
                    }
                },
                new Weapon()
                {
                    Id = 6302,
                    Name = "Gatling Rail Gun",
                    Description = "The Gatling Rail Gun is a more effective version of the rail gun. It uses an understanding of inertia in order to direct a larger amount of matter toward your enemy. This means that when you hit you should usually do a greater amount of damage to your enemy. The fact that you have greater control also means there is less heat built up during the shot, so you can fire more often.",
                    TechLevel = 2,
                    Type = WeaponType.Projectile,
                    AttackRating = 60,
                    DamageRoll = 3,
                    DamageDice = 4,
                    Space = 40,
                    CoolingTime = 15,
                    Range = 800,
                    AngleOfFire = 60,
                    Speed = 5,
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
                            Type = FleetEffectType.ShieldPiercing,
                            ModifierType = ModifierType.Absolute,
                            Amount = 100
                        }
                    }
                },
                new Weapon()
                {
                    Id = 6303,
                    Name = "Mass Driver",
                    Description = "The Mass Driver uses a strong electromagnetic field to propel a projectile towards your enemy at amazing speeds. This weapon does not have as much restraint as the gatling rail gun, however the greater speed and mass allow this weapon to be somewhat more effective both in hitting your enemy and in causing damage.",
                    TechLevel = 2,
                    Type = WeaponType.Projectile,
                    AttackRating = 90,
                    DamageRoll = 4,
                    DamageDice = 5,
                    Space = 30,
                    CoolingTime = 45,
                    Range = 800,
                    AngleOfFire = 60,
                    Speed = 7,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1319
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ShieldPiercing,
                            ModifierType = ModifierType.Absolute,
                            Amount = 100
                        }
                    }
                },
                new Weapon()
                {
                    Id = 6304,
                    Name = "Gauss Cannon",
                    Description = "The Gauss Cannon uses compressed material for the projectile. This material has much greater density and mass, so the force is exerts on its target is much more focused. This means that not only will this weapon pass through shields, but it will even penetrate a portion of your enemies armor.",
                    TechLevel = 4,
                    Type = WeaponType.Projectile,
                    AttackRating = 90,
                    DamageRoll = 6,
                    DamageDice = 8,
                    Space = 45,
                    CoolingTime = 30,
                    Range = 600,
                    AngleOfFire = 60,
                    Speed = 5,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1319
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ShieldPiercing,
                            ModifierType = ModifierType.Absolute,
                            Amount = 100
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ArmorPiercing,
                            ModifierType = ModifierType.Absolute,
                            Amount = 15
                        }
                    }
                },
                new Weapon()
                {
                    Id = 6305,
                    Name = "Anti-Matter Cannon",
                    Description = "The Anti-Matter Cannon work on a combination of the principles used to create the rail gun and the anti-matter missile. Firing shots as though they were normal rail gun charges, the projectiles fired are much more deadly, and so the overall weapon becomes one of the most efficient projectile weapons available.",
                    TechLevel = 3,
                    Type = WeaponType.Projectile,
                    AttackRating = 120,
                    DamageRoll = 6,
                    DamageDice = 6,
                    Space = 50,
                    CoolingTime = 30,
                    Range = 800,
                    AngleOfFire = 60,
                    Speed = 4,
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
                            Type = FleetEffectType.ShieldPiercing,
                            ModifierType = ModifierType.Absolute,
                            Amount = 100
                        }
                    }
                },
                new Weapon()
                {
                    Id = 6306,
                    Name = "Autofire Gauss Cannon",
                    Description = "The Autofire Gauss Cannon improves on the gauss cannon in one significant manner, speed. By using knowledge of space/time you are able to remove the heat from this weapon much faster and thus fire it at a greatly increased rate. It also has a slightly increased ability to make it past enemy defenses.",
                    TechLevel = 5,
                    Type = WeaponType.Projectile,
                    AttackRating = 90,
                    DamageRoll = 6,
                    DamageDice = 8,
                    Space = 50,
                    CoolingTime = 15,
                    Range = 600,
                    AngleOfFire = 60,
                    Speed = 3,
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
                            Type = FleetEffectType.ShieldPiercing,
                            ModifierType = ModifierType.Absolute,
                            Amount = 100
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ArmorPiercing,
                            ModifierType = ModifierType.Absolute,
                            Amount = 15
                        }
                    }
                },
                new Weapon()
                {
                    Id = 6307,
                    Name = "Distortion Blaster",
                    Description = "The Distortion Blaster is one of the most deadly weapons ever to be created. One shot from this monstrous weapon can out-damage some of the weaker weapons. Of course, it fires at speeds almost ten times faster than many missiles, which is why it is so feared. This weapon works by projecting small pieces of material from other dimensions into your enemies midst. These particles have a horrible effect on the enemy as they try to make anything they contact conform to their dimensional laws.",
                    TechLevel = 5,
                    Type = WeaponType.Projectile,
                    AttackRating = 144,
                    DamageRoll = 10,
                    DamageDice = 12,
                    Space = 100,
                    CoolingTime = 30,
                    Range = 800,
                    AngleOfFire = 60,
                    Speed = 3,
                    Prerequisites = new List<PlayerPrerequisite>()
                    {
                        new PlayerPrerequisite()
                        {
                            Type = PrerequisiteType.Tech,
                            Value = 1334
                        }
                    },
                    Effects = new List<FleetEffect>()
                    {
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ShieldPiercing,
                            ModifierType = ModifierType.Absolute,
                            Amount = 150
                        },
                        new FleetEffect()
                        {
                            Type = FleetEffectType.ArmorPiercing,
                            ModifierType = ModifierType.Absolute,
                            Amount = 40
                        }
                    }
                }
            };
        }

        public ValidateResult Validate()
        {
            ValidateResult result = new ValidateResult();

            List<Entity> entities = new List<Entity>();
            entities.AddRange(Armors);
            entities.AddRange(Computers);
            entities.AddRange(Devices);
            entities.AddRange(Engines);
            entities.AddRange(Events);
            entities.AddRange(Projects);
            entities.AddRange(Races);
            entities.AddRange(Shields);
            entities.AddRange(ShipClasses);
            entities.AddRange(SpyActions);
            entities.AddRange(Techs);
            entities.AddRange(Weapons);

            IEnumerable<int> repeatedIds = entities.GroupBy(x => x.Id).Where(x => x.Count() > 1).Select(x => x.Key);
            if (repeatedIds.Any())
            {
                result.Items.AddRange(repeatedIds.Select(x => new ValidateResult.Item() { Severity = Severity.Warning, Message = $"Id {x} is repeated across all configurable entities." }));
            }

            foreach (Type type in entities.Select(x => x.GetType()).Distinct())
            {
                repeatedIds = entities.Where(x => x.GetType().Equals(type)).GroupBy(x => x.Id).Where(x => x.Count() > 1).Select(x => x.Key);
                if (repeatedIds.Any())
                {
                    result.Items.AddRange(repeatedIds.Select(x => new ValidateResult.Item() { Severity = Severity.Error, Message = $"Id {x} is repeated within {type.Name} entities." }));
                }
            }

            IEnumerable<IPlayerUnlockable> unlockables = entities.Where(x => x.GetType().GetInterfaces().Contains(typeof(IPlayerUnlockable))).Cast<IPlayerUnlockable>();
            IEnumerable<IPlayerUnlockable> unobtainable = unlockables.Where(x => x.Prerequisites.Where(y => y.Type == PrerequisiteType.Tech).Select(y => y.Value).Cast<int>().Except(Techs.Select(z => z.Id)).Any());

            if (unobtainable.Any())
            {
                result.Items.AddRange(unobtainable.Select(x => new ValidateResult.Item() { Severity = Severity.Error, Message = $"{x.GetType().Name} {((Entity)x).Id} requires a tech that is not defined." }));
            }

            return result;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
        }
    }
}
