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
            _controller = GetComponent<CharacterController>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update() {
            Vector3 f = transform.TransformDirection(Vector3.forward);
            Vector3 r = transform.TransformDirection(Vector3.right);

            float cSpeedX = walkSpeed * Input.GetAxis("Vertical");
            float cSpeedY = walkSpeed * Input.GetAxis("Horizontal");
            float moveDirectionY = _moveDirection.y;

            _moveDirection = (f * cSpeedX) + (r * cSpeedY);

            _moveDirection.y = moveDirectionY;

            //this._controller.Move(this._moveDirection * Time.deltaTime);
            
            pCamera.transform.position = Vector3.Lerp(pCamera.transform.position, transform.position,
                rlert);
            pCamera.transform.position -= new Vector3(0, -0.150f, 0.3f);
        }
    }
}