using System.Collections;
using System.IO.Compression;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace advent_of_code_csharp_2024;

public class Day02
{
    private readonly string _inputFilename = @"../../../Day02_input.txt";

    private readonly string _testLines = @"7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9";

    private static List<int> ParseLine(string line)
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

    private static bool IsSafe(List<int> line)
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

    private static bool IsSaveTolerant(List<int> line)
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

    [Fact]
    public void ExampleLines_Part1()
    {
        var lines = _testLines.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var parsedLines = lines
            .Select(ParseLine);

        var expected = new List<int> { 7, 6, 4, 2, 1 };
        Assert.Equal(expected, parsedLines.ToList()[0]);

        Assert.Equal(2, parsedLines.Count(IsSafe));
    }

    [Fact]
    public void RealData_Part1()
    {
        var lines = File
            .ReadLines(_inputFilename)
            .Select(ParseLine);

        var ans = lines.Count(IsSafe);

        Assert.Equal(242, ans);
    }

    [Fact]
    public void ExampleLines_Part2()
    {
        var lines = _testLines
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select(ParseLine);

        var expected = new List<int> { 7, 6, 4, 2, 1 };
        Assert.Equal(expected, lines.ToList()[0]);

        Assert.Equal(4, lines.Count(IsSaveTolerant));
    }

    [Fact]
    public void RealData_Part2()
    {
        var lines = File.ReadLines(_inputFilename);

        var parsedLines = lines
            .Select(ParseLine);

        var ans = parsedLines.Count(IsSaveTolerant);

        Assert.Equal(311, ans);
    }
    [Fact]
    public void IsSafeTest()
    {
        Assert.True(IsSafe(new List<int>()), "empty");
        Assert.True(IsSafe(new List<int> { 1, 2 }), "1 2");
        Assert.True(IsSafe(new List<int> { 1, 2 }), "2 1");
        Assert.True(IsSafe(new List<int> { 1, 4 }), "1 4");
        Assert.True(IsSafe(new List<int> { 4, 1 }), "4 1");

        Assert.False(IsSafe(new List<int> { 1, 1 }), "1 1");
        Assert.False(IsSafe(new List<int> { 1, 5 }), "1 5");
        Assert.False(IsSafe(new List<int> { 5, 1 }), "4 1");

        Assert.True(IsSafe(ParseLine("7 6 4 2 1"))); // true
        Assert.False(IsSafe(ParseLine("1 2 7 8 9"))); // false
        Assert.False(IsSafe(ParseLine("9 7 6 2 1"))); // false
        Assert.False(IsSafe(ParseLine("1 3 2 4 5"))); // false
        Assert.False(IsSafe(ParseLine("8 6 4 4 1"))); // false
        Assert.True(IsSafe(ParseLine("1 3 6 7 9"))); // true
    }

    [Fact]
    public void IsSafeTolerantTest()
    {
        Assert.True(IsSaveTolerant(new List<int>()), "empty");
        Assert.True(IsSaveTolerant(new List<int> { 1, 2 }), "1 2");
        Assert.True(IsSaveTolerant(new List<int> { 1, 2 }), "2 1");
        Assert.True(IsSaveTolerant(new List<int> { 1, 4 }), "1 4");
        Assert.True(IsSaveTolerant(new List<int> { 4, 1 }), "4 1");

        Assert.False(IsSaveTolerant(new List<int> { 1, 1, 1 }), "1 1 1");
        Assert.False(IsSaveTolerant(new List<int> { 1, 5, 10 }), "1 5 10");
        Assert.False(IsSaveTolerant(new List<int> { 10, 5, 1 }), "8 4 1");

        Assert.True(IsSaveTolerant(ParseLine("7 6 4 2 1")));
        Assert.False(IsSaveTolerant(ParseLine("1 2 7 8 9")));
        Assert.False(IsSaveTolerant(ParseLine("9 7 6 2 1")));
        Assert.True(IsSaveTolerant(ParseLine("1 3 2 4 5")));
        Assert.True(IsSaveTolerant(ParseLine("8 6 4 4 1")));
        Assert.True(IsSaveTolerant(ParseLine("1 3 6 7 9")));
    }
}
