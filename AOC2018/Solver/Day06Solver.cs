using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace AOC2018.Solver
{
    /// <summary>
    /// Solution for the sixth day advent problem
    /// </summary>
    [AdventDay(day: 6)]
    public class Day06Solver : ISolver
    {
        private const int SAFE_ZONE_THRESHOLD = 10000;

        public string ProblemTitle => "Day 6: Chronal Coordinates";
        
        /// <summary>
        /// Find the point with the largest finite area of points closest to it
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SolveA(string input)
        {
            var points = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                    .Select(s => new Point(int.Parse(s.Split(',')[0]), int.Parse(s.Split(',')[1])))
                    .ToArray();
            
            //Remove points that are closest to any area of the search space limits - they will be infinite 
            var horizonPoints = GeneratePointOutline(points)
                .Select(hp =>
                {
                    var closestPoints = points.Select(p => new { Distance = p.ManhattanDistance(hp), Point = p }).OrderBy(p => p.Distance).Take(2).ToArray();

                    return new
                    {
                        EqualDistance = closestPoints[0].Distance == closestPoints[1].Distance,
                        ClosestPoint = closestPoints[0].Point
                    };
                }).Where(p => !p.EqualDistance)
                .Select(p => p.ClosestPoint);

            //List of all points with finite area
            var candidatePoints = points
                .Where(p => !horizonPoints.Any(hp => hp.Equals(p)))
                .ToArray();

            //See which of the candidate points has the most points close it it
            var largestArea = GeneratePointGrid(points)
                .Select(hp =>
                {
                    var closestPoints = points.Select(p => new { Distance = p.ManhattanDistance(hp), Point = p }).OrderBy(p => p.Distance).Take(2).ToArray();

                    return new
                    {
                        EqualDistance = closestPoints[0].Distance == closestPoints[1].Distance,
                        ClosestPoint = closestPoints[0].Point
                    };
                })
                .Where(p => !p.EqualDistance)
                .Select(p => p.ClosestPoint)
                .Where(p => candidatePoints.Contains(p))
                .GroupBy(p => p)
                .OrderByDescending(p => p.Count())
                .First().Count();

            return $"{largestArea}";
        }

        /// <summary>
        /// Get the number of points within a threshold of all other points
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SolveB(string input)
        {
            var points = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                    .Select(s => new Point(int.Parse(s.Split(',')[0]), int.Parse(s.Split(',')[1])))
                    .ToArray();

            var safeZoneSize = GeneratePointGrid(points)
                .SelectMany(gp => points, (gp, p) => new
                {
                    GridPoint = gp,
                    PointDistance = gp.ManhattanDistance(p)
                }).GroupBy(p => p.GridPoint)
                .Select(gp => new
                {
                    GridPoint = gp.Key,
                    TotalDistance = gp.Sum(v => v.PointDistance)
                }).Where(p => p.TotalDistance < SAFE_ZONE_THRESHOLD).Count();

            return $"{safeZoneSize}";
        }

        /// <summary>
        /// Generate a grid of points for an array of points
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private IEnumerable<Point> GeneratePointGrid(Point[] points)
        {
            var minPoint = new Point(points.Min(p => p.X), points.Min(p => p.Y));
            var maxPoint = new Point(points.Max(p => p.X), points.Max(p => p.Y));

            for (int x = minPoint.X; x <= maxPoint.X; x++)
            {
                for (int y = minPoint.Y; y <= maxPoint.Y; y++)
                {
                    yield return new Point(x, y);
                }
            }
        }
        
        /// <summary>
        /// Generate an outline of points for an array of points
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        private IEnumerable<Point> GeneratePointOutline(Point[] points)
        {
            var minPoint = new Point(points.Min(p => p.X), points.Min(p => p.Y));
            var maxPoint = new Point(points.Max(p => p.X), points.Max(p => p.Y));

            return Enumerable.Range(minPoint.X, maxPoint.X - minPoint.X)
                .Select(x => new[] { new Point(x, minPoint.Y), new Point(x, maxPoint.Y) })
                .Concat(Enumerable.Range(minPoint.Y, maxPoint.Y - minPoint.Y).Select(y => new[] { new Point(minPoint.X, y), new Point(maxPoint.X, y) }))
                .SelectMany(hp => hp);
        }

    }
}
