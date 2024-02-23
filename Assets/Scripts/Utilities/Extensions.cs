using System;
using System.Collections.Generic;

public static class Extensions
{
    public static string ToSpacedString<T>(this List<T> list)
    {
        if (list == null)
            return "null";
        else
            return string.Join(" ", list);
    }

    public static string ToSpacedString<T>(this T[] array)
    {
        if (array == null)
            return "null";
        else
            return string.Join(" ", array);
    }

    public static void Shuffle<T>(this T[] array, System.Random random = null)
    {
        int n = array.Length;
        while (n > 1)
        {
            n--;

            int k;
            if (random == null)
                k = UnityEngine.Random.Range(0, n + 1);
            else
                k = random.Next(0, n + 1);

            (array[n], array[k]) = (array[k], array[n]);
        }
    }

    public static bool IsSorted<T>(this T[] array) where T : IComparable
    {
        var length = array.Length;

        for (int i = 0; i < length - 1; i++)
            if (array[i].CompareTo(array[i + 1]) > 0)
                return false;

        return true;
    }
}
