using System.Numerics;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Converters;

namespace advent_of_code_scala_2024;

public class Day01
{
    //private readonly string _inputFilename = @"../../../Day01_input.txt";

    private readonly string _testLines = @"3   4
4   3
2   5
1   3
3   9
3   3";

    private record Line(int First, int Second);

    private static Line ParseLine(string line){
        var split = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

        return new Line(int.Parse(split[0]), int.Parse(split[1]));
    }

    private int CalculateTotalDistance(IEnumerable<Line> lines){
        return 0;
    }

    [Fact]
    public void Test1()
    {
        // var filelines = File.ReadLines(_inputFilename);

        var lines = _testLines.Split(System.Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var parsedLines = lines
            .Select(ParseLine);

        Assert.Equal(new Line(3,4), parsedLines.First());

        var leftOrdered = parsedLines.OrderBy(x=>x.First);
        var rightOrdered = parsedLines.OrderBy(x=>x.Second);

        var ans = 
            leftOrdered
                .Zip(rightOrdered, (a,b) => Math.Abs(a.First - b.Second))
                .Sum();

        Assert.Equal(11, ans);
    }
}
 