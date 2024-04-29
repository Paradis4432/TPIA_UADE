using UnityEngine;

namespace entities.view.impls {
    public class LineOfSight : MonoBehaviour, ILineOfSight {
        public float range;
        [Range(0, 360)] public float angle;
        public LayerMask layerMask;

        private Vector3 Origin => transform.position;
        private Vector3 Forward => transform.forward;

        public bool CheckRange(Transform target) {
            return Vector3.Distance(target.position, Origin) <= range;
        }

        public bool CheckAngle(Transform target) {
            return Vector3.Angle(Forward, target.position - Origin) <= angle / 2;
        }

        public bool CheckView(Transform target) {
            Vector3 dir = target.position - Origin;
            return !Physics.Raycast(
                Origin,
                dir,
                dir.magnitude,
                layerMask);
        }
    }
}