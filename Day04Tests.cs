namespace advent_of_code_csharp_2024;

public class Day04Tests
{
  private static string _testInput = @"MMMSXXMASM
MSAMXMSMSA
AMXSXMAAMM
MSAMASMSMX
XMASAMXAMM
XXAMMXXAMA
SMSMSASXSS
SAXAMASAAA
MAMMMXMMMM
MXMXAXMASX";

  /*
  .SAMXMS...
  ...S..A...
  ..A.A.MS.X
  XMASAMX.MM
  X.....XA.A
  S.S.S.S.SS
  .A.A.A.A.A
  ..M.M.M.MM
  .X.X.XMASX
  */

  public static readonly string InputFilename = @"../../../Day04_input.txt";

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
      ('X', positionModifier(position,0)),
      ('M', positionModifier(position,1)),
      ('A', positionModifier(position,2)),
      ('S', positionModifier(position,3))
    };

    return !toMatch.Any(match=>{
      var letter = match.Item1;
      var p = match.Item2;
      return GetCellValue(grid, p) != letter;
    });
  }

  private static int XmasCellCount(char[][] grid, Position p)
  {
    bool[] results = [
      CheckXmasInDirection(grid, p, (p,distance) => new Position(p.X + distance , p.Y            )), // Right   - OK
      CheckXmasInDirection(grid, p, (p,distance) => new Position(p.X + distance , p.Y + distance )), // Down and Right - OK
      CheckXmasInDirection(grid, p, (p,distance) => new Position(p.X            , p.Y + distance )), // Down
      CheckXmasInDirection(grid, p, (p,distance) => new Position(p.X - distance , p.Y + distance )), // Down and left
      CheckXmasInDirection(grid, p, (p,distance) => new Position(p.X - distance , p.Y            )), // left
      CheckXmasInDirection(grid, p, (p,distance) => new Position(p.X - distance , p.Y - distance )), // left and up
      CheckXmasInDirection(grid, p, (p,distance) => new Position(p.X            , p.Y - distance )), // up
      CheckXmasInDirection(grid, p, (p,distance) => new Position(p.X + distance , p.Y - distance )), // up and right
    ];

    return results.Count(r => r);
  }

  private static int CountXmas(char[][] grid)
    {
      var count = 
        grid.Select((line, yIndex) => {
          return line.Select((cell, xIndex) => {
            return XmasCellCount(grid, new Position(xIndex,yIndex));
          }).Sum();
        }).Sum();

      return count;
    }

  [Fact]
  public void GetCellValueTest()
  {
    char[][] cells = [
      ['A','B'],
      ['C','D']
    ];

    Assert.Equal('A', GetCellValue(cells, new Position(0, 0)));
    Assert.Equal('B', GetCellValue(cells, new Position(1, 0)));
    Assert.Equal('C', GetCellValue(cells, new Position(0, 1)));
    Assert.Equal('D', GetCellValue(cells, new Position(1, 1)));

    Assert.Equal(' ', GetCellValue(cells, new Position(-1, 0)));
    Assert.Equal(' ', GetCellValue(cells, new Position(0, -1)));
    Assert.Equal(' ', GetCellValue(cells, new Position(2, 0)));
    Assert.Equal(' ', GetCellValue(cells, new Position(0, 2)));
  }

  [Fact]
  public void TestExample_part1()
  {
    char[][] grid = LoadGrid();

    Assert.Equal(18, CountXmas(grid));
  }

  [Fact]
  public void TestRealData_part1()
  {
    var lines = File.ReadLines(InputFilename);
    
    char[][] grid = LinesToChars(lines);

    Assert.Equal(2613, CountXmas(grid));
  }

  private static char[][] LoadGrid()
  {
    var lines = _testInput.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
    return LinesToChars(lines);

  }

  private static char[][] LinesToChars(IEnumerable<string> lines){
    return lines
      .Select(line => line.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray())
      .ToArray();
  }

  [Fact]
  public void XmaxCellCountTest(){
    var grid = LoadGrid();
    Assert.Equal(0, XmasCellCount(grid, new Position(0,0)));

    Assert.Equal(1, XmasCellCount(grid, new Position(4,0)));
    Assert.Equal(1, XmasCellCount(grid, new Position(5,0)));
    Assert.Equal(2, XmasCellCount(grid, new Position(6,4)));
    Assert.Equal(2, XmasCellCount(grid, new Position(6,4)));

    Assert.Equal(3, XmasCellCount(grid, new Position(5,9)));
  }
}
