using System;
using System.Collections.Generic;
using System.Linq;

namespace AOC2018.Solver
{
    /// <summary>
    /// Solution for the fifth day advent problem
    /// </summary>
    [AdventDay(day: 5)]
    public class Day05Solver : ISolver
    {
        public string ProblemTitle => "Day 5: Alchemical Reduction";
        
        /// <summary>
        /// Reduce the given polymer
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SolveA(string input)
        {
            return $"{Reduce(input).Count()}";
        }

        /// <summary>
        /// Reduce the string until it's fully reduced
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string Reduce(string input)
        {
            var candidateStack = new Stack<char>();

            for(int i = 0; i < input.Length ; i++)
            {
                if (candidateStack.TryPeek(out char currentElement))
                {
                    if (!input[i].IsReactiveWith(candidateStack.Peek()))
                    {
                        candidateStack.Push(input[i]);
                    }
                    else
                    {
                        candidateStack.Pop(); //It reacted
                    }
                }
                else
                {
                    candidateStack.Push(input[i]); //It's empty, push
                }
            }

            var reducedString = new String(candidateStack.Reverse().ToArray());

            if(reducedString == input)
                return reducedString;

            return Reduce(reducedString);
        }
        
        /// <summary>
        /// Reduce polymers 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SolveB(string input)
        {
            var bestCandidate = new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' }
            .Select(a => new
            {
                RemovalCandidate = $"{char.ToUpper(a)}/{a}",
                FullyReactedLength = Reduce(new string(input.Where(i => char.ToLower(i) != a).ToArray())).Count()
            }).OrderBy(a => a.FullyReactedLength)
            .First();
            
            return $"{bestCandidate.FullyReactedLength}";
        }
    }
}
