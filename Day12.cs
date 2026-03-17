namespace advent_of_code_csharp_2024;

public class Day12
{
    public static readonly string InputFilename = @"Day12_input.txt";

    private static bool InBounds(char[][] grid, Position p) =>
        p.X >= 0 && p.X < grid[0].Length && p.Y >= 0 && p.Y < grid.Length;

    private static List<HashSet<Position>> FindRegions(char[][] grid)
    {
        var visited = new HashSet<Position>();
        var regions = new List<HashSet<Position>>();

        for (int y = 0; y < grid.Length; y++)
        for (int x = 0; x < grid[y].Length; x++)
        {
            var start = new Position(x, y);
            if (visited.Contains(start)) continue;

            var region = new HashSet<Position>();
            char plant = grid[y][x];
            var queue = new Queue<Position>();
            queue.Enqueue(start);
            while (queue.Count > 0)
            {
                var p = queue.Dequeue();
                if (!region.Add(p)) continue;
                visited.Add(p);
                foreach (var n in new[] {
                    new Position(p.X, p.Y-1), new Position(p.X, p.Y+1),
                    new Position(p.X-1, p.Y), new Position(p.X+1, p.Y) })
                    if (!region.Contains(n) && InBounds(grid, n) && grid[n.Y][n.X] == plant)
                        queue.Enqueue(n);
            }
            regions.Add(region);
        }
        return regions;
    }

    private static int Perimeter(HashSet<Position> region)
    {
        (int dx, int dy)[] dirs = [(0,-1),(0,1),(-1,0),(1,0)];
        return region.Sum(p => dirs.Count(d => !region.Contains(new Position(p.X + d.dx, p.Y + d.dy))));
    }

    // Number of sides = number of corners
    private static int Sides(HashSet<Position> region)
    {
        int corners = 0;
        foreach (var p in region)
            foreach (var (dx, dy) in new[] { (-1,-1),(1,-1),(1,1),(-1,1) })
            {
                bool inH = region.Contains(new Position(p.X + dx, p.Y));
                bool inV = region.Contains(new Position(p.X, p.Y + dy));
                bool inD = region.Contains(new Position(p.X + dx, p.Y + dy));
                if (!inH && !inV) corners++;         // convex corner
                if (inH && inV && !inD) corners++;   // concave corner
            }
        return corners;
    }

    public static long SolvePart1(string[] lines)
    {
        var grid = lines.Select(l => l.ToCharArray()).ToArray();
        return FindRegions(grid).Sum(r => (long)r.Count * Perimeter(r));
    }

    public static long SolvePart2(string[] lines)
    {
        var grid = lines.Select(l => l.ToCharArray()).ToArray();
        return FindRegions(grid).Sum(r => (long)r.Count * Sides(r));
    }
}
