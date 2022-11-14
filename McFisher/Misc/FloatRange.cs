namespace McFisher.Misc;

public struct FloatRange
{
    public float X;
    public float Y;
    public int StartIndex;
    public int EndIndex;

    public FloatRange(float x, float y)
    {
        X = x;
        Y = y;
        StartIndex = 0;
        EndIndex = 0;
    }
}
