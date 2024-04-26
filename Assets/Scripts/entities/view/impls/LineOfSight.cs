using UnityEngine;

namespace entities.view.impls {
    public class LineOfSight : MonoBehaviour, ILineOfSight {
        public float range;
        [Range(0, 360)] public float angle;
        public LayerMask layerMask;

        private Vector3 Origin => this.transform.position;
        private Vector3 Forward => this.transform.forward;

        public bool CheckRange(Transform target) {
            return Vector3.Distance(target.position, this.Origin) <= this.range;
        }

        public bool CheckAngle(Transform target) {
            return Vector3.Angle(this.Forward, target.position - this.Origin) <= this.angle / 2;
        }

        public bool CheckView(Transform target) {
            Vector3 dir = target.position - this.Origin;
            return !Physics.Raycast(
                this.Origin,
                dir,
                dir.magnitude,
                this.layerMask);
        }
    }
}