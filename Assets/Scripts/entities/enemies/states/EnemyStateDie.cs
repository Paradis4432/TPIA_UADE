using fsm.states.impls;
using UnityEngine;

namespace entities.enemies.states {
    public class EnemyStateDie<T> : State<T> {
        public override void Enter() {
            Debug.Log("DEADD");
        }
    }
}