using fsm.states;
using fsm.states.impls;
using UnityEngine;

namespace entities.enemies.states {
    public class EnemyStateDie<T> : State<T> {
        public override void Enter() {
            Log("DEAD");
        }

        public override EStates GetStateType() {
            return EStates.Die;
        }
    }
}