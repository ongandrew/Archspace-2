using System.Collections.Generic;
using System.Linq;

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
