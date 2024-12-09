namespace Utilities;

public class Map
{
    public char[][] Rows { get; set; }

    public int MaxY
    {
        get
        {
            return Rows.Length;
        }
    }

    public int MaxX
    {
        get
        {
            return Rows[0].Length;
        }
    }

    public Vector FindEntry(char entry)
    {
        for (int y = 0; y < MaxY; y++)
        {
            for (int x = 0; x < MaxX; x++)
            {
                if (Rows[y][x] == entry)
                {
                    return new Vector(x, y);
                }
            }
        }
        return new Vector(-1, -1);
    }
    public static Map LoadFromLines(string[] lines, Func<char, char> converter = null)
    {
        var map = new Map();
        map.Rows = new char[lines.Length][];
        int row = 0;
        foreach (var line in lines)
        {
            map.Rows[row++] = converter == null ? line.ToArray() :line.Select(converter).ToArray();
        }

        return map;
    }

    public char this[Vector location]
    {
        get => Rows[location.Y][location.X];  
        set => Rows[location.Y][location.X] = value; 
    }

    public Map Clone()
    {
        char [][] newRows = new char[Rows.Length][];
        for (int i = 0; i < Rows.Length; i++)
        {
            newRows[i] = Rows[i].Clone() as char[];
        }
        return new Map { Rows = newRows };
    }

    public bool Contains(Vector l)
    {
     return (l.X >= 0 && l.X < MaxX && l.Y >= 0 && l.Y < MaxY);   
    }

    public void Print()
    {
        Console.WriteLine();
        foreach (var row in Rows)
        {
            Console.WriteLine(row);
        }
        Console.WriteLine();
    }
}