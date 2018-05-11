using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Universal.Common.Extensions;

namespace Archspace2
{
    public enum SecurityLevel
    {
        Defenseless = 1,
        Loose,
        Wary,
        Alerted,
        Impenetrable
    };

    public class SpecialOperationsCommand
    {
        private int mAlertness;
        public SecurityLevel SecurityLevel { get; set; }
        public int Alertness { 
            get
            {
                return mAlertness;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                mAlertness = value;
            }
        }

        public int SecurityScore
        {
            get
            {
                switch (SecurityLevel)
                {
                    case SecurityLevel.Defenseless:
                        return 0;
                    case SecurityLevel.Loose:
                        return 50;
                    case SecurityLevel.Wary:
                        return 100;
                    case SecurityLevel.Alerted:
                        return 200;
                    case SecurityLevel.Impenetrable:
                        return 400;
                    default:
                        return 0;
                }
            }
        }

        public SpecialOperationsCommand()
        {
            SecurityLevel = SecurityLevel.Defenseless;
            Alertness = 0;
        }

        public Resource CalculateUpkeep(Resource aBalance)
        {
            long upkeep = 0;
            long income = aBalance.ProductionPoint;

            switch (SecurityLevel)
            {
                case SecurityLevel.Loose:
                    upkeep = (int)(income * 2.5 / 100);
                    break;
                case SecurityLevel.Wary:
                    upkeep = (income * 5 / 100);
                    break;
                case SecurityLevel.Alerted:
                    upkeep = (income * 10 / 100);
                    break;
                case SecurityLevel.Impenetrable:
                    upkeep = (income * 20 / 100);
                    break;
                case SecurityLevel.Defenseless:
                default:
                    upkeep = 0;
                    break;
            }

            return new Resource() { ProductionPoint = upkeep };
        }

        public object PerformOperation(SpyAction aSpy, Player target)
        {
            switch((SpyId)aSpy.Id)
            {
                case SpyId.GeneralInformationGathering:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }

        public StealInformationResult GeneralInformationGathering(Player target)
        {
            HashSet<int> infoSet = new HashSet<int>();
            List<string> resultSet = new List<string>();

            int numInfo = 1 + Game.Random.Dice(1, 3);

            do
            {
                int random = Game.Random.Next(1, 8);
                infoSet.Add(random);
            } while (infoSet.Count < numInfo);

            if (infoSet.Contains(1))
            {
                resultSet.Add($"PP : {target.Resource.ProductionPoint}");
            }
            if (infoSet.Contains(2))
            {
                resultSet.Add($"Population : {target.Planets.Sum(x => x.Population)}");
            }
            if (infoSet.Contains(3))
            {
                resultSet.Add($"Concentration Mode : {target.ConcentrationMode.ToString()}");
            }
            if (infoSet.Contains(4))
            {
                resultSet.Add($"Fleets : {target.Fleets.Count}");
            }
            if (infoSet.Contains(5))
            {
                resultSet.Add($"Total Docked Ships : {target.Shipyard.ShipPool.Sum(x => x.Value)}");
            }
            if (infoSet.Contains(6))
            {
                resultSet.Add($"PP Income : {target.Planets.Sum(x => x.CalculateProductionPointPerTurn())}");
            }
            if (infoSet.Contains(7))
            {
                resultSet.Add($"Researched Techs : {target.Techs.Count}");
            }
            if (infoSet.Contains(8))
            {
                resultSet.Add($"Commanders : {target.Admirals.Count}");
            }

            StealInformationResult result = new StealInformationResult();
            result.Information.AddRange(resultSet);

            return result;
        }

        public StealInformationResult DetailedInformationGathering(Player target)
        {
            List<string> resultSet = new List<string>();

            resultSet.AddRange(GeneralInformationGathering(target).Information);

            int detailedInfo = Game.Random.Next(1, 4);

            switch (detailedInfo)
            {
                case 1:
                    {
                        Planet planet = target.Planets.Random();

                        resultSet.Add($"Status of planet {planet.Name}:\nPopulation: {planet.Population}\nResource: {planet.Resource.ToString()}\nBuildings:\nFactory {planet.Infrastructure.Factory}\tResearch Lab {planet.Infrastructure.ResearchLab}\tMilitary Base {planet.Infrastructure.MilitaryBase}");

                        break;
                    }
                case 2:
                    {
                        resultSet.Add($"List of projects:\n{string.Join("\n", target.Projects.Select(x => x.Name))}");
                        break;
                    }
                case 3:
                    {
                        resultSet.Add($"Current Tech Goal: {(target.TargetTech == null ? "None" : target.TargetTech.Name)}");
                        break;
                    }
                case 4:
                    {
                        resultSet.Add($"Researched Techs:\n{string.Join("\n", target.Techs.Select(x => x.Name))}");
                        break;
                    }
            }

            StealInformationResult result = new StealInformationResult();
            result.Information.AddRange(resultSet);

            return result;
        }

        public StealInformationResult StealSecretInfo(Player target)
        {
            StealInformationResult result = new StealInformationResult();
            result.Information.AddRange(DetailedInformationGathering(target).Information);

            switch (Game.Random.Next(1, 2))
            {
                case 1:
                    {
                        result.Information.Add($"Current Fleets ({target.Fleets.Count}):\n" + string.Join("\n", target.Fleets.Select(x => $"{x.GetDisplayName()} {x.ShipDesign.ShipClass.Name} {x.CurrentShipCount}/{x.MaxShipCount}")));

                        break;
                    }
                case 2:
                    {
                        result.Information.Add($"Ship pool ({target.Shipyard.ShipPool.Count}):\n" + string.Join("\n", target.Shipyard.ShipPool.Select(x => $"{x.Key.Name} {x.Key.ShipClass.Name} {x.Value}")));
                        break;
                    }
            }

            return result;
        }

        public ComputerVirusResult ComputerVirusInfiltration(Player target)
        {
            long lostResearch = target.Resource.ResearchPoint * 40 / 100;
            target.Resource.ResearchPoint -= lostResearch;

            return new ComputerVirusResult()
            {
                ResearchPointLost = lostResearch
            };
        }

        public DevastatingNetworkWormResult DevastatingNetworkWorm(Player target)
        {
            long lostResearch = target.Resource.ResearchPoint * 60 / 100;
            target.Resource.ResearchPoint -= lostResearch;

            long lostShipProduction = target.Shipyard.ShipProduction * Game.Random.Next(1, 60) / 100;
            target.Shipyard.ChangeShipProductionInvestment(-lostShipProduction);

            long lostResearchInvestment = target.ResearchInvestment * Game.Random.Next(1, 60) / 100;
            target.ResearchInvestment -= lostResearchInvestment;

            long lostPlanetInvestment = target.PlanetInvestmentPool * Game.Random.Next(1, 60) / 100;
            target.PlanetInvestmentPool -= lostPlanetInvestment;

            return new DevastatingNetworkWormResult()
            {
                ResearchPointLost = lostResearch,
                ShipInvestmentLost = lostShipProduction,
                ResearchInvestmentLost = lostResearchInvestment,
                PlanetInvestmentLost = lostPlanetInvestment
            };
        }

        public SabotageResult Sabotage(Player target)
        {
            long lostFactory = 0;
            long lostInvestment = 0;

            int planetsTargeted = Game.Random.Next(1, target.Planets.Count);

            for (int i = 0; i < planetsTargeted; i++)
            {
                long currentLostFactory;
                long currentLostInvestment;

                Planet planet = target.Planets.Random();
                currentLostFactory = planet.Infrastructure.Factory * Game.Random.Next(1, 20) / 100;
                currentLostInvestment = planet.Investment * Game.Random.Next(1, 40) / 100;

                planet.Infrastructure.Factory -= currentLostFactory;
                planet.Investment -= currentLostInvestment;

                lostFactory += currentLostFactory;
                lostInvestment += currentLostInvestment;
            }

            long currentLostInvestmentPool = target.PlanetInvestmentPool * Game.Random.Next(1, 40) / 100;

            target.PlanetInvestmentPool -= currentLostInvestmentPool;
            lostInvestment += currentLostInvestmentPool;

            return new SabotageResult()
            {
                FactoriesLost = lostFactory,
                InvestmentLost = lostInvestment
            };
        }

        public InciteRiotResult InciteRiot(Player target)
        {
            InciteRiotResult result = new InciteRiotResult();

            int planetsTargeted = Game.Random.Next(1, target.Planets.Count);

            List<Planet> planets = target.Planets.Random(planetsTargeted).ToList();
            foreach (Planet planet in planets)
            {
                InciteRiotResult.PlanetResult planetResult = new InciteRiotResult.PlanetResult();
                planetResult.Id = planet.Id;
                planetResult.Name = planet.Name;

                planetResult.FactoriesLost = planet.Infrastructure.Factory * Game.Random.Next(40, 60) / 100;
                planetResult.ResearchLabsLost = planet.Infrastructure.ResearchLab * Game.Random.Next(40, 60) / 100;
                planetResult.InvestmentLost = planet.Investment;

                planet.Infrastructure.Factory -= planetResult.FactoriesLost;
                planet.Infrastructure.ResearchLab -= planetResult.ResearchLabsLost;
                planet.Investment = 0;

                result.PlanetResults.Add(planetResult);
            }

            return result;
        }

        protected StealTechnologyResult StealTechnology(Player player, Player target, int techLevelCap)
        {
            StealTechnologyResult result = new StealTechnologyResult();

            IEnumerable<Tech> candidates = target.Techs.Intersect(player.Techs).Where(x => x.TechLevel <= techLevelCap);

            if (candidates.Any())
            {
                Tech stolen = candidates.Random();
                result.StolenTech = stolen;
                player.DiscoverTech(stolen);
            }

            return result;
        }

        public StealTechnologyResult StealCommonTechnology(Player player, Player target)
        {
            return StealTechnology(player, target, 4);
        }

        public StealTechnologyResult StealImportantTechnology(Player player, Player target)
        {
            return StealTechnology(player, target, 6);
        }

        public StealTechnologyResult StealSecretTechnology(Player player, Player target)
        {
            return StealTechnology(player, target, 9);
        }

        public ArtificialDiseaseResult ArtificialDisease(Player target)
        {
            ArtificialDiseaseResult result = new ArtificialDiseaseResult();

            int planetsTargeted = Game.Random.Next(1, target.Planets.Count);
            List<Planet> planets = target.Planets.Random(planetsTargeted).ToList();

            foreach (Planet planet in planets)
            {
                ArtificialDiseaseResult.PlanetResult planetResult = new ArtificialDiseaseResult.PlanetResult()
                {
                    Id = planet.Id,
                    Name = planet.Name
                };

                planetResult.PopulationLost = planet.Population * Game.Random.Next(20, 40) / 100;
                planet.Population -= planetResult.PopulationLost;

                result.PlanetResults.Add(planetResult);
            }

            return result;
        }

        public RedDeathResult RedDeath(Player player, Player target)
        {
            RedDeathResult result = new RedDeathResult();

            int planetsTargeted = Game.Random.Next(1, target.Planets.Count);
            List<Planet> planets = target.Planets.Random(planetsTargeted).ToList();

            foreach (Planet planet in planets)
            {
                RedDeathResult.PlanetResult planetResult = new RedDeathResult.PlanetResult()
                {
                    Id = planet.Id,
                    Name = planet.Name
                };

                planetResult.PopulationLost = planet.Population * Game.Random.Next(40, 60) / 100;
                planet.Population -= planetResult.PopulationLost;

                if (player.Traits.Contains(RacialTrait.GeneticEngineeringSpecialist))
                {
                    planet.Attributes.Add(Game.Configuration.PlanetAttributes.Single(x => x.Type == PlanetAttributeType.ObstinateMicrobe));
                    planetResult.GainedObstinateMicrobe = true;
                }

                result.PlanetResults.Add(planetResult);
            }

            return result;
        }

        public StrikeBaseResult StrikeBase(Player target)
        {
            StrikeBaseResult result = new StrikeBaseResult();

            int planetsTargeted = Game.Random.Next(1, target.Planets.Count);
            List<Planet> planets = target.Planets.Random(planetsTargeted).ToList();

            foreach (Planet planet in planets)
            {
                StrikeBaseResult.PlanetResult planetResult = new StrikeBaseResult.PlanetResult()
                {
                    Id = planet.Id,
                    Name = planet.Name
                };

                planetResult.MilitaryBaseLost = planet.Infrastructure.MilitaryBase * Game.Random.Next(40, 80) / 100;
                planet.Infrastructure.MilitaryBase -= planetResult.MilitaryBaseLost;

                result.PlanetResults.Add(planetResult);
            }

            long totalShipsLost = 0;
            if (Game.Random.Next(1, 2) < 2)
            {
                var targetShipDesign = target.Shipyard.ShipPool.Random();
                long currentShipsLost = targetShipDesign.Value * Game.Random.Next(40, 80) / 100;

                target.Shipyard.ShipPool[targetShipDesign.Key] -= currentShipsLost;

                totalShipsLost += currentShipsLost;
            }

            result.ShipsLost = totalShipsLost;

            return result;
        }

        protected AsteroidStrikeResult AsteroidStrike(Player player, Player target, int asteroids)
        {
            AsteroidStrikeResult result = new AsteroidStrikeResult();

            int planetsTargeted = Game.Random.Next(1, target.Planets.Count);
            List<Planet> planets = target.Planets.Random(planetsTargeted).ToList();

            int potency = player.Traits.Contains(RacialTrait.AsteroidManagement) ? 5 * asteroids : asteroids;

            foreach (Planet planet in planets)
            {
                AsteroidStrikeResult.PlanetResult planetResult = new AsteroidStrikeResult.PlanetResult()
                {
                    Id = planet.Id,
                    Name = planet.Name
                };

                for (int i = 0; i < potency; i++)
                {
                    int random = Game.Random.Next(1, 5);

                    switch (random)
                    {
                        case 1:
                            {
                                long factoriesLost = planet.Infrastructure.Factory * Game.Random.Next(20, 40) / 100;
                                planet.Infrastructure.Factory -= factoriesLost;
                                planetResult.FactoriesLost += factoriesLost;

                                break;
                            }
                        case 2:
                            {
                                long researchLabsLost = planet.Infrastructure.ResearchLab * Game.Random.Next(20, 40) / 100;
                                planet.Infrastructure.ResearchLab -= researchLabsLost;
                                planetResult.ResearchLabsLost += researchLabsLost;

                                break;
                            }
                        case 3:
                            {
                                long militaryBasesLost = planet.Infrastructure.MilitaryBase * Game.Random.Next(20, 40) / 100;
                                planet.Infrastructure.MilitaryBase -= militaryBasesLost;
                                planetResult.MilitaryBasesLost += militaryBasesLost;

                                break;
                            }
                        case 4:
                            {
                                long populationLost = planet.Population * Game.Random.Next(20, 40) / 100;
                                planet.Population -= populationLost;
                                planetResult.PopulationLost += populationLost;

                                break;
                            }
                        case 5:
                        default:
                            {
                                break;
                            }
                    }
                }

                result.PlanetResults.Add(planetResult);
            }

            return result;
        }

        public AsteroidStrikeResult MeteorStrike(Player player, Player target)
        {
            return AsteroidStrike(player, target, 1);
        }

        public EmpStormResult EmpStorm(Player target)
        {
            EmpStormResult result = new EmpStormResult();

            List<Planet> planets = target.Planets.Random(1).ToList();

            foreach (Planet planet in planets)
            {
                EmpStormResult.PlanetResult planetResult = new EmpStormResult.PlanetResult()
                {
                    Id = planet.Id,
                    Name = planet.Name
                };

                throw new NotImplementedException();
            }

            return result;
        }

        public AsteroidStrikeResult StellarBombardment(Player player, Player target)
        {
            return AsteroidStrike(player, target, 3);
        }

        public AssassinationResult Assassination(Player target)
        {
            AssassinationResult result = new AssassinationResult();

            List<Admiral> admirals = target.GetAdmiralPool().Random(Game.Random.Next(0, target.GetAdmiralPool().Count / 2)).ToList();

            foreach (Admiral admiral in admirals)
            {
                target.Admirals.Remove(admiral);
            }

            result.AdmiralsLost += admirals.Count;

            return result;
        }
    }
}
