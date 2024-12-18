using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading.Channels;

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

  public static char GetCellValue(char[][] grid, int x, int y)
  {
    if (x < 0 || x >= grid[0].Length || y < 0 || y >= grid.Length){
      return ' ';
    }

    return grid[y][x];
  }

  [Fact]
  public void GetCellValueTest(){
    char[][] cells = [
      ['A','B'],
      ['C','D']
    ];

    Assert.Equal('A', GetCellValue(cells,0,0));
    Assert.Equal('B', GetCellValue(cells,1,0));
    Assert.Equal('C', GetCellValue(cells,0,1));
    Assert.Equal('D', GetCellValue(cells,1,1));

    Assert.Equal(' ', GetCellValue(cells,-1,0));
    Assert.Equal(' ', GetCellValue(cells,0,-1));
    Assert.Equal(' ', GetCellValue(cells,2,0));
    Assert.Equal(' ', GetCellValue(cells,0,2));
  }

  record Position(int x, int y);

  [Fact(Skip = "In progress")]
  public void TestExample_part1()
  {
    char[][] grid = 
      _testInput
        .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
        .Select(line => line.ToCharArray())
        .ToArray();

    // bool CheckXmasInDirection(int x, int y, Func<int,int,<int,int>> positionModifier){
    //   if (GetCellValue(grid, x, y ) != 'X'){
    //     return false;
    //   }

    //   var positionModifier
    // };

    bool IsXmasCell(char[][] grid, int y, int x){
      if (GetCellValue(grid, x, y) !='X'){
        return false;
      }

      if (GetCellValue(grid, x+1, y) !='M'){
        return false;
      }

      if (GetCellValue(grid, x+2, y) !='A'){
        return false;
      }

      if (GetCellValue(grid, x+3, y) !='S'){
        return false;
      }
      return true;
    }

    var count = 0;
    for(var y = 0; y < grid.Length; y++){
      for(var x = 0; x < grid[0].Length;x ++){
        if (IsXmasCell(grid, y, x)){
          count++;
        }
      }
    }

    Assert.Equal(2, count);
  }
}
