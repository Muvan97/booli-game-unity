using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Tools
{
    public static class ListTools
    {
        public static T GetRandomElement<T>(this List<T> list) => list[Random.Range(0, list.Count)];
    }
}