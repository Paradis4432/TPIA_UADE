using fsm.states;
using fsm.states.impls;

namespace entities.enemies.states.attacks {
    public class EnemyStateAttackBig<T> : AbstractEnemyStateAttack<T> {
        public override EStates GetStateType() {
            return EStates.AttackBig;
        }
    }
}