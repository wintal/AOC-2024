using Utilities;

namespace Day1;

class Program
{
    private const string Sample = "sample.txt";
    private const string Input = "input.txt";

    static void Main(string[] args)
    {
        Console.WriteLine("Part 1");
        RunPart1(Sample);
        RunPart1(Input);
        Console.WriteLine("Part 2");
        RunPart2(Sample);
        RunPart2(Input);
    }

    static void RunPart1(string inputFile)
    {
        long result = 0;

        var start = DateTime.Now;
        var lines = System.IO.File.ReadAllLines(inputFile).First().ToCharArray();

        List<int> array = new List<int>();

        int fileIndex = 0;
        bool isFile = true;
        foreach (var character in lines)
        {
            var number = int.Parse(character.ToString());
            for (int i = 0; i < number; i++)
            {
                if (isFile)
                {
                    array.Add(fileIndex);
                }
                else
                {
                    array.Add(-1);
                }
            }

            if (isFile) fileIndex++;
            isFile = !isFile;
        }

        int startIndex = 0;
        int endIndex = array.Count - 1;

        while (endIndex > startIndex)
        {
            while (array[endIndex] == -1)
            {
                endIndex--;
            }

            while (array[startIndex] != -1)
            {
                startIndex++;
            }

            if (startIndex < endIndex)
            {
                array[startIndex] = array[endIndex];
                array[endIndex] = -1;
            }

            startIndex++;
            endIndex--;
        }

        startIndex = 0;
        result = 0;
        for (int i = 0; i < array.Count; i++)
        {
            if (array[i] != -1)
            {
                result = result + (long)startIndex * (long)array[startIndex];
                startIndex++;
            }
        }

        System.Console.WriteLine($"Result {inputFile} is {result} in {(DateTime.Now - start).TotalSeconds} seconds");
    }

    static void RunPart2(string inputFile)
    {
        long result = 0;
        var start = DateTime.Now;

        var lines = System.IO.File.ReadAllLines(inputFile).First().ToCharArray();

        List<(int numberBlocks, int fileId)> blocks = new();

        int fileIndex = 0;
        bool isFile = true;
        foreach (var character in lines)
        {
            var number = int.Parse(character.ToString());
            if (isFile)
            {
                blocks.Add((number, fileIndex));
            }
            else
            {
                blocks.Add((number, -1));
            }

            if (isFile) fileIndex++;
            isFile = !isFile;
        }

        HashSet<int> process = new();
        for (int i = blocks.Count - 1; i >= 0; i--)
        {
            if (blocks[i].fileId == -1 || process.Contains(blocks[i].fileId))
            {
                continue;
            }

            process.Add(blocks[i].fileId);
            var blockToMove = blocks[i];
            for (int j = 0; j < blocks.Count; j++)
            {
                if (j > i) continue;
                if (blocks[j].fileId != -1)
                {
                    continue;
                }

                if (blocks[j].numberBlocks >= blocks[i].numberBlocks)
                {
                    var jcontent = blocks[j];
                    int diff = jcontent.numberBlocks - blockToMove.numberBlocks;
                    blocks[j] = (blockToMove.numberBlocks, blockToMove.fileId);
                    blocks[i] = (blockToMove.numberBlocks, -1);
                    if (diff > 0)
                    {
                        blocks.Insert(j + 1, (diff, -1));
                    }

                    break;
                }
            }
        }

        int index = 0;
        for (int i = 0; i < blocks.Count; i++)
        {
            for (int j = 0; j < blocks[i].numberBlocks; j++)
            {
                if (blocks[i].fileId != -1)
                {
                    result += index * blocks[i].fileId;
                }

                index++;
            }
        }

        System.Console.WriteLine($"Result {inputFile} is {result} in {(DateTime.Now - start).TotalSeconds} seconds");
    }
}