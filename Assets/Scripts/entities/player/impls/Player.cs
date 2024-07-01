using System.Diagnostics.CodeAnalysis;
using DefaultNamespace;
using entities.enemies.impls;
using UnityEngine;

namespace entities.player.impls {
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour, IPlayerModel, IEntity {
        public float speed;
        public int powerUps = 3;
        [NotNull] private Rigidbody _rigidbody;

        private void Awake() {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Move(Vector3 dir) {
            dir *= speed;
            dir.y = _rigidbody.velocity.y;
            _rigidbody.velocity = dir;
        }

        public void Look(Vector3 dir) {
            if (dir is { x: 0, z: 0 }) return;
            transform.forward = dir;
        }

        private void Update() {
            // on press space
            if (Input.GetKeyDown(KeyCode.Space)) {
                if (powerUps <= 0) return;
                powerUps--;
                // get instance of GameManager
                GameManager.Instance.UsePowerUp();
            }
        }


        private void OnCollisionEnter(Collision other) {
            if (other.gameObject.TryGetComponent(out Enemy _)) {
                //SceneChanger.Lost();
            }
            else if (other.gameObject.CompareTag("WinArea")) {
                SceneChanger.Win();
            }
        }
    }
}