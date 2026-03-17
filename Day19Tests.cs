namespace advent_of_code_csharp_2024;

public class Day19Tests
{
    private static readonly string[] Example =
    [
        "r, wr, b, g, bwu, rb, gb, br",
        "",
        "brwrr",
        "bggr",
        "gbbr",
        "rrbgbr",
        "bbrgwb",
        "bwurrg",
        "brgr",
        "ubwu",
    ];

    [Fact]
    public void Example_Part1() => Assert.Equal(6L, Day19.SolvePart1(Example));

    [Fact]
    public void Example_Part2() => Assert.Equal(16L, Day19.SolvePart2(Example));

    [Fact]
    public void RealData_Part1()
    {
        Assert.Equal(280L, Day19.SolvePart1(File.ReadAllLines(Day19.InputFilename)));
    }

    [Fact]
    public void RealData_Part2()
    {
        Assert.Equal(606411968721181L, Day19.SolvePart2(File.ReadAllLines(Day19.InputFilename)));
    }
}
