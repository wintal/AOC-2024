namespace Day4;

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

    static int Xmases(List<char[]> input, int x, int y, char[] remainingLetters, int xInx, int yInc)
    {
        if (y < 0 || y >= input.Count) return 0;
        if (x < 0 || x >= input[y].Length) return 0;
        if (input[y][x] == remainingLetters[0])
        {
            if (remainingLetters.Length == 1)
            {
                return 1;
            }

            var letters = remainingLetters[1..];
            return Xmases(input, x + xInx, y + yInc, letters, xInx, yInc);
        }

        return 0;
    }

    static void RunPart1(string inputFile)
    {
        int result = 0;

        var lines = System.IO.File.ReadAllLines(inputFile).Select(line => line.ToArray()).ToList();
        for (var y = 0; y < lines.Count; y++)
        {
            for (var x = 0; x < lines[0].Length; x++)
            {
                var letters = "XMAS".ToArray();
                result += Xmases(lines, x, y, letters, -1, -1)
                          + Xmases(lines, x, y, letters, -1, 0)
                          + Xmases(lines, x, y, letters, -1, +1)
                          + Xmases(lines, x, y, letters, 0, -1)
                          + Xmases(lines, x, y, letters, 0, +1)
                          + Xmases(lines, x, y, letters, 1, -1)
                          + Xmases(lines, x, y, letters, 1, 0)
                          + Xmases(lines, x, y, letters, 1, 1);
            }
        }

        System.Console.WriteLine($"Result {inputFile} is {result}");
    }

    static void RunPart2(string inputFile)
    {
        int result = 0;

        var lines = System.IO.File.ReadAllLines(inputFile).Select(line => line.ToArray()).ToList();
        for (var y = 1; y < lines.Count - 1; y++)
        {
            for (var x = 1; x < lines[0].Length - 1; x++)
            {
                if (lines[y][x] == 'A' && lines[y - 1][x - 1] == 'M' && lines[y + 1][x - 1] == 'M' &&
                    lines[y - 1][x + 1] == 'S' && lines[y + 1][x + 1] == 'S')
                {
                    result++;
                }

                if (lines[y][x] == 'A' && lines[y - 1][x - 1] == 'S' && lines[y + 1][x - 1] == 'S' &&
                    lines[y - 1][x + 1] == 'M' && lines[y + 1][x + 1] == 'M')
                {
                    result++;
                }

                if (lines[y][x] == 'A' && lines[y - 1][x - 1] == 'S' && lines[y + 1][x - 1] == 'M' &&
                    lines[y - 1][x + 1] == 'S' && lines[y + 1][x + 1] == 'M')
                {
                    result++;
                }

                if (lines[y][x] == 'A' && lines[y - 1][x - 1] == 'M' && lines[y + 1][x - 1] == 'S' &&
                    lines[y - 1][x + 1] == 'M' && lines[y + 1][x + 1] == 'S')
                {
                    result++;
                }
            }
        }

        System.Console.WriteLine($"Result {inputFile} is {result}");
    }
}