using AOC2018.Solver;
using Xunit;

namespace AOC2018.Tests
{
    public class Day05Test
    {
     
        [Theory]
        [InlineData("dabAcCaCBAcCcaDA", "10")]
        public void TestDay05PartASolver(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, new Day05Solver().SolveA(input));
        }
        
        [Theory]
        [InlineData("dabAcCaCBAcCcaDA", "4")]
        public void TestDay05PartBSolver(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, new Day05Solver().SolveB(input));
        }
        
    }
}
