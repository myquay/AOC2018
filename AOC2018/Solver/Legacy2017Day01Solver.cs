using System;
using System.Linq;

namespace AOC2018.Solver
{
    [AdventDay(day: 1)]
    [Obsolete(message:"Last years problem as example")]
    public class Legacy2017Day01Solver : ISolver
    {
        public string ProblemTitle => "Day 1: Inverse Captcha";

        public string Solve(string input)
        {
            var items = $"{input}{input[0]}".Select(c => int.Parse($"{c}")).ToArray();

            return items.Select((item, index) => index + 1 < items.Length ? (items[index] == items[index + 1] ? items[index] : 0) : 0)
                .DefaultIfEmpty(0)
                .Sum().ToString();
        }
    }
}
