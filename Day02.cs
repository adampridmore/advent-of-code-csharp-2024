namespace advent_of_code_csharp_2024;

public class Day02
{
    public static readonly string InputFilename = @"Day02_input.txt";

    public static List<int> ParseLine(string line) =>
        line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

    public static bool IsSafe(IEnumerable<int> levels)
    {
        var diffs = levels.Zip(levels.Skip(1), (a, b) => b - a).ToList();
        return diffs.All(d => d is >= 1 and <= 3) || diffs.All(d => d is >= -3 and <= -1);
    }

    public static bool IsSafeTolerant(List<int> line)
    {
        if (IsSafe(line)) return true;
        return Enumerable.Range(0, line.Count)
            .Any(i => IsSafe(line.Where((_, idx) => idx != i)));
    }
}
