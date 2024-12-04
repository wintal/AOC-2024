using System.Text.RegularExpressions;

namespace Day3;

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

        int result = 0;

        foreach (var line in lines)
        {
            for (int i = 0; i < line.Length - 8; i++)
            {
                var match = Regex.Match(line[i..], "^mul\\((\\d{1,3}),(\\d{1,3})\\)");
                if (match.Success)
                {
                    int first = int.Parse(match.Groups[1].Value);
                    int second = int.Parse(match.Groups[2].Value);
                    result += first * second;
                }
            }
        }

        System.Console.WriteLine($"Result {inputFile} is {result}");
    }

    static void RunPart2(string inputFile)
    {
        var lines = System.IO.File.ReadAllLines(inputFile);
        if (!lines.Any())
        {
            return;
        }

        int result = 0;

        bool doIt = true;
        foreach (var line in lines)
        {
            for (int i = 0; i < line.Length - 7; i++)
            {
                if (line[i..].StartsWith("do()"))
                {
                    doIt = true;
                }
                else if (line[i..].StartsWith("don't()"))
                {
                    doIt = false;
                }
                else 
                {
                    var match = Regex.Match(line[i..], "^mul\\((\\d{1,3}),(\\d{1,3})\\)");
                    if (match.Success)
                    {
                        if (doIt)
                        {
                            int first = int.Parse(match.Groups[1].Value);
                            int second = int.Parse(match.Groups[2].Value);
                            result += first * second;
                        }
                    }
                }
            }
        }

        System.Console.WriteLine($"Result {inputFile} is {result}");
    }
}