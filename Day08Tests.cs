namespace advent_of_code_csharp_2024;

public class Day08Tests
{
    private static readonly string[] TestInput =
    [
        "............",
        "........0...",
        ".....0......",
        ".......0....",
        "....0.......",
        "......A.....",
        "............",
        "............",
        "........A...",
        ".........A..",
        "............",
        "............",
    ];

    [Fact]
    public void Example_Part1()
    {
        Assert.Equal(14, Day08.SolvePart1(TestInput));
    }

    [Fact]
    public void RealData_Part1()
    {
        Assert.Equal(354, Day08.SolvePart1(File.ReadAllLines(Day08.InputFilename)));
    }

    [Fact]
    public void Example_Part2()
    {
        Assert.Equal(34, Day08.SolvePart2(TestInput));
    }

    [Fact]
    public void RealData_Part2()
    {
        Assert.Equal(1263, Day08.SolvePart2(File.ReadAllLines(Day08.InputFilename)));
    }
}
