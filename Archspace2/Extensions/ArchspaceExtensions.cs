using System;
using System.Collections.Generic;
using System.Linq;

namespace Archspace2
{
    public static class ArchspaceExtensions
    {
        public static int CalculateTotalEffect<T>(this IEnumerable<T> tEnumerable, int aBase, Func<T, int> aPredicate)
    where T : IModifier
        {
            int result = aBase;

            int totalAbsolute = tEnumerable.Where(x => x.ModifierType == ModifierType.Absolute).Sum(aPredicate);

            int totalProportional = tEnumerable.Where(x => x.ModifierType == ModifierType.Proportional).Sum(aPredicate);

            result += result * totalProportional / 100;
            result += totalAbsolute;

            return result;
        }

        public static long CalculateTotalEffect<T>(this IEnumerable<T> tEnumerable, long aBase, Func<T, int> aPredicate)
where T : IModifier
        {
            long result = aBase;

            int totalAbsolute = tEnumerable.Where(x => x.ModifierType == ModifierType.Absolute).Sum(aPredicate);

            int totalProportional = tEnumerable.Where(x => x.ModifierType == ModifierType.Proportional).Sum(aPredicate);

            result += result * totalProportional / 100;
            result += totalAbsolute;

            return result;
        }

        public static double CalculateTotalEffect<T>(this IEnumerable<T> tEnumerable, double aBase, Func<T, double> aPredicate) 
            where T : IModifier
        {
            double result = aBase;

            double totalAbsolute = tEnumerable.Where(x => x.ModifierType == ModifierType.Absolute).Sum(aPredicate);

            double totalProportional = tEnumerable.Where(x => x.ModifierType == ModifierType.Proportional).Sum(aPredicate);

            result += result * totalProportional / 100;
            result += totalAbsolute;

            return result;
        }

        public static long CalculateTotalPower(this IEnumerable<IPowerContributor> tEnumerable)
        {
            return tEnumerable.Sum(x => x.Power);
        }
    }
}
