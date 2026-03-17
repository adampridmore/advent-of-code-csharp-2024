namespace advent_of_code_csharp_2024;

public class Day04
{
  public static readonly string InputFilename = @"Day04_input.txt";

  public record Position(int X, int Y);

  public static char GetCellValue(char[][] grid, Position position)
  {
    if (
      position.X < 0 || 
      position.X >= grid[0].Length || 
      position.Y < 0 || 
      position.Y >= grid.Length)
    {
      return ' ';
    }

    return grid[position.Y][position.X];
  }

  private static bool CheckXmasInDirection(char[][] grid, Position position, Func<Position, int, Position> positionModifier)
  {
    var toMatch = new List<(char, Position)> {
      ('M', positionModifier(position,-1)),
      ('A', positionModifier(position,0)),
      ('S', positionModifier(position,1))
    };

    return !toMatch.Any(match=>{
      var letter = match.Item1;
      var p = match.Item2;
      return GetCellValue(grid, p) != letter;
    });
  }

  public static bool IsXmasCell(char[][] grid, Position p)
  {
    bool[] results = [
      CheckXmasInDirection(grid, p, (p,distance) => new Position(p.X + distance , p.Y + distance )), // Down and Right - OK
      CheckXmasInDirection(grid, p, (p,distance) => new Position(p.X - distance , p.Y + distance )), // Down and left
      CheckXmasInDirection(grid, p, (p,distance) => new Position(p.X - distance , p.Y - distance )), // left and up
      CheckXmasInDirection(grid, p, (p,distance) => new Position(p.X + distance , p.Y - distance )), // up and right
    ];

    var count = results.Count(r => r);

    return (count == 2);
  }

  public static int CountXmas(char[][] grid)
  {
    var count = 
      grid.Select((line, yIndex) => {
        return line.Select((cell, xIndex) => {
          return (IsXmasCell(grid, new Position(xIndex,yIndex)))?1:0;
        }).Sum();
      }).Sum();

    return count;
  }

  
  public static int CountXmasWordSearch(char[][] grid)
  {
    int count = 0;
    (int dx, int dy)[] directions = [(1,0),(-1,0),(0,1),(0,-1),(1,1),(1,-1),(-1,1),(-1,-1)];
    for (int y = 0; y < grid.Length; y++)
      for (int x = 0; x < grid[y].Length; x++)
        foreach (var (dx, dy) in directions)
          if ("XMAS".Select((c, k) => GetCellValue(grid, new Position(x + k*dx, y + k*dy)) == c).All(b => b))
            count++;
    return count;
  }

  public static char[][] LinesToChars(IEnumerable<string> lines){
    return lines
      .Select(line => line.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray())
      .ToArray();
  }
}
