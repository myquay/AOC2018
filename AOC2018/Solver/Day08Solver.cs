using AOC2018.Solver.Models;
using System;
using System.Linq;

namespace AOC2018.Solver
{
    /// <summary>
    /// Solution for the eighth day advent problem
    /// </summary>
    [AdventDay(day: 8)]
    public class Day08Solver : ISolver
    {
        public string ProblemTitle => "Day 8: Memory Maneuver";
        
        /// <summary>
        /// Check the license checksum
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SolveA(string input)
        {
            var parsedInput = input
                 .Split(new[] { " " }, StringSplitOptions.None)
                 .Select(s => int.Parse(s)).ToArray();

            var tree = BuildTree(parsedInput, 0);
            
            return $"{BuildTree(parsedInput, 0).CalculateLicense(LicenseModeCalculationMode.Metadata)}";
        }

        /// <summary>
        /// Build the license node tree
        /// </summary>
        /// <param name="input"></param>
        /// <param name="currentIndex"></param>
        /// <returns></returns>
        private NavigationLicenseNode BuildTree(int[] input, int currentIndex)
        {
            var currentNode = new NavigationLicenseNode();

            var children = input[currentIndex];
            var metadata = input[currentIndex + 1];

            currentIndex = currentIndex + 2;

            for (int i = 0; i < children; i++)
            {
                var childNode = BuildTree(input, currentIndex);
                currentNode.Children.Add(childNode);
                currentIndex = childNode.EndOfNodeIndex;
            }

            for (int i = 0; i < metadata; i++)
            {
                currentNode.Metadata.Add(input[currentIndex + i]);
            }

            currentNode.EndOfNodeIndex = currentIndex + metadata;

            return currentNode;
        }
        
        /// <summary>
        /// Calculate the license mode
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SolveB(string input)
        {
            var parsedInput = input
                 .Split(new[] { " " }, StringSplitOptions.None)
                 .Select(s => int.Parse(s)).ToArray();

            var tree = BuildTree(parsedInput, 0);

            return $"{BuildTree(parsedInput, 0).CalculateLicense(LicenseModeCalculationMode.License)}";
        }
    }
}
