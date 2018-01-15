using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2
{
    public class PlanetUpdateTurnResult
    {
        public Resource Income { get; set; }
        public Resource Upkeep { get; set; }

        public PlanetUpdateTurnResult()
        {
            Income = new Resource();
            Upkeep = new Resource();
        }

        public static PlanetUpdateTurnResult operator +(PlanetUpdateTurnResult lhs, PlanetUpdateTurnResult rhs)
        {
            return new PlanetUpdateTurnResult()
            {
                Income = lhs.Income + rhs.Income,
                Upkeep = lhs.Upkeep + rhs.Upkeep
            };
        }
    }
}
