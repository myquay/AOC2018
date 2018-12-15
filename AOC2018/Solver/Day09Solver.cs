using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2018.Solver
{
    /// <summary>
    /// Solution for the ninth day advent problem
    /// </summary>
    [AdventDay(day: 9)]
    public class Day09Solver : ISolver
    {
        public string ProblemTitle => "Day 9: Marble Mania";
        
        /// <summary>
        /// Calculate the winning score
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SolveA(string input)
        {
            var numbers = new Regex(@"\d+")
                .Matches(input);

            int numberOfPlayers = int.Parse(numbers[0].Value);
            int maxMarble = int.Parse(numbers[1].Value);

            return $"{CalculateWinningScore(numberOfPlayers, maxMarble)}";
        }
        
        private Int64 CalculateWinningScore(int numberOfPlayers, int maxMarble)
        {
            var scores = new Int64[numberOfPlayers];
            var marbles = new LinkedList<int>(new[] { 0 });
            var currentMarble = marbles.First;

            for (int i = 1; i <= maxMarble; i++)
            {
                if (i % 23 == 0 && i != 0)
                {
                    var currentPlayer = i % numberOfPlayers;
                    var extraPointMarble = currentMarble;
                    for (int j = 0; j < 8; j++)
                    {
                        if (extraPointMarble.Previous == null)
                        {
                            extraPointMarble = marbles.Last;
                        }
                        else
                        {
                            extraPointMarble = extraPointMarble.Previous;
                        }
                    }

                    scores[currentPlayer] = scores[currentPlayer] + i + extraPointMarble.Value;
                    currentMarble = extraPointMarble.Next;
                    marbles.Remove(extraPointMarble);
                }
                else
                {
                    marbles.AddAfter(currentMarble, i);
                    currentMarble = currentMarble.Next;
                }

                //If last space wrap around
                if (currentMarble.Next == null)
                {
                    currentMarble = marbles.First;
                }
                else
                {
                    currentMarble = currentMarble.Next;
                }
            }

            return scores.Max();
        }

        /// <summary>
        /// Calculate the winning score if 100x more marbles
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SolveB(string input)
        {
            var numbers = new Regex(@"\d+")
                .Matches(input);

            int numberOfPlayers = int.Parse(numbers[0].Value);
            int maxMarble = int.Parse(numbers[1].Value) * 100;

            return $"{CalculateWinningScore(numberOfPlayers, maxMarble)}";
        }
    }
}
