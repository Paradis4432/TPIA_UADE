using UnityEngine;

namespace entities.view {
    public interface ILineOfSight {
        bool CheckRange(Transform target);
        bool CheckAngle(Transform target);
        bool CheckView(Transform target);
    }
}