using AOC2018.Solver;
using Xunit;

namespace AOC2018.Tests
{
    public class Day09Test
    {
        [Theory]
        [InlineData("9 players; last marble is worth 25 points", "32")]
        [InlineData("10 players; last marble is worth 1618 points", "8317")]
        [InlineData("13 players; last marble is worth 7999 points", "146373")]
        [InlineData("17 players; last marble is worth 1104 points", "2764")]
        [InlineData("21 players; last marble is worth 6111 points", "54718")]
        [InlineData("30 players; last marble is worth 5807 points", "37305")]
        public void TestDay09PartASolver(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, new Day09Solver().SolveA(input));
        }
    }
}
