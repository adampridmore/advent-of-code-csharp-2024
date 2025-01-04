using System.Data.SqlTypes;
using System.Text.Json.Serialization;
using Xunit.Abstractions;

namespace advent_of_code_csharp_2024;

public class Day07Tests(ITestOutputHelper testOutputHelper)
{
  public static readonly string InputFilename = @"../../../Day07_input.txt";

  private const string TestInput = @"190: 10 19
3267: 81 40 27
83: 17 5
156: 15 6
7290: 6 8 6 15
161011: 16 10 13
192: 17 8 14
21037: 9 7 18 13
292: 11 6 16 20";

  public enum Operator
  {
    Add,
    Multiply,
    Concatenation
  }

  public record Equation(long TestValue, long[] Numbers)
  {
    private static long ApplyOperatorsToNumbers(Operator[] operators, long[] numbers)
    {
      var product = numbers
        .Select((number, index) => (number, index))
        .Aggregate((a, b) =>
      {
        return operators[b.index-1] switch
        {
          Operator.Add => (a.number + b.number, a.index),
          Operator.Multiply => (a.number * b.number, a.index),
          Operator.Concatenation => (Concatenation(a.number, b.number), a.index),
          _ => throw new NotImplementedException()
        };
      });

      return product.number;
    }

    private static long Concatenation(long fst, long snd)
    {
      return long.Parse(fst.ToString() + snd.ToString());
    }

    public bool IsValid(Operator[] supportedOperators)
    {
      var operatorsList =
        Combinations
          .GenerateCombinations(Numbers.Length - 1, supportedOperators);
          
      return operatorsList
        .Select(operators => ApplyOperatorsToNumbers(operators.ToArray(), Numbers))
        .Any(actualValue => actualValue == TestValue);
    }
  }

  public static Equation ParseRow(string testLine, Operator[] supportedOperators)
  {
    var split = testLine.Split(":");
    var testValue = long.Parse(split[0]);

    var numbers = split[1]
      .Split(" ", StringSplitOptions.RemoveEmptyEntries)
      .Select(long.Parse)
      .ToArray();

    return new Equation(testValue, numbers);
  }

  public static IEnumerable<Equation> ParseLines(IEnumerable<string> lines, Operator[] supportedOperators)
  {
    return lines.Select(row => ParseRow(row, supportedOperators));
  }
  
  public static long Solver(IEnumerable<string> lines, Operator[] supportedOperators)
  {
    return
      lines
        .Select(line=>ParseRow(line, supportedOperators))
        .Where(x => x.IsValid(supportedOperators))
        .Select(x => x.TestValue)
        .Sum();
  }

  private Operator[] SupportedOperatorsPartI = new[] { Operator.Add, Operator.Multiply };
  private Operator[] SupportedOperatorsPartII = Enum.GetValues<Operator>();
  
  [Fact]
  public void ParseInputTest()
  {
    var sr = new StringReader(TestInput);

    var equations = ParseLines(TestInput.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries),SupportedOperatorsPartI)
      .ToArray();

    Assert.Equal(9, equations.Count());
    Assert.Equal(190, equations[0].TestValue);

    Assert.Equal(10, equations[0].Numbers[0]);
    Assert.Equal(19, equations[0].Numbers[1]);
  }

  [Fact]
  public void IsValidWhenProduct()
  {
    ;
    Assert.True(new Equation(1, [1]).IsValid(SupportedOperatorsPartI));
    Assert.True(new Equation(2, [1, 2]).IsValid(SupportedOperatorsPartI));
    Assert.True(new Equation(6, [1, 2, 3]).IsValid(SupportedOperatorsPartI));
  }

  [Fact]
  public void IsValidWhenSum()
  {
    Assert.True(new Equation(1, [1]).IsValid(SupportedOperatorsPartI));
    Assert.True(new Equation(3, [1, 2]).IsValid(SupportedOperatorsPartI));
    Assert.True(new Equation(9, [2, 3, 4]).IsValid(SupportedOperatorsPartI));
  }
  
  [Fact]
  public void IsValidWhenSumOrProduct()
  {
    Assert.True(new Equation(9, [2, 3, 4]).IsValid(SupportedOperatorsPartI));
    Assert.True(new Equation(24, [2, 3, 4]).IsValid(SupportedOperatorsPartI));
  }

  [Fact]
  public void IsNotValid()
  {
    Assert.False(new Equation(1, [2]).IsValid(SupportedOperatorsPartI));
    Assert.False(new Equation(4, [1,2]).IsValid(SupportedOperatorsPartI));
  }
  
  [Fact]
  public void Example_partI()
  {
    Assert.Equal(3749, Solver( TestInput.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries), SupportedOperatorsPartI));
  }

  [Fact]
  public void ReadData_PartI()
  {
    Assert.Equal(3598800864292L, Solver(File.ReadLines(InputFilename), SupportedOperatorsPartI));
  }
  
  [Fact]
  public void Example_partII()
  {
    Assert.Equal(11387, Solver( TestInput.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries), SupportedOperatorsPartII));
  }
  
  [Fact]
  public void ReadData_PartII()
  {
    Assert.Equal(340362529351427L, Solver(File.ReadLines(InputFilename), SupportedOperatorsPartII));
  }
}
