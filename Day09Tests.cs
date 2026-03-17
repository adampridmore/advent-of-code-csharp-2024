namespace advent_of_code_csharp_2024;

public class Day09Tests
{
    private static readonly string[] TestInput = ["2333133121414131402"];

    [Fact]
    public void Example_Part1()
    {
        Assert.Equal(1928, Day09.SolvePart1(TestInput));
    }

    [Fact]
    public void RealData_Part1()
    {
        Assert.Equal(6291146824486L, Day09.SolvePart1(File.ReadAllLines(Day09.InputFilename)));
    }

    [Fact]
    public void Example_Part2()
    {
        Assert.Equal(2858, Day09.SolvePart2(TestInput));
    }

    [Fact]
    public void RealData_Part2()
    {
        Assert.Equal(6307279963620L, Day09.SolvePart2(File.ReadAllLines(Day09.InputFilename)));
    }
}
