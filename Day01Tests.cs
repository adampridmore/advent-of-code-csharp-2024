namespace advent_of_code_csharp_2024;

public class Day01Tests
{
    private readonly string _testLines = @"3   4
4   3
2   5
1   3
3   9
3   3";

    [Fact]
    public void ExampleLines_Part1()
    {
        var lines = _testLines.Split(System.Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var parsedLines = lines
            .Select(Day01.ParseLine);

        Assert.Equal(new Day01.Line(3, 4), parsedLines.First());

        var ans = Day01.SolveDay1_Part1(parsedLines);

        Assert.Equal(11, ans);
    }

    [Fact]
    public void RealData_Part1()
    {
        var lines = File.ReadLines(Day01.InputFilename);

        var parsedLines = lines
            .Select(Day01.ParseLine);

        var ans = Day01.SolveDay1_Part1(parsedLines);

        Assert.Equal(1938424, ans);
    }

    [Fact]
    public void ExampleData_Part2()
    {
        var lines = _testLines.Split(System.Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

        var parsedLines = lines
            .Select(Day01.ParseLine);

        Assert.Equal(new Day01.Line(3, 4), parsedLines.First());

        int answer = Day01.SolveDay1_Part2(parsedLines);

        Assert.Equal(31, answer);
    }

    [Fact]
    public void RealData_Part2()
    {
        var lines = File.ReadLines(Day01.InputFilename);

        var parsedLines = lines
            .Select(Day01.ParseLine);

        var ans = Day01.SolveDay1_Part2(parsedLines);

        Assert.Equal(22014209, ans);
    }
}
