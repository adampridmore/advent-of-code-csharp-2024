namespace advent_of_code_csharp_2024;

public class Day23
{
    public static readonly string InputFilename = @"Day23_input.txt";

    private static Dictionary<string, HashSet<string>> BuildGraph(string[] lines)
    {
        var g = new Dictionary<string, HashSet<string>>();
        foreach (var line in lines)
        {
            var parts = line.Split('-');
            var (a, b) = (parts[0], parts[1]);
            if (!g.ContainsKey(a)) g[a] = [];
            if (!g.ContainsKey(b)) g[b] = [];
            g[a].Add(b); g[b].Add(a);
        }
        return g;
    }

    public static long SolvePart1(string[] lines)
    {
        var g = BuildGraph(lines);
        int count = 0;
        foreach (var a in g.Keys)
        {
            var larger = g[a].Where(b => string.Compare(b, a) > 0).ToList();
            for (int i = 0; i < larger.Count; i++)
                for (int j = i + 1; j < larger.Count; j++)
                {
                    string b = larger[i], c = larger[j];
                    if (g[b].Contains(c) && (a[0] == 't' || b[0] == 't' || c[0] == 't'))
                        count++;
                }
        }
        return count;
    }

    public static string SolvePart2(string[] lines)
    {
        var g = BuildGraph(lines);
        var nodes = g.Keys.ToList();
        List<string> maxClique = [];

        void BronKerbosch(List<string> R, List<string> P, List<string> X)
        {
            if (P.Count == 0 && X.Count == 0)
            {
                if (R.Count > maxClique.Count) maxClique = [..R];
                return;
            }
            // Pivot: choose node in P∪X with most neighbours in P
            var pivot = P.Concat(X).MaxBy(v => P.Count(p => g[v].Contains(p)))!;
            foreach (var v in P.Where(p => !g[pivot].Contains(p)).ToList())
            {
                BronKerbosch(
                    [..R, v],
                    P.Where(p => g[v].Contains(p)).ToList(),
                    X.Where(x => g[v].Contains(x)).ToList()
                );
                P.Remove(v); X.Add(v);
            }
        }

        BronKerbosch([], [..nodes], []);
        maxClique.Sort();
        return string.Join(",", maxClique);
    }
}
