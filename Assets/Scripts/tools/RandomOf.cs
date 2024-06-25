using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomOf {
    public static float Range(float min, float max) {
        return min + Random.value * (max - min);
    }

    public static T Roulette<T>(Dictionary<T, float> items) {
        float t = items.Sum(keyValuePair => keyValuePair.Value);
        float r = Range(0, t);

        foreach (KeyValuePair<T, float> keyValuePair in items) {
            if (r <= keyValuePair.Value)
                return keyValuePair.Key;
            r -= keyValuePair.Value;
        }

        return default;
    }
}