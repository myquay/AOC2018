using System;
using System.Collections.Generic;

namespace AOC2018.Solver.Models
{
    /// <summary>
    /// Node in the graph
    /// </summary>
    public class SleighInstructionNode : IComparable
    {
        private readonly int STEP_EFFORT = 60;

        public int TotalEffort { get => char.ToUpper(Step[0]) - 64 + STEP_EFFORT; }

        public SleighInstructionNode() { }

        public SleighInstructionNode(int stepEffort) {
            STEP_EFFORT = stepEffort;
        }
        
        private string _step;
        public string Step
        {
            get => _step;
            set
            {
                _effortRemaining = char.ToUpper(value[0]) - 64 + STEP_EFFORT;
                _step = value;
            }
        }

        private int _effortRemaining { get; set; }

        public int EffortRemaining { get { return _effortRemaining; } }

        public void Tick(int timeElapsed = 1)
        {
            _effortRemaining = _effortRemaining - timeElapsed;
            if (_effortRemaining <= 0)
                Visited = true;
        }

        public List<SleighInstructionNode> NextSteps { get; set; } = new List<SleighInstructionNode>();

        public List<SleighInstructionNode> Prerequisites { get; set; } = new List<SleighInstructionNode>();

        public bool Visited { get; set; }

        public int CompareTo(object obj)
        {
            var other = obj as SleighInstructionNode;
            return Step.CompareTo(other.Step);
        }

        public override bool Equals(object obj)
        {
            var other = obj as SleighInstructionNode;
            return Step.Equals(other.Step);
        }
    }
}
