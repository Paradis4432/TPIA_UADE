using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace entities.player.impls {
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour, IPlayerModel {
        public float speed;
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
    }
}