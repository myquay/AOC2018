using System;
using System.Drawing;

namespace AOC2018
{
    public static class PointExensions
    {
        public static int ManhattanDistance(this Point value, Point other)
        {
            return Math.Abs(value.X - other.X) + Math.Abs(value.Y - other.Y);
        }
    }
}
