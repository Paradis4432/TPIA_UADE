using UnityEngine;

namespace entities {
    public abstract class AbstractEntity : MonoBehaviour, IEntity {
        public int HP { get; set; }
        public float Speed { get; set; }

        public abstract Vector3 getPosition();

        protected Vector3 NormalizeMovement() {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            Vector3 movement =  new(horizontal, 0, vertical);
            return movement.normalized * Speed * Time.deltaTime;
        }
    }
}