using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archspace2.Battle
{
    public static class Extensions
    {
        public static IEnumerable<FleetEffect> OfType(this IEnumerable<FleetEffect> tFleetEffects, FleetEffectType aType)
        {
            return tFleetEffects.Where(x => x.Type == aType);
        }
    }
}
