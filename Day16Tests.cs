namespace advent_of_code_csharp_2024;

public class Day16Tests
{
    private static readonly string[] Example1 =
    [
        "###############",
        "#.......#....E#",
        "#.#.###.#.###.#",
        "#.....#.#...#.#",
        "#.###.#####.#.#",
        "#.#.#.......#.#",
        "#.#.#####.###.#",
        "#...........#.#",
        "###.#.#####.#.#",
        "#...#.....#.#.#",
        "#.#.#.###.#.#.#",
        "#.....#...#.#.#",
        "#.###.#.#.#.#.#",
        "#S..#.....#...#",
        "###############",
    ];

    private static readonly string[] Example2 =
    [
        "#################",
        "#...#...#...#..E#",
        "#.#.#.#.#.#.#.#.#",
        "#.#.#.#...#...#.#",
        "#.#.#.#.###.#.#.#",
        "#...#.#.#.....#.#",
        "#.#.#.#.#.#####.#",
        "#.#...#.#.#.....#",
        "#.#.#####.#.###.#",
        "#.#.#.......#...#",
        "#.#.###.#####.###",
        "#.#.#...#.....#.#",
        "#.#.#.#####.###.#",
        "#.#.#.........#.#",
        "#.#.#.#########.#",
        "#S#.............#",
        "#################",
    ];

    [Fact]
    public void Example1_Part1() => Assert.Equal(7036L, Day16.SolvePart1(Example1));

    [Fact]
    public void Example1_Part2() => Assert.Equal(45L, Day16.SolvePart2(Example1));

    [Fact]
    public void Example2_Part1() => Assert.Equal(11048L, Day16.SolvePart1(Example2));

    [Fact]
    public void Example2_Part2() => Assert.Equal(64L, Day16.SolvePart2(Example2));

    [Fact]
    public void RealData_Part1()
    {
        Assert.Equal(102460L, Day16.SolvePart1(File.ReadAllLines(Day16.InputFilename)));
    }

    [Fact]
    public void RealData_Part2()
    {
        Assert.Equal(527L, Day16.SolvePart2(File.ReadAllLines(Day16.InputFilename)));
    }
}
