using Xunit.Abstractions;

namespace advent_of_code_csharp_2024;

public static class Combinations
{
    public static IEnumerable<bool[]> GenerateCombinations(int size)
    {
        return GenerateCombinationsRecursive(new bool[size], 0);
    }

    static IEnumerable<bool[]> GenerateCombinationsRecursive(bool[] current, int position)
    {
        if (position == current.Length)
        {
            // Yield the current combination
            yield return (bool[])current.Clone();
            yield break;
        }

        // Set the current position to false and recurse
        current[position] = false;
        foreach (var combination in GenerateCombinationsRecursive(current, position + 1))
        {
            yield return combination;
        }

        // Set the current position to true and recurse
        current[position] = true;
        foreach (var combination in GenerateCombinationsRecursive(current, position + 1))
        {
            yield return combination;
        }
    }
}