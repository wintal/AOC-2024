namespace Utilities;

public static class MathUtils
{
    public static ulong GreatestCommonDivisor(ulong a, ulong b)
    {
        while (b != 0)
        {
            ulong temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }

    public static ulong LowestCommonMultiple(ulong a, ulong b)
    {
        return a * b / GreatestCommonDivisor(a, b);
    }
    
    public static int NumDigits(long number)
    {
        return (int)Math.Floor(Math.Log10(number));
    }

    public static (long, long) SplitNumber(long number, int numdigits)
    {
        int multiplier = (int)Math.Pow(10, numdigits / 2);
        long left = number / multiplier;
        long right = number % multiplier;
        return (left, right);
    }
}