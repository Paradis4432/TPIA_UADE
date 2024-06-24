using System;
using System.Collections.Generic;
using entities;
using grid.points;
using UnityEngine;

namespace grid {
    public class GridManager : MonoBehaviour {
        public const float LeapAmount = 1f; // 1 for points of size 0.5
        public const int Leaps = 1;

        //public static LayerMask PointsMask = LayerMask.GetMask("Points"); // not needed, i think, for now

        private static readonly Dictionary<IEntity, List<Action>> Actions = new();
        private static readonly Dictionary<IEntity, IPoint> Cache = new();

        public static void SavePointForEntityCache(IEntity entity, IPoint point) {
            Cache[entity] = point;
            if (!Actions.TryGetValue(entity, out List<Action> actions)) return;
            foreach (Action action in actions) {
                action();
            }
        }

        public static IPoint GetPointForEntity(IEntity entity) {
            return Cache[entity];  
        }

        public static bool IsEntityCached(IEntity entity) {
            return Cache.ContainsKey(entity);
        }

        public static void RegisterListener(IEntity entity, Action action) {
            Debug.Log("registering listener " + entity + " action " + action);
            Actions.TryGetValue(entity, out List<Action> actions);
            if (actions == null) Actions[entity] = new List<Action> { action };
            else actions.Add(action);
        }
    }
}