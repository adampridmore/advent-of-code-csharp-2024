using System.Collections;
using System.IO.Compression;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;

namespace advent_of_code_scala_2024;

public class Day02
{
    private readonly string _inputFilename = @"../../../Day02_input.txt";

    private readonly string _testLines = @"7 6 4 2 1
1 2 7 8 9
9 7 6 2 1
1 3 2 4 5
8 6 4 4 1
1 3 6 7 9";

    private static List<int> ParseLine(string line){
        return line.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                    .Select(int.Parse)
                    .ToList();
    }

    [Fact]
    public void ExampleLines_Part1()
    {
        var lines = _testLines.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var parsedLines = lines
            .Select(ParseLine);

        var expected = new List<int>{ 7, 6, 4, 2, 1};
        Assert.Equal(expected, parsedLines.ToList()[0]);

        Assert.Equal(2, parsedLines.Count(IsSafe));
    }

    [Fact]
    public void RealData_Part1()
    {
    var lines = File.ReadLines(_inputFilename);

        var parsedLines = lines
            .Select(ParseLine);

        var ans = parsedLines.Count(IsSafe);

        Assert.Equal(242, ans);
    }

    private static bool IsSafe(List<int> line) 
    {
        var groupings = line
            .Zip(line.Skip(1).ToList(),
                (a, b) => {

                    var diff = a - b;

                    if (diff == 0) return 0;
                    if (diff < -3) return 0;
                    if (diff > 3) return 0;
                    if (diff < 0) return -1;
                    if (diff > 0) return 1;

                    throw new Exception($"Unexpected different {diff}");
                })
            .GroupBy(x=>x)
            ;

        if (!groupings.Any()){
            return true;
        }

        if (groupings.Count() == 1){
            if (groupings.First().Key == 0){
                return false;
            } else {
                return true;
            }
        }

        return false;
    }

    [Fact]
    public void IsSafeTest()
    {
        Assert.True(IsSafe(new List<int>()), "empty");
        Assert.True(IsSafe(new List<int> {1,2}), "1 2");
        Assert.True(IsSafe(new List<int> {1,2}), "2 1");
        Assert.True(IsSafe(new List<int> {1,4}), "1 4");
        Assert.True(IsSafe(new List<int> {4,1}), "4 1");
        
        Assert.False(IsSafe(new List<int> {1,1}), "1 1");
        Assert.False(IsSafe(new List<int> {1,5}), "1 5");
        Assert.False(IsSafe(new List<int> {5,1}), "4 1");
        Assert.False(IsSafe(new List<int> {1,1}), "1 1");


        Assert.True(IsSafe(ParseLine("7 6 4 2 1"))); // true
        Assert.False(IsSafe(ParseLine("1 2 7 8 9"))); // false
        Assert.False(IsSafe(ParseLine("9 7 6 2 1"))); // false
        Assert.False(IsSafe(ParseLine("1 3 2 4 5"))); // false
        Assert.False(IsSafe(ParseLine("8 6 4 4 1"))); // false
        Assert.True(IsSafe(ParseLine("1 3 6 7 9"))); // true
     }
}
 