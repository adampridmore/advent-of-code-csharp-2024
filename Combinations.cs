using Xunit.Abstractions;

namespace advent_of_code_csharp_2024;

public static class Combinations
{
    public static IEnumerable<T[]> GenerateCombinations<T>(int size, T[] values)
    {
        return GenerateCombinationsRecursive(new T[size], 0, values);
    }

    static IEnumerable<T[]> GenerateCombinationsRecursive<T>(T[] current, int position, T[] values)
    {
        if (position == current.Length)
        {
            // Yield the current combination
            yield return (T[])current.Clone();
            yield break;
        }

        foreach (var value in values)
        {
            current[position] = value;
            foreach (var combination in GenerateCombinationsRecursive(current, position + 1, values))
            {
                yield return combination;
            }    
        }
    }
}