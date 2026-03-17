namespace advent_of_code_csharp_2024;

public class Day24
{
    public static readonly string InputFilename = @"Day24_input.txt";

    private record Gate(string A, string Op, string B, string Out);

    private static (Dictionary<string, int> wires, List<Gate> gates) Parse(string[] lines)
    {
        var wires = new Dictionary<string, int>();
        var gates = new List<Gate>();
        int i = 0;
        for (; i < lines.Length && lines[i] != ""; i++)
        {
            var p = lines[i].Split(": ");
            wires[p[0]] = int.Parse(p[1]);
        }
        for (i++; i < lines.Length; i++)
        {
            var p = lines[i].Split(' ');
            gates.Add(new Gate(p[0], p[1], p[2], p[4]));
        }
        return (wires, gates);
    }

    private static long Simulate(Dictionary<string, int> wires, List<Gate> gates)
    {
        var queue = new Queue<Gate>(gates);
        while (queue.Count > 0)
        {
            var g = queue.Dequeue();
            if (!wires.ContainsKey(g.A) || !wires.ContainsKey(g.B)) { queue.Enqueue(g); continue; }
            wires[g.Out] = g.Op switch
            {
                "AND" => wires[g.A] & wires[g.B],
                "OR"  => wires[g.A] | wires[g.B],
                "XOR" => wires[g.A] ^ wires[g.B],
                _ => throw new Exception()
            };
        }
        long result = 0;
        foreach (var kv in wires.Where(w => w.Key.StartsWith("z")).OrderByDescending(w => w.Key))
            result = (result << 1) | (long)kv.Value;
        return result;
    }

    public static long SolvePart1(string[] lines)
    {
        var (wires, gates) = Parse(lines);
        return Simulate(wires, gates);
    }

    // Structural analysis of ripple-carry adder to find swapped outputs
    public static string SolvePart2(string[] lines)
    {
        var (_, gates) = Parse(lines);
        var swapped = new HashSet<string>();
        int maxBit = gates.Max(g => g.Out.StartsWith("z") ? int.Parse(g.Out[1..]) : 0);

        // Rules for a correct ripple-carry adder:
        // 1. Output of XOR gate must be a z-wire (except when inputs are x/y)
        // 2. Output of XOR gate with x/y inputs (not bit 0) must feed into XOR and AND
        // 3. Output of AND gate (not x0/y0 pair) must feed into OR only
        // 4. z-wire (not z_max) must come from XOR gate

        bool IsXY(string w) => w.StartsWith('x') || w.StartsWith('y');

        foreach (var g in gates)
        {
            // z-output (not last) must come from XOR
            if (g.Out.StartsWith('z') && g.Out != $"z{maxBit:D2}" && g.Op != "XOR")
                swapped.Add(g.Out);

            if (g.Op == "XOR")
            {
                // XOR with non-x/y inputs must produce z
                if (!IsXY(g.A) && !IsXY(g.B) && !g.Out.StartsWith('z'))
                    swapped.Add(g.Out);

                // XOR with x/y inputs (not bit 0) must be used by another XOR and an AND
                if (IsXY(g.A) && IsXY(g.B) && g.A != "x00" && g.B != "x00")
                {
                    bool usedByXor = gates.Any(other => (other.A == g.Out || other.B == g.Out) && other.Op == "XOR");
                    if (!usedByXor) swapped.Add(g.Out);
                }
            }

            if (g.Op == "AND")
            {
                // AND (not x0/y0) must feed into OR
                if (!((g.A == "x00" && g.B == "y00") || (g.A == "y00" && g.B == "x00")))
                {
                    bool usedByOr = gates.Any(other => (other.A == g.Out || other.B == g.Out) && other.Op == "OR");
                    if (!usedByOr) swapped.Add(g.Out);
                }
            }
        }

        var sorted = swapped.OrderBy(w => w).ToList();
        return string.Join(",", sorted);
    }
}
