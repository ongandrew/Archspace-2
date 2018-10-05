using System.Collections.Generic;

namespace Archspace2
{
    public class EmpStormResult
    {
        public List<PlanetResult> PlanetResults { get; set; }

        public class PlanetResult : Archspace2.PlanetResult
        {
            public int TurnsParalyzed { get; set; }
        }

        public EmpStormResult()
        {
            PlanetResults = new List<PlanetResult>();
        }
    }
}
