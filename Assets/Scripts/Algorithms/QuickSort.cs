using System;

/// <summary>
/// Recursively separates data into halves around the pivot value, moving all lesser values to the left
/// and greater values to the right of the pivot, until the halves become short enough to be trivially sorted.
/// </summary>
// In case the selected pivot adequately divides data into halves - performes log-n division-sort operations, each containing a linear number of swaps.
// In case pivot values turns out to be too close to the lesser or the greater side of the values range, it may be required up to n division-sort operations. 
// There are methods of choosing a decent pivot value, resulting in an average (n * log-n) time performance of the algorithm.
// Order of equal values is not maintained (unstable)
// Is done in place

public class QuickSort<TData> : SortingAlgorithm<TData> where TData : IComparable
{
    public override void Sort(TData[] data) => RecursiveQuickSort(data, 0, data.Length);

    private void RecursiveQuickSort(TData[] data, int from, int to)
    {
        if (!MedianOfThree(data, from, to))
            return;

        var length = to - from;
        var pivotIndex = from + length / 2;
        var pivotValue = data[pivotIndex];

        // swap pivot value with last element
        (data[pivotIndex], data[to - 1]) = (data[to - 1], data[pivotIndex]);

        var lesserIndexer = from;
        // start greater indexer before last position, skipping unnecessary check against pivot value
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
