using System.Collections.Generic;
using UnityEngine;

namespace grid.points {
    public interface IPoint {
        IEnumerable<IPoint> GetNeighborsPoints();
        void GetNeighborsPoints(Vector3 dir);
        Vector3 GetPosition();
        float Weight { get; set; }
        float Heuristic { get; set; }
    }
}