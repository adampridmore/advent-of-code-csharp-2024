namespace advent_of_code_csharp_2024;

public class Day19
{
    public static readonly string InputFilename = @"Day19_input.txt";

    private static (string[] patterns, string[] designs) Parse(string[] lines)
    {
        var patterns = lines[0].Split(", ");
        var designs = lines[2..];
        return (patterns, designs);
    }

    private static long CountWays(string design, string[] patterns, Dictionary<string, long> memo)
    {
        if (design.Length == 0) return 1;
        if (memo.TryGetValue(design, out long cached)) return cached;
        long ways = patterns.Sum(p =>
            design.StartsWith(p) ? CountWays(design[p.Length..], patterns, memo) : 0);
        memo[design] = ways;
        return ways;
    }

    public static long SolvePart1(string[] lines)
    {
        var (patterns, designs) = Parse(lines);
        var memo = new Dictionary<string, long>();
        return designs.Count(d => CountWays(d, patterns, memo) > 0);
    }

    public static long SolvePart2(string[] lines)
    {
        var (patterns, designs) = Parse(lines);
        var memo = new Dictionary<string, long>();
        return designs.Sum(d => CountWays(d, patterns, memo));
    }
}
