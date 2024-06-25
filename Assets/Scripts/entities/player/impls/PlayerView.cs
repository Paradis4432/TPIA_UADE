using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace entities.player.impls {
    [RequireComponent(typeof(Rigidbody))]
    public sealed class PlayerView : MonoBehaviour, IPlayerView {
        public GameObject player;
        public Animator animator;

        [NotNull] private Rigidbody _rigidbody;

        private static readonly int Velocity = Animator.StringToHash("velocity");

        private void Awake() {
            _rigidbody = player.GetComponent<Rigidbody>();
        }

        private void Update() {
            //animator.SetFloat(Velocity, _rigidbody.velocity.magnitude); // TODO reenable animator
        }
    }
}