using System.Diagnostics.CodeAnalysis;
using grid;
using UnityEngine;

namespace entities.enemies.impls {
    public class Enemy : MonoBehaviour, IEnemyModel, IEntity {
        public float speed;
        public int hp = 100;

        private Rigidbody _rigidbody;

        // if found target is set to true it means that the target was found at least once and that this
        // enemy is activated
        private bool _foundTarget = false;

        private void Start() {
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void Move(Vector3 dir) {
            // TODO replace with ai
            dir *= speed;
            dir.y = _rigidbody.velocity.y;
            _rigidbody.velocity = dir;
        }

        public void Look(Vector3 dir) {
            if (dir is { x: 0, z: 0 }) return;
            transform.forward = dir;
        }

        public void Die() {
            Destroy(gameObject);
        }
    }
}