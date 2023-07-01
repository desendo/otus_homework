using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common
{
    public static class Extensions
    {
        public static void Dispose(this List<IDisposable> list)
        {
            foreach (var disposable in list)
            {
                disposable.Dispose();
            }
            list.Clear();
        }
        public static void AddTo(this IDisposable disposable, List<IDisposable> list)
        {
            list.Add(disposable);
        }

        public static float RandomizePercent(this float val, float percent)
        {
            var randomDelta = (UnityEngine.Random.value - 0.5f) * percent * 0.01f * val;
            return val + randomDelta;
        }
    }
}