using entities.player.impls.states;
using FSM;
using UnityEngine;

namespace entities.player.impls {
    public class PlayerController : MonoBehaviour, IPlayer {
        public Rigidbody Rigidbody { get; set; }
        public GameObject Body { get; set; }
        public Animator Animator { get; set; }

        private IPlayer.IModel Model { get; set; }
        private IPlayer.IView View { get; set; }

        private FSM<EPlayerStates> _fsm = new();

        private static readonly int Vel = Animator.StringToHash("Vel");

        public void Awake() {
            this.Rigidbody = this.GetComponent<Rigidbody>();
            this.Body = this.gameObject;
            this.Model = new PModel(this);
            this.View = new PView(this);

            PlayerStates<EPlayerStates>.Idle<EPlayerStates> idle = new(EPlayerStates.IDLE);
            PlayerStates<EPlayerStates>.Walk<EPlayerStates> walk = new(EPlayerStates.WALK);

            idle.Add(EPlayerStates.IDLE, idle);
            idle.Add(EPlayerStates.WALK, walk);

            this._fsm.SetState(idle);
        }

        public void Update() {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 dir = new Vector3(x, 0, z).normalized;
            this.Model.Move(dir);
            this.View.LookDir(dir);

            this._fsm.Update();
        }

        public void ChangeModel(IPlayer.IModel model) {
            this.Model = model;
        }

        private class PModel : IPlayer.IModel {
            public float Speed { get; set; } = 5f;
            public IPlayer Parent { get; set; }

            public PModel(IPlayer parent) {
                this.Parent = parent;
            }

            public void Move(Vector3 dir) {
                dir *= this.Speed;
                dir.y = this.Parent.Rigidbody.velocity.y;
                this.Parent.Rigidbody.velocity = dir;
            }
        }

        private class PView : IPlayer.IView {
            public IPlayer Parent { get; set; }

            public PView(IPlayer parent) {
                this.Parent = parent;
            }

            public void LookDir(Vector3 dir) {
                if (dir is { x: 0, z: 0 }) return;
                this.Parent.Body.transform.forward = dir;
            }

            public void Update() {
                this.Parent.Animator.SetFloat(Vel, this.Parent.Rigidbody.velocity.magnitude);
            }
        }
    }
}