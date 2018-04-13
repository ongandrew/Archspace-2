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

        public Result PerformOperation(SpyAction aSpy, Player aTarget)
        {
            switch((SpyId)aSpy.Id)
            {
                case SpyId.GeneralInformationGathering:
                    return GeneralInformationGathering(aTarget);
                default:
                    throw new NotImplementedException();
            }
        }

        public Result GeneralInformationGathering(Player aTarget)
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
                resultSet.Add($"PP : {aTarget.Resource.ProductionPoint}");
            }
            if (infoSet.Contains(2))
            {
                resultSet.Add($"Population : {aTarget.Planets.Sum(x => x.Population)}");
            }
            if (infoSet.Contains(3))
            {
                resultSet.Add($"Concentration Mode : {aTarget.ConcentrationMode.ToString()}");
            }
            if (infoSet.Contains(4))
            {
                resultSet.Add($"Fleets : {aTarget.Fleets.Count}");
            }
            if (infoSet.Contains(5))
            {
                resultSet.Add($"Total Docked Ships : {aTarget.Shipyard.ShipPool.Sum(x => x.Value)}");
            }
            if (infoSet.Contains(6))
            {
                resultSet.Add($"PP Income : {aTarget.Planets.Sum(x => x.CalculateProductionPointPerTurn())}");
            }
            if (infoSet.Contains(7))
            {
                resultSet.Add($"Researched Techs : {aTarget.Techs.Count}");
            }
            if (infoSet.Contains(8))
            {
                resultSet.Add($"Commanders : {aTarget.Admirals.Count}");
            }

            aTarget.AddNews("Some of your general information has been stolen.");

            return new Result(ResultType.Success, string.Join("\n", resultSet));
        }

        public Result DetailedInformationGathering(Player aTarget)
        {
            List<string> resultSet = new List<string>();

            resultSet.Add(GeneralInformationGathering(aTarget).Message);

            int detailedInfo = Game.Random.Next(1, 4);

            switch (detailedInfo)
            {
                case 1:
                    {
                        Planet planet = aTarget.Planets.Random();

                        resultSet.Add($"Status of planet {planet.Name}:\nPopulation: {planet.Population}\nResource: {planet.Resource.ToString()}\nBuildings:\nFactory {planet.Infrastructure.Factory}\tResearch Lab {planet.Infrastructure.ResearchLab}\tMilitary Base {planet.Infrastructure.MilitaryBase}");

                        break;
                    }
                case 2:
                    {
                        resultSet.Add($"List of projects:\n{string.Join("\n", aTarget.Projects.Select(x => x.Name))}");
                        break;
                    }
                case 3:
                    {
                        resultSet.Add($"Current Tech Goal: {(aTarget.TargetTech == null ? "None" : aTarget.TargetTech.Name)}");
                        break;
                    }
                case 4:
                    {
                        resultSet.Add($"Researched Techs:\n{string.Join("\n", aTarget.Techs.Select(x => x.Name))}");
                        break;
                    }
            }

            aTarget.AddNews("Some of your detailed information has been stolen.");

            return new Result(ResultType.Success, string.Join("\n", resultSet));
        }

        public Result StealSecretInfo(Player aTarget)
        {
            Result result = new Result(ResultType.Success);

            switch (Game.Random.Next(1, 2))
            {
                case 1:
                    {
                        result.Message = $"Current Fleets ({aTarget.Fleets.Count}):\n" + string.Join("\n", aTarget.Fleets.Select(x => $"{x.GetDisplayName()} {x.ShipDesign.ShipClass.Name} {x.CurrentShipCount}/{x.MaxShipCount}"));

                        break;
                    }
                case 2:
                    {
                        result.Message = $"Ship pool ({aTarget.Shipyard.ShipPool.Count}):\n" + string.Join("\n", aTarget.Shipyard.ShipPool.Select(x => $"{x.Key.Name} {x.Key.ShipClass.Name} {x.Value}"));
                        break;
                    }
            }

            return result;
        }

        public Result ComputerVirusInfiltration(Player aTarget)
        {
            Result result = new Result(ResultType.Success);

            long lostResearch = aTarget.Resource.ResearchPoint * 70 / 100;
            aTarget.Resource.ResearchPoint -= lostResearch;

            result.Message = $"{aTarget.GetDisplayName()} lost {lostResearch} RP.";

            return result;
        }

        public Result DevastatingNetworkWorm(Player aTarget)
        {
            Result result = new Result(ResultType.Success);

            long lostResearch = aTarget.Resource.ResearchPoint * 40 / 100;
            aTarget.Resource.ResearchPoint -= lostResearch;

            result.Message = $"{aTarget.GetDisplayName()} lost {lostResearch} RP.";

            return result;
        }
    }
}
