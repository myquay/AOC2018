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
            //Returns the number of strings with the exact number of duplicate letters
            int containsDuplicateLetters (string data, int number) => data.Split(new[] { Environment.NewLine, ", " }, StringSplitOptions.None)
                .Select(s => s.GroupBy(e => e))
                .Where(n => n.Any(m => m.Count() == number))
                .Count();

            return $"{containsDuplicateLetters(input, 2) * containsDuplicateLetters(input, 3)}";
        }
        
        /// <summary>
        /// Find the two ids that differ in only one character
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SolveB(string input)
        {
            //Returns the hamming distance between two strings
            int hamming(string x, string y) => x.Zip(y, (a,b) => a-b == 0 ? 0 : 1).Sum();

            var candidates = input.Split(new[] { Environment.NewLine, ", " }, StringSplitOptions.None);
            var idsWithHammingOfOne = candidates
                .Select((v,index) => new { index, v})
                //Remove duplicate pairs by skipping entites that have already been paired
                .SelectMany(c => candidates.Skip(c.index), (s1, s2) => new 
                    {
                        s1 = s1.v,
                        s2,
                        hamming = hamming(s1.v, s2)
                    }).Where(s => s.hamming == 1).ToArray();

            //Make sure we only have one candidate pair, if not bad puzzel input and we have no method for determining the actual candidate
            if (idsWithHammingOfOne.Count() != 1)
                throw new ArgumentException("List has more than two ids of hamming distance of one");

            var candidatePair = idsWithHammingOfOne.Single();

            //Remove the differing character
            return $"{new String(candidatePair.s1.Where((s, index) => candidatePair.s2[index] == s).ToArray())}";
        }
    }
}
