using System.Collections;
using System.IO.Compression;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace advent_of_code_csharp_2024;

public class Day02Tests
{
    private readonly string _testLines = @"7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9";

    [Fact]
    public void ExampleLines_Part1()
    {
        var lines = _testLines.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var parsedLines = lines
            .Select(Day02.ParseLine);

        var expected = new List<int> { 7, 6, 4, 2, 1 };
        Assert.Equal(expected, parsedLines.ToList()[0]);

        Assert.Equal(2, parsedLines.Count(Day02.IsSafe));
    }

    [Fact]
    public void RealData_Part1()
    {
        var lines = File
            .ReadLines(Day02.InputFilename)
            .Select(Day02.ParseLine);

        var ans = lines.Count(Day02.IsSafe);

        Assert.Equal(242, ans);
    }

    [Fact]
    public void ExampleLines_Part2()
    {
        var lines = _testLines
            .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
            .Select(Day02.ParseLine);

        var expected = new List<int> { 7, 6, 4, 2, 1 };
        Assert.Equal(expected, lines.ToList()[0]);

        Assert.Equal(4, lines.Count(Day02.IsSaveTolerant));
    }

    [Fact]
    public void RealData_Part2()
    {
        var lines = File.ReadLines(Day02.InputFilename);

        var parsedLines = lines
            .Select(Day02.ParseLine);

        var ans = parsedLines.Count(Day02.IsSaveTolerant);

        Assert.Equal(311, ans);
    }
    [Fact]
    public void IsSafeTest()
    {
        Assert.True(Day02.IsSafe(new List<int>()), "empty");
        Assert.True(Day02.IsSafe(new List<int> { 1, 2 }), "1 2");
        Assert.True(Day02.IsSafe(new List<int> { 1, 2 }), "2 1");
        Assert.True(Day02.IsSafe(new List<int> { 1, 4 }), "1 4");
        Assert.True(Day02.IsSafe(new List<int> { 4, 1 }), "4 1");

        Assert.False(Day02.IsSafe(new List<int> { 1, 1 }), "1 1");
        Assert.False(Day02.IsSafe(new List<int> { 1, 5 }), "1 5");
        Assert.False(Day02.IsSafe(new List<int> { 5, 1 }), "4 1");

        Assert.True(Day02.IsSafe(Day02.ParseLine("7 6 4 2 1"))); // true
        Assert.False(Day02.IsSafe(Day02.ParseLine("1 2 7 8 9"))); // false
        Assert.False(Day02.IsSafe(Day02.ParseLine("9 7 6 2 1"))); // false
        Assert.False(Day02.IsSafe(Day02.ParseLine("1 3 2 4 5"))); // false
        Assert.False(Day02.IsSafe(Day02.ParseLine("8 6 4 4 1"))); // false
        Assert.True(Day02.IsSafe(Day02.ParseLine("1 3 6 7 9"))); // true
    }

    [Fact]
    public void IsSafeTolerantTest()
    {
        Assert.True(Day02.IsSaveTolerant(new List<int>()), "empty");
        Assert.True(Day02.IsSaveTolerant(new List<int> { 1, 2 }), "1 2");
        Assert.True(Day02.IsSaveTolerant(new List<int> { 1, 2 }), "2 1");
        Assert.True(Day02.IsSaveTolerant(new List<int> { 1, 4 }), "1 4");
        Assert.True(Day02.IsSaveTolerant(new List<int> { 4, 1 }), "4 1");

        Assert.False(Day02.IsSaveTolerant(new List<int> { 1, 1, 1 }), "1 1 1");
        Assert.False(Day02.IsSaveTolerant(new List<int> { 1, 5, 10 }), "1 5 10");
        Assert.False(Day02.IsSaveTolerant(new List<int> { 10, 5, 1 }), "8 4 1");

        Assert.True(Day02.IsSaveTolerant(Day02.ParseLine("7 6 4 2 1")));
        Assert.False(Day02.IsSaveTolerant(Day02.ParseLine("1 2 7 8 9")));
        Assert.False(Day02.IsSaveTolerant(Day02.ParseLine("9 7 6 2 1")));
        Assert.True(Day02.IsSaveTolerant(Day02.ParseLine("1 3 2 4 5")));
        Assert.True(Day02.IsSaveTolerant(Day02.ParseLine("8 6 4 4 1")));
        Assert.True(Day02.IsSaveTolerant(Day02.ParseLine("1 3 6 7 9")));
    }
}
