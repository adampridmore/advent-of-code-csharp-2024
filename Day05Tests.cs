namespace advent_of_code_csharp_2024;

public class Day05Tests
{
  private static string _testInput = @"47|53
97|13
97|61
97|47
75|29
61|13
75|53
29|13
97|29
53|29
61|53
97|53
61|29
47|13
75|47
97|75
47|61
75|61
47|29
75|13
53|13

75,47,61,53,29
97,61,53,29,13
75,29,13
75,97,47,61,53
61,13,29
97,13,75,29,47";

  public static readonly string InputFilename = @"../../../Day05_input.txt";

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

  [Fact]
  public void ToUpdateValueTests()
  {
    var lines = _testInput.Split(Environment.NewLine);

    var (pageOrderingRules, pageNumbersUpdates) = SplitInput(lines);
    var rules = ParseRules(pageOrderingRules);
    var updates = ParseUpdate(pageNumbersUpdates).ToList();
    Assert.Equal(61, ToUpdateValue(updates[0], rules));
    Assert.Equal(0, ToUpdateValue(updates[4], rules));
  }

  [Fact]
  public void Example_Part1()
  {
    var lines = _testInput.Split(Environment.NewLine);

    Assert.Equal(143, Solver(lines));
  }


  [Fact]
  public void RealData_Part1()
  {
    var lines = File.ReadAllLines(InputFilename);
    Assert.Equal(5651, Solver(lines));
  }

  [Fact]
  public void ParseRulesTest()
  {
    var lines = _testInput.Split(Environment.NewLine);
    var (pageOrderingRules, _) = SplitInput(lines);
    var rules = ParseRules(pageOrderingRules);
    Assert.Equal(rules[97], [13, 61, 47, 29, 53, 75]);
  }

  [Fact]
  public void ParseUpdatesTest()
  {
    var lines = _testInput.Split(Environment.NewLine);
    var (_, pageNumbersUpdates) = SplitInput(lines);
    var updates = ParseUpdate(pageNumbersUpdates);
    Assert.Equal(updates.First(), [75, 47, 61, 53, 29]);
  }
}
