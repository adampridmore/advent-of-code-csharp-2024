namespace advent_of_code_csharp_2024;

public class Day25
{
    public static readonly string InputFilename = @"Day25_input.txt";

    public static long SolvePart1(string[] lines)
    {
        var locks = new List<int[]>();
        var keys = new List<int[]>();

        for (int i = 0; i < lines.Length; i += 8)
        {
            bool isLock = lines[i] == "#####";
            var heights = new int[5];
            for (int r = 1; r <= 5; r++)
                for (int c = 0; c < 5; c++)
                    if (lines[i + r][c] == '#') heights[c]++;
            (isLock ? locks : keys).Add(heights);
        }

        long count = 0;
        foreach (var lk in locks)
            foreach (var key in keys)
                if (Enumerable.Range(0, 5).All(c => lk[c] + key[c] <= 5))
                    count++;
        return count;
    }
}
