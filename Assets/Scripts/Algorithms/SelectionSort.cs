using System;

/// <summary>
/// Separates data into sorted and unsorted parts.
/// Finds the minimum element among the unsorted and places it after the last sorted.
/// Increasing the sorted part and decreasing the unsorted.
/// </summary>
// Performs n sweeps, each on average n/2 long, resulting in n * n / 2 = O(n^2) time complexity
// Does not change the order of equal elements (stable)
// Is done in place
public class SelectionSort<TData> : SortingAlgorithm<TData> where TData : IComparable
{
    public override void Sort(TData[] data)
    {
        var length = data.Length;
        var sortedLength = 0;

        while (sortedLength < length)
        {
            // find minimum in unsorted data
            var minValue = data[sortedLength];
            var minValueIndex = sortedLength;
            // eliminate n additional comparisons testing that the first unsorted element is actually sorted
            var nextElementIsSorted = true;

            for (int i = sortedLength; i < length; i++)
            {
                // strict inequality ensures the selection of the first element among equal
                if (data[i].CompareTo(minValue) < 0)
                {
                    minValue = data[i];
                    minValueIndex = i;
                    nextElementIsSorted = false;
                }
            }

            // swap first unsorted with minimum of unsorted
            if (!nextElementIsSorted)
            {
                (data[sortedLength], data[minValueIndex]) = (data[minValueIndex], data[sortedLength]);
                DataChangeCallback?.Invoke();
            }

            DataSweepCallback?.Invoke(sortedLength);

            // and prolong the sorted region
            sortedLength++;
        }
    }
}
