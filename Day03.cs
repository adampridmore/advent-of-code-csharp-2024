using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace advent_of_code_csharp_2024;

public class Day03
{
  private static int ProcessMul(Match m)
  {
    var x = int.Parse(m.Groups[1].Value);
    var y = int.Parse(m.Groups[2].Value);

    return x * y;
  }

  public static int RunProgram(string testInput)
  {
    var regex = new Regex("""mul\(([0-9]+),([0-9]+)\)""");

    var matches = regex.Matches(testInput);

    var ans = matches
      .Select(ProcessMul)
      .Sum();

    return ans;
  }

  public static int RunProgramPartII(string testInput)
  {
    var regex = new Regex("""mul\(([0-9]+),([0-9]+)\)|don't\(\)|do\(\)""");

    var matches = regex.Matches(testInput);

    bool enable = true;
    int total = 0;
    foreach (Match match in matches)
    {
      if (match.Value == "do()")
      {
        enable = true;
      }
      else if (match.Value == "don't()")
      {
        enable = false;
      }
      else
      {
        if (enable)
        {
          total += ProcessMul(match);
        }
      }
    }

    return total;
  }
}
