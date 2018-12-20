using AOC2018.Solver;
using Xunit;

namespace AOC2018.Tests
{
    public class Day11Test
    {
        [Theory]
        [InlineData(3, 5, 8, "4")]
        [InlineData(122, 79, 57, "-5")]
        [InlineData(217, 196, 39, "0")]
        [InlineData(101, 153, 71, "4")]
        public void TestDay11PowerLevelSolver(int x, int y, int serialNumber, string expectedResult)
        {
            Assert.Equal(expectedResult, $"{new Day11Solver().CalculatePowerLevel(x, y, serialNumber)}");
        }

        [Theory]
        [InlineData("18", "33,45")]
        public void TestDay11PartASolver(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, new Day11Solver().SolveA(input));
        }

        //TODO: TOO SLOW FOR UNIT TESTING
        //[Theory]
        //[InlineData("18", "90,269,16")]
        //[InlineData("42", "232,251,12")]
        //public void TestDay11PartBSolver(string input, string expectedResult)
        //{
        //    Assert.Equal(expectedResult, new Day11Solver().SolveB(input));
        //}
    }
}
