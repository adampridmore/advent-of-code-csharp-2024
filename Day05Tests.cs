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

  [Fact]
  public void ToUpdateValueTests()
  {
    var lines = _testInput.Split(Environment.NewLine);

    var (pageOrderingRules, pageNumbersUpdates) = Day05.SplitInput(lines);
    var rules = Day05.ParseRules(pageOrderingRules);
    var updates = Day05.ParseUpdate(pageNumbersUpdates).ToList();
    Assert.Equal(61, Day05.ToUpdateValue(updates[0], rules));
    Assert.Equal(0, Day05.ToUpdateValue(updates[4], rules));
  }

  [Fact]
  public void Example_Part1()
  {
    var lines = _testInput.Split(Environment.NewLine);

    Assert.Equal(143, Day05.Solver(lines));
  }


  [Fact]
  public void RealData_Part1()
  {
    var lines = File.ReadAllLines(InputFilename);
    Assert.Equal(5651, Day05.Solver(lines));
  }

  [Fact]
  public void ParseRulesTest()
  {
    var lines = _testInput.Split(Environment.NewLine);
    var (pageOrderingRules, _) = Day05.SplitInput(lines);
    var rules = Day05.ParseRules(pageOrderingRules);
    Assert.Equal(rules[97], [13, 61, 47, 29, 53, 75]);
  }

  [Fact]
  public void ParseUpdatesTest()
  {
    var lines = _testInput.Split(Environment.NewLine);
    var (_, pageNumbersUpdates) = Day05.SplitInput(lines);
    var updates = Day05.ParseUpdate(pageNumbersUpdates);
    Assert.Equal(updates.First(), [75, 47, 61, 53, 29]);
  }
}
