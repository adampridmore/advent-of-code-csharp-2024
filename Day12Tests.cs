namespace advent_of_code_csharp_2024;

public class Day12Tests
{
    private static readonly string[] TestInput =
    [
        "RRRRIICCFF",
        "RRRRIICCCF",
        "VVRRRCCFFF",
        "VVRCCCJFFF",
        "VVVVCJJCFE",
        "VVIVCCJJEE",
        "VVIIICJJEE",
        "MIIIIIJJEE",
        "MIIISIJEEE",
        "MMMISSJEEE",
    ];

    [Fact]
    public void Example_Part1()
    {
        Assert.Equal(1930, Day12.SolvePart1(TestInput));
    }

    [Fact]
    public void RealData_Part1()
    {
        Assert.Equal(1446042L, Day12.SolvePart1(File.ReadAllLines(Day12.InputFilename)));
    }

    [Fact]
    public void Example_Part2()
    {
        Assert.Equal(1206, Day12.SolvePart2(TestInput));
    }

    [Fact]
    public void RealData_Part2()
    {
        Assert.Equal(902742L, Day12.SolvePart2(File.ReadAllLines(Day12.InputFilename)));
    }
}
