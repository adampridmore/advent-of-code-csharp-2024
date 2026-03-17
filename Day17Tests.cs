namespace advent_of_code_csharp_2024;

public class Day17Tests
{
    private static readonly string[] Example1 =
    [
        "Register A: 729",
        "Register B: 0",
        "Register C: 0",
        "",
        "Program: 0,1,5,4,3,0",
    ];

    private static readonly string[] Example2 =
    [
        "Register A: 2024",
        "Register B: 0",
        "Register C: 0",
        "",
        "Program: 0,3,5,4,3,0",
    ];

    [Fact]
    public void Example1_Part1() => Assert.Equal("4,6,3,5,6,3,5,2,1,0", Day17.SolvePart1(Example1));

    [Fact]
    public void Example2_Part2() => Assert.Equal(117440L, Day17.SolvePart2(Example2));

    [Fact]
    public void RealData_Part1()
    {
        Assert.Equal("7,6,5,3,6,5,7,0,4", Day17.SolvePart1(File.ReadAllLines(Day17.InputFilename)));
    }

    [Fact]
    public void RealData_Part2()
    {
        Assert.Equal(190615597431823L, Day17.SolvePart2(File.ReadAllLines(Day17.InputFilename)));
    }
}
