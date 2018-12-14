using AOC2018.Solver;
using Xunit;

namespace AOC2018.Tests
{
    public class Day08Test
    {

        [Theory]
        [InlineData("2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2", "138")]
        public void TestDay08PartASolver(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, new Day08Solver().SolveA(input));
        }

        [Theory]
        [InlineData("2 3 0 3 10 11 12 1 1 0 1 99 2 1 1 2", "66")]
        public void TestDay08PartBSolver(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, new Day08Solver().SolveB(input));
        }

    }
}
