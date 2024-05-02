using FSM.state.impls;
using UnityEngine;

namespace entities.enemies.impls.states {
    public abstract class BasicEnemyStates<TE> : State<TE> {
        protected TE Current;
        protected IEnemy parent;

        public class Patrol<TE> : BasicEnemyStates<TE> {
            public Patrol(TE current, IEnemy parent) {
                Current = current;
                this.parent = parent;
            }

            public override void Execute() {
                /*this.parent.Animator.SetBool("Attack01", false);
                this.parent.Animator.SetBool("IdleNormal", true);*/

                parent.Rigidbody.velocity = Vector3.zero;
                // rotate slowly on itself
                parent.Transform.Rotate(Vector3.up, 1f);
            }
        }

        public class Idle<TE> : BasicEnemyStates<TE> {
            public Idle(TE current, BasicEnemy basicEnemy) {
                Current = current;
                parent = basicEnemy; 
            }

            public override void Execute() {
            }
        }

        public class Attack<TE> : BasicEnemyStates<TE> {
            public Attack(TE current, BasicEnemy basicEnemy) {
                Current = current;
                parent = basicEnemy;
            }

            public override void Execute() {
                Vector3 dir = _target.position - parent.Model.Transform.position;
                parent.Model.Move(dir.normalized);
                parent.Model.LookDir(dir.normalized);

                this.parent.Animator.SetBool("IdleNormal", false);
                this.parent.Animator.SetBool("Attack01", true);
            }
        }
    }
}