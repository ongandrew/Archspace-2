using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class InciteRiotResult
    {
        public List<PlanetResult> PlanetResults { get; set; }

        public class PlanetResult
        {
            public int Id { get; set; }
            public string Name { get; set; }

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
