using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Day2;


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
        var lines = System.IO.File.ReadAllLines(inputFile);
        if (!lines.Any())
        {
            return;
        }

        List<int[]> scenarios = new();
        foreach (var line in lines)
        {
            var parts = line.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            scenarios.Add(parts.Select(part => int.Parse(part)).ToArray());
        }

        int safe = 0;
        foreach (var levels in scenarios)
        {
            bool? increasing = null;
            bool thisSafe = true;
            
            for (int i = 1; i < levels.Length; i++)
            {

                if (increasing == null)
                {
                    increasing = levels[i] > levels[i - 1];
                }

                int diff = Math.Abs(levels[i] - levels[i - 1]);
                if (!(diff > 0 && diff <= 3))
                {
                    
                    thisSafe = false;
                    break;
                }
                
                bool thisIncreasing = levels[i] > levels[i - 1];
                if (thisIncreasing != increasing.Value)
                {
                    thisSafe = false;
                    break;
                }

            }

            if (thisSafe)
            {
                safe++;
            }
        }
        System.Console.WriteLine($"Result {inputFile} is {safe}");
    }

    static void RunPart2(string inputFile)
    {
        var lines = System.IO.File.ReadAllLines(inputFile);
        if (!lines.Any())
        {
            return;
        }

        List<int[]> a = new();
        foreach (var line in lines)
        {
            var parts = line.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            a.Add(parts.Select(part => int.Parse(part)).ToArray());
        }

        int safe = 0;
        foreach (var origLevels in a)
        {
            for (int j = 0; j < origLevels.Length; j++)
            {
                bool? increasing = null;
                bool thisSafe = true;
                var levels = origLevels.ToList();
                levels.RemoveAt(j);
                for (int i = 1; i < levels.Count; i++)
                {

                    if (increasing == null)
                    {
                        increasing = levels[i] > levels[i - 1];
                    }

                    int diff = Math.Abs(levels[i] - levels[i - 1]);
                    if (!(diff > 0 && diff <= 3))
                    {

                        thisSafe = false;
                        break;
                    }

                    bool thisIncreasing = levels[i] > levels[i - 1];
                    if (thisIncreasing != increasing.Value)
                    {
                        thisSafe = false;
                        break;
                    }
                }

                if (thisSafe)
                {
                    safe++;
                    break;
                }
            }
        }
        System.Console.WriteLine($"Result {inputFile} is {safe}");
    }
}