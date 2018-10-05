using System;

namespace Archspace2
{
    public static class RandomNumberGenerator
    {
        private static Random Random { get; set; }

        static RandomNumberGenerator()
        {
            Random = new Random();
        }

        public static int Next()
        {
            return Random.Next();
        }

        public static int Next(int maxValue)
        {
            return Random.Next(maxValue);
        }

        public static int Next(int minValue, int maxValue)
        {
            return Random.Next(minValue, maxValue);
        }

        public static int Dice(int number, int max)
        {
            return Random.Dice(number, max);
        }
    }
}
