namespace Utilities;

public record struct Vector(int X, int Y)
{
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
    
    public Vector RotateLeft90(Vector v)
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