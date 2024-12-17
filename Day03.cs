using System.Text.RegularExpressions;

namespace advent_of_code_csharp_2024;

public class Day03
{
  private static string _testInput = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";

  public static readonly string InputFilename = @"../../../Day03_input.txt";

  public static int RunProgram(string testInput){
    var regex = new Regex("mul\\(([0-9]+),([0-9]+)\\)");
    
    var matches = regex.Matches(testInput);

    var ans = matches
      .Select( (Match m) => {
        var x = int.Parse(m.Groups[1].Value);
        var y = int.Parse(m.Groups[2].Value);

        return x * y;
      })
      .Sum();

    return ans;
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


}
