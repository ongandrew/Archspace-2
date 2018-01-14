using System;

namespace Archspace2
{
    public class DistributionRatio : IBuildings
    {
        public DistributionRatio()
        {
            Factory = 40;
            ResearchLab = 30;
            MilitaryBase = 30;
        }

        public int Factory { get; private set; }
        public int ResearchLab { get; private set; }
        public int MilitaryBase { get; private set; }

        public void Set(int aFactory, int aResearchLab, int aMilitaryBase)
        {
            if (aFactory + aResearchLab + aMilitaryBase != 100)
            {
                throw new InvalidOperationException("Ratio must total to 100.");
            }
            else
            {
                Factory = aFactory;
                ResearchLab = aResearchLab;
                MilitaryBase = aMilitaryBase;
            }
        }

        public int Total()
        {
            return Factory + ResearchLab + MilitaryBase;
        }
    }
}
