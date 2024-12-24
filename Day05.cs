namespace advent_of_code_csharp_2024;

public class Day05
{
  public static IDictionary<int, List<int>> ParseRules(IEnumerable<string> lines)
  {
    return lines
      .Select(line =>
      {
        var parts = line.Split('|');
        return (int.Parse(parts[0]), int.Parse(parts[1]));
      })
      .GroupBy(x => x.Item1)
      .ToDictionary(x => x.Key, x => x.Select(y => y.Item2).ToList());
  }

  public static IEnumerable<List<int>> ParseUpdate(IEnumerable<string> lines)
  {
    return lines
      .Select(line =>
      {
        return line
          .Split(",")
          .Select(text => int.Parse(text))
          .ToList();
      });
  }

  public static (IEnumerable<string>, IEnumerable<string>) SplitInput(IEnumerable<string> lines)
  {
    var pageOrderingRules = lines.Where(line => line.Contains('|'));

    var pageNumbersUpdates = lines.Where(line => line.Contains(','));

    return (pageOrderingRules, pageNumbersUpdates);
  }

  public static bool IsRuleOk(List<int> numberRules, List<int> remainingPages)
  {
    return remainingPages.All(numberRules.Contains);
  }

  public static bool IsUpdateValid(List<int> update, IDictionary<int, List<int>> rules)
  {
    for (int i = 1; i < update.Count; i++)
    {
      var currentPageNumber = update[i];
      if (rules.TryGetValue(currentPageNumber, out List<int>? currentPageRules))
      {
        var previousPages = update.Take(i).ToList();
        if (previousPages.Any(previousPage => currentPageRules.Contains(previousPage)))
        {
          return false;
        }
      }
    }

    return true;
  }

  public static int ToUpdateValue(List<int> update, IDictionary<int, List<int>> rules)
  {
    if (IsUpdateValid(update, rules))
    {
      return update[update.Count / 2];
    }
    else
    {
      return 0;
    }
  }

  public static int Solver(IEnumerable<string> lines)
  {
    var (pageOrderingRules, pageNumbersUpdates) = SplitInput(lines);
    var rules = ParseRules(pageOrderingRules);
    var updates = ParseUpdate(pageNumbersUpdates).ToList();
    return updates
        .Select(update => ToUpdateValue(update, rules))
        .Sum();
  }
}
