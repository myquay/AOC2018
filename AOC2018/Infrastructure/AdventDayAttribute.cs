using System;
namespace AOC2018
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class AdventDayAttribute : Attribute
    {
        public int Day { get; set; }
        
        public AdventDayAttribute(int day)
        {
            if (day < 1 || day > 25)
                throw new ArgumentException("Input range is 1-25 (inclusive)");
            Day = day;
        }
    }
}
