using System.Collections.Generic;

namespace Archspace2
{
    public class UniverseEntityComparer : IEqualityComparer<UniverseEntity>
    {
        public bool Equals(UniverseEntity x, UniverseEntity y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(UniverseEntity obj)
        {
            return obj.GetHashCode();
        }
    }
}
