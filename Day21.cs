namespace advent_of_code_csharp_2024;

public class Day21
{
    public static readonly string InputFilename = @"Day21_input.txt";

    // Numeric keypad layout:
    //  7 8 9
    //  4 5 6
    //  1 2 3
    //    0 A
    private static readonly Dictionary<char, (int r, int c)> NumPos = new()
    {
        ['7']=(0,0), ['8']=(0,1), ['9']=(0,2),
        ['4']=(1,0), ['5']=(1,1), ['6']=(1,2),
        ['1']=(2,0), ['2']=(2,1), ['3']=(2,2),
                     ['0']=(3,1), ['A']=(3,2),
    };

    // Directional keypad layout:
    //    ^ A
    //  < v >
    private static readonly Dictionary<char, (int r, int c)> DirPos = new()
    {
                     ['^']=(0,1), ['A']=(0,2),
        ['<']=(1,0), ['v']=(1,1), ['>']=(1,2),
    };

    // Generate all shortest paths between two keys on a given keypad
    private static List<string> Paths(
        Dictionary<char, (int r, int c)> pad, char from, char to, (int r, int c) gap)
    {
        var (fr, fc) = pad[from];
        var (tr, tc) = pad[to];
        int dr = tr - fr, dc = tc - fc;
        string vert = new(dr > 0 ? 'v' : '^', Math.Abs(dr));
        string horiz = new(dc > 0 ? '>' : '<', Math.Abs(dc));

        var results = new List<string>();
        // Try horizontal first then vertical (avoid gap)
        if (dr == 0) { results.Add(horiz + "A"); }
        else if (dc == 0) { results.Add(vert + "A"); }
        else
        {
            // Horizontal first: moves through (fr, tc); avoid gap
            if ((fr, tc) != gap) results.Add(horiz + vert + "A");
            // Vertical first: moves through (tr, fc); avoid gap
            if ((tr, fc) != gap) results.Add(vert + horiz + "A");
        }
        return results.Distinct().ToList();
    }

    private static long MinPresses(string seq, int depth, Dictionary<(string, int), long> memo)
    {
        var key = (seq, depth);
        if (memo.TryGetValue(key, out long cached)) return cached;
        if (depth == 0) { memo[key] = seq.Length; return seq.Length; }

        var gap = (0, 0); // directional keypad gap
        long total = 0;
        char prev = 'A';
        foreach (char ch in seq)
        {
            var paths = Paths(DirPos, prev, ch, gap);
            long best = paths.Min(p => MinPresses(p, depth - 1, memo));
            total += best;
            prev = ch;
        }
        memo[key] = total;
        return total;
    }

    private static long Solve(string[] lines, int depth)
    {
        var memo = new Dictionary<(string, int), long>();
        long total = 0;
        var numGap = (3, 0); // numeric keypad gap
        foreach (var code in lines)
        {
            long presses = 0;
            char prev = 'A';
            foreach (char ch in code)
            {
                var paths = Paths(NumPos, prev, ch, numGap);
                presses += paths.Min(p => MinPresses(p, depth, memo));
                prev = ch;
            }
            long num = long.Parse(code.TrimEnd('A'));
            total += presses * num;
        }
        return total;
    }

    public static long SolvePart1(string[] lines) => Solve(lines, 2);
    public static long SolvePart2(string[] lines) => Solve(lines, 25);
}
