using UnityEngine;

namespace DefaultNamespace {
    public class PlayerCamera : MonoBehaviour {
        public Transform camTarget;
        public float plert = 0.2f;
        public float rlert = 0.1f;

        private void Update() {
            this.transform.position = Vector3.Lerp(this.transform.position, this.camTarget.position, this.plert);
            this.transform.position -= new Vector3(0, -0.3f, 1.2f);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, this.camTarget.rotation, this.rlert);
        }
    }
}