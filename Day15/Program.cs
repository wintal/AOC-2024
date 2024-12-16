using System.Numerics;
using Utilities;
using Vector = Utilities.Vector;

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

        var width = 101;
        var height = 103;
        long iterations = 100;

        List<string> maplines = new List<string>();
        string movements = "";
        bool mapDone = false;
        for (int i = 0; i < lines.Length; i++)
        {
            if (!mapDone)
            {
                while (lines[i].Length > 5)
                {
                    
                    maplines.Add(lines[i]);
                    i++;
                }

                mapDone = true;
            }

            if (!string.IsNullOrEmpty(lines[i]))
            {
                movements += lines[i];
            }
            
        }
       
        var map = Map.LoadFromLines(maplines.ToArray());

        ExecuteMovements(map, movements);

        result = CalcScore(map);
        map.Print();
        System.Console.WriteLine($"Result {inputFile} is {result} in {(DateTime.Now - start).TotalSeconds} seconds");
    }

    private static long  CalcScore(Map map)
    {
        long score = 0;
        for (int y = 0; y < map.MaxY; y++)
        {
            for (int x = 0; x < map.MaxX; x++)
            {
                if (map.Rows[y][x] == 'O')
                {
                    score += y * 100 + x;
                }
            }
        }

        return score;
    }

    private static void ExecuteMovements(Map map, string movements)
    {
        Vector position = new Vector(0, 0);
        for (int y = 0; y < map.MaxY; y++)
        {
            for (int x = 0; x < map.MaxX; x++)
            {
                if (map.Rows[y][x] == '@')
                {
                    position = new Vector(x, y);
                    break;
                }
            }
        }

        map[position] = '.';
        Vector TryMove(Map thisMap, Vector direction, Vector thisPos)
        {
            Vector returnDirection = thisPos;
            ;
            if (thisMap[thisPos + direction] == '.')
            {
                returnDirection += direction;
            }
            else
            {
                bool done = false;
                int count = 0;
                var testpos = thisPos;
                while (!done)
                {
                    count++;
                    testpos = testpos + direction;
                    if (map[testpos] == '.')
                    {
                        // found - reverse out, moving things
                        for (int c = 0; c < count-1; c++)
                        {
                            var newVec = testpos - direction;
                            thisMap[testpos] = thisMap[newVec];
                            
                        }
                        thisMap[thisPos + direction] = '.';
                        returnDirection = thisPos + direction;
                        break;
                    }
                    else if (thisMap[testpos] == '#')
                    {
                        // can't move
                        return thisPos;
                    }
                }

            }

            return returnDirection;
        }

        foreach (var movement in movements)
        {
            switch (movement)
            {
                case '<':
                    position = TryMove(map, Vector.Left, position);
                    break;
                case '>':
                    position = TryMove(map, Vector.Right, position);
                    break;
                case '^':
                    position = TryMove(map, Vector.Up, position);
                    break;
                case 'v':
                    position = TryMove(map, Vector.Down, position);
                    break;
            }

       //     Console.SetCursorPosition(0, 0);
        //    map.Print();
        }
    }

    static void RunPart2(string inputFile)
    {
        var start = DateTime.Now;
        long result = 0;

        var lines = System.IO.File.ReadAllLines(inputFile);

        var width = 101;
        var height = 103;
        long iterations = 100;

        List<string> maplines = new List<string>();
        string movements = "";
        bool mapDone = false;
        for (int i = 0; i < lines.Length; i++)
        {
            if (!mapDone)
            {

                while (lines[i].Length > 5)
                {
                    var newLine = "";
                    foreach (var e in lines[i])
                    {
                        switch (e)
                        {
                            case 'O':
                                newLine += '[';
                                newLine += ']';
                                break;
                            case '@':
                                newLine += '@';
                                newLine += '.';
                                break;
                            default:
                                newLine += e;
                                newLine += e;
                                break;
                        }
                    }

                    maplines.Add(newLine);
                    i++;
                }

                mapDone = true;
            }

            if (!string.IsNullOrEmpty(lines[i]))
            {
                movements += lines[i];
            }
            
        }
       
        var map = Map.LoadFromLines(maplines.ToArray());

        ExecuteMovementsPt2(map, movements);

        result = CalcScorePt2(map);
        map.Print();
        System.Console.WriteLine($"Result {inputFile} is {result} in {(DateTime.Now - start).TotalSeconds} seconds");
    }
    
      private static void ExecuteMovementsPt2(Map map, string movements)
    {
        Vector position = new Vector(0, 0);
        for (int y = 0; y < map.MaxY; y++)
        {
            for (int x = 0; x < map.MaxX; x++)
            {
                if (map.Rows[y][x] == '@')
                {
                    position = new Vector(x, y);
                    break;
                }
            }
        }

        map[position] = '.';
        Vector TryMoveLR(Map thisMap, Vector direction, Vector thisPos)
        {
            Vector returnDirection = thisPos;
            ;
            if (thisMap[thisPos + direction] == '.')
            {
                returnDirection += direction;
            }
            else
            {
                bool done = false;
                int count = 0;
                var testpos = thisPos;
                List<(int offset, int length)> pushers = new List<(int offset, int length)>();
                while (!done)
                {
                    count++;
                    testpos = testpos + direction;
                    if (map[testpos] == '.')
                    {
                        // found - reverse out, moving things
                        for (int c = 0; c < count-1; c++)
                        {
                            var newVec = testpos - direction;
                            thisMap[testpos] = thisMap[newVec];
                            testpos = newVec;
                        }
                        thisMap[thisPos + direction] = '.';
                        returnDirection = thisPos + direction;
                        break;
                    }
                    else if (thisMap[testpos] == '#')
                    {
                        // can't move
                        return thisPos;
                    }
                }

            }

            return returnDirection;
        }
        
        Vector TryMoveUD(Map thisMap, Vector direction, Vector thisPos)
        {
            Vector returnDirection = thisPos;
            if (thisMap[thisPos + direction] == '.')
            {
                returnDirection += direction;
            }
            else
            {
                bool done = false;
                int count = 0;
                var testpos = thisPos;
                List<(int offset, int countOffset)> beingPushed = new();
                beingPushed.Add((0,0));
                List<(Vector pos, int count)> toBePushed = new();
                while (beingPushed.Count > 0)
                {
                    count++;
                    List<(int, int)> toRemove = new();
                    List<(int, int)> toAdd = new();
                    foreach (var xOffset in beingPushed)
                    {

                        var thisTestpos = new Vector(testpos.X + direction.X + xOffset.offset, testpos.Y + direction.Y ); 
                        if (map[thisTestpos] == '.')
                        {
                            toBePushed.Add((thisTestpos, count - xOffset.countOffset));
                            toRemove.Add(xOffset);
                        }
                        else if (thisMap[thisTestpos] == '#')
                        {
                            // can't move
                            return thisPos;
                        }
                        else if (thisMap[thisTestpos] == '[')
                        {
                            if (!beingPushed.Any( option => option.offset == xOffset.offset + 1))
                            {
                                toAdd.Add((xOffset.offset + 1, count - 1));
                            }
                        }
                        else if (thisMap[thisTestpos] == ']')
                        {
                            if (!beingPushed.Any( option => option.offset == xOffset.offset - 1))
                            {
                                toAdd.Add((xOffset.offset - 1, count - 1));
                            }
                        }
                    }

                    foreach (var v in toRemove)
                    {
                        beingPushed.Remove(v);
                    }
                    foreach (var v in toAdd)
                    {
                        beingPushed.Add(v);
                    }

                    testpos += direction;
                }

                foreach (var pusher in toBePushed)
                {
                    // found - reverse out, moving things
                    var startPos = pusher.pos;
                    for (int c = 0; c < pusher.count -1; c++)
                    {
                        var newVec = startPos - direction;
                        thisMap[startPos] = thisMap[newVec];
                        startPos = newVec;

                    }

                    thisMap[startPos] = '.';
                }

                returnDirection = thisPos + direction;
            }
            return returnDirection;
        }

        foreach (var movement in movements)
        {
            switch (movement)
            {
                case '<':
                    position = TryMoveLR(map, Vector.Left, position);
                    break;
                case '>':
                    position = TryMoveLR(map, Vector.Right, position);
                    break;
                case '^':
                    position = TryMoveUD(map, Vector.Up, position);
                    break;
                case 'v':
                    position = TryMoveUD(map, Vector.Down, position);
                    break;
            }

            map[position] = movement;
            Console.SetCursorPosition(0, 0);
               map.Print();
            
            map[position] = '.';
          //  Console.WriteLine($"movement {movement} is {position}");
        //    Console.ReadKey();
        }
    }
      
    private static long  CalcScorePt2(Map map)
    {
        long score = 0;
        for (int y = 0; y < map.MaxY; y++)
        {
            for (int x = 0; x < map.MaxX; x++)
            {
                if (map.Rows[y][x] == '[')
                {
                    score += y * 100 + x;
                }
            }
        }

        return score;
        
    }
}