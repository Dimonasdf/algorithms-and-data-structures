using System;

/// <summary>
/// Separates data into sorted and unsorted parts.
/// Takes unsorted elements one after another and correctly inserts them between the sorted ones.
/// </summary>
// Performs n sweeps, each from 0 (data is already sorted) to n/2 average (data is reversed) long, resulting in net n * n / 2 = O(n^2) time complexity
// Does not change the order of equal elements (stable)
// Is done in place
public class InsertionSort<TData> : SortingAlgorithm<TData> where TData : IComparable
{
    public override void Sort(TData[] data)
    {
        var length = data.Length;
        var sortedLength = 1;

        while (sortedLength < length)
        {
            var indexer = sortedLength;

            // move first unsorted item to its correct place within sorted data
            while (indexer > 0 && (data[indexer].CompareTo(data[indexer - 1]) < 0))
            {
                (data[indexer], data[indexer - 1]) = (data[indexer - 1], data[indexer]);

                DataChangeCallback?.Invoke();

                indexer--;
            }

            DataSweepCallback?.Invoke(sortedLength);

            sortedLength++;
        }
    }
}
