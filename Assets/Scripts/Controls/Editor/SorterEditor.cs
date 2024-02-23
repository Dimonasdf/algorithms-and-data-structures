using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Sorter))]
public class SorterEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var sorter = (Sorter)target;

        if (GUILayout.Button(nameof(Sorter.Initialize)))
            sorter.Initialize();

        if (GUILayout.Button(nameof(Sorter.BubbleSort)))
            sorter.BubbleSort();

        if (GUILayout.Button(nameof(Sorter.SelectionSort)))
            sorter.SelectionSort();

        if (GUILayout.Button(nameof(Sorter.InsertionSort)))
            sorter.InsertionSort();
    }
}
