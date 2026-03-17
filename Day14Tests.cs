namespace advent_of_code_csharp_2024;

public class Day14Tests
{
    private static readonly string[] TestInput =
    [
        "p=0,4 v=3,-3",
        "p=6,3 v=-1,-3",
        "p=10,3 v=-1,2",
        "p=2,0 v=2,-1",
        "p=0,0 v=1,3",
        "p=3,0 v=-2,-2",
        "p=7,6 v=-1,-3",
        "p=3,0 v=-1,-2",
        "p=9,3 v=2,3",
        "p=7,3 v=-1,2",
        "p=2,4 v=2,-3",
        "p=9,5 v=-3,-3",
    ];

    [Fact]
    public void Example_Part1()
    {
        Assert.Equal(12, Day14.SolvePart1(TestInput, W: 11, H: 7));
    }

    [Fact]
    public void RealData_Part1()
    {
        Assert.Equal(225943500L, Day14.SolvePart1(File.ReadAllLines(Day14.InputFilename)));
    }

    [Fact]
    public void RealData_Part2()
    {
        Assert.Equal(6377L, Day14.SolvePart2(File.ReadAllLines(Day14.InputFilename)));
    }
}
