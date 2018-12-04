using System;

namespace AOC2018
{
    public static class StringExensions
    {
        public static bool IsInt(this string value)
        {
            return Int32.TryParse(value, out int parsedValue);
        }

        public static int? ToInt(this string value)
        {
            if (Int32.TryParse(value, out int parsedValue))
                return parsedValue;
            return null;
        }
    }
}
