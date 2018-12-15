namespace AdventOfCode
{
    public interface IPuzzle
    {
        bool Active { get; }

        string RunOne();

        string RunTwo();
    }
}
