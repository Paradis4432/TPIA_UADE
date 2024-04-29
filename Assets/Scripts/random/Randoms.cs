using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace random {
    public class Randoms {
        public static float Range(float min, float max) => min + Random.value * (min - max);

        // returns a random value of a list by weight
        public static T RandomOf<T>(Dictionary<T, float> items) {
            float t = 0;
            foreach ((T _, float value) in items) t += value;

            float random = Range(0, t);

            foreach ((T key, float value) in items) {
                if (random <= value) return key;
                random -= value;
            }

            return default;
        }

        // shuffles a list of elements
        public static void Shuffle<T>(T[] items, Action<T, T> onSwap = null) {
            for (int i = 0; i < items.Length; i++) {
                int random = (int)Range(i, items.Length);

                if (onSwap != null) onSwap(items[i], items[random]);
                else (items[i], items[random]) = (items[random], items[i]);
            }
        }
    }
}