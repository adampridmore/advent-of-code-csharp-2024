namespace advent_of_code_csharp_2024;

public class Day16
{
    public static readonly string InputFilename = @"Day16_input.txt";

    private static readonly (int dr, int dc)[] Dirs = [(-1, 0), (0, 1), (1, 0), (0, -1)];

    private static int TurnCost(int from, int to)
    {
        int diff = Math.Abs(from - to);
        return Math.Min(diff, 4 - diff) * 1000;
    }

    private static long[,,] Dijkstra(char[][] grid, int rows, int cols,
        IEnumerable<(int r, int c, int d, long cost)> starts, bool backward = false)
    {
        var dist = new long[rows, cols, 4];
        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
                for (int d = 0; d < 4; d++)
                    dist[r, c, d] = long.MaxValue;

        var pq = new PriorityQueue<(int r, int c, int d), long>();
        foreach (var (r, c, d, cost) in starts)
        {
            dist[r, c, d] = cost;
            pq.Enqueue((r, c, d), cost);
        }

        while (pq.Count > 0)
        {
            var (r, c, d) = pq.Dequeue();
            long cost = dist[r, c, d];

            // Move (forward or backward)
            int sign = backward ? -1 : 1;
            int nr = r + sign * Dirs[d].dr, nc = c + sign * Dirs[d].dc;
            if (nr >= 0 && nr < rows && nc >= 0 && nc < cols && grid[nr][nc] != '#')
            {
                long nc2 = cost + 1;
                if (nc2 < dist[nr, nc, d]) { dist[nr, nc, d] = nc2; pq.Enqueue((nr, nc, d), nc2); }
            }

            // Turn (same cost in both directions)
            for (int td = 0; td < 4; td++)
            {
                if (td == d) continue;
                long nc2 = cost + TurnCost(d, td);
                if (nc2 < dist[r, c, td]) { dist[r, c, td] = nc2; pq.Enqueue((r, c, td), nc2); }
            }
        }
        return dist;
    }

    private static (long best, int tiles) Solve(char[][] grid)
    {
        int rows = grid.Length, cols = grid[0].Length;
        int sr = 0, sc = 0, er = 0, ec = 0;
        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
            {
                if (grid[r][c] == 'S') { sr = r; sc = c; }
                if (grid[r][c] == 'E') { er = r; ec = c; }
            }

        // Forward: start facing East (direction index 1)
        var distS = Dijkstra(grid, rows, cols, [(sr, sc, 1, 0L)]);
        long best = Enumerable.Range(0, 4).Min(d => distS[er, ec, d]);

        // Backward: start from end in all directions
        var endStarts = Enumerable.Range(0, 4).Select(d => (er, ec, d, 0L));
        var distE = Dijkstra(grid, rows, cols, endStarts, backward: true);

        int tiles = 0;
        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
            {
                bool onPath = Enumerable.Range(0, 4).Any(d =>
                    distS[r, c, d] != long.MaxValue &&
                    distE[r, c, d] != long.MaxValue &&
                    distS[r, c, d] + distE[r, c, d] == best);
                if (onPath) tiles++;
            }

        return (best, tiles);
    }

    public static long SolvePart1(string[] lines)
    {
        var grid = lines.Select(l => l.ToCharArray()).ToArray();
        return Solve(grid).best;
    }

    public static long SolvePart2(string[] lines)
    {
        var grid = lines.Select(l => l.ToCharArray()).ToArray();
        return Solve(grid).tiles;
    }
}
