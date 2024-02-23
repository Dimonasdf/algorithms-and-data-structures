using System;

public class InsertionSort<TData> : SortingAlgorithm<TData> where TData : IComparable
{
    public override void Sort(TData[] data)
    {
        var length = data.Length;
        var sortedLength = 0;

        while (sortedLength < length)
        {
            var indexer = sortedLength;

            // move first unsorted item to its correct place withing sorted data
            while (indexer > 0 && (data[indexer].CompareTo(data[indexer - 1]) <= 0))
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
