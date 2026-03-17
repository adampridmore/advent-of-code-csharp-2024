namespace advent_of_code_csharp_2024;

public class Day18
{
    public static readonly string InputFilename = @"Day18_input.txt";

    private static List<(int x, int y)> Parse(string[] lines) =>
        lines.Select(l => { var p = l.Split(','); return (int.Parse(p[0]), int.Parse(p[1])); }).ToList();

    private static int? BfsSteps(bool[,] blocked, int size)
    {
        var dist = new int[size, size];
        for (int i = 0; i < size; i++) for (int j = 0; j < size; j++) dist[i, j] = -1;
        dist[0, 0] = 0;
        var q = new Queue<(int x, int y)>();
        q.Enqueue((0, 0));
        while (q.Count > 0)
        {
            var (x, y) = q.Dequeue();
            if (x == size - 1 && y == size - 1) return dist[x, y];
            foreach (var (dx, dy) in new[] { (1, 0), (-1, 0), (0, 1), (0, -1) })
            {
                int nx = x + dx, ny = y + dy;
                if (nx >= 0 && nx < size && ny >= 0 && ny < size && !blocked[nx, ny] && dist[nx, ny] == -1)
                {
                    dist[nx, ny] = dist[x, y] + 1;
                    q.Enqueue((nx, ny));
                }
            }
        }
        return null;
    }

    public static long SolvePart1(string[] lines, int size = 71, int after = 1024)
    {
        var bytes = Parse(lines);
        var blocked = new bool[size, size];
        foreach (var (x, y) in bytes.Take(after)) blocked[x, y] = true;
        return BfsSteps(blocked, size)!.Value;
    }

    public static string SolvePart2(string[] lines, int size = 71)
    {
        var bytes = Parse(lines);
        int lo = 0, hi = bytes.Count - 1;
        while (lo < hi)
        {
            int mid = (lo + hi) / 2;
            var blocked = new bool[size, size];
            foreach (var (x, y) in bytes.Take(mid + 1)) blocked[x, y] = true;
            if (BfsSteps(blocked, size) == null) hi = mid;
            else lo = mid + 1;
        }
        return $"{bytes[lo].x},{bytes[lo].y}";
    }
}
