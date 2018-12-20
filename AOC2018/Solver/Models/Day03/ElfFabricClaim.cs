using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;

namespace AOC2018.Solver.Models
{
    /// <summary>
    /// Fabric claim made by an elf
    /// </summary>
    public class ElfFabricClaim
    {
        /// <summary>
        /// Claim Id
        /// </summary>
        public string Id { get; set; }

        //The claim
        public Rectangle Claim { get; set; }
        
        /// <summary>
        /// Parse the claim format
        /// </summary>
        /// <param name="claim"></param>
        public ElfFabricClaim(string claim)
        {
            var match = Regex.Match(claim, "#([0-9]*) @ ([0-9]*),([0-9]*): ([0-9]*)x([0-9]*)");
            Id = match.Groups[1].Value;
            Claim = new Rectangle(int.Parse(match.Groups[2].Value), int.Parse(match.Groups[3].Value), int.Parse(match.Groups[4].Value), int.Parse(match.Groups[5].Value));
        }

        /// <summary>
        /// Whether two claims intersect
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Intersects(ElfFabricClaim other)
        {
            return Claim.IntersectsWith(other.Claim);
        }

        /// <summary>
        /// Get the points that this claim covers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Point> GetCoveredPoints()
        {
            for (int x = Claim.Left; x < Claim.Right; x++)
            {
                for (int y = Claim.Top; y < Claim.Bottom; y++)
                {
                    yield return new Point(x, y);
                }
            }
        }

        /// <summary>
        /// String representation of the claim
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"#{Id} @ {Claim.X},{Claim.Y}: {Claim.Width}x{Claim.Height}";
        }
    }
}
