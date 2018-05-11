using System.Collections.Generic;

namespace Archspace2
{
    public class AsteroidStrikeResult
    {
        public List<PlanetResult> PlanetResults { get; set; }

        public class PlanetResult : Archspace2.PlanetResult
        {
            public long FactoriesLost { get; set; }
            public long ResearchLabsLost { get; set; }
            public long MilitaryBasesLost { get; set; }
            public long PopulationLost { get; set; }
        }

        public AsteroidStrikeResult()
        {
            PlanetResults = new List<PlanetResult>();
        }
    }
}
