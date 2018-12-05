using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AOC2018
{
    class Program
    {
        static void Main(string[] args)
        {
            var allSolvers = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x => x.GetInterfaces().Contains(typeof(ISolver)))
                .Select(x => Activator.CreateInstance(x) as ISolver)
                .ToDictionary(x => (x.GetType().GetCustomAttributes(typeof(AdventDayAttribute), true).Single() as AdventDayAttribute).Day, x => x);
            
            Console.WriteLine("Advent of Code 2018");
            Console.WriteLine("------------------------");
            Console.WriteLine("Enter a day (1-25) or q to quit");
            Console.WriteLine("========================");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine();
                var command = Console.ReadLine();
                Console.WriteLine();

                switch (command)
                {
                    case var o when o.IsInt():
                        var day = int.Parse(o);
                        if (allSolvers.ContainsKey(day))
                        {
                            Console.WriteLine($"Solution for {allSolvers[day].ProblemTitle}");

                            var input = File.Exists($"{Directory.GetCurrentDirectory()}/Input/input-a-{day:00}.txt") ?
                                File.ReadAllText($"{Directory.GetCurrentDirectory()}/Input/input-a-{day:00}.txt") :
                                File.ReadAllText($"{Directory.GetCurrentDirectory()}/Input/input-{day:00}.txt");

                            var stopwatch = Stopwatch.StartNew();
                            var result = allSolvers[day].SolveA(input);
                            stopwatch.Stop();

                            Console.WriteLine($"{result} - {stopwatch.ElapsedMilliseconds}ms");
                            
                            input = File.Exists($"{Directory.GetCurrentDirectory()}/Input/input-b-{day:00}.txt") ?
                                File.ReadAllText($"{Directory.GetCurrentDirectory()}/Input/input-b-{day:00}.txt") :
                                File.ReadAllText($"{Directory.GetCurrentDirectory()}/Input/input-{day:00}.txt");

                            stopwatch = Stopwatch.StartNew();
                            result = allSolvers[day].SolveB(input);
                            stopwatch.Stop();

                            Console.WriteLine($"{result} - {stopwatch.ElapsedMilliseconds}ms");
                        }
                        else
                        {
                            Console.WriteLine("No solution for that day");
                        }
                        break;
                    case "q":
                        return;
                    default:
                        Console.WriteLine("Unknown command");
                        break;
                }
            }
        }
    }
}
