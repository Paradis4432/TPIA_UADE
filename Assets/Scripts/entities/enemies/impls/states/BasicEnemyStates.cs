using FSM.state.impls;

namespace entities.enemies.impls.states {
    public abstract class BasicEnemyStates<TE> : State<TE> {
        protected TE Current;
        protected abstract void Execute();

        public class Patrol<TE> : BasicEnemyStates<TE> {
            public Patrol(TE current) {
                this.Current = current;
            }

            protected override void Execute() {
                
            }
        }

        public class Idle<TE> : BasicEnemyStates<TE> {
            public Idle(TE current) {
                this.Current = current;
            }

            protected override void Execute() {
            }
        }

        public class Attack<TE> : BasicEnemyStates<TE> {
            public Attack(TE current) {
                this.Current = current;
            }

            protected override void Execute() {
            }
        }
    }
}