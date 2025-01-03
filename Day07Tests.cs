using System.Collections;

namespace advent_of_code_csharp_2024;

using System.Collections.Specialized;
using advent_of_code_csharp_2024.Day06;

public class Day07Tests
{
  public static readonly string InputFilename = @"../../../Day07_input.txt";
  private static string _testInput = @"190: 10 19
3267: 81 40 27
83: 17 5
156: 15 6
7290: 6 8 6 15
161011: 16 10 13
192: 17 8 14
21037: 9 7 18 13
292: 11 6 16 20";

  public record Equation(int TestValue, int[] Numbers)
  {
    enum Operator
    {
      Add,
      Multiply
    };

    private static int ApplyOperatorsToNumbers(Operator[] operators, int[] Numbers)
    {
      var product = Numbers
        .Select((number, index) => (number, index))
        .Aggregate((a, b) =>
      {
        return operators[a.index] switch
        {
          Operator.Add => (a.number + b.number, a.index),
          Operator.Multiply => (a.number * b.number, a.index),
          _ => throw new NotImplementedException()
        };
      });

      return product.number;
    }

    public List<T> CreateFilledArray<T>(T t, int count){
      var array = new T[count];
      Array.Fill(array, t);
      return array.ToList();
    }

    public bool IsValid()
    {
      // var maxSize = 3;
      // var bv = new BitArray(new int[2]);

      // TODO: Get all combinations of add and multiply
      var operatorsList = new List<List<Operator>> {
        CreateFilledArray(Operator.Add, Numbers.Length),
        CreateFilledArray(Operator.Multiply, Numbers.Length)
      };

      return operatorsList
        .Select(operators => ApplyOperatorsToNumbers(operators.ToArray(), Numbers))
        .Any(actualValue => actualValue == TestValue);
    }
  }

  public static Equation ParseRow(string testLine)
  {
    var split = testLine.Split(":");
    var testValue = int.Parse(split[0]);

    var numbers = split[1]
      .Split(" ", StringSplitOptions.RemoveEmptyEntries)
      .Select(int.Parse)
      .ToArray();

    return new Equation(testValue, numbers);
  }

  public static IEnumerable<Equation> ParseLines(IEnumerable<string> lines)
  {
    return lines.Select(ParseRow);
  }

  [Fact]
  public void ParseInputTest()
  {
    var sr = new StringReader(_testInput);

    var equations = ParseLines(_testInput.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries)).ToArray();

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
  public void IsNotValid()
  {
    ;
    var equation = new Equation(1, [2]);
    Assert.False(equation.IsValid());
  }
}
