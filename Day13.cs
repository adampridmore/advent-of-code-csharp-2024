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
            var a = lines[i].Split(new[] { '+', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var b = lines[i + 1].Split(new[] { '+', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var p = lines[i + 2].Split(new[] { '=', ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            machines.Add(new Machine(
                long.Parse(a[3]), long.Parse(a[5]),
                long.Parse(b[3]), long.Parse(b[5]),
                long.Parse(p[2]), long.Parse(p[4])
            ));
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
