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


// Part I Filtered
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

 // Part II Filtered
 /*
  .M.S......
  ..A..MSMS.
  .M.S.MAA..
  ..A.ASMSM.
  .M.S.M....
  ..........
  S.S.S.S.S.
  .A.A.A.A..
  M.M.M.M.M.
  ..........
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

  [Fact(Skip="In progress")]
  public void TestExample_part1()
  {
    char[][] grid = LoadGrid();

    Assert.Equal(18, Day04.CountXmas(grid));
  }

  [Fact(Skip="In progress")]
  public void TestRealData_part1()
  {
    var lines = File.ReadLines(InputFilename);
    
    char[][] grid = LinesToChars(lines);

    Assert.Equal(2613, Day04.CountXmas(grid));
  }

  
  [Fact]
  public void TestExample_part2()
  {
    System.Console.WriteLine("TestExample_part2 Start");
    char[][] grid = LoadGrid();

    Assert.Equal(9, Day04.CountXmas(grid));

    System.Console.WriteLine("TestExample_part2 End");
  }

  [Fact]
  public void TestRealData_part2()
  {
    var lines = File.ReadLines(InputFilename);

    char[][] grid = LinesToChars(lines);

    Assert.Equal(1905, Day04.CountXmas(grid));
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
    Assert.False(Day04.IsXmasCell(grid, new Day04.Position(0,0)));

    Assert.True(Day04.IsXmasCell(grid, new Day04.Position(2,1)));
    Assert.True(Day04.IsXmasCell(grid, new Day04.Position(6,2)));
    // Assert.Equal(2, Day04.XmasCellCount(grid, new Day04.Position(6,4)));
    // Assert.Equal(2, Day04.XmasCellCount(grid, new Day04.Position(6,4)));

    // Assert.Equal(3, Day04.XmasCellCount(grid, new Day04.Position(5,9)));
  }
}
