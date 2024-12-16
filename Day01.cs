using System.IO.Compression;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Converters;

namespace advent_of_code_csharp_2024;

public class Day01
{
    private readonly string _inputFilename = @"../../../Day01_input.txt";

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

    private static int SolveDay1_Part1(IEnumerable<Line> lines){
        var leftOrdered = lines.OrderBy(x=>x.First);
        var rightOrdered = lines.OrderBy(x=>x.Second);

        return leftOrdered
                .Zip(rightOrdered, (a,b) => Math.Abs(a.First - b.Second))
                .Sum();
    }

    private static int SolveDay1_Part2(IEnumerable<Line> parsedLines)
    {
        var list1 = parsedLines.Select(_ => _.First);
        var list2 = parsedLines.Select(_ => _.Second);
        var groupedList2 = list2.GroupBy(_ => _).ToList();

        var answer =
            list1.Select(list1Item =>
            {
                var x = groupedList2
                    .FirstOrDefault(x => x.Key == list1Item);

                if (x == null)
                {
                    return 0;
                }
                else
                {
                    return list1Item * x.Count();
                }
            }).Sum();
        return answer;
    }

    [Fact]
    public void ExampleLines_Part1()
    {
        var lines = _testLines.Split(System.Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var parsedLines = lines
            .Select(ParseLine);

        Assert.Equal(new Line(3,4), parsedLines.First());

        var ans = SolveDay1_Part1(parsedLines);

        Assert.Equal(11, ans);
    }

    [Fact]
    public void RealData_Part1()
    {
        var lines = File.ReadLines(_inputFilename);

        var parsedLines = lines
            .Select(ParseLine);

        var ans = SolveDay1_Part1(parsedLines);

        Assert.Equal(1938424, ans);
    }

    [Fact]
    public void ExampleData_Part2()
    {
        var lines = _testLines.Split(System.Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var parsedLines = lines
            .Select(ParseLine);

        Assert.Equal(new Line(3, 4), parsedLines.First());

        int answer = SolveDay1_Part2(parsedLines);

        Assert.Equal(31, answer);
    }

   [Fact]
    public void RealData_Part2()
    {
        var lines = File.ReadLines(_inputFilename);

        var parsedLines = lines
            .Select(ParseLine);

        var ans = SolveDay1_Part2(parsedLines);

        Assert.Equal(22014209, ans);
    }
}
 