using AOC2018.Solver.Models;
using System;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AOC2018.Solver
{
    /// <summary>
    /// Solution for the tenth day advent problem
    /// </summary>
    [AdventDay(day: 10)]
    public class Day10Solver : ISolver
    {
        public string ProblemTitle => "Day 10: The Stars Align";

        private readonly double DEVIATION_THRESHOLD = 10;
        private readonly bool SHOW_CANDIDATES = false;

        public Day10Solver()
        {}

        public Day10Solver(double deviationThreshold, bool showCandidates = false)
        {
            DEVIATION_THRESHOLD = deviationThreshold;
            SHOW_CANDIDATES = showCandidates;
        }

        /// <summary>
        /// Draw the stars
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SolveA(string input)
        {
            var mappedStars = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                .Select(s => s.Replace(" ", ""))
                .Select(s => Regex.Matches(s, @"(-?\d+)"))
                .Select(s => new PointWithVelocity(int.Parse(s[0].Value), int.Parse(s[1].Value), int.Parse(s[2].Value), int.Parse(s[3].Value)))
                .ToArray();
            
            return BuildStarString(FindMessage(mappedStars).Stars);
        }
        
        private int StarFieldArea(PointWithVelocity[] stars)
        {
            var minX = stars.Select(s => s.Point.X).Min();
            var maxX = stars.Select(s => s.Point.X).Max();
            var minY = stars.Select(s => s.Point.Y).Min();
            var maxY = stars.Select(s => s.Point.Y).Max();

            return Math.Abs((maxX - minX) * (maxY - minY));
        }

        private StarFieldResult FindMessage(PointWithVelocity[] stars)
        {
            double? lastSd = null;
            var steps = 0;

            while (true)
            {
                //Use standard deviation as measure (assume the points become less random)
                var counts = stars.GroupBy(s => s.Point.Y).Select(s => s.Count());
                var average = counts.Average();
                double sumOfSquaresOfDifferences = counts.Select(val => (val - average) * (val - average)).Sum();
                double sd = Math.Sqrt(sumOfSquaresOfDifferences / counts.Count());

                //Candidate of lineup found, print
                if (lastSd != null && sd > lastSd && sd > DEVIATION_THRESHOLD)
                {
                    if (SHOW_CANDIDATES)
                    {
                        Console.WriteLine();
                        Console.WriteLine(BuildStarString(stars));
                        Console.WriteLine();
                        Console.WriteLine($"Is this the message (Step {steps} - SD: {sd})? ");
                        Console.WriteLine("Hit q to quit searching, any key to continue");

                        if (Console.ReadKey().KeyChar == 'q')
                        {
                            Console.WriteLine();
                            Console.WriteLine();
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                //Advance time
                lastSd = sd;
                stars = stars.Select(s => s.Forward()).ToArray();
                steps++;
            }

            return new StarFieldResult
            {
                Seconds = steps,
                Stars = stars
            };
        }

        private string BuildStarString(PointWithVelocity[] stars)
        {
            var minX = stars.Select(s => s.Point.X).Min();
            var maxX = stars.Select(s => s.Point.X).Max();
            var minY = stars.Select(s => s.Point.Y).Min();
            var maxY = stars.Select(s => s.Point.Y).Max();

            var field = new bool[maxX - minX + 1, maxY - minY + 1];

            foreach (var star in stars)
            {
                field[star.Point.X - minX, star.Point.Y - minY] = true;
            }

            var sb = new StringBuilder();

            for (int j = 0; j <= maxY - minY; j++)
            {
                for (int i = 0; i <= maxX - minX; i++)
                {
                    sb.Append(field[i, j] ? "#" : ".");
                }
                sb.AppendLine();
            }

            return sb.ToString();
        }
        
        /// <summary>
        /// Calculate the step the star message appears
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SolveB(string input)
        {
            var mappedStars = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                 .Select(s => s.Replace(" ", ""))
                 .Select(s => Regex.Matches(s, @"(-?\d+)"))
                 .Select(s => new PointWithVelocity(int.Parse(s[0].Value), int.Parse(s[1].Value), int.Parse(s[2].Value), int.Parse(s[3].Value)))
                 .ToArray();
            
            return $"{FindMessage(mappedStars).Seconds}";
        }
    }
}
