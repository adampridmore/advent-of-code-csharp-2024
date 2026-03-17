namespace advent_of_code_csharp_2024;

public class Day10Tests
{
    private static readonly string[] TestInput =
    [
        "89010123",
        "78121874",
        "87430965",
        "96549874",
        "45678903",
        "32019012",
        "01329801",
        "10456732",
    ];

    [Fact]
    public void Example_Part1()
    {
        Assert.Equal(36, Day10.SolvePart1(TestInput));
    }

    [Fact]
    public void RealData_Part1()
    {
        Assert.Equal(644, Day10.SolvePart1(File.ReadAllLines(Day10.InputFilename)));
    }

    [Fact]
    public void Example_Part2()
    {
        Assert.Equal(81, Day10.SolvePart2(TestInput));
    }

    [Fact]
    public void RealData_Part2()
    {
        Assert.Equal(1366, Day10.SolvePart2(File.ReadAllLines(Day10.InputFilename)));
    }
}
