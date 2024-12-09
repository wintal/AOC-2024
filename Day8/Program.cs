
using Utilities;

namespace Day8;

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
        long result = 0;

        var lines = System.IO.File.ReadAllLines(inputFile);

        var map = Map.LoadFromLines(lines);

        Dictionary<char, List<Vector>> locations = new Dictionary<char, List<Vector>>();
        map.Print();
        for (int y = 0; y < map.MaxY; y++)
        {
            for (int x = 0; x < map.MaxX; x++)
            {
                if (map.Rows[y][x] != '.' && map.Rows[y][x] != '#')
                {
                    if (!locations.TryGetValue(map.Rows[y][x], out var list))
                    {
                        list = new List<Vector>();
                        locations[map.Rows[y][x]] =  list;
                    }
                    list.Add(new Vector(x, y));
                }
            }
        }

        HashSet<Vector> nodes = new HashSet<Vector>();
        foreach (var location in locations)
        {
            var b = GetNodes(map, location.Value);
            foreach (var x in b)
            {
                nodes.Add(x);
                map[x] = '#';
            }
        }
        map.Print();
        result = nodes.Count;
        
        System.Console.WriteLine($"Result {inputFile} is {result}");
    }

    private static HashSet<Vector>  GetNodes(Map map, List<Vector> locationValue)
    {
        HashSet< Vector> nodes = new ();
        for (int i = 0; i < locationValue.Count; i++)
        {
            for (int j = i + 1; j < locationValue.Count; j++)
            {
                var node1 = locationValue[i];
                var node2 = locationValue[j];
                var diff = node1 - node2;
                var location1 = node1 + diff;
                var location2 = node2 - diff;
                if (map.Contains(location1))
                {
                    nodes.Add(location1);
                }
                if (map.Contains(location2))
                {
                    nodes.Add(location2);
                }
            }
        }
        return nodes;
    }

    static void RunPart2(string inputFile)
    {
        long result = 0;

        var lines = System.IO.File.ReadAllLines(inputFile);

        var map = Map.LoadFromLines(lines);

        Dictionary<char, List<Vector>> locations = new Dictionary<char, List<Vector>>();

        for (int y = 0; y < map.MaxY; y++)
        {
            for (int x = 0; x < map.MaxX; x++)
            {
                if (map.Rows[y][x] != '.' && map.Rows[y][x] != '#')
                {
                    if (!locations.TryGetValue(map.Rows[y][x], out var list))
                    {
                        list = new List<Vector>();
                        locations[map.Rows[y][x]] =  list;
                    }
                    list.Add(new Vector(x, y));
                }
            }
        }

        HashSet<Vector> nodes = new HashSet<Vector>();
        foreach (var location in locations)
        {
            var b = GetNodes2(map, location.Value);
            foreach (var x in b)
            {
                nodes.Add(x);
                map[x] = '#';
            }
        }
        map.Print();
        result = nodes.Count;
        
        System.Console.WriteLine($"Result {inputFile} is {result}");
    }
    private static List<Vector>  GetNodes2(Map map, List<Vector> locationValue)
    {
        List<Vector> nodes = new ();
        for (int i = 0; i < locationValue.Count; i++)
        {
            for (int j = i + 1; j < locationValue.Count; j++)
            {
                var node1 = locationValue[i];
                var node2 = locationValue[j];
                var diff = node1 - node2;
                
                var location1 = node1 + diff;
                
                var location2 = node2 - diff;
                while (map.Contains(location1))
                {
                    nodes.Add(location1);
                    location1 = location1 + diff;
                }
                while (map.Contains(location2))
                {
                    nodes.Add(location2);
                    location2 = location2 - diff;
                }

              
            }
        }

        if (locationValue.Count > 1)
        {
            nodes.AddRange(locationValue);
        }
        return nodes;
    }
}