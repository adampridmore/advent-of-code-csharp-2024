namespace advent_of_code_csharp_2024;

public class Day07
{
    public static readonly string InputFilename = @"Day07_input.txt";

    public enum Operator { Add, Multiply, Concatenation }

    public record Equation(long TestValue, long[] Numbers)
    {
        private static long ApplyOperatorsToNumbers(Operator[] operators, long[] numbers)
        {
            var product = numbers
                .Select((number, index) => (number, index))
                .Aggregate((a, b) =>
                {
                    return operators[b.index - 1] switch
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
            => long.Parse(fst.ToString() + snd.ToString());

        public bool IsValid(Operator[] supportedOperators)
        {
            var operatorsList = Combinations.GenerateCombinations(Numbers.Length - 1, supportedOperators);
            return operatorsList
                .Select(ops => ApplyOperatorsToNumbers(ops.ToArray(), Numbers))
                .Any(actualValue => actualValue == TestValue);
        }
    }

    public static Equation ParseRow(string line)
    {
        var split = line.Split(":");
        var testValue = long.Parse(split[0]);
        var numbers = split[1]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse)
            .ToArray();
        return new Equation(testValue, numbers);
    }

    public static long Solver(IEnumerable<string> lines, Operator[] supportedOperators)
    {
        return lines
            .Select(ParseRow)
            .Where(x => x.IsValid(supportedOperators))
            .Select(x => x.TestValue)
            .Sum();
    }
}
