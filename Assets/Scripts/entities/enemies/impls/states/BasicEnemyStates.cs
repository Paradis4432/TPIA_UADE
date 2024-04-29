using FSM.state.impls;
using UnityEngine;

namespace entities.enemies.impls.states {
    public abstract class BasicEnemyStates<TE> : State<TE> {
        protected TE Current;
        protected IEnemy parent;
        
        public abstract override void Execute();

        public class Patrol<TE> : BasicEnemyStates<TE> {
            public Patrol(TE current, IEnemy parent) {
                Current = current;
                this.parent = parent;
            }

            public override void Execute() {
                Debug.Log("PATROLING");
                
            }
        }

        public class Idle<TE> : BasicEnemyStates<TE> {
            public Idle(TE current, BasicEnemy basicEnemy) {
                Current = current;
                parent = basicEnemy;
            }

            public override void Execute() {
                Debug.Log("IDLE");
            }
        }

        public class Attack<TE> : BasicEnemyStates<TE> {

            public Attack(TE current, BasicEnemy basicEnemy) {
                Current = current;
                parent = basicEnemy;
            }

            public override void Execute() {
                Debug.Log("ATTACKING");
                
                Vector3 dir = _target.position - parent.Model.Transform.position;
                parent.Model.Move(dir.normalized);
                parent.Model.LookDir(dir.normalized);
            }
        }
    }
}