using UnityEngine;

namespace los {
    public interface ILineOfSight {
        bool CheckRange(Transform target);
        bool CheckRange(Transform target, float r);
        bool CheckAngle(Transform target);
        bool CheckAngle(Transform target, float a);
        bool CheckView(Transform target);
        bool CheckView(Transform target, LayerMask mask);
    }
}