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
        
        var lines = System.IO.File.ReadAllLines(inputFile);
        if (!lines.Any())
        {
            return;
        }

        int current = 0;

        List<int> a = new();
        List<int> b = new();
        foreach (var line in lines)
        {
            var parts = line.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
           a.Add(int.Parse(parts[0]));
           b.Add(int.Parse(parts[1]));
            
        }

        a.Sort();
        b.Sort();
        int diff = 0;
        for (int i = 0; i < a.Count; i++)
        {
            diff += Math.Abs(a[i] - b[i]);
        }
            
        System.Console.WriteLine($"Result {inputFile} is {diff}");
    }

    static void RunPart2(string inputFile)
    {
        int result = 0;
        
        var lines = System.IO.File.ReadAllLines(inputFile);
        if (!lines.Any())
        {
            return;
        }

        int current = 0;

        List<int> a = new();
        List<int> b = new();
        foreach (var line in lines)
        {
            var parts = line.Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            a.Add(int.Parse(parts[0]));
            b.Add(int.Parse(parts[1]));
            
        }

        a.Sort();
        b.Sort();
        int diff = 0;
        for (int i = 0; i < a.Count; i++)
        {
            diff += a[i] * b.Where(elem => elem == a[i]).Count();
        }
            
        System.Console.WriteLine($"Result {inputFile} is {diff}");
    }
}