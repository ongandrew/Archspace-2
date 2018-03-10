using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

        public static string ToFriendlyString(this string tString)
        {
            return Regex.Replace(tString, "([a-z](?=[A-Z]|[0-9])|[A-Z](?=[A-Z][a-z]|[0-9])|[0-9](?=[^0-9]))", "$1 ");
        }
    }
}
