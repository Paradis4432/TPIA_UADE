using System.Collections.Generic;
using UnityEngine;

namespace grid.points {
    public interface IPoint {
        List<IPoint> GetNeighborsPoints();
        void GetNeighborsPoints(Vector3 dir);
        Vector3 GetPosition();
    }
}