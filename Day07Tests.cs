using Xunit.Abstractions;

namespace advent_of_code_csharp_2024;

public class Day07Tests(ITestOutputHelper testOutputHelper)
{
  private const string TestInput = @"190: 10 19
3267: 81 40 27
83: 17 5
156: 15 6
7290: 6 8 6 15
161011: 16 10 13
192: 17 8 14
21037: 9 7 18 13
292: 11 6 16 20";

  private Day07.Operator[] SupportedOperatorsPartI = [Day07.Operator.Add, Day07.Operator.Multiply];
  private Day07.Operator[] SupportedOperatorsPartII = Enum.GetValues<Day07.Operator>();

  [Fact]
  public void ParseInputTest()
  {
    var equations = TestInput
      .Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)
      .Select(Day07.ParseRow)
      .ToArray();

    Assert.Equal(9, equations.Length);
    Assert.Equal(190, equations[0].TestValue);
    Assert.Equal(10, equations[0].Numbers[0]);
    Assert.Equal(19, equations[0].Numbers[1]);
  }

  [Fact]
  public void IsValidWhenProduct()
  {
    Assert.True(new Day07.Equation(1, [1]).IsValid(SupportedOperatorsPartI));
    Assert.True(new Day07.Equation(2, [1, 2]).IsValid(SupportedOperatorsPartI));
    Assert.True(new Day07.Equation(6, [1, 2, 3]).IsValid(SupportedOperatorsPartI));
  }

  [Fact]
  public void IsValidWhenSum()
  {
    Assert.True(new Day07.Equation(1, [1]).IsValid(SupportedOperatorsPartI));
    Assert.True(new Day07.Equation(3, [1, 2]).IsValid(SupportedOperatorsPartI));
    Assert.True(new Day07.Equation(9, [2, 3, 4]).IsValid(SupportedOperatorsPartI));
  }

  [Fact]
  public void IsValidWhenSumOrProduct()
  {
    Assert.True(new Day07.Equation(9, [2, 3, 4]).IsValid(SupportedOperatorsPartI));
    Assert.True(new Day07.Equation(24, [2, 3, 4]).IsValid(SupportedOperatorsPartI));
  }

  [Fact]
  public void IsNotValid()
  {
    Assert.False(new Day07.Equation(1, [2]).IsValid(SupportedOperatorsPartI));
    Assert.False(new Day07.Equation(4, [1, 2]).IsValid(SupportedOperatorsPartI));
  }

  [Fact]
  public void Example_partI()
  {
    Assert.Equal(3749, Day07.Solver(TestInput.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries), SupportedOperatorsPartI));
  }

  [Fact]
  public void ReadData_PartI()
  {
    Assert.Equal(3598800864292L, Day07.Solver(File.ReadLines(Day07.InputFilename), SupportedOperatorsPartI));
  }

  [Fact]
  public void Example_partII()
  {
    Assert.Equal(11387, Day07.Solver(TestInput.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries), SupportedOperatorsPartII));
  }

  [Fact]
  public void ReadData_PartII()
  {
    Assert.Equal(340362529351427L, Day07.Solver(File.ReadLines(Day07.InputFilename), SupportedOperatorsPartII));
  }
}
