using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
