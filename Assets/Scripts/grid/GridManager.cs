using System.Collections.Generic;
using entities;
using entities.enemies;
using grid.points;
using UnityEngine;

namespace grid {
    public class GridManager : MonoBehaviour {
        public static readonly Dictionary<IEntity, IPoint> Cache = new();
        public const float LeapAmount = 1f; // 1 for points of size 0.5
        public const int Leaps = 1;

        //public static LayerMask PointsMask = LayerMask.GetMask("Points"); // not needed, i think, for now
    }
}