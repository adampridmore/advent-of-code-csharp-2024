namespace advent_of_code_csharp_2024;

public class Day22Tests
{
    [Fact]
    public void Example_Part1()
    {
        string[] lines = ["1", "10", "100", "2024"];
        Assert.Equal(37327623L, Day22.SolvePart1(lines));
    }

    [Fact]
    public void Example_Part2()
    {
        string[] lines = ["1", "2", "3", "2024"];
        Assert.Equal(23L, Day22.SolvePart2(lines));
    }

    [Fact]
    public void RealData_Part1()
    {
        Assert.Equal(15335183969L, Day22.SolvePart1(File.ReadAllLines(Day22.InputFilename)));
    }

    [Fact]
    public void RealData_Part2()
    {
        Assert.Equal(1696L, Day22.SolvePart2(File.ReadAllLines(Day22.InputFilename)));
    }
}
