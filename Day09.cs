namespace advent_of_code_csharp_2024;

public class Day09
{
    public static readonly string InputFilename = @"Day09_input.txt";

    private static int?[] ExpandDisk(string input)
    {
        var disk = new List<int?>();
        for (int i = 0; i < input.Length; i++)
        {
            int len = input[i] - '0';
            int? value = i % 2 == 0 ? i / 2 : null;
            for (int j = 0; j < len; j++) disk.Add(value);
        }
        return disk.ToArray();
    }

    public static long SolvePart1(string[] lines)
    {
        var disk = ExpandDisk(lines[0]);
        int left = 0, right = disk.Length - 1;
        while (left < right)
        {
            while (left < right && disk[left] != null) left++;
            while (left < right && disk[right] == null) right--;
            if (left < right)
            {
                disk[left++] = disk[right];
                disk[right--] = null;
            }
        }
        return Checksum(disk);
    }

    public static long SolvePart2(string[] lines)
    {
        var input = lines[0];
        var files = new List<(int id, int start, int len)>();
        var freeSpans = new List<(int start, int len)>();

        int pos = 0;
        for (int i = 0; i < input.Length; i++)
        {
            int len = input[i] - '0';
            if (i % 2 == 0) { if (len > 0) files.Add((i / 2, pos, len)); }
            else             { if (len > 0) freeSpans.Add((pos, len)); }
            pos += len;
        }

        for (int f = files.Count - 1; f >= 0; f--)
        {
            var (id, fileStart, fileLen) = files[f];

            int bestSpan = -1;
            for (int s = 0; s < freeSpans.Count; s++)
            {
                if (freeSpans[s].start >= fileStart) break;
                if (freeSpans[s].len >= fileLen) { bestSpan = s; break; }
            }
            if (bestSpan == -1) continue;

            var (spanStart, spanLen) = freeSpans[bestSpan];
            files[f] = (id, spanStart, fileLen);

            if (spanLen == fileLen) freeSpans.RemoveAt(bestSpan);
            else freeSpans[bestSpan] = (spanStart + fileLen, spanLen - fileLen);
        }

        long sum = 0;
        foreach (var (id, start, len) in files)
            for (int i = 0; i < len; i++)
                sum += (long)(start + i) * id;
        return sum;
    }

    private static long Checksum(int?[] disk)
    {
        long sum = 0;
        for (int i = 0; i < disk.Length; i++)
            if (disk[i] != null) sum += (long)i * disk[i]!.Value;
        return sum;
    }
}
