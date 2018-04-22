using System;

public class Pair<F, S> : IComparable<Pair<F, S>> where S : IComparable
{
    public F First;
    public S Second;
    public int CompareTo(Pair<F, S> other)
    {
        return Second.CompareTo(other.Second)*-1;
    }
}