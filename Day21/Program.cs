using Utilities;

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
        var start = DateTime.Now;

        long result = 0;
        var lines = System.IO.File.ReadAllLines(inputFile);

        var numPadTransitions = NumPadTransitions();
        var dirPadTransitions = DirPadTransitions();

        foreach (var line in lines)
        {
            var shortest = long.MaxValue;
            var numpadPaths = GetAllPaths('A' + line, numPadTransitions);
            foreach (var path in numpadPaths)
            {
                shortest = Math.Min(shortest, GetBestPathLength(path.ToArray(), dirPadTransitions, 3));
            }

            result += shortest * long.Parse(line[0..3]);
        }

        System.Console.WriteLine(
            $"Result {inputFile} is {string.Join(',', result)} in {(DateTime.Now - start).TotalSeconds} seconds");
    }

    static char MapDir(Vector dir)
    {
        if (dir == Vector.Up)
        {
            return '^';
        }

        if (dir == Vector.Down)
        {
            return 'v';
        }

        if (dir == Vector.Left)
        {
            return '<';
        }

        if (dir == Vector.Right)
        {
            return '>';
        }

        throw new Exception();
    }

    private static List<string> GetAllPaths(string line, Dictionary<(char, char), List<List<char>>> transitions)
    {
        List<List<char>> GetTrans(char a, char b)
        {
            if (a == b)
            {
                return new List<List<char>>() { new List<char>() { } };
            }

            return transitions[(a, b)];
        }

        List<string> returns = new List<string>() { string.Empty };
        for (int i = 1; i < line.Length; i++)
        {
            var newReturns = new List<string>();

            foreach (var transition in GetTrans(line[i - 1], line[i]))
            {
                var transString = new String(transition.ToArray());
                foreach (var retvalue in returns)
                {
                    newReturns.Add(retvalue + transString + 'A');
                }
            }

            returns = newReturns;
        }

        return returns;
    }

    private static Dictionary<(char, char), List<List<char>>> NumPadTransitions()
    {
        Dictionary<(char, char), List<List<char>>> transitions = new();
        Dictionary<char, Vector> locations = new Dictionary<char, Vector>()
        {
            { 'd', new Vector(0, 3) },
            { 'A', new Vector(2, 3) },
            {
                '0', new Vector(1, 3)
            },
            {
                '1', new Vector(0, 2)
            },
            {
                '2', new Vector(1, 2)
            },
            {
                '3', new Vector(2, 2)
            },
            {
                '4', new Vector(0, 1)
            },
            {
                '5', new Vector(1, 1)
            },
            {
                '6', new Vector(2, 1)
            },
            {
                '7', new Vector(0, 0)
            },
            {
                '8', new Vector(1, 0)
            },
            {
                '9', new Vector(2, 0)
            }
        };
        foreach (var pos1 in locations)
        {
            foreach (var pos2 in locations)
            {
                if (pos1.Key == pos2.Key || pos1.Key == 'd' || pos2.Key == 'd')
                {
                    continue;
                }

                var possibilities = GetPoss(locations['d'], pos1.Value, pos2.Value);
                transitions.Add((pos1.Key, pos2.Key), possibilities);
            }
        }

        return transitions;
    }


    private static Dictionary<(char, char), List<List<char>>> DirPadTransitions()
    {
        Dictionary<(char, char), List<List<char>>> transitions = new();
        Dictionary<char, Vector> locations = new Dictionary<char, Vector>()
        {
            { 'd', new Vector(0, 0) },
            { 'v', new Vector(1, 1) },
            { '^', new Vector(1, 0) },
            { '<', new Vector(0, 1) },
            { '>', new Vector(2, 1) },
            { 'A', new Vector(2, 0) },
        };
        foreach (var pos1 in locations)
        {
            foreach (var pos2 in locations)
            {
                if (pos1.Key == pos2.Key)
                {
                    continue;
                }

                var possibilities = GetPoss(locations['d'], pos1.Value, pos2.Value);
                transitions.Add((pos1.Key, pos2.Key), possibilities);
            }
        }

        foreach (var key in locations.Keys)
        {
            transitions[(key, key)] = new List<List<char>>()
            {
                new List<char>()
            };
        }

        return transitions;
    }

    private static List<List<char>> GetPoss(Vector badLocation, Vector pos1, Vector pos2)
    {
        List<List<char>> results = new();
        if (pos1 == pos2)
        {
            results.Add(new List<char>());
            return results;
        }

        if (pos1 == badLocation)
        {
            return results;
        }

        if (pos1.X > pos2.X)
        {
            var dir = Vector.Left;
            var possR = GetPoss(badLocation, pos1 + dir, pos2);

            foreach (var list in possR)
            {
                List<char> thisDir = new List<char>() { MapDir(dir) };
                thisDir.AddRange(list);
                results.Add(thisDir);
            }
        }
        else if (pos1.X < pos2.X)
        {
            var dir = Vector.Right;
            var possL = GetPoss(badLocation, pos1 + dir, pos2);
            foreach (var list in possL)
            {
                List<char> thisDir = new List<char>() { MapDir(dir) };
                thisDir.AddRange(list);
                results.Add(thisDir);
            }
        }

        if (pos1.Y > pos2.Y)
        {
            var dir = Vector.Up;
            var possU = GetPoss(badLocation, pos1 + dir, pos2);
            foreach (var list in possU)
            {
                List<char> thisDir = new List<char>() { MapDir(dir) };
                thisDir.AddRange(list);
                results.Add(thisDir);
            }
        }
        else if (pos1.Y < pos2.Y)
        {
            var dir = Vector.Down;
            var possD = GetPoss(badLocation, pos1 + dir, pos2);
            foreach (var list in possD)
            {
                List<char> thisDir = new List<char>() { MapDir(dir) };
                thisDir.AddRange(list);
                results.Add(thisDir);
            }
        }

        return results;
    }

    static void RunPart2(string inputFile)
    {
        var start = DateTime.Now;

        long result = 0;
        var lines = System.IO.File.ReadAllLines(inputFile);


        var numPadTransitions = NumPadTransitions();
        var dirPadTransitions = DirPadTransitions();

        foreach (var line in lines)
        {
            var shortest = long.MaxValue;
            List<string> finalPaths = new List<string>();
            var numpadPaths = GetAllPaths('A' + line, numPadTransitions);
            foreach (var path in numpadPaths)
            {
                shortest = Math.Min(shortest, GetBestPathLength(path.ToArray(), dirPadTransitions, 26));
            }

            result += shortest * long.Parse(line[0..3]);
        }

        System.Console.WriteLine(
            $"Result {inputFile} is {string.Join(',', result)} in {(DateTime.Now - start).TotalSeconds} seconds");
    }

    private static long GetBestPathLength(IList<char> values,
        Dictionary<(char, char), List<List<char>>> dirPadTransitions, int maxDepth)
    {
        var best = long.MaxValue;
        Dictionary<(char, char, int), long> bestValues = new();

        best = Math.Min(best, GetBestPathLengthRec(values, maxDepth, dirPadTransitions, bestValues));

        return best;
    }

    private static long GetBestPathLengthRec(IList<char> sequence, int depth,
        Dictionary<(char, char), List<List<char>>> dirPadTransitions,
        Dictionary<(char, char, int), long> bestValues)
    {
        if (depth == 0)
        {
            return 1;
        }

        long total = 0;

        char last = 'A';

        for (int i = 0; i < sequence.Count; i++)
        {
            char next = sequence[i];
            if (!bestValues.TryGetValue((last, next, depth), out var bestValue))
            {
                long bestTransition = long.MaxValue;
                var transitions = dirPadTransitions[(last, next)];
                foreach (var transition in transitions)
                {
                    List<char> path = new List<char>();
                    path.AddRange(transition);
                    path.Add('A');
                    bestTransition = Math.Min(bestTransition,
                        GetBestPathLengthRec(path, depth - 1, dirPadTransitions, bestValues));
                }

                bestValues[(last, next, depth)] = bestTransition;
                bestValue = bestTransition;
            }

            total += bestValue;
            last = next;
        }

        return total;
    }
}