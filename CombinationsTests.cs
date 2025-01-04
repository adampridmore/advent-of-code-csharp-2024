using Xunit.Abstractions;

namespace advent_of_code_csharp_2024;

public class CombinationsTests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void CombinationsForBools()
    {
        var values = new []{ true, false };

        var arraySize = 2;
        var combinations = Combinations.GenerateCombinations(arraySize, values).ToList();

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
    
    [Fact]
    public void CombinationsForSomeInts()
    {
        var values = new []{ 1,2,3 };

        var arraySize = 2; // Example size of the boolean array
        var combinations = Combinations.GenerateCombinations(arraySize, values).ToList();

        foreach (var combination in combinations)
        {
            testOutputHelper.WriteLine(string.Join(", ", combination));
        }
        
        Assert.Contains([1, 1], combinations);
        Assert.Contains([1, 2], combinations);
        Assert.Contains([1, 3], combinations);
        
        Assert.Equal(9, combinations.Count);
    }
}