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

    private static long CountWays(string design, string[] patterns)
    {
        var dp = new long[design.Length + 1];
        dp[0] = 1;
        for (int i = 0; i < design.Length; i++)
        {
            if (dp[i] == 0) continue;
            foreach (var p in patterns)
                if (i + p.Length <= design.Length && design.AsSpan(i, p.Length).SequenceEqual(p))
                    dp[i + p.Length] += dp[i];
        }
        return dp[design.Length];
    }

    public static long SolvePart1(string[] lines)
    {
        var (patterns, designs) = Parse(lines);
        return designs.Count(d => CountWays(d, patterns) > 0);
    }

    public static long SolvePart2(string[] lines)
    {
        var (patterns, designs) = Parse(lines);
        return designs.Sum(d => CountWays(d, patterns));
    }
}
