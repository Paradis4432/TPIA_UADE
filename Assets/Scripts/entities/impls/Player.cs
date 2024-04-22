using UnityEngine;

namespace entities.impls {
    public class Player : AbstractEntity {
        private void Update() {
            Vector3 movement = NormalizeMovement();
            transform.Translate(movement, Space.World);

            if (movement == Vector3.zero) return;
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation =
                Quaternion.RotateTowards(transform.rotation, toRotation, Speed * 100 * Time.deltaTime);
        }

        public override Vector3 getPosition() {
            // transform.Translate(NormalizeMovement(), Space.World);
            return transform.position;
        }
    }
}