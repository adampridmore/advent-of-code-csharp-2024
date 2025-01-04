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

  public record Equation(long TestValue, long[] Numbers)
  {
    public enum Operator
    {
      Add,
      Multiply
    }
    
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
          _ => throw new NotImplementedException()
        };
      });

      return product.number;
    }

    public bool IsValid()
    {
      var operatorsList = 
        Combinations
          .GenerateCombinations(Numbers.Length - 1)
          .Select(x => x.Select(y=>y? Operator.Add : Operator.Multiply));
        
      return operatorsList
        .Select(operators => ApplyOperatorsToNumbers(operators.ToArray(), Numbers))
        .Any(actualValue => actualValue == TestValue);
    }
  }

  public static Equation ParseRow(string testLine)
  {
    var split = testLine.Split(":");
    var testValue = long.Parse(split[0]);

    var numbers = split[1]
      .Split(" ", StringSplitOptions.RemoveEmptyEntries)
      .Select(long.Parse)
      .ToArray();

    return new Equation(testValue, numbers);
  }

  public static IEnumerable<Equation> ParseLines(IEnumerable<string> lines)
  {
    return lines.Select(ParseRow);
  }
  
  public static long Solver(IEnumerable<string> lines)
  {
    return
      lines
        .Select(ParseRow)
        .Where(x => x.IsValid())
        .Select(x => x.TestValue)
        .Sum();
  }

  [Fact]
  public void ParseInputTest()
  {
    var sr = new StringReader(TestInput);

    var equations = ParseLines(TestInput.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)).ToArray();

    Assert.Equal(9, equations.Count());
    Assert.Equal(190, equations[0].TestValue);

    Assert.Equal(10, equations[0].Numbers[0]);
    Assert.Equal(19, equations[0].Numbers[1]);
  }

  [Fact]
  public void IsValidWhenProduct()
  {
    ;
    Assert.True(new Equation(1, [1]).IsValid());
    Assert.True(new Equation(2, [1, 2]).IsValid());
    Assert.True(new Equation(6, [1, 2, 3]).IsValid());
  }

  [Fact]
  public void IsValidWhenSum()
  {
    Assert.True(new Equation(1, [1]).IsValid());
    Assert.True(new Equation(3, [1, 2]).IsValid());
    Assert.True(new Equation(9, [2, 3, 4]).IsValid());
  }
  
  [Fact]
  public void IsValidWhenSumOrProduct()
  {
    Assert.True(new Equation(9, [2, 3, 4]).IsValid());
    Assert.True(new Equation(24, [2, 3, 4]).IsValid());
  }

  [Fact]
  public void IsNotValid()
  {
    Assert.False(new Equation(1, [2]).IsValid());
    Assert.False(new Equation(4, [1,2]).IsValid());
  }
  
  [Fact]
  public void Example_partI()
  {
    Assert.Equal(3749, Solver( TestInput.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)));
  }

  [Fact]
  public void ReadData_PartI()
  {
    Assert.Equal(3598800864292L, Solver(File.ReadLines(InputFilename)));
  }
}
