using AOC2018.Solver.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AOC2018.Solver
{
    /// <summary>
    /// Solution for the third day advent problem
    /// </summary>
    [AdventDay(day: 3)]
    public class Day03Solver : ISolver
    {
        public string ProblemTitle => "Day 3: No Matter How You Slice It";

        /// <summary>
        /// Get the number of points that intersect more than one place
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SolveA(string input)
        {
            var coveredPoints = input.Split(new[] { Environment.NewLine, ", " }, StringSplitOptions.None)
                .Select(claim => new ElfFabricClaim(claim)).SelectMany(c => c.GetCoveredPoints());

            var points = new HashSet<Point>();
            var duplicates = new HashSet<Point>();

            foreach (var point in coveredPoints)
            {
                if (!points.Contains(point))
                {
                    points.Add(point);
                }
                else if (!duplicates.Contains(point))
                {
                    duplicates.Add(point);
                }
            }

            return $"{duplicates.Count}";
        }
        
        /// <summary>
        /// Find the claim which does not overlap any other
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SolveB(string input)
        {
            var claims = input.Split(new[] { Environment.NewLine, ", " }, StringSplitOptions.None)
                .Select(claim => new ElfFabricClaim(claim)).ToArray();
            
            return claims.Where(c => !claims.Any(c2 => c2.Intersects(c) && c.Id != c2.Id)).First().Id;
        }
    }
}
