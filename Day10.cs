namespace advent_of_code_csharp_2024;

public class Day10
{
    public static readonly string InputFilename = @"Day10_input.txt";

    private record Position(int X, int Y);

    private static int[][] Parse(string[] lines) =>
        lines.Select(l => l.Select(c => c - '0').ToArray()).ToArray();

    private static IEnumerable<Position> Neighbours(int[][] grid, Position p)
    {
        int h = grid.Length, w = grid[0].Length;
        (int dx, int dy)[] dirs = [(0,-1),(0,1),(-1,0),(1,0)];
        foreach (var (dx, dy) in dirs)
        {
            int nx = p.X + dx, ny = p.Y + dy;
            if (nx >= 0 && nx < w && ny >= 0 && ny < h)
                yield return new Position(nx, ny);
        }
    }

    // Part I: count distinct peaks (9s) reachable from a trailhead
    private static int TrailheadScore(int[][] grid, Position start)
    {
        var reachable = new HashSet<Position>();
        var stack = new Stack<Position>();
        stack.Push(start);
        while (stack.Count > 0)
        {
            var p = stack.Pop();
            if (grid[p.Y][p.X] == 9) { reachable.Add(p); continue; }
            foreach (var n in Neighbours(grid, p))
                if (grid[n.Y][n.X] == grid[p.Y][p.X] + 1)
                    stack.Push(n);
        }
        return reachable.Count;
    }

    // Part II: count distinct paths from a trailhead to any 9
    private static int TrailheadRating(int[][] grid, Position p)
    {
        if (grid[p.Y][p.X] == 9) return 1;
        return Neighbours(grid, p)
            .Where(n => grid[n.Y][n.X] == grid[p.Y][p.X] + 1)
            .Sum(n => TrailheadRating(grid, n));
    }

    public static int SolvePart1(string[] lines)
    {
        var grid = Parse(lines);
        return grid.SelectMany((row, y) => row.Select((v, x) => (v, x, y)))
            .Where(t => t.v == 0)
            .Sum(t => TrailheadScore(grid, new Position(t.x, t.y)));
    }

    public static int SolvePart2(string[] lines)
    {
        var grid = Parse(lines);
        return grid.SelectMany((row, y) => row.Select((v, x) => (v, x, y)))
            .Where(t => t.v == 0)
            .Sum(t => TrailheadRating(grid, new Position(t.x, t.y)));
    }
}
