using AOC2018.Solver.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AOC2018.Solver
{
    /// <summary>
    /// Solution for the eleventh day advent problem
    /// </summary>
    [AdventDay(day: 11)]
    public class Day11Solver : ISolver
    {
        public string ProblemTitle => "Day 11: Chronal Charge";

        /// <summary>
        /// Find the subcell with the greatest total power
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SolveA(string input)
        {
            var powerGrid = GeneratePowerGrid(int.Parse(input));

            //Local function that calculates the total power of a grid cell
            int calulateTotalPower(int x, int y)
            {
                int totalPower = 0;
                for (int i = 0; i < 3; i++)
                    for (int j = 0; j < 3; j++)
                        totalPower += powerGrid[x + i, y + j];
                return totalPower;
            }

            //Find the subcell with the largest total power
            var maxFuelCell = Enumerable.Range(0, 297).SelectMany(x => Enumerable.Range(0, 297), (x, y) => new
            {
                X = x,
                Y = y,
                TotalPower = calulateTotalPower(x, y)
            }).OrderByDescending(p => p.TotalPower)
            .First();

            //Return the coordinate
            return $"{maxFuelCell.X + 1},{maxFuelCell.Y + 1}";
        }

        /// <summary>
        /// Generate the current powergrid with a given serial number
        /// </summary>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        private int[,] GeneratePowerGrid(int serialNumber)
        {
            var powerGrid = new int[300, 300];

            foreach (var cell in Enumerable.Range(1, 300).SelectMany(x => Enumerable.Range(1, 300), (x, y) => new
            {
                X = x,
                Y = y
            }).Select(c => new
            {
                c.X,
                c.Y,
                PowerLevel = CalculatePowerLevel(c.X, c.Y, serialNumber)
            }))
            {
                powerGrid[cell.X - 1, cell.Y - 1] = cell.PowerLevel;
            }

            return powerGrid;
        }

        /// <summary>
        /// Given a grid reference calculate the power level
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="serialNumber"></param>
        /// <returns></returns>
        public int CalculatePowerLevel(int x, int y, int serialNumber)
        {
            return ((x + 10) * y + serialNumber) * (x + 10) / 100 % 10 - 5;
        }

        /// <summary>
        /// Find the best subcell of all subcell sizes
        /// </summary>
        /// <remarks>
        /// TODO: This is pretty slow, investigate improving
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SolveB(string input)
        {
           
            var bestCellForSizes = new List<(int, string)> ();

            //Find the subcell with the largest total power
            var mostEfficientSize = Parallel.ForEach(Enumerable.Range(1, 299), size => {

                var powerGrid = GeneratePowerGrid(int.Parse(input));

                //Local function that calculates the total power of a grid cell
                int calulateTotalPower(int x, int y)
                {
                    int totalPower = 0;
                    for (int i = 0; i < size; i++)
                        for (int j = 0; j < size; j++)
                            totalPower += powerGrid[x + i, y + j];
                    return totalPower;
                }
                
                var maxFuelCell = Enumerable.Range(0, 300 - size).SelectMany(x => Enumerable.Range(0, 300 - size), (x, y) => new
                {
                    X = x,
                    Y = y,
                    TotalPower = calulateTotalPower(x, y)
                }).OrderByDescending(p => p.TotalPower).First();

                bestCellForSizes.Add((maxFuelCell.TotalPower, $"{maxFuelCell.X + 1},{maxFuelCell.Y + 1},{size}"));
            });
            
            return bestCellForSizes.OrderByDescending(s => s.Item1)
                .First().Item2;
        }
    }
}
