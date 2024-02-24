using System;

public abstract class SortingAlgorithm<TData> where TData : IComparable
{
    protected virtual bool DebugLog { get; } = false;

    public Action DataChangeCallback;
    public Action<int> DataSweepCallback;

    public abstract void Sort(TData[] data);
}