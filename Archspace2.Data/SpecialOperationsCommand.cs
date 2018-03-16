using System;
using System.Collections.Generic;
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
    }
}
