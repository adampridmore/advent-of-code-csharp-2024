namespace advent_of_code_csharp_2024;

public static class Day02
{
    public static readonly string InputFilename = @"../../../Day02_input.txt";

    public static List<int> ParseLine(string line)
    {
        return line.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();
    }

    public enum IsSafeEnum
    {
        Increasing,
        Decreasing,
        Unsafe
    }

    public static bool IsSafe(List<int> line)
    {
        var lookup =
            line
                .Zip(line.Skip(1).ToList(),
                    (a, b) =>
                    {
                        var diff = a - b;
                        if (diff == 0) return IsSafeEnum.Unsafe;
                        if (diff < -3) return IsSafeEnum.Unsafe;
                        if (diff > 3) return IsSafeEnum.Unsafe;
                        if (diff < 0) return IsSafeEnum.Decreasing;
                        if (diff > -3) return IsSafeEnum.Increasing;
                        throw new Exception($"Unexpected difference {diff}");
                    })
                .ToLookup(x => x);

        if (lookup.Count == 0)
        {
            return true;
        }

        return IsIncreasingOrDecreasingOnly(lookup);
    }

    public static bool IsSaveTolerant(List<int> line)
    {
        if (IsSafe(line))
        {
            return true;
        }

        for (int i = 0; i < line.Count; i++)
        {
            var removeLevel = new List<int>(line);
            removeLevel.RemoveAt(i);
            if (IsSafe(removeLevel))
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsIncreasingOrDecreasingOnly(ILookup<IsSafeEnum, IsSafeEnum> lookup)
    {
        if (lookup.Contains(IsSafeEnum.Unsafe))
        {
            return false;
        }

        if (lookup.Contains(IsSafeEnum.Decreasing) && !lookup.Contains(IsSafeEnum.Increasing))
        {
            return true;
        }

        if (!lookup.Contains(IsSafeEnum.Decreasing) && lookup.Contains(IsSafeEnum.Increasing))
        {
            return true;
        }

        return false;
    }
}
