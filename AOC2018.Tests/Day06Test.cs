using AOC2018.Solver;
using Xunit;

namespace AOC2018.Tests
{
    public class Day06Test
    {

        private const string INPUT = @"1, 1
1, 6
8, 3
3, 4
5, 5
8, 9";
        

        [Theory]
        [InlineData(INPUT, "17")]
        public void TestDay06PartASolver(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, new Day06Solver().SolveA(input));
        }
        
        //TODO: WITH THRESHOLD
        //[Theory]
        //[InlineData(INPUT, "4")]
        //public void TestDay06PartBSolver(string input, string expectedResult)
        //{
        //    Assert.Equal(expectedResult, new Day06Solver().SolveB(input));
        //}
        
    }
}
