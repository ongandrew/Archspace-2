using System.Collections.Generic;

namespace Archspace2
{
    public class InciteRiotResult
    {
        public List<PlanetResult> PlanetResults { get; set; }

        public class PlanetResult : Archspace2.PlanetResult
        {
            public long FactoriesLost { get; set; }
            public long ResearchLabsLost { get; set; }
            public long InvestmentLost { get; set; }
        }

        public InciteRiotResult()
        {
            PlanetResults = new List<PlanetResult>();
        }
    }
}
