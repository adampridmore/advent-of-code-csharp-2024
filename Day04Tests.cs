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

  [Fact]
  public void GetCellValueTest()
  {
    char[][] cells = [
      ['A','B'],
      ['C','D']
    ];

    Assert.Equal('A', Day04.GetCellValue(cells, new Day04.Position(0, 0)));
    Assert.Equal('B', Day04.GetCellValue(cells, new Day04.Position(1, 0)));
    Assert.Equal('C', Day04.GetCellValue(cells, new Day04.Position(0, 1)));
    Assert.Equal('D', Day04.GetCellValue(cells, new Day04.Position(1, 1)));

    Assert.Equal(' ', Day04.GetCellValue(cells, new Day04.Position(-1, 0)));
    Assert.Equal(' ', Day04.GetCellValue(cells, new Day04.Position(0, -1)));
    Assert.Equal(' ', Day04.GetCellValue(cells, new Day04.Position(2, 0)));
    Assert.Equal(' ', Day04.GetCellValue(cells, new Day04.Position(0, 2)));
  }

  [Fact]
  public void TestExample_part1()
  {
    char[][] grid = LoadGrid();

    Assert.Equal(18, Day04.CountXmas(grid));
  }

  [Fact]
  public void TestRealData_part1()
  {
    var lines = File.ReadLines(InputFilename);
    
    char[][] grid = LinesToChars(lines);

    Assert.Equal(2613, Day04.CountXmas(grid));
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
    Assert.Equal(0, Day04.XmasCellCount(grid, new Day04.Position(0,0)));

    Assert.Equal(1, Day04.XmasCellCount(grid, new Day04.Position(4,0)));
    Assert.Equal(1, Day04.XmasCellCount(grid, new Day04.Position(5,0)));
    Assert.Equal(2, Day04.XmasCellCount(grid, new Day04.Position(6,4)));
    Assert.Equal(2, Day04.XmasCellCount(grid, new Day04.Position(6,4)));

    Assert.Equal(3, Day04.XmasCellCount(grid, new Day04.Position(5,9)));
  }
}
