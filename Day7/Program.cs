using Utilities;

namespace Day7;


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
        long result = 0;

        var lines = System.IO.File.ReadAllLines(inputFile);

        List<(long, long[])> equations = new();
        foreach (var line in lines)
        {
            var parts = line.Split(":", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.RemoveEmptyEntries);
            var parts2 = parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.RemoveEmptyEntries);
            equations.Add((long.Parse(parts[0]), parts2.Select(long.Parse).ToArray()));
        }

        foreach (var equation in equations)
        {
            if (FindSolutions(equation.Item1, equation.Item2[0], equation.Item2[1..])  > 0)
            {
                result += equation.Item1;
            }
        }
        System.Console.WriteLine($"Result {inputFile} is {result}");
    }


    private static int FindSolutions(long equationItem1, long current, long[] ints)
    {
        if (current > equationItem1)
        {
            return 0;
        }
        if (ints.Length == 0)
        {
            if (equationItem1 == current)
            {
                return 1;
            }

            return 0;
        }

        long concated = current;
        long val = ints[0];
        while (val > 0)
        {
            val /= 10;
            concated *= 10;
        }

        return FindSolutions(equationItem1, current + ints[0], ints[1..]) +
               FindSolutions(equationItem1, current * ints[0], ints[1..]);
    }
    
    private static int FindSolutionsConcat(long equationItem1, long current, long[] ints)
    {
        if (current > equationItem1)
        {
            return 0;
        }
        if (ints.Length == 0)
        {
            if (equationItem1 == current)
            {
                return 1;
            }

            return 0;
        }

        long concated = current;
        long val = ints[0];
        while (val > 0)
        {
            val /= 10;
            concated *= 10;
        }

        concated += ints[0];

        return FindSolutionsConcat(equationItem1, current + ints[0], ints[1..]) +
               FindSolutionsConcat(equationItem1, current * ints[0], ints[1..]) + 
               FindSolutionsConcat(equationItem1, concated, ints[1..]);
    }


    static void RunPart2(string inputFile)
    {
        long result = 0;

        var lines = System.IO.File.ReadAllLines(inputFile);

        List<(long, long[])> equations = new();
        foreach (var line in lines)
        {
            var parts = line.Split(":", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.RemoveEmptyEntries);
            var parts2 = parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.RemoveEmptyEntries);
            equations.Add((long.Parse(parts[0]), parts2.Select(long.Parse).ToArray()));
        }

        foreach (var equation in equations)
        {
            if (FindSolutionsConcat(equation.Item1, equation.Item2[0], equation.Item2[1..])  > 0)
            {
                result += equation.Item1;
            }
        }
        System.Console.WriteLine($"Result {inputFile} is {result}");
    }
}