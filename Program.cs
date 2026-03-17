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

// Day 04
PrintDay(4,
    () => Day04.CountXmasWordSearch(Day04.LinesToChars(File.ReadLines(Day04.InputFilename))),
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

// Day 08
PrintDay(8,
    () => Day08.SolvePart1(File.ReadAllLines(Day08.InputFilename)),
    () => Day08.SolvePart2(File.ReadAllLines(Day08.InputFilename)));

// Day 09
PrintDay(9,
    () => Day09.SolvePart1(File.ReadAllLines(Day09.InputFilename)),
    () => Day09.SolvePart2(File.ReadAllLines(Day09.InputFilename)));

// Day 10
PrintDay(10,
    () => Day10.SolvePart1(File.ReadAllLines(Day10.InputFilename)),
    () => Day10.SolvePart2(File.ReadAllLines(Day10.InputFilename)));

// Day 11
PrintDay(11,
    () => Day11.SolvePart1(File.ReadAllLines(Day11.InputFilename)),
    () => Day11.SolvePart2(File.ReadAllLines(Day11.InputFilename)));

// Day 12
PrintDay(12,
    () => Day12.SolvePart1(File.ReadAllLines(Day12.InputFilename)),
    () => Day12.SolvePart2(File.ReadAllLines(Day12.InputFilename)));

// Day 13
PrintDay(13,
    () => Day13.SolvePart1(File.ReadAllLines(Day13.InputFilename)),
    () => Day13.SolvePart2(File.ReadAllLines(Day13.InputFilename)));

// Day 14
PrintDay(14,
    () => Day14.SolvePart1(File.ReadAllLines(Day14.InputFilename)),
    () => Day14.SolvePart2(File.ReadAllLines(Day14.InputFilename)));

// Day 15
PrintDay(15,
    () => Day15.SolvePart1(File.ReadAllLines(Day15.InputFilename)),
    () => Day15.SolvePart2(File.ReadAllLines(Day15.InputFilename)));

// Day 16
PrintDay(16,
    () => Day16.SolvePart1(File.ReadAllLines(Day16.InputFilename)),
    () => Day16.SolvePart2(File.ReadAllLines(Day16.InputFilename)));

// Day 17
PrintDay(17,
    () => Day17.SolvePart1(File.ReadAllLines(Day17.InputFilename)),
    () => Day17.SolvePart2(File.ReadAllLines(Day17.InputFilename)));

// Day 18
PrintDay(18,
    () => Day18.SolvePart1(File.ReadAllLines(Day18.InputFilename)),
    () => Day18.SolvePart2(File.ReadAllLines(Day18.InputFilename)));

// Day 19
PrintDay(19,
    () => Day19.SolvePart1(File.ReadAllLines(Day19.InputFilename)),
    () => Day19.SolvePart2(File.ReadAllLines(Day19.InputFilename)));

// Day 20
PrintDay(20,
    () => Day20.SolvePart1(File.ReadAllLines(Day20.InputFilename)),
    () => Day20.SolvePart2(File.ReadAllLines(Day20.InputFilename)));

// Day 21
PrintDay(21,
    () => Day21.SolvePart1(File.ReadAllLines(Day21.InputFilename)),
    () => Day21.SolvePart2(File.ReadAllLines(Day21.InputFilename)));

// Day 22
PrintDay(22,
    () => Day22.SolvePart1(File.ReadAllLines(Day22.InputFilename)),
    () => Day22.SolvePart2(File.ReadAllLines(Day22.InputFilename)));

// Day 23
PrintDay(23,
    () => Day23.SolvePart1(File.ReadAllLines(Day23.InputFilename)),
    () => Day23.SolvePart2(File.ReadAllLines(Day23.InputFilename)));

// Day 24
PrintDay(24,
    () => Day24.SolvePart1(File.ReadAllLines(Day24.InputFilename)),
    () => Day24.SolvePart2(File.ReadAllLines(Day24.InputFilename)));

// Day 25
PrintDay(25,
    () => Day25.SolvePart1(File.ReadAllLines(Day25.InputFilename)),
    () => "⭐");
