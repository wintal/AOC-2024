using System.Collections.Immutable;
using System.Transactions;
using Utilities;

namespace Day16;

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

        var startPos = map.Find('S', new HashSet<Vector>());

        var actResult = FindCheapestPath(startPos.Value, map);
        System.Console.WriteLine(
            $"Result {inputFile} is {actResult.Item1} in {(DateTime.Now - start).TotalSeconds} seconds");
    }

    private static (int, int) FindCheapestPath(Vector start, Map map)
    {
        var queue = new Queue<(Vector Position, Vector Direction, int Score, ImmutableStack<Vector> Path)>();
        queue.Enqueue((start, Vector.Right, 0, ImmutableStack.Create<Vector>(start)));

        var lowestScore = int.MaxValue;
        var bestPaths = new List<IEnumerable<Vector>>();
        var baseScores = new Dictionary<(Vector pos, Vector dir), int>();
        void Enqueue(Vector position, Vector direction, int score, ImmutableStack<Vector> path)
        {
            var currentScore = baseScores.GetValueOrDefault((position, direction), int.MaxValue);

            if (currentScore >= score)
            {
                baseScores[(position, direction)] = score;
                queue.Enqueue((position, direction, score, path));
            }
        }
        while (queue.Count != 0)
        {
            var state = queue.Dequeue();
            if (state.Score > lowestScore)
            {
                continue;
            }

            if (map[state.Position] == 'E')
            {
                if (state.Score == lowestScore)
                {
                    bestPaths.Add(state.Path);
                }
                else if (state.Score < lowestScore)
                {
                    bestPaths = new List<IEnumerable<Vector>>() { state.Path };
                    lowestScore = state.Score;
                }

                continue;
            }

            if (map[state.Position + state.Direction] != '#')
            {
                Enqueue(state.Position + state.Direction, state.Direction, state.Score + 1,
                    state.Path.Push(state.Position + state.Direction));
            }

            Enqueue(state.Position, state.Direction.RotateLeft90(), state.Score + 1000, state.Path);
            Enqueue(state.Position, state.Direction.RotateRight90(), state.Score + 1000, state.Path);
        }

        HashSet<Vector> visited = new();
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
        long result = 0;

        var lines = System.IO.File.ReadAllLines(inputFile);


        var map = Map.LoadFromLines(lines);

        var startPos = map.Find('S', new HashSet<Vector>());
        var endPos = map.Find('E', new HashSet<Vector>());

        var actResult = FindCheapestPath(startPos.Value, map);
        System.Console.WriteLine(
            $"Result {inputFile} is {actResult.Item2} in {(DateTime.Now - start).TotalSeconds} seconds");
    }
}