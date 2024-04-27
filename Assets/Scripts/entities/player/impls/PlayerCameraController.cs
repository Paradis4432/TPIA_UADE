using UnityEngine;

namespace entities.player.impls {
    [RequireComponent(typeof(CharacterController))]
    public class PlayerCameraController : MonoBehaviour {
        public float rlert = 0.1f;

        public Camera pCamera;
        public float walkSpeed = 6f;

        private CharacterController _controller;
        private Vector3 _moveDirection = Vector3.zero;

        private void Start() {
            this._controller = this.GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update() {
            Vector3 f = this.transform.TransformDirection(Vector3.forward);
            Vector3 r = this.transform.TransformDirection(Vector3.right);

            float cSpeedX = this.walkSpeed * Input.GetAxis("Vertical");
            float cSpeedY = this.walkSpeed * Input.GetAxis("Horizontal");
            float moveDirectionY = this._moveDirection.y;

            this._moveDirection = (f * cSpeedX) + (r * cSpeedY);

            this._moveDirection.y = moveDirectionY;

            this._controller.Move(this._moveDirection * Time.deltaTime);
            
            this.pCamera.transform.position = Vector3.Lerp(this.pCamera.transform.position, this.transform.position,
                this.rlert);
            this.pCamera.transform.position -= new Vector3(0, -0.150f, 0.3f);
        }
    }
}