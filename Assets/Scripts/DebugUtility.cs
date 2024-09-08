using System.Collections.Generic;
using UnityEngine;


public static class DebugUtility
{
    public static void DebugList<T>(List<T> list)
    {
        foreach (T element in list)
        {
            Debug.Log(element);
        }
    }
}
