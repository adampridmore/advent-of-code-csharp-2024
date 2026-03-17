namespace advent_of_code_csharp_2024;

public class Day25Tests
{
    [Fact]
    public void RealData_Part1()
    {
        Assert.Equal(3116L, Day25.SolvePart1(File.ReadAllLines(Day25.InputFilename)));
    }
}
