using fsm.states;
using fsm.states.impls;
using UnityEngine;

namespace entities.enemies.states.attacks {
    public class EnemyStateAttackMed<T> : AbstractEnemyStateAttack<T> {

        public override EStates GetStateType() {
            return EStates.AttackMed;
        }
    }
}