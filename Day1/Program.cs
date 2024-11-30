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
        int result = 0;
        int max = 0;
        
        var lines = System.IO.File.ReadAllLines(inputFile);
        if (!lines.Any())
        {
            return;
        }

        int current = 0;

        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                max = Math.Max(current, max);
                current = 0;
                continue;
            }
            current += Int32.Parse(line);
            
        }
        max = Math.Max(max, current);
        
        System.Console.WriteLine($"Result {inputFile} is {max}");
    }

    static void RunPart2(string inputFile)
    {
        int result = 0;
        int max = 0;

        List<int> allValues = new List<int>();
        var lines = System.IO.File.ReadAllLines(inputFile);
        if (!lines.Any())
        {
            return;
        }

        int current = 0;

        foreach (var line in lines)
        {
            if (string.IsNullOrEmpty(line))
            {
                allValues.Add(current);
                current = 0;
                continue;
            }
            current += Int32.Parse(line);
            
        }
        allValues.Add(current);
        allValues.Sort();
        var length = allValues.Count;
        var total = allValues[length - 1] + allValues[length -2 ] + allValues[length - 3];
        
        System.Console.WriteLine($"Result {inputFile} is {total}");
    }
}