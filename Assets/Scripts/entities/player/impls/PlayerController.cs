using entities.player.impls.states;
using FSM;
using UnityEngine;

namespace entities.player.impls {
    public class PlayerController : MonoBehaviour, IPlayer {
        public Rigidbody Rigidbody { get; set; }
        public GameObject Body { get; set; }
        public Animator Animator { get; set; }

        public IPlayer.IModel Model { get; set; }
        public IPlayer.IView View { get; set; }

        private FSM<EPlayerStates> _fsm;

        public void Awake() {
            Rigidbody = GetComponent<Rigidbody>();
            Body = gameObject;
            Model = new PModel(this);
            View = new PView(this);

            
            PlayerStates<EPlayerStates>.Idle<EPlayerStates> idle = new(EPlayerStates.IDLE, this);
            PlayerStates<EPlayerStates>.Walk<EPlayerStates> walk = new(EPlayerStates.WALK, this);

            idle.Add(EPlayerStates.WALK, walk);

            walk.Add(EPlayerStates.IDLE, idle);

            _fsm = new FSM<EPlayerStates>(idle);
        }

        public void Update() {
            /*float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 dir = new Vector3(x, 0, z).normalized;
            Model.Move(dir);
            View.LookDir(dir);

            _fsm.Update();*/

            _fsm.Update();

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 dir = new Vector3(x, 0, z).normalized;
            // porque el execute del fsm no se ejecuta???
            _fsm.Transition(dir == Vector3.zero ? EPlayerStates.IDLE : EPlayerStates.WALK);
            //_fsm.Transition(dir is { x: 0, z: 0 } ? EPlayerStates.IDLE : EPlayerStates.WALK);

            // esto no deberia llamarse asi pero no anda sino
            Model.Move(dir);
            View.LookDir(dir);
        }

        private void OnCollisionEnter(Collision other) {
            if (other.gameObject.CompareTag("Enemy")) {
                Debug.Log("LOST");
            }
        }

        public void ChangeModel(IPlayer.IModel model) {
            Model = model;
        }

        private class PModel : IPlayer.IModel {
            public float Speed { get; set; } = 5f;
            public IPlayer Parent { get; set; }

            public PModel(IPlayer parent) {
                Parent = parent;
            }

            public Transform Transform { get; set; }

            public void Move(Vector3 dir) {
                dir *= Speed;
                dir.y = Parent.Rigidbody.velocity.y;
                Parent.Rigidbody.velocity = dir;
            }

            public void LookDir(Vector3 dirNormalized) {
                Parent.Body.transform.forward = dirNormalized;
            }
        }

        private class PView : IPlayer.IView {
            public IPlayer Parent { get; set; }

            public PView(IPlayer parent) {
                Parent = parent;
            }

            public void LookDir(Vector3 dir) {
                if (dir is { x: 0, z: 0 }) return;
                Parent.Body.transform.forward = dir;
            }

            public void Update() {
                Parent.Animator.SetFloat("Vel", Parent.Rigidbody.velocity.magnitude);
            }
        }
    }
}