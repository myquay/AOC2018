using AOC2018.Solver;
using Xunit;

namespace AOC2018.Tests
{
    public class Day07Test
    {

        private const string INPUT = @"Step C must be finished before step A can begin.
Step C must be finished before step F can begin.
Step A must be finished before step B can begin.
Step A must be finished before step D can begin.
Step B must be finished before step E can begin.
Step D must be finished before step E can begin.
Step F must be finished before step E can begin.";
        

        [Theory]
        [InlineData(INPUT, "CABDFE")]
        public void TestDay07PartASolver(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, new Day07Solver().SolveA(input));
        }

        [Theory]
        [InlineData(INPUT, "15")]
        public void TestDay07PartBSolver(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, new Day07Solver(numberOfWorkers: 2, stepEffort: 0).SolveB(input));
        }

    }
}
