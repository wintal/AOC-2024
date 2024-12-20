using System.Collections.Immutable;
using Utilities;

namespace Day20;

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
        var start = DateTime.Now;

        long result = 0;
        var lines = System.IO.File.ReadAllLines(inputFile);


        var map = Map.LoadFromLines(lines);


        var startP = map.FindEntry('S');
        var endP = map.FindEntry('E');

        var results = FindCheapestPath(startP, endP, map);

        var path = results.PathCosts.ToList();
        path.Sort((a, b) => a.Value.CompareTo(b.Value));
        var newPath = path.Select(a => a.Key).ToList();

        var newResults = FindCheapestPathWithCheat(newPath, results.Item2, 2, false, 100);

        System.Console.WriteLine(
            $"Result {inputFile} is {string.Join(',', newResults)} in {(DateTime.Now - start).TotalSeconds} seconds");
    }


    private static int FindCheapestPathWithCheat(List<Vector> path, Dictionary<Vector, int> scores, int cheatLength, bool allowInt, int threshold)
    {
        HashSet<(Vector, int)> allPossibilities = new();
        HashSet<(Vector, int)> visited = new();
        HashSet<(Vector, Vector, int)> savings = new();
        GetAllPossibilities(new Vector(0, 0), cheatLength, allPossibilities, visited);

        Dictionary<Vector, int> cheapest = new();
        foreach (var poss in allPossibilities)
        {
            if (!cheapest.ContainsKey(poss.Item1))
            {
                cheapest[poss.Item1] = poss.Item2;
            }
            else
            {
                cheapest[poss.Item1] = Math.Max(poss.Item2, cheapest[poss.Item1]);
            }
        }
        allPossibilities = cheapest.Select(a => (a.Key, a.Value)).ToHashSet();

        if (!allowInt)
        {
            allPossibilities = allPossibilities.Where(a => a.Item2 == 0).ToHashSet();
        }
        HashSet<(Vector, Vector)> results = new HashSet<(Vector, Vector)>();
        foreach (var point in path)
        {
            var thisScore = scores[point];

            foreach (var firstDir in allPossibilities)
            {
                var position = point + firstDir.Item1;
                if (scores.TryGetValue(position, out var newScore))
                {
                    if ((newScore -thisScore ) >= (threshold + (cheatLength-firstDir.Item2)))
                    {
                        results.Add((point, position));
                        savings.Add((point,position, newScore - thisScore - cheatLength+firstDir.Item2));
                    }
                }
            }
        }

        return results.Count;
    }

    private static void GetAllPossibilities(Vector vector, int i, HashSet<(Vector, int)> allPossibilities, HashSet<(Vector, int)> visited)
    {
     
        if (i == 0)
        {
            return;
        }

        foreach (var firstDir in new[] { Vector.Left, Vector.Right, Vector.Up, Vector.Down })
        {  
            var pos = vector + firstDir;
            if (!visited.Contains((pos,i-1)))
            {
                allPossibilities.Add((pos, i-1));
                visited.Add((pos, i-1));
                GetAllPossibilities(pos, i - 1, allPossibilities,visited);
            }
        }
    }

    private static (int PathLength, Dictionary<Vector, int> PathCosts) FindCheapestPath(Vector start, Vector end, Map map)
    {
        var queue = new PriorityQueue<(Vector pos, int score ), int>();
        queue.Enqueue((start, 0), 0);

        var lowestScore = int.MaxValue;
        var bestPaths = new List<IEnumerable<Vector>>();
        var baseScores = new Dictionary<Vector, int>();
        baseScores[start] = 0;
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

        return (lowestScore, baseScores);
    }

    static void RunPart2(string inputFile)
    {
        var start = DateTime.Now;

        long result = 0;
        var lines = System.IO.File.ReadAllLines(inputFile);
        var map = Map.LoadFromLines(lines);
        var startP = map.FindEntry('S');
        var endP = map.FindEntry('E');

        var results = FindCheapestPath(startP, endP, map);

        var path = results.PathCosts.ToList();
        path.Sort((a, b) => a.Value.CompareTo(b.Value));
        var newPath = path.Select(a => a.Key).ToList();

        var newResults = FindCheapestPathWithCheat(newPath, results.Item2, 20, true, 100);

        System.Console.WriteLine(
            $"Result {inputFile} is {string.Join(',', newResults)} in {(DateTime.Now - start).TotalSeconds} seconds");
    }
}