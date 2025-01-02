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

        List<int[]> pins = new();
        List<int[]> tumblers = new();

        int lineIndex = 0;
        while (lineIndex < lines.Length)
        {

            bool isPin = true;
            if (lines[lineIndex].StartsWith("....."))
            {
                isPin = false;
            }

            int[] thesePinds = new int[5];
            for (int i = lineIndex; i < lineIndex + 7; i++)
            {
                for (int index = 0; index < 5; index++)
                {
                    if (lines[i][index] == '#')
                    {
                        thesePinds[index]++;
                    }
                }
            }

            if (isPin)
            {
                pins.Add(thesePinds);
            }
            else
            {
                tumblers.Add(thesePinds);
                
            }
            lineIndex += 8;
        }

        foreach (var pin in pins)
        {
            foreach (var tumbler in tumblers)
            {
                bool fits = true;
                for (int i = 0; i < 5; i++)
                {
                    if (pin[i] + tumbler[i] > 7)
                    {
                        fits = false;
                    }
                }

                if (fits)
                {
                    result++;
                }
            }
        }
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