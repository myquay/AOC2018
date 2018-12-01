using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2018.Solver
{
    /// <summary>
    /// Solution for the first day advent problem
    /// </summary>
    [AdventDay(day: 1)]
    public class Day01Solver : ISolver
    {
        public string ProblemTitle => "Day 1: Chronal Calibration";

        /// <summary>
        /// Sum all the integers in the stream to find the frequency
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SolveA(string input)
        {
            return $"{TokenisedInputStream(input).Sum()}";
        }
        
        /// <summary>
        /// Sum all the elements in the stream (repeating) until frequency is duplicated
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SolveB(string input)
        {
            //Frequencies
            var results = new HashSet<int>(new[] { 0 });
            var currentFrequency = 0;

            foreach(var token in TokenisedInputStream(input, repeat: true))
            {
                currentFrequency = currentFrequency + token;
                if (results.Contains(currentFrequency))
                    return $"{currentFrequency}";
                results.Add(currentFrequency);
            }

            throw new ArgumentException("Input stream cannot be empty");
        }

        /// <summary>
        /// Tokenise the input stream
        /// </summary>
        /// <param name="seedInput">The raw data to be tokenised</param>
        /// <param name="repeat">Set to true if an indefinite repeating stream is needed</param>
        /// <returns></returns>
        private IEnumerable<int> TokenisedInputStream(string seedInput, bool repeat = false)
        {
            var parsedInput = seedInput
                .Split(new[] { Environment.NewLine, ", " }, StringSplitOptions.None)
                .Select(s => int.Parse(s));

            while (true)
            {
                foreach (var item in parsedInput)
                    yield return item;

                if (!repeat)
                    break;
            }
        }
    }
}
