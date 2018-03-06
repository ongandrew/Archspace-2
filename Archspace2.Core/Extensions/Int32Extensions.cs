using System;
using System.Collections.Generic;
using System.Text;

namespace Archspace2.Extensions
{
    public static class Int32Extensions
    {
        public static string ToOrdinal(this int tInt)
        {
            if (tInt <= 0) return tInt.ToString();

            switch (tInt % 100)
            {
                case 11:
                case 12:
                case 13:
                    return tInt + "th";
            }

            switch (tInt % 10)
            {
                case 1:
                    return tInt + "st";
                case 2:
                    return tInt + "nd";
                case 3:
                    return tInt + "rd";
                default:
                    return tInt + "th";
            }
        }

        public static string ToRoman(this int tInt)
        {
            if ((tInt < 0) || (tInt > 3999)) throw new ArgumentOutOfRangeException("Only values between 1 and 3999 are supported.");
            if (tInt < 1) return string.Empty;
            if (tInt >= 1000) return "M" + ToRoman(tInt - 1000);
            if (tInt >= 900) return "CM" + ToRoman(tInt - 900);
            if (tInt >= 500) return "D" + ToRoman(tInt - 500);
            if (tInt >= 400) return "CD" + ToRoman(tInt - 400);
            if (tInt >= 100) return "C" + ToRoman(tInt - 100);
            if (tInt >= 90) return "XC" + ToRoman(tInt - 90);
            if (tInt >= 50) return "L" + ToRoman(tInt - 50);
            if (tInt >= 40) return "XL" + ToRoman(tInt - 40);
            if (tInt >= 10) return "X" + ToRoman(tInt - 10);
            if (tInt >= 9) return "IX" + ToRoman(tInt - 9);
            if (tInt >= 5) return "V" + ToRoman(tInt - 5);
            if (tInt >= 4) return "IV" + ToRoman(tInt - 4);
            if (tInt >= 1) return "I" + ToRoman(tInt - 1);

            return null;
        }
    }
}
