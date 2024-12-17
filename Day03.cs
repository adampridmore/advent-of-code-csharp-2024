using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace advent_of_code_csharp_2024;

public class Day03
{
  private static string _testInput = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";

  private static string _testInput_Part2 = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";

  public static readonly string InputFilename = @"../../../Day03_input.txt";

  private static int ProcessMul(Match m){
    var x = int.Parse(m.Groups[1].Value);
    var y = int.Parse(m.Groups[2].Value);

    return x * y;
  }

  public static int RunProgram(string testInput){
    var regex = new Regex("""mul\(([0-9]+),([0-9]+)\)""");
    
    var matches = regex.Matches(testInput);

    var ans = matches
      .Select(ProcessMul)
      .Sum();

    return ans;
  }

  public static int RunProgramPartII(string testInput){
    var regex = new Regex("""mul\(([0-9]+),([0-9]+)\)|don't\(\)|do\(\)""");

    var matches = regex.Matches(testInput);

    bool enable = true;
    int total = 0;
    foreach(Match match in matches){
      if (match.Value == "do()"){
        enable = true;
      } else if (match.Value == "don't()"){
        enable = false;
      } else {
        if (enable) {
          total += ProcessMul(match);
        }
      }
    }

    return total;
  }

  [Fact]
  public void TestExample_part1(){
    var ans = RunProgram(_testInput);

    Assert.Equal(161,ans);
  }

  [Fact]
  public void ReadData_part1(){
    var ans = RunProgram(File.ReadAllText(InputFilename));

    Assert.Equal(159833790,ans);
  }

  [Fact]
  public void TestExample_part2(){
    var ans = RunProgramPartII(_testInput_Part2);

    Assert.Equal(48, ans);
  }

  [Fact]
  public void RealData_part2(){
    var ans = RunProgramPartII(File.ReadAllText(InputFilename));

    Assert.Equal(1, ans);
  }
}
