namespace advent_of_code_csharp_2024;

public class Day15
{
    public static readonly string InputFilename = @"Day15_input.txt";

    private static (char[][] grid, string moves) Parse(string[] lines)
    {
        int blank = Array.IndexOf(lines, "");
        var grid = lines[..blank].Select(l => l.ToCharArray()).ToArray();
        var moves = string.Concat(lines[(blank + 1)..]);
        return (grid, moves);
    }

    private static (int dr, int dc) Dir(char m) => m switch
    {
        '^' => (-1, 0), 'v' => (1, 0), '<' => (0, -1), '>' => (0, 1), _ => (0, 0)
    };

    private static long Gps(char[][] grid, char box) =>
        (from r in Enumerable.Range(0, grid.Length)
         from c in Enumerable.Range(0, grid[r].Length)
         where grid[r][c] == box
         select 100L * r + c).Sum();

    // Part 1: single-cell boxes
    public static long SolvePart1(string[] lines)
    {
        var (grid, moves) = Parse(lines);
        var (rr, rc) = FindRobot(grid);

        foreach (var m in moves)
        {
            var (dr, dc) = Dir(m);
            int nr = rr + dr, nc = rc + dc;
            if (grid[nr][nc] == '#') continue;
            if (grid[nr][nc] == 'O')
            {
                // Find end of box chain
                int er = nr, ec = nc;
                while (grid[er][ec] == 'O') { er += dr; ec += dc; }
                if (grid[er][ec] == '#') continue;
                grid[er][ec] = 'O';
                grid[nr][nc] = '.';
            }
            grid[rr][rc] = '.';
            grid[nr][nc] = '@';
            rr = nr; rc = nc;
        }
        return Gps(grid, 'O');
    }

    // Part 2: double-wide boxes
    public static long SolvePart2(string[] lines)
    {
        var (small, moves) = Parse(lines);
        var grid = Widen(small);
        var (rr, rc) = FindRobot(grid);

        foreach (var m in moves)
        {
            var (dr, dc) = Dir(m);
            int nr = rr + dr, nc = rc + dc;
            char next = grid[nr][nc];
            if (next == '#') continue;

            if (next == '.' )
            {
                grid[rr][rc] = '.'; grid[nr][nc] = '@';
                rr = nr; rc = nc;
            }
            else if (dc != 0) // horizontal push
            {
                int ec = nc;
                while (grid[nr][ec] == '[' || grid[nr][ec] == ']') ec += dc;
                if (grid[nr][ec] == '#') continue;
                // Shift row
                while (ec != nc) { grid[nr][ec] = grid[nr][ec - dc]; ec -= dc; }
                grid[nr][nc] = '.';
                grid[rr][rc] = '.'; grid[nr][nc] = '@';
                rr = nr; rc = nc;
            }
            else // vertical push
            {
                var boxes = new HashSet<(int r, int c)>();
                if (!CollectBoxes(grid, nr, nc, dr, boxes)) continue;
                // Move all collected box cells
                var sorted = boxes.OrderBy(b => dr < 0 ? b.r : -b.r).ToList();
                foreach (var (br, bc) in sorted)
                {
                    grid[br + dr][bc] = grid[br][bc];
                    grid[br][bc] = '.';
                }
                grid[rr][rc] = '.'; grid[nr][nc] = '@';
                rr = nr; rc = nc;
            }
        }
        return Gps(grid, '[');
    }

    private static bool CollectBoxes(char[][] grid, int r, int c, int dr, HashSet<(int, int)> boxes)
    {
        char ch = grid[r][c];
        if (ch == '#') return false;
        if (ch == '.') return true;
        if (!boxes.Add((r, c))) return true;
        // Add partner cell
        int partner = ch == '[' ? c + 1 : c - 1;
        boxes.Add((r, partner));
        return CollectBoxes(grid, r + dr, c, dr, boxes) &&
               CollectBoxes(grid, r + dr, partner, dr, boxes);
    }

    private static char[][] Widen(char[][] grid) =>
        grid.Select(row => row.SelectMany(ch => ch switch
        {
            '#' => "##", 'O' => "[]", '.' => "..", '@' => "@.", _ => $"{ch}"
        }).ToArray()).ToArray();

    private static (int r, int c) FindRobot(char[][] grid)
    {
        for (int r = 0; r < grid.Length; r++)
            for (int c = 0; c < grid[r].Length; c++)
                if (grid[r][c] == '@') return (r, c);
        throw new Exception("No robot found");
    }
}
