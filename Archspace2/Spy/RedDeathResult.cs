using System.Collections.Generic;

namespace Archspace2
{
    public class RedDeathResult
    {
        public List<PlanetResult> PlanetResults { get; set; }

        public class PlanetResult : Archspace2.PlanetResult
        {
            public long PopulationLost { get; set; }
            public bool GainedObstinateMicrobe { get; set; }

            public PlanetResult()
            {
                PopulationLost = 0;
                GainedObstinateMicrobe = false;
            }
        }

        public RedDeathResult()
        {
            PlanetResults = new List<PlanetResult>();
        }
    }
}
