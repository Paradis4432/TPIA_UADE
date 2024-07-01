using fsm.states.impls;
using UnityEngine;

namespace entities.enemies.states.attacks {
    public abstract class AbstractEnemyStateAttack<T> : State<T> {
        public override void Enter() {
            Log("ATTACK");
        }
    }
}