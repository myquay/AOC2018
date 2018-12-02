using System;
using System.Linq;

namespace AOC2018.Solver
{
    /// <summary>
    /// Solution for the second day advent problem
    /// </summary>
    [AdventDay(day: 2)]
    public class Day02Solver : ISolver
    {
        public string ProblemTitle => "Day 2: Inventory Management System";

        /// <summary>
        /// Find the checksum (number with 2 letters * number with 3 letters)
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SolveA(string input)
        {
            var candidates = input.Split(new[] { Environment.NewLine, ", " }, StringSplitOptions.None)
                .Select(s => new
                {
                    HasTwo = s.GroupBy(e => e).Any(e => e.Count() == 2),
                    HasThree = s.GroupBy(e => e).Any(e => e.Count() == 3)
                });

            return $"{candidates.Where(c => c.HasTwo).Count() * candidates.Where(c => c.HasThree).Count()}";
        }
        
        /// <summary>
        /// Find the two ids that differ in only one character
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SolveB(string input)
        {
            int hamming(string x, string y) => x.Zip(y, (a,b) => a-b == 0 ? 0 : 1).Sum();

            var candidates = input.Split(new[] { Environment.NewLine, ", " }, StringSplitOptions.None).ToArray();
            var idsWithHammingOfOne = candidates.Select((v,index) => new { index, v}).SelectMany(c => candidates.Skip(c.index), (s1, s2) => new
            {
                s1 = s1.v,
                s2,
                hamming = hamming(s1.v, s2)
            }).Where(s => s.hamming == 1).ToArray();

            if (idsWithHammingOfOne.Count() != 1)
                throw new ArgumentException("List has more than two ids of hamming distance of one");

            return $"{new String(idsWithHammingOfOne.Single().s1.Where((s, index) => idsWithHammingOfOne.Single().s2[index] == s).ToArray())}";
        }
    }
}
