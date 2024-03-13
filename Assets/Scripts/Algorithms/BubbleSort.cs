using System;

/// <summary>
/// Iterates along the elements of the array, swapping the bigger element towards the sorting direction.
/// </summary>
// Performs n sweeps, each on average n/2 long, resulting in n * n / 2 = O(n^2) time complexity
// Does not change the order of equal elements (stable)
// Is done in place
public class BubbleSort<TData> : SortingAlgorithm<TData> where TData : IComparable
{
    public override void Sort(TData[] data)
    {
        var length = data.Length;
        var bubblingsLeft = length - 1;

        for (int i = 0; i < length - 1; i++)
        {
            var isSorted = true;

            // iterate along the array, bubble the largest element to the end
            // do not check further than the last fully bubbled element
            for (int j = 0; j < bubblingsLeft; j++)
            {
                if (data[j].CompareTo(data[j + 1]) > 0)
                {
                    isSorted = false;
                    (data[j], data[j + 1]) = (data[j + 1], data[j]);

                    DataChangeCallback?.Invoke();
                }
            }

            DataSweepCallback?.Invoke(i);

            bubblingsLeft--;

            if (isSorted)
                break;
        }
    }
}
