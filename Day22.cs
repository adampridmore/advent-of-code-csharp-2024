namespace advent_of_code_csharp_2024;

public class Day22
{
    public static readonly string InputFilename = @"Day22_input.txt";

    private static long Next(long s)
    {
        s = (s ^ (s << 6)) & 0xFFFFFF;
        s = (s ^ (s >> 5)) & 0xFFFFFF;
        s = (s ^ (s << 11)) & 0xFFFFFF;
        return s;
    }

    public static long SolvePart1(string[] lines) =>
        lines.Sum(l =>
        {
            long s = long.Parse(l);
            for (int i = 0; i < 2000; i++) s = Next(s);
            return s;
        });

    public static long SolvePart2(string[] lines)
    {
        var totals = new Dictionary<(int, int, int, int), long>();

        foreach (var line in lines)
        {
            long s = long.Parse(line);
            var prices = new int[2001];
            prices[0] = (int)(s % 10);
            for (int i = 1; i <= 2000; i++) { s = Next(s); prices[i] = (int)(s % 10); }

            var seen = new HashSet<(int, int, int, int)>();
            for (int i = 4; i <= 2000; i++)
            {
                var key = (prices[i - 3] - prices[i - 4],
                           prices[i - 2] - prices[i - 3],
                           prices[i - 1] - prices[i - 2],
                           prices[i] - prices[i - 1]);
                if (seen.Add(key))
                    totals[key] = totals.GetValueOrDefault(key) + prices[i];
            }
        }
        return totals.Values.Max();
    }
}
