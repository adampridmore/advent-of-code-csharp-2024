using System.Text.RegularExpressions;

namespace advent_of_code_csharp_2024;

public class Day13
{
    public static readonly string InputFilename = @"Day13_input.txt";

    private record Machine(long Ax, long Ay, long Bx, long By, long Px, long Py);

    private static List<Machine> Parse(string[] lines)
    {
        var machines = new List<Machine>();
        for (int i = 0; i < lines.Length; i += 4)
        {
            var n = lines.Skip(i).Take(3)
                .SelectMany(l => Regex.Matches(l, @"\d+"))
                .Select(m => long.Parse(m.Value)).ToArray();
            machines.Add(new Machine(n[0], n[1], n[2], n[3], n[4], n[5]));
        }
        return machines;
    }

    private static long? Tokens(Machine m)
    {
        long det = m.Ax * m.By - m.Ay * m.Bx;
        if (det == 0) return null;
        long aDet = m.Px * m.By - m.Py * m.Bx;
        long bDet = m.Ax * m.Py - m.Ay * m.Px;
        if (aDet % det != 0 || bDet % det != 0) return null;
        long a = aDet / det;
        long b = bDet / det;
        if (a < 0 || b < 0) return null;
        return 3 * a + b;
    }

    public static long SolvePart1(string[] lines) =>
        Parse(lines).Sum(m => Tokens(m) ?? 0);

    public static long SolvePart2(string[] lines)
    {
        const long offset = 10_000_000_000_000L;
        return Parse(lines)
            .Select(m => m with { Px = m.Px + offset, Py = m.Py + offset })
            .Sum(m => Tokens(m) ?? 0);
    }
}
