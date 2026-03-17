namespace advent_of_code_csharp_2024;

public class Day15Tests
{
    private static readonly string[] SmallExample =
    [
        "########",
        "#..O.O.#",
        "##@.O..#",
        "#...O..#",
        "#.#.O..#",
        "#...O..#",
        "#......#",
        "########",
        "",
        "<^^>>>vv<v>>v<<",
    ];

    private static readonly string[] SmallPart2Example =
    [
        "#######",
        "#...#.#",
        "#.....#",
        "#..OO@#",
        "#..O..#",
        "#.....#",
        "#######",
        "",
        "<vv<<^^<<^^",
    ];

    [Fact]
    public void SmallExample_Part1()
    {
        Assert.Equal(2028, Day15.SolvePart1(SmallExample));
    }

    [Fact]
    public void SmallPart2Example_Part2()
    {
        Assert.Equal(618, Day15.SolvePart2(SmallPart2Example));
    }

    [Fact]
    public void RealData_Part1()
    {
        Assert.Equal(1414416L, Day15.SolvePart1(File.ReadAllLines(Day15.InputFilename)));
    }

    [Fact]
    public void RealData_Part2()
    {
        Assert.Equal(1386070L, Day15.SolvePart2(File.ReadAllLines(Day15.InputFilename)));
    }
}
