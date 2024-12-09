namespace Utilities;

public record struct Range(int MinInclusive, int MaxInclusive)
{
    public bool Valid()
    {
        return MaxInclusive >= MinInclusive;
    }
    
    public (Range below, Range above) Split(int boundary)
    {

        if (boundary <= MinInclusive)
        {
            return ( new Range(0, -1), this);
        } 
        else if (boundary >= MaxInclusive)
        {
            return (this,  new Range(0, -1) );
        }
        return (new Range(MinInclusive, boundary), new Range(boundary, MaxInclusive));
    }

    public bool InRange(int testValue)
    {
        if (testValue < MinInclusive || testValue > MaxInclusive) return false;
        return true;
    }
}