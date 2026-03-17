namespace advent_of_code_csharp_2024;

public class Day24Tests
{
    [Fact]
    public void RealData_Part1()
    {
        Assert.Equal(38869984335432L, Day24.SolvePart1(File.ReadAllLines(Day24.InputFilename)));
    }

    [Fact]
    public void RealData_Part2()
    {
        Assert.Equal("drg,gvw,jbp,jgc,qjb,z15,z22,z35", Day24.SolvePart2(File.ReadAllLines(Day24.InputFilename)));
    }
}
