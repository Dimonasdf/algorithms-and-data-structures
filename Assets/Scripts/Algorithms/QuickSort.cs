using System;

public class QuickSort<TData> : SortingAlgorithm<TData> where TData : IComparable
{
    public override void Sort(TData[] data) => RecursiveQuickSort(data, 0, data.Length);

    private void RecursiveQuickSort(TData[] data, int from, int to)
    {
        // note1: range notation works as "[from, to)"
        // note2: passing data[from..to] into the method would create a copy of that part of the array
        // rather than reference members of the original array
        if (!MedianOfThree(data, from, to))
            return;

        var length = to - from;
        var pivotIndex = from + length / 2;
        var pivotValue = data[pivotIndex];

        // swap pivot value with last element
        (data[pivotIndex], data[to - 1]) = (data[to - 1], data[pivotIndex]);

        var lesserIndexer = from;
        // start greater indexer before last position, skipping unnecessary check agains pivot value
        var greaterIndexer = to - 2;

        for (; lesserIndexer < greaterIndexer; lesserIndexer++)
        {
            if (data[lesserIndexer].CompareTo(pivotValue) > 0)
            {
                for (; lesserIndexer < greaterIndexer; greaterIndexer--)
                {
                    if (data[greaterIndexer].CompareTo(pivotValue) < 0)
                    {
                        (data[lesserIndexer], data[greaterIndexer]) = (data[greaterIndexer], data[lesserIndexer]);
                        break;
                    }
                }
            }
        }

        // place pivot value in its final place
        (data[greaterIndexer], data[to - 1]) = (data[to - 1], data[greaterIndexer]);

        // include all up to where pivot value landed, excluding
        RecursiveQuickSort(data, from, greaterIndexer + 1);
        // inclute all after pivot value, excluding
        RecursiveQuickSort(data, greaterIndexer + 1, to);
    }

    // a method to ensure the selection of decent pivot,
    // involving a sorting of the first, middle and last elements of data slice
    private bool MedianOfThree(TData[] data, int from, int to)
    {
        var length = to - from;

        if (length <= 1)
            return false;

        var middlePoint = from + length / 2;

        // simple conditional bubble
        if (data[from].CompareTo(data[middlePoint]) > 0)
            (data[from], data[middlePoint]) = (data[middlePoint], data[from]);

        if (length > 2)
        {
            if (data[middlePoint].CompareTo(data[to - 1]) > 0)
            {
                (data[middlePoint], data[to - 1]) = (data[to - 1], data[middlePoint]);

                if (data[from].CompareTo(data[middlePoint]) > 0)
                    (data[from], data[middlePoint]) = (data[middlePoint], data[from]);
            }
        }

        // for length <= 3 return false as the data is essentially already sorted
        return length > 3;
    }
}
