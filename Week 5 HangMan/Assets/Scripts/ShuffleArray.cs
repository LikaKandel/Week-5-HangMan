using System;
using System.Collections;
using System.Collections.Generic;

public static class ShuffleArray 
{
    public static void Shuffle(string[] answ)
    {
        // Knuth shuffle algorithm :: courtesy of Wikipedia 🙂
        for (int t = 0; t < answ.Length; t++)
        {
            string tmp = answ[t];
            int r = UnityEngine.Random.Range(t, answ.Length);
            answ[t] = answ[r];
            answ[r] = tmp;
        }
    }
}
