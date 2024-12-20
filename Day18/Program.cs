using System.Collections.Immutable;
using Utilities;

namespace Day18;


class Program
{
    private const string Sample = "sample.txt";
    private const string Input = "input.txt";
    static void Main(string[] args)
    {
        //RunPart1(Sample);
        RunPart1(Input);
        //RunPart2(Sample);
        RunPart2(Input);
    }

    static void RunPart1(string inputFile)
    {
        var start = DateTime.Now;

        int result = 0;
        var lines = System.IO.File.ReadAllLines(inputFile);

        int width = 71;
        int height = 71;
        int steps = 1024;
        Vector startP = new Vector(0, 0);
        Vector end = new Vector(width-1, height-1);

        var map = Map.Create(width, height);
        List<Vector> vectors = new List<Vector>();

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                map.Rows[y][x] = '.';
            }
        }
        
        foreach (var line in lines)
        {
            var operations = line.Split(",", StringSplitOptions.RemoveEmptyEntries);
            vectors.Add(new Vector(int.Parse(operations[0]), int.Parse(operations[1])));
        }

        for (int i = 0; i < 1024; i++)
        {
            map[vectors[i]]= '#';
        }

        map.Print();
        var results = FindCheapestPath(startP, end, map);
        result = results.Item1;
        System.Console.WriteLine(
            $"Result {inputFile} is {string.Join(',', result)} in {(DateTime.Now - start).TotalSeconds} seconds");
    }

    private static (int, int) FindCheapestPath(Vector start, Vector end, Map map)
    {
        var queue = new PriorityQueue<(Vector pos, int score ), int>();
        queue.Enqueue((start, 0), 0);

        var lowestScore = int.MaxValue;
        var bestPaths = new List<IEnumerable<Vector>>();
        var baseScores = new Dictionary<Vector, int>();

        HashSet<Vector> visited = new HashSet<Vector>();
        void Enqueue(Vector position, Vector direction, int score)
        {
            visited.Add(position);
            var currentScore = baseScores.GetValueOrDefault(position, int.MaxValue);

            if (currentScore >= score)
            {
                baseScores[position] = score;
                queue.Enqueue((position, score), score);
            }
        }

        while (queue.Count != 0)
        {
            var state = queue.Dequeue();
            if (state.score > lowestScore)
            {
                continue;
            }

            if (state.pos == end)
            {
                if (state.score < lowestScore)
                {
                    lowestScore = state.score;
                }

                continue;
            }

            foreach (var dir in new[] { Vector.Left, Vector.Right, Vector.Up, Vector.Down })
            {
                var candidate = state.pos + dir;
                if (map.Contains(candidate) && map[candidate] != '#' && !visited.Contains(candidate))
                {
                    Enqueue(candidate, Vector.Right, state.score + 1);
                }
            }
        }  
        foreach (var path in bestPaths)
        {
            foreach (var pos in path)
            {
                visited.Add(pos);
            }
        }

        return (lowestScore, visited.Count);

    }

    static void RunPart2(string inputFile)
    {
        var start = DateTime.Now;

        int result = 0;
        var lines = System.IO.File.ReadAllLines(inputFile);

        int width = 71;
        int height = 71;
        int steps = 1024;
        Vector startP = new Vector(0, 0);
        Vector end = new Vector(width-1, height-1);
        List<Vector> vectors = new List<Vector>();
        foreach (var line in lines)
        {
            var operations = line.Split(",", StringSplitOptions.RemoveEmptyEntries);
            vectors.Add(new Vector(int.Parse(operations[0]), int.Parse(operations[1])));
        }

        int min = 0;
        int max = vectors.Count ;
        
        while (min < max - 1)
        {
            int j = (max - min) / 2 + min;
            var map = Map.Create(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    map.Rows[y][x] = '.';
                }
            }
            for (int i = 0; i < j; i++)
            {
                map[vectors[i]] = '#';
            }
            var results = FindCheapestPath(startP, end, map);
            result = results.Item1;
            if (result == int.MaxValue)
            {
                max = j;
            }
            else
            {
                min = j;
            }
        }

        
        System.Console.WriteLine(
            $"Result {inputFile} is {vectors[min].X},{vectors[min].Y} in {(DateTime.Now - start).TotalSeconds} seconds");
    }
}