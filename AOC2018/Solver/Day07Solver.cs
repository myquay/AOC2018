using AOC2018.Solver.Models;
using System;
using System.Drawing;
using System.Linq;

namespace AOC2018.Solver
{
    /// <summary>
    /// Solution for the seventh day advent problem
    /// </summary>
    [AdventDay(day: 7)]
    public class Day07Solver : ISolver
    {
        public string ProblemTitle => "Day 7: The Sum of Its Parts";

        private readonly int NUMBER_OF_WORKERS = 5;
        private readonly int STEP_EFFORT = 60;

        /// <summary>
        /// Default for input
        /// </summary>
        public Day07Solver() { }

        /// <summary>
        /// Configurable puzzle parameters
        /// </summary>
        /// <param name="numberOfWorkers"></param>
        /// <param name="stepEffort"></param>
        public Day07Solver(int numberOfWorkers, int stepEffort) {
            NUMBER_OF_WORKERS = numberOfWorkers;
            STEP_EFFORT = stepEffort;
        }

        /// <summary>
        /// Find out the order in which to build the sleigh
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SolveA(string input)
        {
            var graphRoot = BuildSleighInstructionGraph(input);

            var instructions = StepsForNodes(graphRoot);

            return instructions;
        }

        /// <summary>
        /// Build the sleigh grpah
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public SleighInstructionNode[] BuildSleighInstructionGraph(string input)
        {
            //Parsed steps
            var steps = input.Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                .Select(s => s.Replace(" must be finished before step ", ",").Replace("Step ", "").Replace(" can begin.", ""))
                .Select(s => new
                {
                    Initial = s.Split(",")[0],
                    Subsequent = s.Split(",")[1]
                }).ToArray();

            //Legal steps from one state to the next
            var stepNotes = steps.Select(s => s.Initial).Concat(steps.Select(s => s.Subsequent)).Distinct()
                .Select(s => new SleighInstructionNode(stepEffort: STEP_EFFORT)
                {
                    Step = s
                }).ToDictionary(s => s.Step);

            foreach (var step in steps)
            {
                var initial = stepNotes[step.Initial];
                var subsequent = stepNotes[step.Subsequent];

                if (!initial.NextSteps.Contains(subsequent))
                {
                    initial.NextSteps.Add(subsequent);
                }

                if (!subsequent.Prerequisites.Contains(initial))
                {
                    subsequent.Prerequisites.Add(initial);
                }
            }
            
            return stepNotes.Select(s => s.Value).Where(s => !s.Prerequisites.Any()).ToArray();
        }

        /// <summary>
        /// Step through the build graph while recording order of steps
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public string StepsForNodes(SleighInstructionNode[] options)
        {
            if (!options.Any())
                return String.Empty;

            //Next step is the smallest where all prerequisites have been met
            var nextStep = options.Where(o => !o.Prerequisites.Where(p => !p.Visited).Any()).Min();
            nextStep.Visited = true; //Visited this node

            //Options are current non-visited options, plus the visited node's childern
            options = options.Where(o => o != nextStep).Concat(nextStep.NextSteps.Where(ns => !ns.Visited)).ToArray();

            return $"{nextStep.Step}{StepsForNodes(options)}";
        }

        /// <summary>
        /// Calculate the amount of time to put the sleigh together
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public string SolveB(string input)
        {
            var graphRoot = BuildSleighInstructionGraph(input);

            var effort = StepsForNodesWithEffort(graphRoot, 0);

            return $"{effort}";
        }

        /// <summary>
        /// Step through the graph while recording effort
        /// </summary>
        /// <param name="options"></param>
        /// <param name="secondsElapsed"></param>
        /// <returns></returns>
        public int StepsForNodesWithEffort(SleighInstructionNode[] options, int secondsElapsed)
        {
            if (!options.Any())
                return secondsElapsed;

            //Can work on upto four steps simultaneously
            var nextSteps = options.Where(o => !o.Prerequisites.Where(p => !p.Visited).Any())
                .OrderBy(a => a.TotalEffort == a.EffortRemaining) //Keep ones we're working on up top
                .ThenBy(a => a)
                .Take(NUMBER_OF_WORKERS).ToArray();

            //Wait until a step is complete
            var timeToElapse = nextSteps.Select(s => s.EffortRemaining).Min();
            secondsElapsed = secondsElapsed + timeToElapse;
            foreach (var step in nextSteps)
                step.Tick(timeToElapse);

            var nextStep = nextSteps.Where(s => s.Visited).Min();

            //Options are current non-visited options, plus the visited node's childern
            options = options.Where(o => o != nextStep).Concat(nextStep.NextSteps.Where(ns => !ns.Visited)).Distinct().ToArray();
            
            return StepsForNodesWithEffort(options, secondsElapsed);
        }

    }
}
