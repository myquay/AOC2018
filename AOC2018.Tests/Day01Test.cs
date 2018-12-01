using AOC2018.Solver;
using Xunit;

namespace AOC2018.Tests
{
    public class Day01Test
    {
        [Theory]
        [InlineData("+1, +1, +1", "3")]
        [InlineData("+1, +1, -2", "0")]
        [InlineData("-1, -2, -3", "-6")]
        public void TestDay01PartASolver(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, new Day01Solver().SolveA(input));
        }

        [Theory]
        [InlineData("+1, -1", "0")]
        [InlineData("+3, +3, +4, -2, -4", "10")]
        [InlineData("-6, +3, +8, +5, -6", "5")]
        [InlineData("+7, +7, -2, -7, -4", "14")]
        public void TestDay01PartBSolver(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, new Day01Solver().SolveB(input));
        }
    }
}
