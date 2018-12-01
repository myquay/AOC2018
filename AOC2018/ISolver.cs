namespace AOC2018
{
    public interface ISolver
    {
        string ProblemTitle { get; }
        string SolveA(string input);
        string SolveB(string input);
    }
}
