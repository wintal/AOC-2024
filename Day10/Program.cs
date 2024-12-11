using Utilities;

namespace Day10;

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
        var start = DateTime.Now;
        long result = 0;

        var lines = System.IO.File.ReadAllLines(inputFile);

        var map = Map.LoadFromLines(lines);


        var trailheads = new List<Vector>();
        for (int y = 0; y < map.Rows.Length; y++)
        {
            for (int x = 0; x < map.Rows[y].Length; x++)
            {
                if (map.Rows[y][x] == '0')
                {
                    trailheads.Add(new Vector(x, y));
                }
            }
        }

        foreach (var trailhead in trailheads)
        {
            var visited = new HashSet<Vector>();
            var nines = new HashSet<Vector>();
            CountNines(map, trailhead, visited, nines);
            result += nines.Count;
        }

        System.Console.WriteLine($"Result {inputFile} is {result} in {(DateTime.Now - start).TotalSeconds} seconds");
    }

    private static void CountNines(Map map, Vector trailhead, HashSet<Vector> visited, HashSet<Vector> nines)
    {
        if (!map.Contains(trailhead))
        {
            return;
        }

        if (visited.Contains(trailhead))
        {
            return;
        }

        if (map[trailhead] == '9')
        {
            nines.Add(trailhead);
        }

        visited.Add(trailhead);

        int current = map[trailhead] - '0';

        foreach (var dir in new[]{Vector.Up, Vector.Right, Vector.Down, Vector.Left})
        {
            var above = trailhead + dir;
            if (map.Contains(above))
            {
                var next = map[above] - '0';
                if ((next - current) == 1)
                {
                    CountNines(map, above, visited, nines);
                }
            }
        }
    }

    static void RunPart2(string inputFile)
    {
        var start = DateTime.Now;
        long result = 0;

        var lines = System.IO.File.ReadAllLines(inputFile);

        var map = Map.LoadFromLines(lines);


        var trailheads = new List<Vector>();
        for (int y = 0; y < map.Rows.Length; y++)
        {
            for (int x = 0; x < map.Rows[y].Length; x++)
            {
                if (map.Rows[y][x] == '0')
                {
                    trailheads.Add(new Vector(x, y));
                }
            }
        }

        foreach (var trailhead in trailheads)
        {
            var nines = new List<Vector>();
            CountNineVariants(map, trailhead, nines);
            result += nines.Count;
        }

        System.Console.WriteLine($"Result {inputFile} is {result} in {(DateTime.Now - start).TotalSeconds} seconds");
    }

    private static readonly Vector[] Directions = new[] { Vector.Up, Vector.Right, Vector.Down, Vector.Left };
    private static void CountNineVariants(Map map, Vector trailhead, List<Vector> nines)
    {
        if (map[trailhead] == '9')
        {
            nines.Add(trailhead);
            return;
        }

        int current = map[trailhead];

        foreach (var dir in Directions)
        {
            var above = trailhead + dir;
            if (map.Contains(above))
            {
                var next = map[above];
                if ((next - current) == 1)
                {
                    CountNineVariants(map, above, nines);
                }
            }
        }
    }
}