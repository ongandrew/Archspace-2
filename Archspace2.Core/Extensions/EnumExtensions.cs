using System;
using System.Linq;

namespace Archspace2.Extensions
{
    public static class EnumExtensions
    {
        public static T Min<T>(this Type tType) where T : struct, IComparable, IConvertible, IFormattable
        {
            return Enum.GetValues(tType).Cast<T>().Min();
        }

        public static T Max<T>(this Type tType) where T : struct, IComparable, IConvertible, IFormattable
        {
            return Enum.GetValues(tType).Cast<T>().Max();
        }

        public static T ModifyByInt<T>(this T tEnum, int aInt) where T : struct, IComparable, IConvertible, IFormattable
        {
            dynamic castable = tEnum;

            if (aInt == 0)
            {
                return tEnum;
            }

            int targetValue = (int)castable + aInt;

            if (Enum.GetValues(typeof(T)).Cast<int>().Contains(targetValue))
            {
                return (T)(dynamic)targetValue;
            }
            else if (aInt > 0)
            {
                return (T)(dynamic)Enum.GetValues(typeof(T)).Cast<int>().Where(x => x < targetValue).Max();
            }
            else
            {
                return (T)(dynamic)Enum.GetValues(typeof(T)).Cast<int>().Where(x => x > targetValue).Min();
            }
        }

        public static int Min(this Type tType)
        {
            return Enum.GetValues(tType).Cast<int>().Min();
        }

        public static int Max(this Type tType)
        {
            return Enum.GetValues(tType).Cast<int>().Max();
        }
    }
}
