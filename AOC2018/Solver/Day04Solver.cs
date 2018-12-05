using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace AOC2018.Solver
{
    /// <summary>
    /// Solution for the third day advent problem
    /// </summary>
    [AdventDay(day: 4)]
    public class Day04Solver : ISolver
    {
        public string ProblemTitle => "Day 4: Repose Record";
        
        /// <summary>
        /// Find the most likely minute the sleepiest guard will be asleep for
        /// </summary>
        /// <remarks>
        /// Assumptions: Only one minute is most likely minute, guard falls asleep first, guard always wakes up before shift change
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SolveA(string input)
        {
            //Parse the input
            var guardData = GetGuardSleepPatterns(input);

            //Find the sleepiest guard
            var sleepiestGuard = guardData.Select(g => new
            {
                GuardId = g.Key,
                NumMinutesAsleep = g.Value.Sum(),
                MostLikelyMinute = Array.IndexOf(g.Value, g.Value.Max())
            }).OrderByDescending(s => s.NumMinutesAsleep).First();
            
            return $"{sleepiestGuard.GuardId*sleepiestGuard.MostLikelyMinute}";
        }

        /// <summary>
        /// Find the likeliest minute the most consistent guard will be asleep for
        /// </summary>
        /// <remarks>
        /// Assumptions: Only one minute is most consistent minute, guard falls asleep first, guard always wakes up before shift change
        /// </remarks>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SolveB(string input)
        {
            //Parse the input
            var guardData = GetGuardSleepPatterns(input);

            //Find the most consistent guard
            var consistentGuard = guardData
                .OrderByDescending(d => d.Value.Max())
                .First();
            
            return $"{consistentGuard.Key * Array.IndexOf(consistentGuard.Value, consistentGuard.Value.Max())}";
        }


        /// <summary>
        /// Parse the input into our guard guard sleep pattern data structure
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private Dictionary<int, int[]> GetGuardSleepPatterns(string input)
        {
            //Parse the input
            var commands = input.Split(new[] { Environment.NewLine, ", " }, StringSplitOptions.None)
                .Select(s =>
                {
                    var timeStamp = DateTime.ParseExact(s.Split("]")[0].Remove(0, 1), "yyyy-MM-dd HH:mm", new CultureInfo("en-NZ"));
                    var command = s.Split("]")[1].Trim();

                    return new
                    {
                        TimeStamp = timeStamp,
                        IsWakeUp = command == "wakes up",
                        IsAlseep = command == "falls asleep",
                        GuardId = Regex.Replace(command, @"[^\d]", "").ToInt()
                    };

                }).OrderBy(c => c.TimeStamp).ToArray();

            //Data structure for storing sleep patterns
            var guardDictionary = commands.Where(c => c.GuardId != null)
                .Select(c => c.GuardId.Value)
                .Distinct()
                .ToDictionary(key => key, value => new int[60]);

            //Record the minutes asleep for a guard
            var currentGuard = commands[0].GuardId.Value;
            for (int i = 1; i < commands.Length; i++)
            {
                if (commands[i].IsAlseep)
                {
                    var start = commands[i].TimeStamp;
                    var end = commands[i + 1].TimeStamp; //Assume a sleep event is followed by a wake event
                    for (int j = start.Minute; j < end.Minute; j++) //End minute is exclusive
                    {
                        //Hit each minute the guard is asleep
                        guardDictionary[currentGuard][j]++;
                    }
                }

                //Move to next guard when required
                currentGuard = commands[i].GuardId ?? currentGuard;
            }

            return guardDictionary;
        }
    }
}
