using Utilities;

namespace Day6;

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
  
        Map map = Map.LoadFromLines(lines);
        Vector start = map.FindEntry('^');
        map[start] = '.';

        FindPath(map, start, new Vector(0, -1));
        foreach (var line in map.Rows)
        {
            result += line.Count(x => x == 'v');
        }

        System.Console.WriteLine($"Result {inputFile} is {result}");
    }

    private static void FindPath(Map map, Vector start, Vector direction)   
    {
        Vector location = start;
        do
        {
            if (!location.InsideBox(0, 0, map.MaxX, map.MaxY))
            {
                return;
                
            }
            map[location] = 'v';

            
            var newLocation = location + direction;

            if (!newLocation.InsideBox(0, 0, map.MaxX, map.MaxY))
            {
                return;
            }

            switch (map[newLocation])
            {
                case '.':
                case 'v':
                    location = newLocation;
                    break;
                default:
                {
                    direction = direction.RotateRight90();
                }
                    break;
            }
        } while (true);
    }

    
    static void RunPart2(string inputFile)
    {
        int result = 0;

    
        var lines = System.IO.File.ReadAllLines(inputFile);
  
        Map map = Map.LoadFromLines(lines);
        Vector start = map.FindEntry('^');
        map[start] = '.';

        FindPath(map, start, new Vector(0, -1));
        
        List<Vector> obstacles = new List<Vector>();
        for (int y = 0; y < map.MaxY; y++)
        {
            for (int x = 0; x < map.MaxX; x++)
            {
                if (map[new Vector(x,y)] == 'v') obstacles.Add(new Vector(x, y));
            }
            
        }
        foreach (var obstacle in obstacles)
        {
            var mapCopy = map.Clone();
            mapCopy[obstacle] = '#';
            if (FindLoops(mapCopy, start, new Vector(0, -1)))
            {
                result++;
            }
        }

        System.Console.WriteLine($"Result {inputFile} is {result}");
    }
    
    private static bool FindLoops(Map map, Vector start, Vector direction)
    {
        int loops = 0;
        Vector location = start;
        HashSet<(Vector location, Vector direction)> visited = new HashSet<(Vector location, Vector direction)>();
        do
        {
            if (!location.InsideBox(0, 0, map.MaxX, map.MaxY))
            {
                return false;
            }

            if (visited.Contains((location, direction)))
            {
                return true;
            }
            visited.Add((location, direction));
            map[location] = 'v';

            var newLocation = location + direction;


            if (!newLocation.InsideBox(0, 0, map.MaxX, map.MaxY))
            {
                return false;
            }

            switch (map[newLocation])
            {
                case '.':
                case 'v':
                    // visited this before, check if we can loop;
                    location = newLocation;
                    break;
                default:
                {
                    direction = direction.RotateRight90();
                }
                    break;
            }
        } while (true);
    }

  
}