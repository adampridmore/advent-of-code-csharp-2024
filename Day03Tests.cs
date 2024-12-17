namespace advent_of_code_csharp_2024;

public class Day03Tests
{
  private static string _testInput = "xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))";

  private static string _testInput_Part2 = "xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))";

  public static readonly string InputFilename = @"../../../Day03_input.txt";


  [Fact]
  public void TestExample_part1()
  {
    var ans = Day03.RunProgram(_testInput);

    Assert.Equal(161, ans);
  }

  [Fact]
  public void ReadData_part1()
  {
    var ans = Day03.RunProgram(File.ReadAllText(InputFilename));

    Assert.Equal(159833790, ans);
  }

  [Fact]
  public void TestExample_part2()
  {
    var ans = Day03.RunProgramPartII(_testInput_Part2);

    Assert.Equal(48, ans);
  }

  [Fact]
  public void RealData_part2()
  {
    var ans = Day03.RunProgramPartII(File.ReadAllText(InputFilename));

    Assert.Equal(89349241, ans);
  }
}
