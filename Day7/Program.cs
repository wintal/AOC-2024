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
        int result = 0;

        var lines = System.IO.File.ReadAllLines(inputFile);
  
        Map map = Map.LoadFromLines(lines);
        Vector start = map.FindEntry('^');
        map[start] = '.';

        System.Console.WriteLine($"Result {inputFile} is {result}");
    }

    static void RunPart2(string inputFile)
    {
        int result = 0;
        
        var lines = System.IO.File.ReadAllLines(inputFile);
        result = lines.Length;
        System.Console.WriteLine($"Result {inputFile} is {result}");
    }
}