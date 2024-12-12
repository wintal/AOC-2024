using Utilities;

namespace Day12;

class Program
{
    private const string Sample = "sample.txt";
    private const string Input = "input.txt";

    static void Main(string[] args)
    {
        /*RunPart1(Sample);
        RunPart1(Input);
        RunPart2(Sample);*/
        RunPart2(Input);
    }

    static void RunPart1(string inputFile)
    {
        var start = DateTime.Now;
        long result = 0;

        var lines = System.IO.File.ReadAllLines(inputFile);

        var map = Map.LoadFromLines(lines);

        HashSet<char> cropTypes = new HashSet<char>();
        foreach (var y in map.Rows)
        {
            foreach (var x in y)
            {
                cropTypes.Add(x);
            }
        }

        foreach (var crop in cropTypes)
        {
            HashSet<Vector> used = new HashSet<Vector>();
            foreach (var vector in map.FindNotUsed(crop, used))
            {
                HashSet<Vector> area = new HashSet<Vector>();
                area = FollowArea(map, vector, area, used);
                var perimeter = GetPerimeter(area);
                result += perimeter * area.Count;
            }
        }

        System.Console.WriteLine($"Result {inputFile} is {result} in {(DateTime.Now - start).TotalSeconds} seconds");
    }

    private static int GetPerimeter(HashSet<Vector> area)
    {
        int perimeter = 0;
        foreach (var v in area)
        {
            foreach (var dir in new Vector[] { Vector.Up, Vector.Down, Vector.Left, Vector.Right })
            {
                var location = v + dir;
                if (!area.Contains(location))
                {
                    perimeter++;
                }
            }
        }

        return perimeter;
    }

    private static HashSet<Vector> FollowArea(Map map, Vector start, HashSet<Vector> area, HashSet<Vector> used)
    {
        used.Add(start);
        area.Add(start);
        foreach (var dir in new Vector[] { Vector.Up, Vector.Down, Vector.Left, Vector.Right })
        {
            var location = start + dir;
            if (map.Contains(location)  && map[location] == map[start] && !used.Contains(location))
            {
                FollowArea(map, location, area, used);
            }
        }

        return area;
    }

    static void RunPart2(string inputFile)
    {
        long result = 0;

        var lines = System.IO.File.ReadAllLines(inputFile);

        var start = DateTime.Now;
        var map = Map.LoadFromLines(lines);

        HashSet<char> cropTypes = new HashSet<char>();
        foreach (var y in map.Rows)
        {
            foreach (var x in y)
            {
                cropTypes.Add(x);
            }
        }

        foreach (var crop in cropTypes)
        {
            HashSet<Vector> used = new HashSet<Vector>();
            
            foreach (var vector in map.FindNotUsed(crop, used))
            {
                HashSet<Vector> area = new HashSet<Vector>();
                area = FollowArea(map, vector, area, used);
                var perimeter = GetCorners(area);
                result += perimeter * area.Count;
            }
          
        }

        System.Console.WriteLine($"Result {inputFile} is {result} in {(DateTime.Now - start).TotalSeconds} seconds");
    }

    private static int GetCorners(HashSet<Vector> area)
    {
        int corners = 0;
        foreach (var v in area)
        {
            int emptyEdges = 0;
            foreach (var dir in new Vector[]
                     {
                         Vector.Down, Vector.DownLeft, Vector.Left, Vector.UpLeft, Vector.Up, Vector.UpRight,
                         Vector.Right, Vector.DownRight, Vector.Down
                     })
            {
                var location = v + dir;
                emptyEdges <<= 1;
                if (!area.Contains(location))
                {
                    emptyEdges += 1;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                var theseEdges = emptyEdges & 5;
                switch (theseEdges)
                {
                    case 0 or 2 or 5:
                        corners += 1;
                        break;
                }

                emptyEdges >>= 2;
            }
        }

        return corners;
    }
}