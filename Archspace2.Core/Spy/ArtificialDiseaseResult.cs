using System.Collections.Generic;

namespace Archspace2
{
    public class ArtificialDiseaseResult
    {
        public List<PlanetResult> PlanetResults { get; set; }

        public class PlanetResult : Archspace2.PlanetResult
        {
            public long PopulationLost { get; set; }
        }

        public ArtificialDiseaseResult()
        {
            PlanetResults = new List<PlanetResult>();
        }
    }
}
