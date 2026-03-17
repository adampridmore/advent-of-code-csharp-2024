using System.Diagnostics;
using advent_of_code_csharp_2024;
using advent_of_code_csharp_2024.Day06;

static void PrintDay(int day, Func<object> part1, Func<object> part2)
{
    var sw = Stopwatch.StartNew();
    var p1 = part1();
    var t1 = sw.ElapsedMilliseconds;
    sw.Restart();
    var p2 = part2();
    var t2 = sw.ElapsedMilliseconds;
    Console.WriteLine($"Day {day:D2} | Part I: {p1,-20} ({t1}ms) | Part II: {p2,-20} ({t2}ms)");
}

// Day 01
PrintDay(1,
    () => Day01.SolvePart1(File.ReadLines(Day01.InputFilename).Select(Day01.ParseLine)),
    () => Day01.SolvePart2(File.ReadLines(Day01.InputFilename).Select(Day01.ParseLine)));

// Day 02
PrintDay(2,
    () => (object)File.ReadLines(Day02.InputFilename).Select(Day02.ParseLine).Count(Day02.IsSafe),
    () => (object)File.ReadLines(Day02.InputFilename).Select(Day02.ParseLine).Count(Day02.IsSafeTolerant));

// Day 03
PrintDay(3,
    () => Day03.RunProgram(File.ReadAllText(Day03.InputFilename)),
    () => Day03.RunProgramPartII(File.ReadAllText(Day03.InputFilename)));

// Day 04 (Part I not implemented)
PrintDay(4,
    () => "N/A",
    () => Day04.CountXmas(Day04.LinesToChars(File.ReadLines(Day04.InputFilename))));

// Day 05 (Part II not implemented)
PrintDay(5,
    () => Day05.Solver(File.ReadAllLines(Day05.InputFilename)),
    () => Day05.SolverPartII(File.ReadAllLines(Day05.InputFilename)));

// Day 06 (Part II not implemented)
PrintDay(6,
    () =>
    {
        var cells = Cells.FromLines(File.ReadLines(Cells.InputFilename));
        var guard = new Guard(cells.GetStartPosition(), Direction.Up);
        guard.DoRun(cells);
        return (object)cells.VisitCount();
    },
    () => Guard.CountLoopPositions(File.ReadLines(Cells.InputFilename)));

// Day 07
var operatorsI = new[] { Day07.Operator.Add, Day07.Operator.Multiply };
var operatorsII = Enum.GetValues<Day07.Operator>();
PrintDay(7,
    () => Day07.Solver(File.ReadLines(Day07.InputFilename), operatorsI),
    () => Day07.Solver(File.ReadLines(Day07.InputFilename), operatorsII));
