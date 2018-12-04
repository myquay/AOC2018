using System;
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
            var commands = input.Split(new[] { Environment.NewLine, ", " }, StringSplitOptions.None)
                .Select(s =>
                {
                    var timeStamp = DateTime.ParseExact(s.Split("]")[0].Remove(0, 1), "yyyy-MM-dd HH:mm", new CultureInfo("en-NZ"));
                    return new
                    {
                        TimeStamp = timeStamp,
                        Command = s.Split("]")[1].Trim()
                    };
                }).OrderBy(c => c.TimeStamp)
                .Select(n =>  new
                {
                    n.TimeStamp,
                    IsWakeUp = n.Command == "wakes up",
                    IsAlseep = n.Command == "falls asleep",
                    GuardId = Regex.Replace(n.Command, @"[^\d]", "").ToInt()
                }).ToArray();

            var guardDictionary = commands.Where(c => c.GuardId != null)
                .Select(c => c.GuardId.Value)
                .Distinct()
                .ToDictionary(key => key, value => new int[60]);

            //Record the minutes asleep for a guard
            var currentGuard = commands[0].GuardId.Value;
            
            for(int i = 1; i< commands.Length; i++)
            {
                if (commands[i].IsAlseep)
                {
                    var start = commands[i].TimeStamp;
                    var end = commands[i + 1].TimeStamp;
                    for(int j = start.Minute; j < end.Minute; j++)
                    {
                        guardDictionary[currentGuard][j]++;
                    }
                }

                currentGuard = commands[i].GuardId ?? currentGuard;
            }

            var sleepiestGuard = guardDictionary.Select(g => new
            {
                GuardId = g.Key,
                NumMinutesAsleep = g.Value.Sum(),
                MostLikelyMinute = Array.IndexOf(g.Value, g.Value.Max())
            }).OrderByDescending(s => s.NumMinutesAsleep).First(); ;
            
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
            var commands = input.Split(new[] { Environment.NewLine, ", " }, StringSplitOptions.None)
                .Select(s =>
                {
                    var timeStamp = DateTime.ParseExact(s.Split("]")[0].Remove(0, 1), "yyyy-MM-dd HH:mm", new CultureInfo("en-NZ"));
                    return new
                    {
                        TimeStamp = timeStamp,
                        Command = s.Split("]")[1].Trim()
                    };
                }).OrderBy(c => c.TimeStamp)
                .Select(n => new
                {
                    n.TimeStamp,
                    IsWakeUp = n.Command == "wakes up",
                    IsAlseep = n.Command == "falls asleep",
                    GuardId = Regex.Replace(n.Command, @"[^\d]", "").ToInt()
                }).ToArray();

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
                    var end = commands[i + 1].TimeStamp;
                    for (int j = start.Minute; j < end.Minute; j++)
                    {
                        guardDictionary[currentGuard][j]++;
                    }
                }

                currentGuard = commands[i].GuardId ?? currentGuard;
            }

            var consistentGuard = guardDictionary
                .OrderByDescending(d => d.Value.Max())
                .First();
            
            return $"{consistentGuard.Key * Array.IndexOf(consistentGuard.Value, consistentGuard.Value.Max())}";
        }
    }
}
