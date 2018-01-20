using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2.Extensions
{
    public static class RandomExtensions
    {
        public static int Dice(this Random tRandom, int aNumber, int aMax)
        {
            int result = 0;

            for (int i = 0; i < aNumber; i++)
            {
                result += tRandom.Next(1, aMax);
            }

            return result;
        }
    }
}
