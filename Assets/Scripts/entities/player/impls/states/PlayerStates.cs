using FSM.state.impls;
using UnityEngine;

namespace entities.player.impls.states {
    public abstract class PlayerStates<TE> : State<TE> {
        protected TE Current;
        protected PlayerController Controller;

        public abstract void Execute();

        public class Idle<TE> : PlayerStates<TE> {
            public Idle(TE current, PlayerController playerController) {
                Current = current;
                Controller = playerController;
            }

            public override void Execute() {
                Debug.LogWarning("PLAYER IDLE");
                if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0) return;
                Fsm.Transition(Current);

                Controller.Animator.SetTrigger("Dizzy 0");

                Controller.Rigidbody.velocity = Vector3.zero;
            }
        }

        public class Walk<TE> : PlayerStates<TE> {
            public Walk(TE current, PlayerController playerController) {
                Current = current;
                Controller = playerController;
            }

            public override void Execute() {
                Debug.LogWarning("WALKING");
                Controller.Animator.SetTrigger("WalkForwardBattle");

                /*float x = Input.GetAxis("Horizontal");
                float z = Input.GetAxis("Vertical");
                Vector3 dir = new Vector3(x, 0, z).normalized;*/
                /*Controller.Model.Move(dir);
                Controller.View.LookDir(dir);*/
            }
        }
    }
}