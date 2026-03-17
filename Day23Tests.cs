namespace advent_of_code_csharp_2024;

public class Day23Tests
{
    private static readonly string[] Example =
    [
        "kh-tc", "qp-kh", "de-cg", "ka-co", "yn-aq", "qp-ub", "cg-tb",
        "vc-aq", "tb-ka", "wh-tc", "yn-cg", "kh-ub", "ta-co", "de-co",
        "tc-td", "tb-wq", "wh-td", "ta-ka", "qp-tc", "an-ub", "de-ta",
        "wq-aq", "wq-vc", "wh-yn", "ka-de", "kh-ta", "co-tc", "wh-qp",
        "tb-vc", "td-yn",
    ];

    [Fact]
    public void Example_Part2() => Assert.Equal("co,de,ka,ta", Day23.SolvePart2(Example));

    [Fact]
    public void RealData_Part1()
    {
        Assert.Equal(1284L, Day23.SolvePart1(File.ReadAllLines(Day23.InputFilename)));
    }

    [Fact]
    public void RealData_Part2()
    {
        Assert.Equal("bv,cm,dk,em,gs,jv,ml,oy,qj,ri,uo,xk,yw", Day23.SolvePart2(File.ReadAllLines(Day23.InputFilename)));
    }
}
