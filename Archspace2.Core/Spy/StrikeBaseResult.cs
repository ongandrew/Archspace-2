using System.Collections.Generic;

namespace Archspace2
{
    public class StrikeBaseResult
    {
        public long ShipsLost { get; set; }
        public List<PlanetResult> PlanetResults { get; set; }


        public class PlanetResult : Archspace2.PlanetResult
        {
            public long MilitaryBaseLost { get; set; }
        }

        public StrikeBaseResult()
        {
            PlanetResults = new List<PlanetResult>();
        }
    }
}
