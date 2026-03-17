namespace advent_of_code_csharp_2024;

public class Day08
{
    public static readonly string InputFilename = @"Day08_input.txt";

    public record Position(int X, int Y);

    public static Dictionary<char, List<Position>> ParseAntennas(string[] lines)
    {
        var result = new Dictionary<char, List<Position>>();
        for (int y = 0; y < lines.Length; y++)
            for (int x = 0; x < lines[y].Length; x++)
            {
                var c = lines[y][x];
                if (c != '.')
                {
                    if (!result.ContainsKey(c)) result[c] = [];
                    result[c].Add(new Position(x, y));
                }
            }
        return result;
    }

    private static bool InBounds(Position p, int width, int height)
        => p.X >= 0 && p.X < width && p.Y >= 0 && p.Y < height;

    public static int SolvePart1(string[] lines)
    {
        int height = lines.Length, width = lines[0].Length;
        var antinodes = new HashSet<Position>();

        foreach (var positions in ParseAntennas(lines).Values)
            for (int i = 0; i < positions.Count; i++)
                for (int j = i + 1; j < positions.Count; j++)
                {
                    var (a, b) = (positions[i], positions[j]);
                    var p1 = new Position(2 * a.X - b.X, 2 * a.Y - b.Y);
                    var p2 = new Position(2 * b.X - a.X, 2 * b.Y - a.Y);
                    if (InBounds(p1, width, height)) antinodes.Add(p1);
                    if (InBounds(p2, width, height)) antinodes.Add(p2);
                }

        return antinodes.Count;
    }

    public static int SolvePart2(string[] lines)
    {
        int height = lines.Length, width = lines[0].Length;
        var antinodes = new HashSet<Position>();

        foreach (var positions in ParseAntennas(lines).Values)
            for (int i = 0; i < positions.Count; i++)
                for (int j = i + 1; j < positions.Count; j++)
                {
                    var (a, b) = (positions[i], positions[j]);
                    int dx = b.X - a.X, dy = b.Y - a.Y;

                    for (var p = a; InBounds(p, width, height); p = new Position(p.X - dx, p.Y - dy))
                        antinodes.Add(p);
                    for (var p = new Position(a.X + dx, a.Y + dy); InBounds(p, width, height); p = new Position(p.X + dx, p.Y + dy))
                        antinodes.Add(p);
                }

        return antinodes.Count;
    }
}
