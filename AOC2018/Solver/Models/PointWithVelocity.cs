using System.Drawing;

namespace AOC2018.Solver.Models
{
    public class PointWithVelocity
    {
        private Point _point;
        public Point Point
        {
            get
            {
                return _point;
            }
        }

        private int dX { get; }
        private int dY { get; }
        
        public PointWithVelocity(int x, int y, int dX, int dY)
        {
            _point = new Point(x, y);
            this.dX = dX;
            this.dY = dY;
        }

        public PointWithVelocity Forward()
        {
            return new PointWithVelocity(_point.X + dX, _point.Y + dY, dX, dY);
        }
        
        public PointWithVelocity Reverse()
        {
            return new PointWithVelocity(_point.X - dX, _point.Y - dY, dX, dY);
        }

    }
}
