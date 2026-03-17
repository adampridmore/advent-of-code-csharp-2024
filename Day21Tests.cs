namespace advent_of_code_csharp_2024;

public class Day21Tests
{
    private static readonly string[] Example =
    [
        "029A", "980A", "179A", "456A", "379A",
    ];

    [Fact]
    public void Example_Part1() => Assert.Equal(126384L, Day21.SolvePart1(Example));

    [Fact]
    public void RealData_Part1()
    {
        Assert.Equal(128962L, Day21.SolvePart1(File.ReadAllLines(Day21.InputFilename)));
    }

    [Fact]
    public void RealData_Part2()
    {
        Assert.Equal(159684145150108L, Day21.SolvePart2(File.ReadAllLines(Day21.InputFilename)));
    }
}
