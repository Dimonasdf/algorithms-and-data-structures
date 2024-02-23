using System;

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

            for (int i = sortedLength; i < length; i++)
            {
                if (data[i].CompareTo(minValue) <= 0)
                {
                    minValue = data[i];
                    minValueIndex = i;
                }
            }

            // swap first unsorted with minimum of unsorted
            if (data[sortedLength].CompareTo(minValue) >= 0)
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
