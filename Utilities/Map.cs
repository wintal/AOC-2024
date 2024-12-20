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
    
    public static Map Create(int width, int height)
    {
        var map = new Map();
        map.Rows = new char[height][];
        int row = 0;
        for (int i = 0; i <  height; i++)
        {
            map.Rows[row++] = new char[width];
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

    public Vector? Find(char crop, HashSet<Vector> excluded)
    {
        for (int y = 0; y < MaxY; y++)
        {
            for (int x = 0; x < MaxX; x++)
            {
                Vector pos = new Vector(x, y);
                if (!(excluded?.Contains(pos)??false) && this[pos] == crop)
                {
                    return pos;
                }
            }
        }

        return null;
    }
    
    public static void Print2DArray<T>(T[,] matrix)
    {
        int rows = matrix.GetLength(0); // 0 is first dimension, 1 is 2nd 
        //dimension of 2d array 
        int cols = matrix.GetLength(1);
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                Console.Write(matrix[i, j]);
            }
            Console.Write('\n');
        }
    }
}