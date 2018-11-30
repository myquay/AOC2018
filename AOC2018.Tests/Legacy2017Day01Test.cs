using AOC2018.Solver;
using System;
using Xunit;

namespace AOC2018.Tests
{
    public class Legacy2017Day01Test
    {
        [Theory]
        [InlineData("1122", "3")]
        [InlineData("1111", "4")]
        [InlineData("1234", "0")]
        [InlineData("91212129", "9")]
        public void TestLegacy2017Day01Solver(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, new Legacy2017Day01Solver().Solve(input));
        }
    }
}
