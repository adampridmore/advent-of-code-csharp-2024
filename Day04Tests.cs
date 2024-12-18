using System.ComponentModel.Design;
using System.Runtime.CompilerServices;

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

  public record Position(int x, int y);

  public static char GetCellValue(char[][] grid, Position position)
  {
    if (position.x < 0 || position.x >= grid[0].Length || position.y < 0 || position.y >= grid.Length)
    {
      return ' ';
    }

    try
    {
      return grid[position.y][position.x];
    }
    catch (IndexOutOfRangeException ex)
    {
      throw new Exception($"Position: {position} {grid[0].Length}, {grid.Length}", ex);
    }
  }

  private static bool CheckXmasInDirection(char[][] grid, Position position, Func<Position, Position> positionModifier)
  {
    if (GetCellValue(grid, position) != 'X')
    {
      return false;
    }

    var MPos = positionModifier(position);
    if (GetCellValue(grid, MPos) != 'M')
    {
      return false;
    }

    var APos = positionModifier(MPos);
    if (GetCellValue(grid, APos) != 'A')
    {
      return false;
    }

    var SPos = positionModifier(APos);
    if (GetCellValue(grid, SPos) != 'S')
    {
      return false;
    }

    return true;
  }

  private static int XmasCellCount(char[][] grid, Position p)
  {
    bool[] results = [
      CheckXmasInDirection(grid, p, p => new Position(p.x + 1 , p.y     )), // Right   - OK
      CheckXmasInDirection(grid, p, p => new Position(p.x + 1 , p.y + 1 )), // Down and Right - OK
      CheckXmasInDirection(grid, p, p => new Position(p.x,      p.y + 1 )), // Down
      CheckXmasInDirection(grid, p, p => new Position(p.x - 1 , p.y + 1 )), // Down and left
      CheckXmasInDirection(grid, p, p => new Position(p.x - 1 , p.y     )), // left
      CheckXmasInDirection(grid, p, p => new Position(p.x - 1 , p.y - 1 )), // left and up
      CheckXmasInDirection(grid, p, p => new Position(p.x     , p.y - 1 )), // up
      CheckXmasInDirection(grid, p, p => new Position(p.x +1  , p.y - 1 )), // up and right
    ];

    var count = results.Count(r => r);
    
    // if (count >0) System.Console.Out.WriteLine($"{p}: {count}");
    
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

    // var results = new int[10][];
    // for(int i = 0 ; i < results.Length;i++){
    //   results[i] = new int[10];
    //   Array.Fill(results[i], 0);
    // }

    var count = 0;
    for (var y = 0; y < grid.Length; y++)
    {
      for (var x = 0; x < grid[0].Length; x++)
      {
        var cellCount = XmasCellCount(grid, new Position(x, y));
        count += cellCount;
        // results[y][x] = cellCount;
      }
    }

    // foreach(var r in results){
    //   var line = String.Join("", r.Select(r => r.ToString()));
    //   System.Console.Out.WriteLine(line);
    // }

    Assert.Equal(18, count);
  }

   [Fact]
  public void TestRealData_part1()
  {
    char[][] grid = File.ReadLines(InputFilename)
        .Select(line => line.ToCharArray().Where(c => !char.IsWhiteSpace(c)).ToArray())
        .ToArray();

    // var results = new int[10][];
    // for(int i = 0 ; i < results.Length;i++){
    //   results[i] = new int[10];
    //   Array.Fill(results[i], 0);
    // }

    var count = 0;
    for (var y = 0; y < grid.Length; y++)
    {
      for (var x = 0; x < grid[0].Length; x++)
      {
        var cellCount = XmasCellCount(grid, new Position(x, y));
        count += cellCount;
        // results[y][x] = cellCount;
      }
    }

    // foreach(var r in results){
    //   var line = String.Join("", r.Select(r => r.ToString()));
    //   System.Console.Out.WriteLine(line);
    // }

    Assert.Equal(2613, count);
  }

    private static char[][] LoadGrid()
    {
      return _testInput
        .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
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
