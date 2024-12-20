using Utilities;

namespace Day19;

class Program
{
    private const string Sample = "sample.txt";
    private const string Input = "input.txt";

    static void Main(string[] args)
    {
        RunPart1(Sample);
        RunPart1(Input);
        RunPart2(Sample);
        RunPart2(Input);
    }

    static void RunPart1(string inputFile)
    {
        var start = DateTime.Now;

        long result = 0;
        var lines = System.IO.File.ReadAllLines(inputFile);

        var towelPatterns = lines[0].Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var requiredPatterns = lines[2..];

        foreach (var pattern in requiredPatterns)
        {
            Dictionary<string, long> cache = new();
            if (CanMatch(pattern, towelPatterns, cache) > 0)
            {
                result++;
            }
        }

        System.Console.WriteLine(
            $"Result {inputFile} is {string.Join(',', result)} in {(DateTime.Now - start).TotalSeconds} seconds");
    }

    private static long CanMatch(string pattern, string[] towelPatterns, Dictionary<string, long> cache)
    {
        long count = 0;
        if (cache.ContainsKey(pattern))
        {
            return cache[pattern];
        }

        foreach (var tp in towelPatterns)
        {
            if (pattern.StartsWith(tp))
            {
                if (pattern.Length == tp.Length)
                {
                    count++;
                }
                else
                {
                    count += CanMatch(pattern[tp.Length..], towelPatterns, cache);
                }
            }
        }

        cache[pattern] = count;
        return count;
    }

    static void RunPart2(string inputFile)
    {
        var start = DateTime.Now;

        long result = 0;
        var lines = System.IO.File.ReadAllLines(inputFile);

        var towelPatterns = lines[0].Split(",", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var requiredPatterns = lines[2..];

        foreach (var pattern in requiredPatterns)
        {
            Dictionary<string, long> cache = new();
            result += CanMatch(pattern, towelPatterns, cache);
        }

        System.Console.WriteLine(
            $"Result {inputFile} is {string.Join(',', result)} in {(DateTime.Now - start).TotalSeconds} seconds");
    }
}