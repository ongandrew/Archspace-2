using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archspace2
{
    public static class ArchspaceExtensions
    {
        public static int CalculateTotalEffect<T>(this IEnumerable<T> tEnumerable, int aBase, Func<T, int> aPredicate) where T : IModifier
        {
            int result = aBase;

            int totalAbsolute = tEnumerable.Where(x => x.ModifierType == ModifierType.Absolute).Sum(aPredicate);

            int totalProportional = tEnumerable.Where(x => x.ModifierType == ModifierType.Proportional).Sum(aPredicate);

            result += result * (totalProportional / 100);
            result += totalAbsolute;

            return result;
        }
    }
}
