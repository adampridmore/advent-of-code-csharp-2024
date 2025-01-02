namespace advent_of_code_csharp_2024;

using advent_of_code_csharp_2024.Day06;

public class Day06Tests
{
  
  public static readonly string InputFilename = @"../../../Day06_input.txt";
  private static string _testInput = @"....#.....
.........#
..........
..#.......
.......#..
..........
.#..^.....
........#.
#.........
......#...
";

  private static string _testInput2 = @"###
#.#
...
.^.";

  // Start cell x : 4
  // Start cell y : 6

  [Fact]
  public void LoadCellsTest()
  {
    var cells = Cells.FromText(_testInput);

    Assert.Equal("....#.....".ToCharArray(), cells[0]);
  }

  [Fact]
  public void GetStartPositionTest()
  {
    var cells = Cells.FromText(_testInput);

    var startCellPosition = cells.GetStartPosition();

    Assert.Equal(new Position(4, 6), startCellPosition);
  }

  [Fact]
  public void GetCellAtPosition()
  {
    var cells = Cells.FromText(_testInput);

    char cell = cells.GetCell(new Position(4, 6));

    Assert.Equal('^', cell);
  }

  [Fact]
  public void MoveAndTurnAFewTimeTest()
  {
    var cells = Cells.FromText(_testInput);

    var startCellPosition = cells.GetStartPosition();

    Guard guard = new Guard(startCellPosition, Direction.Up);

    guard.Move(cells);

    Assert.Equal(new Position(4, 5), guard.Position);
    Assert.Equal(Direction.Up, guard.Direction);

    guard.Move(cells);
    guard.Move(cells);
    guard.Move(cells);
    guard.Move(cells);

    Assert.Equal(new Position(4, 1), guard.Position);

    guard.Move(cells);
    Assert.Equal(Direction.Right, guard.Direction);
    Assert.Equal(new Position(5, 1), guard.Position);
  }

  [Fact]
  public void Example_partI()
  {
    var cells = Cells.FromText(_testInput);

    var startCellPosition = cells.GetStartPosition();

    Guard guard = new Guard(startCellPosition, Direction.Up);

    guard.DoRun(cells);

    Assert.Equal(41,cells.VisitCount());
  }

  [Fact]
  public void Real_partI()
  {
    var cells = Cells.FromLines(File.ReadLines(InputFilename));

    var startCellPosition = cells.GetStartPosition();

    Guard guard = new Guard(startCellPosition, Direction.Up);

    guard.DoRun(cells);

    // To Low...
    Assert.Equal(4559,cells.VisitCount());
  }

  [Fact]
  public void Edge_case_1(){
    var cells = Cells.FromText(_testInput2);

    var startCellPosition = cells.GetStartPosition();

    Guard guard = new Guard(startCellPosition, Direction.Up);

    guard.DoRun(cells);

    Assert.Equal(3,cells.VisitCount());
  }
}
