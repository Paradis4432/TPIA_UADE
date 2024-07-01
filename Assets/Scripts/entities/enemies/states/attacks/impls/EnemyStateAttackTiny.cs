using fsm.states;
using fsm.states.impls;

namespace entities.enemies.states.attacks {
    public class EnemyStateAttackTiny<T> : AbstractEnemyStateAttack<T> {
        
        public override EStates GetStateType() {
            return EStates.AttackTiny;
        }
    }
}