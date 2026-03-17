namespace advent_of_code_csharp_2024;

public class Day11
{
    public static readonly string InputFilename = @"Day11_input.txt";

    private static Dictionary<long, long> Blink(Dictionary<long, long> stones)
    {
        var next = new Dictionary<long, long>();
        foreach (var (stone, count) in stones)
        {
            if (stone == 0)
            {
                next[1] = next.GetValueOrDefault(1) + count;
            }
            else
            {
                var digits = stone.ToString();
                if (digits.Length % 2 == 0)
                {
                    int half = digits.Length / 2;
                    long left = long.Parse(digits[..half]);
                    long right = long.Parse(digits[half..]);
                    next[left]  = next.GetValueOrDefault(left)  + count;
                    next[right] = next.GetValueOrDefault(right) + count;
                }
                else
                {
                    long v = stone * 2024;
                    next[v] = next.GetValueOrDefault(v) + count;
                }
            }
        }
        return next;
    }

    private static long Solve(string[] lines, int blinks)
    {
        var stones = lines[0].Split(' ')
            .Select(long.Parse)
            .GroupBy(x => x)
            .ToDictionary(g => g.Key, g => (long)g.Count());

        for (int i = 0; i < blinks; i++)
            stones = Blink(stones);

        return stones.Values.Sum();
    }

    public static long SolvePart1(string[] lines) => Solve(lines, 25);
    public static long SolvePart2(string[] lines) => Solve(lines, 75);
}
