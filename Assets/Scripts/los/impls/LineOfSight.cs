using UnityEngine;

namespace los.impls {
    public class LineOfSight : MonoBehaviour, ILineOfSight {
        public float range;
        public float angle;
        public LayerMask maskObs;

        private Vector3 Origin => transform.position;
        private Vector3 Forward => transform.forward;

        public bool CheckRange(Transform target) {
            return CheckRange(target, this.range);
        }

        public bool CheckRange(Transform target, float r) {
            return Vector3.Distance(target.position, Origin) <= r;
        }
        
        public float CheckDistance(Transform target) {
            return Vector3.Distance(target.position, Origin);
        }

        public bool CheckAngle(Transform target) {
            return CheckAngle(target, angle);
        }

        public bool CheckAngle(Transform target, float a) {
            return Vector3.Angle(Forward, target.position - Origin) <= a / 2;
        }

        public bool CheckView(Transform target) {
            return CheckView(target, maskObs);
        }

        public bool CheckView(Transform target, LayerMask mask) {
            Vector3 dirToTarget = target.position - Origin;
            bool isClear = !Physics.Raycast(Origin, dirToTarget, dirToTarget.magnitude, mask);

            Debug.DrawLine(Origin, target.position, isClear ? Color.green : Color.red);

            return isClear;
        }


        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(Origin, range);
            Gizmos.color = Color.red;
            Gizmos.DrawRay(Origin, Quaternion.Euler(0, angle / 2, 0) * Forward * range);
            Gizmos.DrawRay(Origin, Quaternion.Euler(0, -(angle / 2), 0) * Forward * range);
        }
    }
}