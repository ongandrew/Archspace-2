using System.Collections.Generic;

namespace Archspace2
{
    public class EntityComparer : IEqualityComparer<Entity>
    {
        public bool Equals(Entity x, Entity y)
        {
            return x.Id == y.Id && x.GetType() == y.GetType();
        }

        public int GetHashCode(Entity obj)
        {
            return obj.Id;
        }
    }
}