using fsm.states.impls;
using UnityEngine;

namespace entities.enemies.states {
    public class EnemyStateAttack<T> : State<T> {
        public override void Enter() {
            Debug.Log("ATACK");
        }
    }
}