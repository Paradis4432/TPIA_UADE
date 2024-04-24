using UnityEngine;

namespace entities.player.impls {
    public class PlayerController : MonoBehaviour, IPlayer {
        [SerializeField] private Animator animator;
        private static readonly int Vel = Animator.StringToHash("Vel");

        private IPlayer.IModel Model { get; set; }
        private IPlayer.IView View { get; set; }

        public void Awake() {
            this.Model = new PModel(this.GetComponent<Rigidbody>());
            this.View = new PView(this.GetComponent<Rigidbody>(), this.animator, this.gameObject);
        }

        public void Update() {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 dir = new Vector3(x, 0, z).normalized;
            this.Model.Move(dir);
            this.View.LookDir(dir);
        }

        public void ChangeModel(IPlayer.IModel model) {
            this.Model = model;
        }


        private class PModel : IPlayer.IModel {
            public float Speed { get; set; } = 5f;
            public Rigidbody Rigidbody { get; set; }

            public PModel(Rigidbody rigidbody) {
                this.Rigidbody = rigidbody;
            }

            public void Move(Vector3 dir) {
                dir *= this.Speed;
                dir.y = this.Rigidbody.velocity.y;
                this.Rigidbody.velocity = dir;
            }
        }

        private class PView : IPlayer.IView {
            public Rigidbody Rigidbody { get; set; }
            public GameObject Body { get; set; }
            public Animator Animator { get; set; }

            public PView(Rigidbody rigidbody, Animator animator, GameObject body) {
                this.Rigidbody = rigidbody;
                this.Animator = animator;
                this.Body = body;
            }

            public void LookDir(Vector3 dir) {
                if (dir is { x: 0, z: 0 }) return;
                this.Body.transform.forward = dir;
            }

            public void Update() {
                this.Animator.SetFloat(Vel, this.Rigidbody.velocity.magnitude);
            }
        }
    }
}