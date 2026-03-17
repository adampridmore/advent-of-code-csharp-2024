namespace advent_of_code_csharp_2024;

public class Day17
{
    public static readonly string InputFilename = @"Day17_input.txt";

    private static (long A, long B, long C, int[] prog) Parse(string[] lines)
    {
        long a = long.Parse(lines[0].Split(": ")[1]);
        long b = long.Parse(lines[1].Split(": ")[1]);
        long c = long.Parse(lines[2].Split(": ")[1]);
        int[] prog = lines[4].Split(": ")[1].Split(',').Select(int.Parse).ToArray();
        return (a, b, c, prog);
    }

    private static List<long> Run(long A, long B, long C, int[] prog)
    {
        var output = new List<long>();
        int ip = 0;
        while (ip < prog.Length - 1)
        {
            int opcode = prog[ip], operand = prog[ip + 1];
            long combo = operand switch { 4 => A, 5 => B, 6 => C, _ => operand };
            switch (opcode)
            {
                case 0: A >>= (int)combo; break;
                case 1: B ^= operand; break;
                case 2: B = combo & 7; break;
                case 3: if (A != 0) { ip = operand; continue; } break;
                case 4: B ^= C; break;
                case 5: output.Add(combo & 7); break;
                case 6: B = A >> (int)combo; break;
                case 7: C = A >> (int)combo; break;
            }
            ip += 2;
        }
        return output;
    }

    public static string SolvePart1(string[] lines)
    {
        var (A, B, C, prog) = Parse(lines);
        return string.Join(",", Run(A, B, C, prog));
    }

    // Find A such that the program outputs itself (quine)
    // The program processes A 3 bits at a time, outputting one value per iteration
    // We work backwards from the last output digit
    public static long SolvePart2(string[] lines)
    {
        var (_, B, C, prog) = Parse(lines);
        return Search(prog, B, C, 0, prog.Length - 1)!.Value;
    }

    private static long? Search(int[] prog, long B0, long C0, long aBase, int targetIdx)
    {
        for (int bits = 0; bits < 8; bits++)
        {
            long a = (aBase << 3) | bits;
            if (a == 0 && targetIdx == prog.Length - 1) continue;
            var output = Run(a, B0, C0, prog);
            if (output.Count > 0 && output[0] == prog[targetIdx])
            {
                if (targetIdx == 0) return a;
                var result = Search(prog, B0, C0, a, targetIdx - 1);
                if (result.HasValue) return result;
            }
        }
        return null;
    }
}
