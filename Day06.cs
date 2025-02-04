namespace advent_of_code_csharp_2024.Day06;

public record Position(int X, int Y);

public class Cells
{
  private readonly char[][] _cells;

  public char[] this[int row]
  {
    get
    {
      return _cells[0];
    }
  }

  public Cells(char[][] cells)
  {
    _cells = cells;
  }

  public static Cells FromText(string lines)
  {
    return new Cells(LoadCells(lines));
  }

  public static Cells FromLines(IEnumerable<string> lines)
  {
    return new Cells(LoadCells(lines));
  }

  public Position GetStartPosition()
  {
    var x = _cells
      .SelectMany((line, y) => line.Select((cell, x) => (new Position(x, y), cell)))
      .First(_ => _.cell == '^');

    return x.Item1;
  }

  private static char[][] LoadCells(string text)
  {
    return LoadCells(text.Split(Environment.NewLine));
  }

  private static char[][] LoadCells(IEnumerable<string> lines)
  {
    return lines
        .Select(line => line.ToCharArray().Where(c => ".#^".Contains(c)).ToArray())
        .Where(line => line.Length > 0)
        .ToArray();
  }

  public char GetCell(Position p)
  {
    return _cells[p.Y][p.X];
  }

  internal bool IsObstruction(Position p)
  {
    if (IsOutsideOfBounds(p))
    {
      return false;
    }

    return GetCell(p) == '#';
  }

  internal bool IsOutsideOfBounds(Position p)
  {
    return
      p.X < 0 ||
      p.X >= _cells[0].Length ||
      p.Y < 0 ||
      p.Y >= _cells.Length;
  }

  internal void SetTrace(Position position)
  {
    _cells[position.Y][position.X] = 'X';
  }

  internal int VisitCount()
  {
    return _cells
      .SelectMany(row => row)
      .Count(cell => cell == 'X');
  }
}

public enum Direction { Up, Right, Down, Left };

public class Guard
{

  public Position Position { get; private set; }
  public Direction Direction { get; private set; }

  public Guard(Position position, Direction direction)
  {
    Position = position;
    Direction = direction;
  }

  internal bool Move(Cells cells)
  {
    Position nextPosition;
    for (nextPosition = GetNextPosition(); cells.IsObstruction(nextPosition); Turn(), nextPosition = GetNextPosition())
    {
    }

    if (cells.IsOutsideOfBounds(nextPosition))
    {
      return false;
    }

    Position = nextPosition;

    // Console.WriteLine($"Direction: {Direction} {Position}");

    cells.SetTrace(Position);

    return true;
  }

  private void Turn()
  {
    Direction = Direction switch
    {
      Direction.Up => Direction.Right,
      Direction.Right => Direction.Down,
      Direction.Down => Direction.Left,
      Direction.Left => Direction.Up,
      _ => throw new Exception($"Unknown direction: {Direction}")
    };
  }

  private Position GetNextPosition()
  {
    return Direction switch
    {
      Direction.Up => new Position(Position.X, Position.Y - 1),
      Direction.Right => new Position(Position.X + 1, Position.Y),
      Direction.Down => new Position(Position.X, Position.Y + 1),
      Direction.Left => new Position(Position.X - 1, Position.Y),
      _ => throw new Exception($"Unknown direction: {Direction}")
    };
  }

  public void DoRun(Cells cells)
  {
    cells.SetTrace(Position);

    do { } while (Move(cells));
  }
}
