namespace Utilities;

public readonly record struct Vector(long X, long Y)
{
    
    public static readonly Vector Zero = new(0, 0);
    public static readonly Vector Up = new(0, -1);
    public static readonly Vector UpLeft = new(-1, -1);
    public static readonly Vector UpRight = new(1, -1);
    public static readonly Vector Down = new(0, 1);
    public static readonly Vector DownLeft = new(-1, 1);
    public static readonly Vector DownRight = new(1, 1);
    public static readonly Vector Left = new(-1, 0);
    public static readonly Vector Right = new(1, 0);

    public static Vector operator +(Vector v, Vector w)
    {
        return new Vector(v.X + w.X, v.Y + w.Y);
    }

    public static Vector operator -(Vector v, Vector w)
    {
        return new Vector(v.X - w.X, v.Y - w.Y);
    }

    public Vector RotateRight90()
    {
        return new Vector(-Y, X);
    }

    public Vector RotateLeft90()
    {
        return new Vector(Y, -X);
    }

    public bool InsideBox(int minX, int minY, int maxX, int maxY)
    {
        if (X < minX || X >= maxX)
        {
            return false;
        }

        if (Y < 0 || Y >= maxY)
        {
            return false;
        }

        return true;
    }
}