namespace AOC2018
{
    public interface ISolver
    {
        string ProblemTitle { get; }
        string Solve(string input);
    }
}
