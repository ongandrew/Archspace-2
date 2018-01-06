using System;
using System.Collections.Generic;
using System.Linq;

namespace Archspace2.Extensions
{
    public static class StringExtensions
    {
        public static string SerializeIds(this IEnumerable<int> tList)
        {
            return string.Join(",", tList.Select(x => x.ToString()));
        }
        public static IEnumerable<int> DeserializeIds(this string tString)
        {
            return tString.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(x => int.Parse(x)).ToList();
        }
    }
}
