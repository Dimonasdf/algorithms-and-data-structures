using UnityEngine;

public class Sorter : MonoBehaviour
{
    [SerializeField]
    private int[] dataSet;

    [SerializeField]
    private int dataSetLength = 256;

    public void Initialize()
    {
        dataSet = new int[dataSetLength];

        for (int i = 0; i < dataSetLength; i++)
            dataSet[i] = i;

        dataSet.Shuffle();

        Debug.Log($"{dataSet.ToSpacedString()} : Initial data set");
    }

    public void BubbleSort()
    {
        SortWith(new BubbleSort<int>());
    }

    public void SelectionSort()
    {
        SortWith(new SelectionSort<int>());
    }

    public void InsertionSort()
    {
        SortWith(new InsertionSort<int>());
    }

    public void MergeSort()
    {
        SortWith(new MergeSort<int>());
    }

    public void QuickSort()
    {
        SortWith(new QuickSort<int>());
    }

    public void HeapSort()
    {
        SortWith(new HeapSort<int>());
    }

    private void SortWith(SortingAlgorithm<int> algorithm)
    {
        var dataCopy = new int[dataSet.Length];
        dataSet.CopyTo(dataCopy, 0);

        algorithm.DataSweepCallback += (int sweepID) => Debug.Log($"{dataCopy.ToSpacedString()} : Sweep ID {sweepID}");

        algorithm.Sort(dataCopy);

        Debug.Log($"{dataCopy.ToSpacedString()} : Data set after sort");

        if (dataCopy.IsSorted())
            Debug.LogWarning("Data is sorted");
        else
            Debug.LogError("Data is not sorted");
    }
}
