namespace advent_of_code_csharp_2024;

public class Day07
{
    public static readonly string InputFilename = @"Day07_input.txt";

    public enum Operator { Add, Multiply, Concatenation }

    public record Equation(long TestValue, long[] Numbers)
    {
        private static long Concatenation(long a, long b)
        {
            long multiplier = 1;
            while (multiplier <= b) multiplier *= 10;
            return a * multiplier + b;
        }

        private static bool Search(long[] numbers, int index, long current, long target, Operator[] ops)
        {
            if (index == numbers.Length) return current == target;
            if (current > target) return false;
            foreach (var op in ops)
            {
                var next = op switch
                {
                    Operator.Add => current + numbers[index],
                    Operator.Multiply => current * numbers[index],
                    Operator.Concatenation => Concatenation(current, numbers[index]),
                    _ => throw new NotImplementedException()
                };
                if (Search(numbers, index + 1, next, target, ops)) return true;
            }
            return false;
        }

        public bool IsValid(Operator[] supportedOperators)
            => Search(Numbers, 1, Numbers[0], TestValue, supportedOperators);
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
