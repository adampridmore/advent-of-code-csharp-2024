namespace advent_of_code_csharp_2024;

public class Day13Tests
{
    private static readonly string[] TestInput =
    [
        "Button A: X+94, Y+34",
        "Button B: X+22, Y+67",
        "Prize: X=8400, Y=5400",
        "",
        "Button A: X+26, Y+66",
        "Button B: X+67, Y+21",
        "Prize: X=12748, Y=12176",
        "",
        "Button A: X+17, Y+86",
        "Button B: X+84, Y+37",
        "Prize: X=7870, Y=6450",
        "",
        "Button A: X+69, Y+23",
        "Button B: X+27, Y+71",
        "Prize: X=18641, Y=10279",
    ];

    [Fact]
    public void Example_Part1()
    {
        Assert.Equal(480, Day13.SolvePart1(TestInput));
    }

    [Fact]
    public void RealData_Part1()
    {
        Assert.Equal(39290L, Day13.SolvePart1(File.ReadAllLines(Day13.InputFilename)));
    }

    [Fact]
    public void RealData_Part2()
    {
        Assert.Equal(73458657399094L, Day13.SolvePart2(File.ReadAllLines(Day13.InputFilename)));
    }
}
