namespace advent_of_code_csharp_2024;

public class Day18Tests
{
    private static readonly string[] Example =
    [
        "5,4", "4,2", "4,5", "3,0", "2,1", "6,3", "2,4", "1,5", "0,6",
        "3,3", "2,6", "5,1", "1,2", "5,5", "2,5", "6,5", "1,4", "0,4",
        "6,4", "1,1", "6,1", "1,0", "0,5", "1,6", "2,0",
    ];

    [Fact]
    public void Example_Part1() => Assert.Equal(22L, Day18.SolvePart1(Example, size: 7, after: 12));

    [Fact]
    public void Example_Part2() => Assert.Equal("6,1", Day18.SolvePart2(Example, size: 7));

    [Fact]
    public void RealData_Part1()
    {
        Assert.Equal(404L, Day18.SolvePart1(File.ReadAllLines(Day18.InputFilename)));
    }

    [Fact]
    public void RealData_Part2()
    {
        Assert.Equal("27,60", Day18.SolvePart2(File.ReadAllLines(Day18.InputFilename)));
    }
}
