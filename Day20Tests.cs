namespace advent_of_code_csharp_2024;

public class Day20Tests
{
    [Fact]
    public void RealData_Part1()
    {
        Assert.Equal(1367L, Day20.SolvePart1(File.ReadAllLines(Day20.InputFilename)));
    }

    [Fact]
    public void RealData_Part2()
    {
        Assert.Equal(1006850L, Day20.SolvePart2(File.ReadAllLines(Day20.InputFilename)));
    }
}
