using entities.enemies.impls;
using fsm.states;
using fsm.states.impls;

namespace entities.enemies.states {
    public class EnemyStateMove<T> : State<T> {
        private readonly Enemy _enemy;

        public EnemyStateMove(Enemy enemy) {
            _enemy = enemy;
        }

        public override void Execute() {
            _enemy.Move(_enemy.transform.forward / 4); // TODO replace with moving to the closest node 
        }

        public override EStates GetStateType() {
            return EStates.Move;
        }

        public override void Enter() {
            Log("MOVE");
        }
    }
}