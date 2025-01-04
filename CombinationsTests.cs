using Xunit.Abstractions;

namespace advent_of_code_csharp_2024;

public class CombinationsTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Test()
    {
        var arraySize = 2; // Example size of the boolean array
        var combinations = Combinations.GenerateCombinations(arraySize).ToList();

        foreach (var combination in combinations)
        {
            testOutputHelper.WriteLine(string.Join(", ", combination));
        }
        
        Assert.Contains([false, false], combinations);
        Assert.Contains([false, true], combinations);
        Assert.Contains([true, false], combinations);
        Assert.Contains([true, true], combinations);
        
        Assert.Equal(4, combinations.Count);
    }
}