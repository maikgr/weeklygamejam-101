using System;
using System.Collections.Generic;

namespace Assets.Scripts.Extensions {
    public static class IListExtension {
        public static void Randomize<T>(this IList<T> list) {
            for (int i = 0; i < list.Count; ++i) {
                int randIndex = UnityEngine.Random.Range(i, list.Count);
                T temp = list[i];
                list[i] = list[randIndex];
                list[randIndex] = temp;
            }
        }

        public static T Random<T> (this IList<T> list) {
            int randIndex = UnityEngine.Random.Range(0, list.Count);
            return list[randIndex];
        }
    }
}
