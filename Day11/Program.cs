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

        var map = Map.LoadFromLines(lines);

        var stones = new List<long>(lines[0]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(long.Parse));


        for (int i = 0; i < 25; i++)
        {
            Blink(stones);
        }

        result = stones.Count;
        System.Console.WriteLine($"Result {inputFile} is {result} in {(DateTime.Now - start).TotalSeconds} seconds");
    }

    private static void Blink(List<long> stones)
    {
        var count = stones.Count;
        for (int i = 0; i < count; i++)
        {
            var numdigits = MathUtils.NumDigits(stones[i]);
            if (stones[i] == 0)
            {
                stones[i] = 1;
            }
            else if (numdigits % 2 == 0)
            {
                long right = 0;
                long left = stones[i];
                int multiplier = 1;
                for (int j = 0; j < numdigits / 2; j++)
                {
                    right = right + left % 10 * multiplier;
                    multiplier *= 10;
                    left = left / 10;
                }

                stones[i] = right;
                stones.Add(left);
            }
            else
            {
                stones[i] *= 2024;
            }
        }
    }


    static void RunPart2(string inputFile)
    {
        long result = 0;
        var lines = System.IO.File.ReadAllLines(inputFile);

        var start = DateTime.Now;
        var stones = new List<long>(lines[0]
            .Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries).Select(long.Parse));

        Dictionary<(long, int), long> cache = new Dictionary<(long, int), long>();
        foreach (var entry in stones)
        {
            result += BlinkCached(entry, 75, cache);
        }

        System.Console.WriteLine($"Result {inputFile} is {result} in {(DateTime.Now - start).TotalSeconds} seconds");
    }

    private static long BlinkCached(long thisValue, int depthLeft, Dictionary<(long, int), long> cache)
    {
        if (depthLeft == 0) return 1;

        if (cache.TryGetValue((thisValue, depthLeft), out long value))
        {
            return value;
        }

        var numdigits = MathUtils.NumDigits(thisValue);

        if (thisValue == 0)
        {
            long downStream = BlinkCached(1, depthLeft - 1, cache);
            cache[(thisValue, depthLeft)] = downStream;
            return downStream;
        }
        else if (numdigits % 2 == 0)
        {
            var (left, right) = MathUtils.SplitNumber(thisValue, numdigits);
            long downStreamLeft = BlinkCached(left, depthLeft - 1, cache);

            long downStreamRight = BlinkCached(right, depthLeft - 1, cache);
            cache[(thisValue, depthLeft)] = downStreamLeft + downStreamRight;
            return downStreamLeft + downStreamRight;
        }
        else
        {
            long downStream = BlinkCached(thisValue * 2024, depthLeft - 1, cache);
            cache[(thisValue, depthLeft)] = downStream;
            return downStream;
        }
    }
}