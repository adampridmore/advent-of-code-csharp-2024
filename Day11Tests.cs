namespace advent_of_code_csharp_2024;

public class Day11Tests
{
    private static readonly string[] TestInput = ["125 17"];

    [Fact]
    public void Example_Part1()
    {
        Assert.Equal(55312, Day11.SolvePart1(TestInput));
    }

    [Fact]
    public void RealData_Part1()
    {
        Assert.Equal(199982L, Day11.SolvePart1(File.ReadAllLines(Day11.InputFilename)));
    }

    [Fact]
    public void RealData_Part2()
    {
        Assert.Equal(237149922829154L, Day11.SolvePart2(File.ReadAllLines(Day11.InputFilename)));
    }
}
