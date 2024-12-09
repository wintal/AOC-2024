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
        List<(int, int)> rules = new();
        foreach (var line in lines)
        {
            var parts = line.Split("|", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2)
            {
                rules.Add((int.Parse(parts[0]), int.Parse(parts[1])));
                continue;
            }
            
            var parts2 = line.Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            if (parts2.Any())
            {
                bool good = true;
                foreach (var rule in rules)
                {
                    var firstIndex = parts2.IndexOf(rule.Item1);
                    var secondIndex = parts2.IndexOf(rule.Item2);
                    if (firstIndex != -1 && secondIndex != -1)
                    {
                        if (firstIndex > secondIndex)
                        {
                            good = false;
                        }
                    }

                }

                if (good)
                {
                    result += parts2[parts2.Count / 2];
                }
            }
        }

        System.Console.WriteLine($"Result {inputFile} is {result}");
    }

    static void RunPart2(string inputFile)
    {
        int result = 0;
        
        var lines = System.IO.File.ReadAllLines(inputFile);
        List<(int, int)> rules = new();
        foreach (var line in lines)
        {
            var parts = line.Split("|", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 2)
            {
                rules.Add((int.Parse(parts[0]), int.Parse(parts[1])));
                continue;
            }
            
            var parts2 = line.Split(",", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

            if (parts2.Any())
            {
                bool good = true;
                
                foreach (var rule in rules)
                {
                    var firstIndex = parts2.IndexOf(rule.Item1);
                    var secondIndex = parts2.IndexOf(rule.Item2);
                    if (firstIndex != -1 && secondIndex != -1)
                    {
                        if (firstIndex > secondIndex)
                        {
                            good = false;
                          
                        }
                    }

                }

                if (!good)
                {
                    parts2.Sort((i, i1) =>
                    {
                        foreach (var rule in rules)
                        {
                            if (rule.Item1 == i && rule.Item2 == i1)
                            {
                                return 1;
                            }
                            else if (rule.Item1 == i1 && rule.Item2 == i)
                            {
                                return -1;
                            }

                        }
                        return 0;
                    });
                    result += parts2[parts2.Count / 2];
                }
            }
        }

        System.Console.WriteLine($"Result {inputFile} is {result}");
    }
}