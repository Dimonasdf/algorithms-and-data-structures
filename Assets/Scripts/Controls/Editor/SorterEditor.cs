using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Sorter))]
public class SorterEditor : Editor
{
    private const int Spacing = 5;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var sorter = (Sorter)target;

        if (GUILayout.Button(nameof(Sorter.Initialize)))
            sorter.Initialize();

        GUILayout.Space(Spacing);

        if (GUILayout.Button(nameof(Sorter.BubbleSort)))
            sorter.BubbleSort();

        if (GUILayout.Button(nameof(Sorter.SelectionSort)))
            sorter.SelectionSort();

        if (GUILayout.Button(nameof(Sorter.InsertionSort)))
            sorter.InsertionSort();

        GUILayout.Space(Spacing);

        if (GUILayout.Button(nameof(Sorter.MergeSort)))
            sorter.MergeSort();

        if (GUILayout.Button(nameof(Sorter.QuickSort)))
            sorter.QuickSort();
    }
}
