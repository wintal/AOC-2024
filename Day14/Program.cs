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
        //  RunPart2(Sample);
        RunPart2(Input);
    }

    struct Robot
    {
        public Vector Position;
        public Vector Direction;
    }

    static void RunPart1(string inputFile)
    {
        var start = DateTime.Now;
        long result = 0;

        var lines = System.IO.File.ReadAllLines(inputFile);

        var width = 101;
        var height = 103;
        long iterations = 100;
        List<Robot> robots = new List<Robot>();

        foreach (var line in lines)
        {
            var part = line.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var location = part[0][2..]
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var direction = part[1][2..]
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            var vdirection = new Vector(long.Parse(direction[0]), long.Parse(direction[1]));
            var vposition = new Vector(long.Parse(location[0]), long.Parse(location[1]));
            if (vdirection.X < 0) vdirection.X = width + vdirection.X;
            if (vdirection.Y < 0) vdirection.Y = height + vdirection.Y;
            robots.Add(new Robot()
            {
                Direction = vdirection,
                Position = vposition
            });
        }

        int[][] map = new int[height][];
        for (int i = 0; i < height; i++)
        {
            map[i] = new int[width];
        }

        foreach (var robot in robots)
        {
            var endX = robot.Position.X + robot.Direction.X * iterations;
            endX = endX % width;
            var endY = robot.Position.Y + robot.Direction.Y * iterations;
            endY = endY % height;
            map[endY][endX]++;
        }

        long safety = 0;
        var left = (width / 2);
        var right = (width / 2) + 1;
        var top = height / 2;
        var bottom = height / 2 + 1;

        int SumQuadrant(int minX, int maxX, int minY, int maxY)
        {
            int thisResult = 0;
            for (int x = minX; x < maxX; x++)
            {
                for (int y = minY; y < maxY; y++)
                {
                    thisResult += map[y][x];
                }
            }

            return thisResult;
        }


        result = SumQuadrant(0, left, 0, top);

        result *= SumQuadrant(right, width, 0, top);

        result *= SumQuadrant(0, left, bottom, height);

        result *= SumQuadrant(right, width, bottom, height);

        System.Console.WriteLine($"Result {inputFile} is {result} in {(DateTime.Now - start).TotalSeconds} seconds");
    }

    static void RunPart2(string inputFile)
    {
        var start = DateTime.Now;
        long result = 0;

        var lines = System.IO.File.ReadAllLines(inputFile);

        var width = 101;
        var height = 103;
        long iterations = 100;
        List<Robot> robots = new List<Robot>();

        foreach (var line in lines)
        {
            var part = line.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var location = part[0][2..]
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            var direction = part[1][2..]
                .Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            var vdirection = new Vector(long.Parse(direction[0]), long.Parse(direction[1]));
            var vposition = new Vector(long.Parse(location[0]), long.Parse(location[1]));
            if (vdirection.X < 0) vdirection.X = width + vdirection.X;
            if (vdirection.Y < 0) vdirection.Y = height + vdirection.Y;
            robots.Add(new Robot()
            {
                Direction = vdirection,
                Position = vposition
            });
        }

        int[,] map = new int[height, width];
      

        foreach (var robot in robots)
        {
            var endX = robot.Position.X + robot.Direction.X * iterations;
            endX = endX % width;
            var endY = robot.Position.Y + robot.Direction.Y * iterations;
            endY = endY % height;
            map[endY,endX]++;
        }

        int counter = 0;
        while (true)
        {
            counter++;
            Array.Clear(map);

            for (int i = 0; i < robots.Count; i++)
            {
                var robot = robots[i];
                var endX = robot.Position.X + robot.Direction.X;
                endX = endX % width;
                var endY = robot.Position.Y + robot.Direction.Y;
                endY = endY % height;
                map[endY,endX]++;
                robot.Position.X = endX;
                robot.Position.Y = endY;
                robots[i] = robot;
            }

            if (CheckXmasTree(map))
            {
                Map.Print2DArray(map);

                Console.WriteLine($"Counter is {counter}");
                return;
            }
        }

        System.Console.WriteLine($"Result {inputFile} is {result} in {(DateTime.Now - start).TotalSeconds} seconds");
    }

    private static bool CheckXmasTree(int[,] map)
    {
        int onTree = 0;
        int xOffset = 0;
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 50 - xOffset; x <= 50 + xOffset; x++)
            {
                onTree += map[y,x];
            }

            if ((y & 2) == 2) xOffset++;
        }


        Console.WriteLine($"{onTree} are on the tree shape");
        return onTree > 400;
    }
}