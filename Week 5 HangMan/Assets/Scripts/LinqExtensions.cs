using System.Collections.Generic;
using System;
using UnityEngine;

public static class LinqExtensions
{
    public static void Do<T>(this IEnumerable<T> enumerable, Action<T> action)
    {
        if (enumerable == null)
        {
            Debug.LogError("enumerable is null");
            return;
        }

        foreach (var item in enumerable)
        {
            action(item);
        }
    }
}
