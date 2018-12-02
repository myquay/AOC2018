using AOC2018.Solver;
using Xunit;

namespace AOC2018.Tests
{
    public class Day02Test
    {
        [Theory]
        [InlineData("abcdef, bababc, abbcde, abcccd, aabcdd, abcdee, ababab", "12")]
        public void TestDay02PartASolver(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, new Day02Solver().SolveA(input));
        }
        
        [Theory]
        [InlineData("abcde, fghij, klmno, pqrst, fguij, axcye, wvxyz", "fgij")]
        public void TestDay02PartBSolver(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, new Day02Solver().SolveB(input));
        }
    }
}
