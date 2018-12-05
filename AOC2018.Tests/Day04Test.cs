using AOC2018.Solver;
using Xunit;

namespace AOC2018.Tests
{
    public class Day04Test
    {
        private const string QUESTION_FOUR_INPUT =

            //Test input
            @"[1518-11-01 00:00] Guard #10 begins shift
[1518-11-01 00:05] falls asleep
[1518-11-01 00:25] wakes up
[1518-11-01 00:30] falls asleep
[1518-11-01 00:55] wakes up
[1518-11-01 23:58] Guard #99 begins shift
[1518-11-02 00:40] falls asleep
[1518-11-02 00:50] wakes up
[1518-11-03 00:05] Guard #10 begins shift
[1518-11-03 00:24] falls asleep
[1518-11-03 00:29] wakes up
[1518-11-04 00:02] Guard #99 begins shift
[1518-11-04 00:36] falls asleep
[1518-11-04 00:46] wakes up
[1518-11-05 00:03] Guard #99 begins shift
[1518-11-05 00:45] falls asleep
[1518-11-05 00:55] wakes up";

        [Theory]
        [InlineData(QUESTION_FOUR_INPUT, "240")]
        public void TestDay04PartASolver(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, new Day04Solver().SolveA(input));
        }

        [Theory]
        [InlineData(QUESTION_FOUR_INPUT, "4455")]
        public void TestDay04PartBSolver(string input, string expectedResult)
        {
            Assert.Equal(expectedResult, new Day04Solver().SolveB(input));
        }
    }
}
