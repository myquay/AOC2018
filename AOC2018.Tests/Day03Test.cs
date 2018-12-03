using AOC2018.Solver;
using AOC2018.Solver.Models;
using Xunit;

namespace AOC2018.Tests
{
    public class Day03Test
    {
        [Theory]
        [InlineData("#123 @ 3,2: 5x4", "#123 @ 3,2: 5x4")]
        public void TestDay03ElfClaimParser(string input, string expectedResult)
        {
            var claim = new ElfFabricClaim(input);
            Assert.Equal(expectedResult, claim.ToString());
        }

        [Theory]
        [InlineData("#1 @ 1,3: 4x4, #2 @ 3,1: 4x4, #3 @ 5,5: 2x2", "4")]
        public void TestDay03PartASolver(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, new Day03Solver().SolveA(input));
        }

        [Theory]
        [InlineData("#1 @ 1,3: 4x4, #2 @ 3,1: 4x4, #3 @ 5,5: 2x2", "3")]
        public void TestDay03PartBSolver(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, new Day03Solver().SolveB(input));
        }
    }
}
