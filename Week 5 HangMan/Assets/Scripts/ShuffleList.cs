using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;

public static class ShuffleList 
{
    private static Random rng = new Random();

    public static void ListShuffle<T>(this IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}
