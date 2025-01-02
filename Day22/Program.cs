using Utilities;

namespace Day1;

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

        int count = 2000;
        foreach (var line in lines)
        {
            long num = long.Parse(line);
            for (int i = 0; i < count; i++)
            {
                num = Evolve(num);
            }

            result += num;
        }


        System.Console.WriteLine(
            $"Result {inputFile} is {string.Join(',', result)} in {(DateTime.Now - start).TotalSeconds} seconds");
    }

    private static long Evolve(long num)
    {
        long newNum = num * 64;
        long mixed = Mix(num, newNum);
        long prune = Prune(mixed);
        long divided = prune / 32;
        mixed = Mix(divided, prune);
        prune = Prune(mixed);
        newNum = prune * 2048;

        mixed = Mix(newNum, prune);
        prune = Prune(mixed);
        return prune;
    }

    public static long Mix(long a, long b)
    {
        return a ^ b;
    }

    public static long Prune(long a)
    {
        return a % 16777216;
    }

    static void RunPart2(string inputFile)
    {
        var start = DateTime.Now;

        long result = 0;
        var lines = System.IO.File.ReadAllLines(inputFile);

        int count = 2000;
        List<List<int>> sequences = new List<List<int>>();
        List<List<int>> mindigitsSequences = new List<List<int>>();

        foreach (var line in lines)
        {
            List<int> sequence = new List<int>();
            List<int> minDigits = new List<int>();
            long num = long.Parse(line);
            int lastMin = (int)num % 10;
            for (int i = 0; i < count; i++)
            {
                num = Evolve(num);
                int min = (int)num % 10;
                sequence.Add(min - lastMin);
                minDigits.Add(min);
                lastMin = min;
            }

            sequences.Add(sequence);
            mindigitsSequences.Add(minDigits);
        }

        var scores = new Dictionary<(int, int, int, int), int>();
        for (int s = 0; s < sequences.Count; s++)
        {
            var sequence = sequences[s];
            var digits = mindigitsSequences[s];
            var seen = new HashSet<(int, int, int, int)>();
            for (int i = 3; i < sequence.Count; i++)
            {
                if (!seen.Contains((sequence[i - 3], sequence[i - 2], sequence[i - 1], sequence[i - 0])))
                {
                    var val = scores.GetValueOrDefault((sequence[i - 3], sequence[i - 2], sequence[i - 1], sequence[i]),
                        0);
                    val += digits[i];
                    scores[(sequence[i - 3], sequence[i - 2], sequence[i - 1], sequence[i])] = val;
                    seen.Add((sequence[i - 3], sequence[i - 2], sequence[i - 1], sequence[i]));
                }
            }
        }

        List<((int, int, int, int), int)> sortedKeys = new();

        sortedKeys.AddRange(scores.ToList().Select(kvp => (kvp.Key, kvp.Value)));

        sortedKeys.Sort((a, b) => b.Item2.CompareTo(a.Item2));


        long bestScore = sortedKeys.First().Item2;


        System.Console.WriteLine(
            $"Result {inputFile} is {string.Join(',', bestScore)} in {(DateTime.Now - start).TotalSeconds} seconds");
    }
}