namespace advent_of_code_csharp_2024;

public class Day20
{
    public static readonly string InputFilename = @"Day20_input.txt";

    private static int[,] Bfs(char[][] grid, int sr, int sc)
    {
        int rows = grid.Length, cols = grid[0].Length;
        var dist = new int[rows, cols];
        for (int r = 0; r < rows; r++) for (int c = 0; c < cols; c++) dist[r, c] = -1;
        dist[sr, sc] = 0;
        var q = new Queue<(int r, int c)>();
        q.Enqueue((sr, sc));
        while (q.Count > 0)
        {
            var (r, c) = q.Dequeue();
            foreach (var (dr, dc) in new[] { (-1, 0), (1, 0), (0, -1), (0, 1) })
            {
                int nr = r + dr, nc = c + dc;
                if (nr >= 0 && nr < rows && nc >= 0 && nc < cols && grid[nr][nc] != '#' && dist[nr, nc] == -1)
                {
                    dist[nr, nc] = dist[r, c] + 1;
                    q.Enqueue((nr, nc));
                }
            }
        }
        return dist;
    }

    private static long CountCheats(string[] lines, int maxCheat, int minSave)
    {
        var grid = lines.Select(l => l.ToCharArray()).ToArray();
        int rows = grid.Length, cols = grid[0].Length;
        int sr = 0, sc = 0, er = 0, ec = 0;
        for (int r = 0; r < rows; r++)
            for (int c = 0; c < cols; c++)
            {
                if (grid[r][c] == 'S') { sr = r; sc = c; }
                if (grid[r][c] == 'E') { er = r; ec = c; }
            }

        var distS = Bfs(grid, sr, sc);
        var distE = Bfs(grid, er, ec);
        int normal = distS[er, ec];

        long count = 0;
        for (int r1 = 0; r1 < rows; r1++)
            for (int c1 = 0; c1 < cols; c1++)
            {
                if (distS[r1, c1] < 0) continue;
                for (int r2 = r1 - maxCheat; r2 <= r1 + maxCheat; r2++)
                {
                    if (r2 < 0 || r2 >= rows) continue;
                    int rDist = Math.Abs(r2 - r1);
                    int colRange = maxCheat - rDist;
                    for (int c2 = c1 - colRange; c2 <= c1 + colRange; c2++)
                    {
                        if (c2 < 0 || c2 >= cols) continue;
                        if (distE[r2, c2] < 0) continue;
                        int manhattan = rDist + Math.Abs(c2 - c1);
                        int saved = normal - distS[r1, c1] - manhattan - distE[r2, c2];
                        if (saved >= minSave) count++;
                    }
                }
            }
        return count;
    }

    public static long SolvePart1(string[] lines) => CountCheats(lines, 2, 100);
    public static long SolvePart2(string[] lines) => CountCheats(lines, 20, 100);
}
