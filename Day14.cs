using System.Text.RegularExpressions;

namespace advent_of_code_csharp_2024;

public class Day14
{
    public static readonly string InputFilename = @"Day14_input.txt";

    private record Robot(int Px, int Py, int Vx, int Vy);

    private static List<Robot> Parse(string[] lines) =>
        lines.Select(line =>
        {
            var n = Regex.Matches(line, @"-?\d+").Select(m => int.Parse(m.Value)).ToArray();
            return new Robot(n[0], n[1], n[2], n[3]);
        }).ToList();

    private static (int x, int y) Step(Robot r, int t, int W, int H) =>
        (((r.Px + r.Vx * t) % W + W) % W, ((r.Py + r.Vy * t) % H + H) % H);

    public static long SolvePart1(string[] lines, int W = 101, int H = 103)
    {
        var robots = Parse(lines);
        var positions = robots.Select(r => Step(r, 100, W, H)).ToList();
        int mx = W / 2, my = H / 2;
        long q1 = positions.Count(p => p.x < mx && p.y < my);
        long q2 = positions.Count(p => p.x > mx && p.y < my);
        long q3 = positions.Count(p => p.x < mx && p.y > my);
        long q4 = positions.Count(p => p.x > mx && p.y > my);
        return q1 * q2 * q3 * q4;
    }

    public static long SolvePart2(string[] lines, int W = 101, int H = 103)
    {
        var robots = Parse(lines);

        // Find t that minimises safety factor via CRT:
        // find tx in [0,W) minimising x-variance, ty in [0,H) minimising y-variance
        long minVarX = long.MaxValue, minVarY = long.MaxValue;
        int tx = 0, ty = 0;

        for (int t = 0; t < W; t++)
        {
            var xs = robots.Select(r => Step(r, t, W, H).x).ToList();
            double mean = xs.Average();
            long varX = xs.Sum(x => (long)((x - mean) * (x - mean)));
            if (varX < minVarX) { minVarX = varX; tx = t; }
        }
        for (int t = 0; t < H; t++)
        {
            var ys = robots.Select(r => Step(r, t, W, H).y).ToList();
            double mean = ys.Average();
            long varY = ys.Sum(y => (long)((y - mean) * (y - mean)));
            if (varY < minVarY) { minVarY = varY; ty = t; }
        }

        // CRT: find t such that t ≡ tx (mod W) and t ≡ ty (mod H)
        // t = tx + W * k, need tx + W*k ≡ ty (mod H) → k ≡ (ty - tx) * W^-1 (mod H)
        long wInvH = ModInverse(W, H);
        long t0 = (tx + (long)W * ((wInvH * ((ty - tx) % H + H)) % H)) % ((long)W * H);
        return t0;
    }

    private static long ModInverse(long a, long m)
    {
        // Extended Euclidean
        long g = m, x = 0, y = 1;
        long ta = a;
        while (ta != 0) { long q = g / ta; (g, ta) = (ta, g - q * ta); (x, y) = (y, x - q * y); }
        return (x % m + m) % m;
    }
}
