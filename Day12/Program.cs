using Utilities;

namespace Day12;

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
            var startV = map.Find(crop, used);
            while (startV.HasValue)
            {
                HashSet<Vector> area = new HashSet<Vector>();
                area = FollowArea(map, startV.Value, area, used);
                var perimeter = GetPerimeter(area);
                result += perimeter * area.Count;
                startV = map.Find(crop, used);
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
            if (map.Contains(location) && !used.Contains(location) && map[location] == map[start])
            {
                FollowArea(map, location, area, used);
            }
        }

        return area;
    }

    static void RunPart2(string inputFile)
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
            var startV = map.Find(crop, used);
            while (startV.HasValue)
            {
                HashSet<Vector> area = new HashSet<Vector>();
                area = FollowArea(map, startV.Value, area, used);
                var corners = GetCorners(area);
                result += corners * area.Count;
                startV = map.Find(crop, used);
            }
        }

        System.Console.WriteLine($"Result {inputFile} is {result} in {(DateTime.Now - start).TotalSeconds} seconds");
    }

    /*private static int GetNumberStraightSides(Map map, HashSet<Vector> area)
    {
        int perimeter = 0;
        HashSet<(Vector, Vector)> edges = new HashSet<(Vector, Vector)>();
        foreach (var v in area)
        {
            foreach (var dir in new Vector[] {  Vector.Left, Vector.Right,  Vector.Up, Vector.Down })
            {
                var location = v  + dir;
                if (!area.Contains(location))
                {
                    edges.Add((v, dir));
                }
            }
        }

        foreach (var dir in new Vector[] {  Vector.Up, Vector.Down })
        {
            for (int y = 0 ; y < map.MaxY; y++)
            {
                int lastHitX = -3;
                int lastHitY = -3;
                for (int x = 0 ; x < map.MaxX; x++)
                {
                    if (edges.Contains((new Vector(x, y), dir)))
                    {
                        if (lastHitX != x - 1 || lastHitY != y)
                        {
                            perimeter++;
                        }

                        lastHitX = x;
                        lastHitY = y;

                    }
                }
            }
        }

        foreach (var dir in new Vector[] {  Vector.Left, Vector.Right })
        {
            for (int x = 0; x < map.MaxX ; x++)
            {
                int lastHitY = -2;
                int lastHitX = -2;
                for (int y = 0 ; y < map.MaxY; y++)
                {
                    if (edges.Contains((new Vector(x, y), dir)))
                    {
                        if (lastHitY != y - 1 || lastHitX != x)
                        {
                            perimeter++;
                        }
                        lastHitY = y;
                        lastHitX = x;
                    }
                }
            }
        }

        return perimeter;
    }*/

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
                emptyEdges = emptyEdges << 1;
                if (!area.Contains(location))
                {
                    emptyEdges += 1;
                }
            }

            for (int i = 0; i < 4; i++)
            {
                var theseEdges = emptyEdges & 7;
                switch (theseEdges)
                {
                    case 7 or 2 or 5:
                        corners += 1;
                        break;
                }

                emptyEdges = emptyEdges >> 2;
            }
        }

        return corners;
    }
}