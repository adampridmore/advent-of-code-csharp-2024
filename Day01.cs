namespace advent_of_code_csharp_2024;

public class Day01
{
    public static readonly string InputFilename = @"../../../Day01_input.txt";

    public record Line(int First, int Second);

    public static Line ParseLine(string line)
    {
        var split = line.Split(" ", StringSplitOptions.RemoveEmptyEntries);

        return new Line(int.Parse(split[0]), int.Parse(split[1]));
    }

    public static int SolveDay1_Part1(IEnumerable<Line> lines)
    {
        var leftOrdered = lines.OrderBy(x => x.First);
        var rightOrdered = lines.OrderBy(x => x.Second);

        return leftOrdered
                .Zip(rightOrdered, (a, b) => Math.Abs(a.First - b.Second))
                .Sum();
    }

    public static int SolveDay1_Part2(IEnumerable<Line> parsedLines)
    {
        var list1 = parsedLines.Select(_ => _.First);
        var list2 = parsedLines.Select(_ => _.Second);
        var groupedList2 = list2.GroupBy(_ => _).ToList();

        var answer =
            list1.Select(list1Item =>
            {
                var x = groupedList2
                    .FirstOrDefault(x => x.Key == list1Item);

                if (x == null)
                {
                    return 0;
                }
                else
                {
                    return list1Item * x.Count();
                }
            }).Sum();
        return answer;
    }
}
