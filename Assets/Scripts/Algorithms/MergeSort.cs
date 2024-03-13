using System;
using UnityEngine;

/// <summary>
/// Divides source data into halves recursively until they reach the size 2, sorts them,
/// and then reconstructs the halves back, merge-sorting them along the way without search.
/// </summary>
// Takes log-n division operations with linear time to merge them, resulting in O(n * log-n) time complexity
// The order of equal elements is maintained
// Usually is implemented requiring extra space for the dividing collections
public class MergeSort<TData> : SortingAlgorithm<TData> where TData : IComparable
{
    public override void Sort(TData[] data)
    {
        // recursively divide and sort splitted arrays
        if (Divide(data, out var split1, out var split2))
        {
            Sort(split1);
            Sort(split2);
        }

        Merge(data, split1, split2);
    }

    // returns true if it makes sense to continue division, i.e. data length is >= 2
    private bool Divide(TData[] data, out TData[] split1, out TData[] split2)
    {
        var dataLength = data.Length;
        var halfLength = dataLength == 1 ? 1 : dataLength / 2;

        split1 = new TData[halfLength];
        Array.Copy(data, 0, split1, 0, halfLength);

        split2 = new TData[dataLength - halfLength];
        Array.Copy(data, halfLength, split2, 0, dataLength - halfLength);

        return dataLength > 1;
    }

    // sum of lengths of splits is presumed to be equal to the length of source, because that's how we've split them
    // split1 and split2 are merged back into source in non-decreasing order
    private void Merge(TData[] source, TData[] split1, TData[] split2)
    {
        var sourceLength = source.Length;
        var split1Length = split1.Length;
        var split2Length = split2.Length;
        var indexer1 = 0;
        var indexer2 = 0;

        for (int i = 0; i < sourceLength; i++)
        {
            if (indexer1 == split1Length)
            {
                source[i] = split2[indexer2];
                indexer2++;
            }
            else if (indexer2 == split2Length)
            {
                source[i] = split1[indexer1];
                indexer1++;
            }
            else if (split1[indexer1].CompareTo(split2[indexer2]) <= 0)
            {
                source[i] = split1[indexer1];
                indexer1++;
            }
            else
            {
                source[i] = split2[indexer2];
                indexer2++;
            }
        }

        if (DebugLog)
            Debug.Log($"Merge : [{split1.ToSpacedString()}] + [{split2.ToSpacedString()}] = [{source.ToSpacedString()}]");
    }
}
