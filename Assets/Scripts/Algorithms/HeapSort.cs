using System;
using System.Runtime.CompilerServices;

public class HeapSort<TData> : SortingAlgorithm<TData> where TData : IComparable
{
    public override void Sort(TData[] data) => SortInPlace(data);

    // subsequent design, which treats given array as a heap, and by sorting it,
    // with first - bubbling the minimum value to the top and then pushing it to the end
    // consequently sorts the original array
    private void SortInPlace(TData[] data)
    {
        var zeroBaseMinBinaryHeap = new ZeroBaseMinBinaryHeap<TData>(data);

        for (int i = 0; i < data.Length; i++)
        {
            zeroBaseMinBinaryHeap.Extract();
            DataSweepCallback?.Invoke(i);
        }

        // should have used MaxBinaryHeap, oh well
        // reverse the array in place
        for (int i = (data.Length - 1) / 2; i >= 0; i--)
            (data[i], data[^(i + 1)]) = (data[^(i + 1)], data[i]);
    }

    // original design, which simply produces next minimum element upon request
    private void SortWithExtraSpace(TData[] data)
    {
        var minBinaryHeap = new MinBinaryHeap<TData>((data));

        for (int i = 0; i < data.Length; i++)
        {
            data[i] = minBinaryHeap.Extract();
            DataSweepCallback?.Invoke(i);
        }
    }
}

// custom zero-based heap to use original array instead of creating new one with offset
// note1: 'protected new const FirstIndex' will not hide original completely and it will mess the Heapify()
// note2: 'GetLeftChildIndex()' and 'GetLeftChildIndex()' somehow work without override to account for different 'FirstIndex'
public class ZeroBaseMinBinaryHeap<TData> : MinBinaryHeap<TData> where TData : IComparable
{
    protected override int FirstIndex => 0;

    public ZeroBaseMinBinaryHeap(TData[] data) : base(data)
    {
        _size = data.Length;
        _data = data;

        for (int i = _size - 1; i >= FirstIndex; i--)
            HeapifyTopToBottom(i);
    }
}

// simple min-first heap without the ability to add new elements
// (resizing arrays is out of scope of this exercise, and using List<> would be too easy)
// private array is 1-based, as recommended to simplify elements access calculations and improve locality of reference
// https://en.wikipedia.org/wiki/Binary_tree#Methods_for_storing_binary_trees
public class MinBinaryHeap<TData> where TData : IComparable
{
    protected virtual int FirstIndex => 1;

    protected TData[] _data;
    protected int _size;

    public MinBinaryHeap(int size)
    {
        _size = size + FirstIndex;
        _data = new TData[_size];
    }

    public MinBinaryHeap(TData[] data)
    {
        _size = data.Length + FirstIndex;
        _data = new TData[_size];
        Array.Copy(data, 0, _data, FirstIndex, data.Length);

        // build the heap structure
        for (int i = _size - 1; i >= FirstIndex; i--)
            HeapifyTopToBottom(i);
    }

    public TData Peek() => _data[FirstIndex];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private int GetParentIndex(int index) => index / 2;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private int GetLeftChildIndex(int index) => index * 2;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private int GetRightChildIndex(int index) => index * 2 + 1;

    public TData Extract()
    {
        try
        {
            // swap root value with the last leaf value
            (_data[FirstIndex], _data[_size - 1]) = (_data[_size - 1], _data[FirstIndex]);
            // shrink accessible limits
            _size--;

            // return the value
            return _data[_size];
        }
        finally
        {
            HeapifyTopToBottom(FirstIndex);
        }
    }

    protected void HeapifyTopToBottom(int fromIndex)
    {
        var leftChildIndex = GetLeftChildIndex(fromIndex);
        // has no children, is a leaf
        if (leftChildIndex >= _size)
            return;

        var rightChildIndex = GetRightChildIndex(fromIndex);
        var onlyHasLeftChild = rightChildIndex >= _size;

        if (onlyHasLeftChild)
            CompareAgainstChild(leftChildIndex);
        else
        {
            // left child value is less than right child value
            if (_data[leftChildIndex].CompareTo(_data[rightChildIndex]) < 0)
                CompareAgainstChild(leftChildIndex);
            else
                CompareAgainstChild(rightChildIndex);
        }

        // == //
        void CompareAgainstChild(int index)
        {
            // value in fromIndex is greater than its child's value
            if (_data[fromIndex].CompareTo(_data[index]) > 0)
            {
                // swap and propagate Heapify further down
                (_data[fromIndex], _data[index]) = (_data[index], _data[fromIndex]);
                HeapifyTopToBottom(index);
            }
        }
    }
}
